
public class Binder
{
  public string File { get; }
  public SymbolTable SymbolTable { get; } = new SymbolTable();
  private readonly AstVisitor visitor = new AstVisitor();
  public Node Ast { get; private set; }
  public Diagnostics Diagnostics { get; }

  public Binder(string file, Node ast, Diagnostics diagnostics)
  {
    registerActions();
    File = file;
    Ast = ast;
    Diagnostics = diagnostics;
  }

  private void annotateSymbol(Node node, SymbolAnnotation annotation) => node.Annotations.Add("symbol", annotation);

  public void Bind()
  {
    visitor.Visit(Ast);
    reportSymbolsDefinedButNotUsed();
  }

  private void registerActions()
  {
    visitor.RegisterEnterAction<VarDeclNode>(enterVarDeclNode);
    visitor.RegisterEnterAction<ParamNode>(enterParamNode);

    visitor.RegisterEnterAction<FuncDeclNode>(enterFuncDeclNode);
    visitor.RegisterExitAction<FuncDeclNode>(exitFuncDeclNode);

    visitor.RegisterEnterAction<ForNode>(enterForNode);
    visitor.RegisterExitAction<ForNode>(exitForNode);

    visitor.RegisterEnterAction<SymbolNode>(enterSymbolNode);

    visitor.RegisterEnterAction<ScopeBlockNode>(enterScopeBlockNode);
    visitor.RegisterExitAction<ScopeBlockNode>(exitScopeBlockNode);
  }

  private string symbolKindToString(SymbolKind kind)
  {
    var str = "variable";
    switch (kind)
    {
    case SymbolKind.Variable:
      break;
    case SymbolKind.Parameter:
      str = "parameter";
      break;
    case SymbolKind.Function:
      str = "function";
      break;
    default:
      throw new ArgumentOutOfRangeException(nameof(kind), kind, null);
    }
    return str;
  }

  private void enterVarDeclNode(Node node)
  {
    var varDeclNode = (VarDeclNode) node;
    var identNode = varDeclNode.Children[1];
    var ident = identNode.Token;

    var (defined, symbol) = SymbolTable.Define(ident, SymbolKind.Variable);

    if (defined)
    {
      var annotation = new SymbolAnnotation(symbol);
      annotateSymbol(node, annotation);
      return;
    }

    var name = ident.Lexeme;
    var line = ident.Line;
    var column = ident.Column;
    Diagnostics.Error(File, line, column, $"Cannot define variable with name '{name}'");

    var kind = symbolKindToString(symbol.Kind);
    line = symbol.Ident.Line;
    column = symbol.Ident.Column;
    Diagnostics.Note(File, line, column, $"There is a {kind} with the same name");
  }

  private void enterParamNode(Node node)
  {
    var paramNode = (ParamNode) node;
    var identNode = paramNode.Children[2];
    var ident = identNode.Token;

    var (defined, symbol) = SymbolTable.Define(ident, SymbolKind.Parameter);

    if (defined)
    {
      var annotation = new SymbolAnnotation(symbol);
      annotateSymbol(node, annotation);
      return;
    }

    var name = ident.Lexeme;
    var line = ident.Line;
    var column = ident.Column;
    Diagnostics.Error(File, line, column, $"Cannot define parameter with name '{name}'");

    var kind = symbolKindToString(symbol.Kind);
    line = symbol.Ident.Line;
    column = symbol.Ident.Column;
    Diagnostics.Note(File, line, column, $"There is a {kind} with the same name");
  }

  private void enterFuncDeclNode(Node node)
  {
    var funcDeclNode = (FuncDeclNode) node;
    var identNode = funcDeclNode.Children[1];
    var ident = identNode.Token;

    var (defined, symbol) = SymbolTable.Define(ident, SymbolKind.Function);

    if (defined)
    {
      var annotation = new SymbolAnnotation(symbol);
      annotateSymbol(node, annotation);

      SymbolTable.EnterScope();
      return;
    }

    var name = ident.Lexeme;
    var line = ident.Line;
    var column = ident.Column;
    Diagnostics.Error(File, line, column, $"Cannot define function with name '{name}'");

    var kind = symbolKindToString(symbol.Kind);
    line = symbol.Ident.Line;
    column = symbol.Ident.Column;
    Diagnostics.Note(File, line, column, $"There is a {kind} with the same name");
  }

  private void enterForNode(Node node)
  {
    SymbolTable.EnterScope();
  }

  private void exitForNode(Node node)
  {
    SymbolTable.ExitScope();
  }

  private void exitFuncDeclNode(Node node)
  {
    SymbolTable.ExitScope();
  }

  private void enterSymbolNode(Node node)
  {
    var symbolNode = (SymbolNode) node;
    var ident = symbolNode.Token;

    var symbol = SymbolTable.Resolve(ident);

    if (symbol == null)
    {
      var name = ident.Lexeme;
      var line = ident.Line;
      var column = ident.Column;
      Diagnostics.Error(File, line, column, $"Symbol '{name}' used but not defined");
      return;
    }

    annotateSymbol(node, new SymbolAnnotation(symbol));
  }

  private void enterScopeBlockNode(Node node)
  {
    SymbolTable.EnterScope();
  }

  private void exitScopeBlockNode(Node node)
  {
    SymbolTable.ExitScope();
  }

  private void reportSymbolsDefinedButNotUsed()
  {
    foreach (var symbol in SymbolTable.Symbols)
    {
      if (symbol.IsGlobal) continue;
      if (symbol.Used > 0) continue;

      var kind = symbol.Kind;
      var name = symbol.Name;
      var ident = symbol.Ident;
      var line = ident.Line;
      var column = ident.Column;

      Diagnostics.Warning(File, line, column, $"{kind} '{name}' defined but not used");
    }
  }
}

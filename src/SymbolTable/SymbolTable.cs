
public class SymbolTable
{
  public Dictionary<string, Symbol> Symbols = new Dictionary<string, Symbol>();
  private int scopeDepth { get; set; } = 0;
  private int nextIndex = 0;

  public void EnterScope()
  {
    scopeDepth++;
  }

  public void ExitScope()
  {
    scopeDepth--;
  }

  public Symbol? Define(Token ident, SymbolKind kind)
  {
    var name = ident.Lexeme;

    if (Symbols.ContainsKey(name))
      return null;

    var symbol = new Symbol(ident, kind, scopeDepth, nextIndex);
    Symbols[name] = symbol;
    nextIndex++;

    return symbol;
  }

  public Symbol? Resolve(Token ident)
  {
    var name = ident.Lexeme;
    Symbol? symbol;

    if (!Symbols.TryGetValue(name, out symbol))
      return null;

    symbol.Used++;
    return symbol;
  }
}

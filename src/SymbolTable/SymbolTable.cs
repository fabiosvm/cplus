
public class SymbolTable
{
  public List<Symbol> Symbols = new List<Symbol>();
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

  public (bool, Symbol?) Define(Token ident, SymbolKind kind)
  {
    var name = ident.Lexeme;

    for (var i = Symbols.Count - 1; i > -1; i--)
    {
      var existing = Symbols[i];
      if (existing.Depth < scopeDepth) break;
      if (existing.Name != name) continue;
      return (false, existing);
    }

    var symbol = new Symbol(ident, kind, scopeDepth, nextIndex);
    Symbols.Add(symbol);

    nextIndex++;
    return (true, symbol);
  }

  public Symbol? Resolve(Token ident)
  {
    var name = ident.Lexeme;
    Symbol? symbol = null;

    for (var i = Symbols.Count - 1; i > -1; i--)
    {
      var existing = Symbols[i];
      if (existing.Name != name) continue;
      symbol = existing;
    }

    if (symbol != null) symbol.Used++;

    return symbol;
  }
}

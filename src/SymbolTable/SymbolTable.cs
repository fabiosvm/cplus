
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

  public (bool, Symbol) Define(int line, int column, string lexeme, SymbolKind kind)
  {
    for (var i = Symbols.Count - 1; i > -1; i--)
    {
      var existing = Symbols[i];
      if (existing.Depth < scopeDepth) break;
      if (!existing.Lexeme.Equals(lexeme)) continue;
      return (false, existing);
    }

    var symbol = new Symbol(line, column, lexeme, kind, scopeDepth, nextIndex);
    Symbols.Add(symbol);

    nextIndex++;
    return (true, symbol);
  }

  public Symbol? Resolve(string lexeme)
  {
    Symbol? symbol = null;

    for (var i = Symbols.Count - 1; i > -1; i--)
    {
      var existing = Symbols[i];
      if (!existing.Lexeme.Equals(lexeme)) continue;
      symbol = existing;
      break;
    }

    if (symbol != null) symbol.Used++;

    return symbol;
  }
}


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

  public (bool, Symbol) Define(int line, int column, string name, SymbolKind kind, Node node)
  {
    for (var i = Symbols.Count - 1; i > -1; i--)
    {
      var existing = Symbols[i];
      if (existing.Depth < scopeDepth) break;
      if (!existing.Name.Equals(name)) continue;
      return (false, existing);
    }

    var symbol = new Symbol(line, column, name, kind, scopeDepth, nextIndex, node);
    Symbols.Add(symbol);

    nextIndex++;
    return (true, symbol);
  }

  public Symbol? Resolve(string name)
  {
    Symbol? symbol = null;

    for (var i = Symbols.Count - 1; i > -1; i--)
    {
      var existing = Symbols[i];
      if (!existing.Name.Equals(name)) continue;
      symbol = existing;
      break;
    }

    if (symbol != null) symbol.Used++;

    return symbol;
  }
}

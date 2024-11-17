
public class Symbol
{
  public Token Ident { get; }
  public string Name { get; }
  public SymbolKind Kind { get; }
  public int Depth { get; }
  public int Index { get; }
  public int Used { get; set; } = 0;

  public bool IsLocal => Depth > 0;

  public Symbol(Token ident, SymbolKind kind, int depth, int index)
  {
    Ident = ident;
    Name = ident.Lexeme;
    Kind = kind;
    Depth = depth;
    Index = index;
  }
}

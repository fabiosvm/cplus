
public class Symbol
{
  public Token Ident { get; }
  public SymbolKind Kind { get; }
  public int Depth { get; }
  public int Index { get; }
  public int Used { get; set; } = 0;

  public string Name => Ident.Lexeme;

  public bool IsGlobal => Depth == 0;

  public Symbol(Token ident, SymbolKind kind, int depth, int index)
  {
    Ident = ident;
    Kind = kind;
    Depth = depth;
    Index = index;
  }
}

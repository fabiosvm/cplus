
public class Symbol
{
  public int Line { get; }
  public int Column { get; }
  public string Lexeme { get; }
  public SymbolKind Kind { get; }
  public int Depth { get; }
  public int Index { get; }
  public int Used { get; set; } = 0;

  public bool IsGlobal => Depth == 0;

  public Symbol(int line, int column, string lexeme, SymbolKind kind, int depth, int index)
  {
    Line = line;
    Column = column;
    Lexeme = lexeme;
    Kind = kind;
    Depth = depth;
    Index = index;
  }
}

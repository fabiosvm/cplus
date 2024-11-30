
public class Symbol
{
  public int Line { get; }
  public int Column { get; }
  public string Name { get; }
  public SymbolKind Kind { get; }
  public int Depth { get; }
  public int Index { get; }
  public Node Node { get; }
  public int Used { get; set; } = 0;

  public bool IsGlobal => Depth == 0;

  public Symbol(int line, int column, string name, SymbolKind kind, int depth, int index, Node node)
  {
    Line = line;
    Column = column;
    Name = name;
    Kind = kind;
    Depth = depth;
    Index = index;
    Node = node;
  }
}

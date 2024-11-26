
public class BreakNode : LeafNode
{
  public override string Name { get; } = "Break";
  public override int Line { get; }
  public override int Column { get; }

  public BreakNode(Token token)
  {
    Line = token.Line;
    Column = token.Column;
  }
}


public class LongTypeNode : LeafNode
{
  public override string Name { get; } = "LongType";
  public override int Line { get; }
  public override int Column { get; }

  public LongTypeNode(Token token)
  {
    Line = token.Line;
    Column = token.Column;
  }
}

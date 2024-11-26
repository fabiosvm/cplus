
public class DoubleTypeNode : LeafNode
{
  public override string Name { get; } = "DoubleType";
  public override int Line { get; }
  public override int Column { get; }

  public DoubleTypeNode(Token token)
  {
    Line = token.Line;
    Column = token.Column;
  }
}

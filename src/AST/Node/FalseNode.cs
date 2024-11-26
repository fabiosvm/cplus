
public class FalseNode : LeafNode
{
  public override string Name { get; } = "False";
  public override int Line { get; }
  public override int Column { get; }

  public FalseNode(Token token)
  {
    Line = token.Line;
    Column = token.Column;
  }
}

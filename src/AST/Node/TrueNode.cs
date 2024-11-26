
public class TrueNode : LeafNode
{
  public override string Name { get; } = "True";
  public override int Line { get; }
  public override int Column { get; }

  public TrueNode(Token token)
  {
    Line = token.Line;
    Column = token.Column;
  }
}

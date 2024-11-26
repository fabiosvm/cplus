
public class ContinueNode : LeafNode
{
  public override string Name { get; } = "Continue";
  public override int Line { get; }
  public override int Column { get; }

  public ContinueNode(Token token)
  {
    Line = token.Line;
    Column = token.Column;
  }
}

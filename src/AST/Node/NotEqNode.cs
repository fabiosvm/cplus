
public class NeNode : NonLeafNode
{
  public override string Name { get; } = "Ne";
  public override int Line { get; }
  public override int Column { get; }

  public NeNode(Token token)
  {
    Line = token.Line;
    Column = token.Column;
  }
}

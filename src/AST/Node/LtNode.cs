
public class LtNode : NonLeafNode
{
  public override string Name { get; } = "Lt";
  public override int Line { get; }
  public override int Column { get; }

  public LtNode(Token token)
  {
    Line = token.Line;
    Column = token.Column;
  }
}

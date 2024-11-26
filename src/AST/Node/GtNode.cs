
public class GtNode : NonLeafNode
{
  public override string Name { get; } = "Gt";
  public override int Line { get; }
  public override int Column { get; }

  public GtNode(Token token)
  {
    Line = token.Line;
    Column = token.Column;
  }
}

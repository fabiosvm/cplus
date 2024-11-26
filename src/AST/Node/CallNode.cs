
public class CallNode : NonLeafNode
{
  public override string Name { get; } = "Call";
  public override int Line { get; }
  public override int Column { get; }

  public CallNode(Token token)
  {
    Line = token.Line;
    Column = token.Column;
  }
}

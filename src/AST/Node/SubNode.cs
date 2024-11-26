
public class SubNode : NonLeafNode
{
  public override string Name { get; } = "Sub";
  public override int Line { get; }
  public override int Column { get; }

  public SubNode(Token token)
  {
    Line = token.Line;
    Column = token.Column;
  }
}

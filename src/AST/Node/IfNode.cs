
public class IfNode : NonLeafNode
{
  public override string Name { get; } = "If";
  public override int Line { get; }
  public override int Column { get; }

  public IfNode(Token token)
  {
    Line = token.Line;
    Column = token.Column;
  }
}

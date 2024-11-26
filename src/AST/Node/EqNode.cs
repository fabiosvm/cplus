
public class EqNode : NonLeafNode
{
  public override string Name { get; } = "Eq";
  public override int Line { get; }
  public override int Column { get; }

  public EqNode(Token token)
  {
    Line = token.Line;
    Column = token.Column;
  }
}


public class MulNode : NonLeafNode
{
  public override string Name { get; } = "Mul";
  public override int Line { get; }
  public override int Column { get; }

  public MulNode(Token token)
  {
    Line = token.Line;
    Column = token.Column;
  }
}

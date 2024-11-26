
public class UnaryMinusNode : NonLeafNode
{
  public override string Name { get; } = "UnaryMinus";
  public override int Line { get; }
  public override int Column { get; }

  public UnaryMinusNode(Token token)
  {
    Line = token.Line;
    Column = token.Column;
  }
}

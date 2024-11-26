
public class UnaryPlusNode : NonLeafNode
{
  public override string Name { get; } = "UnaryPlus";
  public override int Line { get; }
  public override int Column { get; }

  public UnaryPlusNode(Token token)
  {
    Line = token.Line;
    Column = token.Column;
  }
}


public class AndNode : NonLeafNode
{
  public override string Name { get; } = "And";
  public override int Line { get; }
  public override int Column { get; }

  public AndNode(Token token)
  {
    Line = token.Line;
    Column = token.Column;
  }
}

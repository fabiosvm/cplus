
public class ForNode : NonLeafNode
{
  public override string Name { get; } = "For";
  public override int Line { get; }
  public override int Column { get; }

  public ForNode(Token token)
  {
    Line = token.Line;
    Column = token.Column;
  }
}

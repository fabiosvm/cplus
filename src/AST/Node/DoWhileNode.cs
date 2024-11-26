
public class DoWhileNode : NonLeafNode
{
  public override string Name { get; } = "DoWhile";
  public override int Line { get; }
  public override int Column { get; }

  public DoWhileNode(Token token)
  {
    Line = token.Line;
    Column = token.Column;
  }
}

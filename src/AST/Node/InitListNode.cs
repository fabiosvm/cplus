
public class InitListNode : NonLeafNode
{
  public override string Name { get; } = "InitList";
  public override int Line { get; }
  public override int Column { get; }

  public InitListNode(Token token)
  {
    Line = token.Line;
    Column = token.Column;
  }
}


public class NotNode : NonLeafNode
{
  public override string Name { get; } = "Not";
  public override int Line { get; }
  public override int Column { get; }

  public NotNode(Token token)
  {
    Line = token.Line;
    Column = token.Column;
  }
}

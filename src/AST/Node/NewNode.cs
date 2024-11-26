
public class NewNode : NonLeafNode
{
  public override string Name { get; } = "New";
  public override int Line { get; }
  public override int Column { get; }

  public NewNode(Token token)
  {
    Line = token.Line;
    Column = token.Column;
  }
}

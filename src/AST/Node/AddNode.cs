
public class AddNode : NonLeafNode
{
  public override string Name { get; } = "Add";
  public override int Line { get; }
  public override int Column { get; }

  public AddNode(Token token)
  {
    Line = token.Line;
    Column = token.Column;
  }
}

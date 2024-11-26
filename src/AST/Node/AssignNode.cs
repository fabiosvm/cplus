
public class AssignNode : NonLeafNode
{
  public override string Name { get; } = "Assign";
  public override int Line { get; }
  public override int Column { get; }

  public AssignNode(Token token)
  {
    Line = token.Line;
    Column = token.Column;
  }
}

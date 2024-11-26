
public class WhileNode : NonLeafNode
{
  public override string Name { get; } = "While";
  public override int Line { get; }
  public override int Column { get; }

  public WhileNode(Token token)
  {
    Line = token.Line;
    Column = token.Column;
  }
}

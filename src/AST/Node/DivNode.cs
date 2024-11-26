
public class DivNode : NonLeafNode
{
  public override string Name { get; } = "Div";
  public override int Line { get; }
  public override int Column { get; }

  public DivNode(Token token)
  {
    Line = token.Line;
    Column = token.Column;
  }
}

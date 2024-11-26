
public class OrNode : NonLeafNode
{
  public override string Name { get; } = "Or";
  public override int Line { get; }
  public override int Column { get; }

  public OrNode(Token token)
  {
    Line = token.Line;
    Column = token.Column;
  }
}

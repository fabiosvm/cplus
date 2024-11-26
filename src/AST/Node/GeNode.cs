
public class GeNode : NonLeafNode
{
  public override string Name { get; } = "Ge";
  public override int Line { get; }
  public override int Column { get; }

  public GeNode(Token token)
  {
    Line = token.Line;
    Column = token.Column;
  }
}

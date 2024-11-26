
public class LeNode : NonLeafNode
{
  public override string Name { get; } = "Le";
  public override int Line { get; }
  public override int Column { get; }

  public LeNode(Token token)
  {
    Line = token.Line;
    Column = token.Column;
  }
}

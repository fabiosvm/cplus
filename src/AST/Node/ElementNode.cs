
public class ElementNode : NonLeafNode
{
  public override string Name { get; } = "Element";
  public override int Line { get; }
  public override int Column { get; }

  public ElementNode(Token token)
  {
    Line = token.Line;
    Column = token.Column;
  }
}

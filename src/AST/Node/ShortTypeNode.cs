
public class ShortTypeNode : LeafNode
{
  public override string Name { get; } = "ShortType";
  public override int Line { get; }
  public override int Column { get; }

  public ShortTypeNode(Token token)
  {
    Line = token.Line;
    Column = token.Column;
  }
}

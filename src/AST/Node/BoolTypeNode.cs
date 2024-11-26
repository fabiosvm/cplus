
public class BoolTypeNode : LeafNode
{
  public override string Name { get; } = "BoolType";
  public override int Line { get; }
  public override int Column { get; }

  public BoolTypeNode(Token token)
  {
    Line = token.Line;
    Column = token.Column;
  }
}

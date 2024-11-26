
public class IntTypeNode : LeafNode
{
  public override string Name { get; } = "IntType";
  public override int Line { get; }
  public override int Column { get; }

  public IntTypeNode(Token token)
  {
    Line = token.Line;
    Column = token.Column;
  }
}

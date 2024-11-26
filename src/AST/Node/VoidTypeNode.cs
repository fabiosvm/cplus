
public class VoidTypeNode : LeafNode
{
  public override string Name { get; } = "VoidType";
  public override int Line { get; }
  public override int Column { get; }

  public VoidTypeNode(Token token)
  {
    Line = token.Line;
    Column = token.Column;
  }
}

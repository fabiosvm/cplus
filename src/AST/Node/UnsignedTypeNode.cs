
public class UnsignedTypeNode : NonLeafNode
{
  public override string Name { get; } = "UnsignedType";
  public override int Line { get; }
  public override int Column { get; }

  public UnsignedTypeNode(Token token)
  {
    Line = token.Line;
    Column = token.Column;
  }
}

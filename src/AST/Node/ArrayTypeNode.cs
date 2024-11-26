
public class ArrayTypeNode : NonLeafNode
{
  public override string Name { get; } = "ArrayType";
  public override int Line { get; }
  public override int Column { get; }

  public ArrayTypeNode(Token token)
  {
    Line = token.Line;
    Column = token.Column;
  }
}

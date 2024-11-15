
public class ArrayTypeNode : NonLeafNode
{
  public override Token Token { get; }
  public override string Name { get; } = "ArrayType";

  public ArrayTypeNode(Token token)
  {
    Token = token;
  }
}

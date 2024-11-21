
public class UnsignedTypeNode : NonLeafNode
{
  public override Token Token { get; }
  public override string Name { get; } = "UnsignedType";

  public UnsignedTypeNode(Token token)
  {
    Token = token;
  }
}

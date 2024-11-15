
public class VoidTypeNode : LeafNode
{
  public override Token Token { get; }
  public override string Name { get; } = "VoidType";

  public VoidTypeNode(Token token)
  {
    Token = token;
  }
}

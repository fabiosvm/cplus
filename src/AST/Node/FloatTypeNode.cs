
public class FloatTypeNode : LeafNode
{
  public override Token Token { get; }
  public override string Name { get; } = "FloatType";

  public FloatTypeNode(Token token)
  {
    Token = token;
  }
}

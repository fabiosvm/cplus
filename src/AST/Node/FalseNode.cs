
public class FalseNode : LeafNode
{
  public override Token Token { get; }
  public override string Name { get; } = "False";

  public FalseNode(Token token)
  {
    Token = token;
  }
}

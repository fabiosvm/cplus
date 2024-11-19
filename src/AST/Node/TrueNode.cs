
public class TrueNode : LeafNode
{
  public override Token Token { get; }
  public override string Name { get; } = "True";

  public TrueNode(Token token)
  {
    Token = token;
  }
}

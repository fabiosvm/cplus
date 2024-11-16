
public class ContinueNode : LeafNode
{
  public override Token Token { get; }
  public override string Name { get; } = "Continue";

  public ContinueNode(Token token)
  {
    Token = token;
  }
}

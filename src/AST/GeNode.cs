
public class GeNode : NonLeafNode
{
  public override Token Token { get; }
  public override string Name { get; } = "Ge";

  public GeNode(Token token)
  {
    Token = token;
  }
}

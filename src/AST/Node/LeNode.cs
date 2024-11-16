
public class LeNode : NonLeafNode
{
  public override Token Token { get; }
  public override string Name { get; } = "Le";

  public LeNode(Token token)
  {
    Token = token;
  }
}

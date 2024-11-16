
public class EqNode : NonLeafNode
{
  public override Token Token { get; }
  public override string Name { get; } = "Eq";

  public EqNode(Token token)
  {
    Token = token;
  }
}

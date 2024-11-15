
public class MulNode : NonLeafNode
{
  public override Token Token { get; }
  public override string Name { get; } = "Mul";

  public MulNode(Token token)
  {
    Token = token;
  }
}

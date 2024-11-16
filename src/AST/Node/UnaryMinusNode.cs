
public class UnaryMinusNode : NonLeafNode
{
  public override Token Token { get; }
  public override string Name { get; } = "UnaryMinus";

  public UnaryMinusNode(Token token)
  {
    Token = token;
  }
}

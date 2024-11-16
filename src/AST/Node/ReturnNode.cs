
public class ReturnNode : NonLeafNode
{
  public override Token Token { get; }
  public override string Name { get; } = "Return";

  public ReturnNode(Token token)
  {
    Token = token;
  }
}

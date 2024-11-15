
public class IfElseNode : NonLeafNode
{
  public override Token Token { get; }
  public override string Name { get; } = "IfElse";

  public IfElseNode(Token token)
  {
    Token = token;
  }
}

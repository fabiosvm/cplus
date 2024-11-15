
public class ParamNode : NonLeafNode
{
  public override Token Token { get; }
  public override string Name { get; } = "Param";

  public ParamNode(Token token)
  {
    Token = token;
  }
}

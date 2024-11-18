
public class ScopeBlockNode : NonLeafNode
{
  public override Token Token { get; }
  public override string Name { get; } = "ScopeBlock";

  public ScopeBlockNode(Token token)
  {
    Token = token;
  }
}

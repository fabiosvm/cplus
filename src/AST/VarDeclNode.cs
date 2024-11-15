
public class VarDeclNode : NonLeafNode
{
  public override Token Token { get; }
  public override string Name { get; } = "VarDecl";

  public VarDeclNode(Token token)
  {
    Token = token;
  }
}


public class FuncDeclNode : NonLeafNode
{
  public override Token Token { get; }
  public override string Name { get; } = "FuncDecl";

  public FuncDeclNode(Token token)
  {
    Token = token;
  }
}

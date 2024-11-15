
public class CharLiteralNode : LeafNode
{
  public override Token Token { get; }
  public override string Name { get; } = "CharLiteral";

  public CharLiteralNode(Token token)
  {
    Token = token;
  }
}

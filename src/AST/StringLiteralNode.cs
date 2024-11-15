
public class StringLiteralNode : LeafNode
{
  public override Token Token { get; }
  public override string Name { get; } = "StringLiteral";

  public StringLiteralNode(Token token)
  {
    Token = token;
  }
}

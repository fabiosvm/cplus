
public class IntLiteralNode : LiteralNode
{
  public override Token Token { get; }
  public override string Name { get; } = "IntLiteral";

  public IntLiteralNode(Token token)
  {
    Token = token;
  }
}
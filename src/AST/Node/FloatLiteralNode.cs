
public class FloatLiteralNode : LiteralNode
{
  public override Token Token { get; }
  public override string Name { get; } = "FloatLiteral";

  public FloatLiteralNode(Token token)
  {
    Token = token;
  }
}

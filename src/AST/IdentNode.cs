
public class IdentNode : LiteralNode
{
  public override Token Token { get; }
  public override string Name { get; } = "Ident";

  public IdentNode(Token token)
  {
    Token = token;
  }
}

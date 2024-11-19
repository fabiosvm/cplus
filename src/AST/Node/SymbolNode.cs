
public class SymbolNode : LiteralNode
{
  public override Token Token { get; }
  public override string Name { get; } = "Symbol";

  public SymbolNode(Token token)
  {
    Token = token;
  }
}

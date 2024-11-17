
public class SymbolNode : LeafNode
{
  public override Token Token { get; }
  public override string Name { get; } = "Symbol";

  public SymbolNode(Token token)
  {
    Token = token;
  }
}


public class IdentNode : LeafNode
{
  public override Token Token { get; }
  public override string Name { get; } = "Ident";

  public IdentNode(Token token)
  {
    Token = token;
  }
}

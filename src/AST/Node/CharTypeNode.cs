
public class CharTypeNode : LeafNode
{
  public override Token Token { get; }
  public override string Name { get; } = "CharType";

  public CharTypeNode(Token token)
  {
    Token = token;
  }
}

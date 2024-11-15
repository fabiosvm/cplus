
public class CharNode : LeafNode
{
  public override Token Token { get; }
  public override string Name { get; } = "Char";

  public CharNode(Token token)
  {
    Token = token;
  }
}

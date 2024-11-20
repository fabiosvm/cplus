
public class InoutNode : LeafNode
{
  public override Token Token { get; }
  public override string Name { get; } = "Inout";

  public InoutNode(Token token)
  {
    Token = token;
  }
}

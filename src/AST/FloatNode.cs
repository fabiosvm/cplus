
public class FloatNode : LeafNode
{
  public override Token Token { get; }
  public override string Name { get; } = "Float";

  public FloatNode(Token token)
  {
    Token = token;
  }
}


public class ModNode : NonLeafNode
{
  public override Token Token { get; }
  public override string Name { get; } = "Mod";

  public ModNode(Token token)
  {
    Token = token;
  }
}

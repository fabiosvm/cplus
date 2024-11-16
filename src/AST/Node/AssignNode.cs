
public class AssignNode : NonLeafNode
{
  public override Token Token { get; }
  public override string Name { get; } = "Assign";

  public AssignNode(Token token)
  {
    Token = token;
  }
}

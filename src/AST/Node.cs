
public interface Node
{
  public static readonly Node Invalid = new InvalidNode();

  Token Token { get; }
  bool IsLeaf { get; }
  string Name { get; }

  void Print(int depth);
}

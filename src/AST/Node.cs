
public interface Node
{
  public static readonly Node None = new NoneNode();

  Token Token { get; }
  bool IsLeaf { get; }
  string Name { get; }

  void Print(int depth);
}

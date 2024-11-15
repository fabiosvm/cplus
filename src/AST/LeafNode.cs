
abstract public class LeafNode : Node
{
  public abstract Token Token { get; }
  public bool IsLeaf { get; } = true;
  public abstract string Name { get; }

  public virtual void Print(int depth)
  {
    Console.WriteLine($"{new string(' ', depth * 2)}{Name}");
  }
}

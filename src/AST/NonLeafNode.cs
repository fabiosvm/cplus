
abstract public class NonLeafNode : Node
{
  public abstract Token Token { get; }
  public bool IsLeaf { get; } = false;
  public abstract string Name { get; }
  public List<Node> Children { get; protected set; } = new List<Node>();

  public void Print(int depth)
  {
    Console.WriteLine($"{new string(' ', depth * 2)}{Name}:");

    if (Children.Count == 0)
    {
      Console.WriteLine($"{new string(' ', (depth + 1) * 2)}<empty>");
      return;
    }

    Console.WriteLine();

    foreach (var child in Children)
    {
      child.Print(depth + 1);
    }
  }
}

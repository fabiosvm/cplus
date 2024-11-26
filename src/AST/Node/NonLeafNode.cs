
abstract public class NonLeafNode : Node
{
  public List<Node> Children { get; protected set; } = new List<Node>();

  public override void Print(int depth)
  {
    Console.WriteLine($"{new string(' ', depth * 2)}{Name}:");

    if (Annotations.Count > 0)
    {
      Console.WriteLine($"{new string(' ', (depth + 1) * 2)}annotations:");

      foreach (var (key, value) in Annotations)
      {
        Console.WriteLine($"{new string(' ', (depth + 2) * 2)}{key}:");
        value.Print(depth + 3);
      }
    }

    if (Children.Count == 0) return;

    Console.WriteLine($"{new string(' ', (depth + 1) * 2)}children:");

    foreach (var child in Children)
      child.Print(depth + 2);
  }
}

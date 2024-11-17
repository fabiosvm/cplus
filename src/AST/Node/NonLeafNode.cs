
abstract public class NonLeafNode : Node
{
  public List<Node> Children { get; protected set; } = new List<Node>();

  public override void Print(int depth)
  {
    Console.WriteLine($"{new string(' ', depth * 2)}{Name}:");

    if (Children.Count == 0)
    {
      Console.WriteLine($"{new string(' ', (depth + 1) * 2)}<empty>");
      return;
    }

    foreach (var child in Children)
      child.Print(depth + 1);
  }
}

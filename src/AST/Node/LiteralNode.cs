
abstract public class LiteralNode : LeafNode
{
  abstract public string Lexeme { get; }

  public override void Print(int depth)
  {
    Console.Write($"{new string(' ', depth * 2)}{Name}('{Lexeme}')");

    if (Annotations.Count == 0)
    {
      Console.WriteLine();
      return;
    }
    Console.WriteLine(":");

    Console.WriteLine($"{new string(' ', (depth + 1) * 2)}(");

    foreach (var (key, value) in Annotations)
    {
      Console.WriteLine($"{new string(' ', (depth + 2) * 2)}{key}:");
      value.Print(depth + 3);
    }

    Console.WriteLine($"{new string(' ', (depth + 1) * 2)})");
  }
}

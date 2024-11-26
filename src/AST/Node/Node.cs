
abstract public class Node
{
  public static readonly Node None = new NoneNode();

  public abstract string Name { get; }
  public abstract int Line { get; }
  public abstract int Column { get; }
  public Dictionary<string, Annotation> Annotations { get; set; } = new Dictionary<string, Annotation>();

  public virtual void Print(int depth)
  {
    Console.Write($"{new string(' ', depth * 2)}{Name}");

    if (Annotations.Count == 0)
    {
      Console.WriteLine();
      return;
    }
    Console.WriteLine(":");

    Console.WriteLine($"{new string(' ', (depth + 1) * 2)}annotations:");

    foreach (var (key, value) in Annotations)
    {
      Console.WriteLine($"{new string(' ', (depth + 2) * 2)}{key}:");
      value.Print(depth + 3);
    }
  }
}

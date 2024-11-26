
abstract public class Annotation
{
  public abstract string Name { get; }

  public virtual void Print(int depth)
  {
    Console.WriteLine($"{new string(' ', depth * 2)}{Name}");
  }
}

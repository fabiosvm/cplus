
abstract public class Node
{
  public static readonly Node None = new NoneNode();

  public abstract Token Token { get; }
  public abstract string Name { get; }
  public Dictionary<string, Annotation> Annotations { get; set; } = new Dictionary<string, Annotation>();

  public abstract void Print(int depth);
}

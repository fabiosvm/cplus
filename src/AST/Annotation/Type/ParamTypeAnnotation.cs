
public class ParamTypeAnnotation : TypeAnnotation
{
  public override string Name => IsInout ? "inout " + Type.Name : Type.Name;
  public bool IsInout { get; }
  public TypeAnnotation Type { get; }

  public ParamTypeAnnotation(bool isInout, TypeAnnotation type)
  {
    IsInout = isInout;
    Type = type;
  }

  public override void Print(int depth)
  {
    Console.WriteLine($"{new string(' ', depth * 2)}{Name}:");
    Console.WriteLine($"{new string(' ', (depth + 1) * 2)}isInout={IsInout}");
    Type.Print(depth + 1);
  }
}


public class FunctionTypeAnnotation : TypeAnnotation
{
  public override string Name { get; } = "FunctionType";
  public TypeAnnotation ReturnType { get; }
  public List<TypeAnnotation> ParameterTypes { get; } = new List<TypeAnnotation>();

  public FunctionTypeAnnotation(TypeAnnotation returnType)
  {
    ReturnType = returnType;
  }

  public override void Print(int depth)
  {
    Console.WriteLine($"{new string(' ', depth * 2)}{Name}:");

    Console.WriteLine($"{new string(' ', (depth + 1) * 2)}returnType:");
    ReturnType.Print(depth + 2);

    Console.WriteLine($"{new string(' ', (depth + 1) * 2)}parameterTypes:");

    foreach (var parameterType in ParameterTypes)
      parameterType.Print(depth + 2);
  }
}

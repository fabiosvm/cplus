
public class FunctionTypeAnnotation : TypeAnnotation
{
  public TypeAnnotation ReturnType { get; }
  public List<TypeAnnotation> ParameterTypes { get; } = new List<TypeAnnotation>();

  public FunctionTypeAnnotation(TypeAnnotation returnType)
  {
    ReturnType = returnType;
  }
}

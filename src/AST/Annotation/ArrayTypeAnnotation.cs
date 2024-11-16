
public class ArrayTypeAnnotation : TypeAnnotation
{
  public TypeAnnotation ElementType { get; }

  public ArrayTypeAnnotation(TypeAnnotation elementType) : base(TypeAnnotationKind.Array)
  {
    ElementType = elementType;
  }
}

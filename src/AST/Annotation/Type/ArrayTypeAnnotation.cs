
public class ArrayTypeAnnotation : TypeAnnotation
{
  public TypeAnnotation ElementType { get; }

  public ArrayTypeAnnotation(TypeAnnotation elementType)
  {
    ElementType = elementType;
  }
}

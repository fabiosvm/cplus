
public class ArrayTypeAnnotation : TypeAnnotation
{
  public override string Name => ElementType.Name + "[]";

  public TypeAnnotation ElementType { get; }

  public ArrayTypeAnnotation(TypeAnnotation elementType)
  {
    ElementType = elementType;
  }

  public ArrayTypeAnnotation(ArrayTypeNode arrayType)
  {
    var type = arrayType.Children[0];
    ElementType = TypeAnnotation.FromNode(type);
  }
}

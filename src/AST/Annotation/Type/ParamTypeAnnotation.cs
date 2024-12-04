
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

  public ParamTypeAnnotation(ParamNode param)
  {
    var inout = param.Children[0];
    var type = param.Children[1];

    IsInout = inout is InoutNode;
    Type = TypeAnnotation.FromNode(type);
  }
}

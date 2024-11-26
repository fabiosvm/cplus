
public class NoneNode : Node
{
  public override string Name { get; } = "None";
  public override int Line => throw new NotImplementedException();
  public override int Column => throw new NotImplementedException();
}

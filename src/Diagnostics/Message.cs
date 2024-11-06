
public class Message
{
  public MessageKind Kind { get; }
  public string Text { get; }

  public Message(MessageKind kind, string text)
  {
    Kind = kind;
    Text = text;
  }

  public override string ToString()
  {
    return $"{Kind}: {Text}";
  }
}

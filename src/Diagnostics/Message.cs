
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
    var kind = kindToString();
    return $"{kind}: {Text}";
  }

  private string kindToString()
  {
    var str = "NOTE";
    switch (Kind)
    {
    case MessageKind.Note:
      break;
    case MessageKind.Warning:
      str = "WARN";
      break;
    case MessageKind.Error:
      str = "ERROR";
      break;
    case MessageKind.Fatal:
      str = "FATAL";
      break;
    default:
      throw new ArgumentOutOfRangeException(nameof(Kind), Kind, null);
    }
    return str;
  }
}


public class Message
{
  public string File { get; }
  public int Line { get; }
  public int Column { get; }
  public MessageKind Kind { get; }
  public string Text { get; }

  public Message(string file, int line, int column, MessageKind kind, string text)
  {
    File = file;
    Line = line;
    Column = column;
    Kind = kind;
    Text = text;
  }

  public void Print()
  {
    var kind = kindToString();
    Console.WriteLine($"{File}({Line}, {Column}): {kind}: {Text}");
  }

  private string kindToString()
  {
    var str = "note";
    switch (Kind)
    {
    case MessageKind.Note:
      break;
    case MessageKind.Warning:
      str = "warning";
      break;
    case MessageKind.Error:
      str = "error";
      break;
    case MessageKind.Fatal:
      str = "fatal";
      break;
    default:
      throw new ArgumentOutOfRangeException(nameof(Kind), Kind, null);
    }
    return str;
  }
}

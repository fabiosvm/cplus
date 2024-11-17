
internal static class Program
{
  private static int Main(string[] args)
  {
    checkArgs(args);

    var path = args[0];
    var source = loadSource(path);

    compile(source);

    return 0;
  }

  private static void checkArgs(string[] args)
  {
    if (args.Length < 1)
    {
      printError("No input file");
      printUsage();
      Environment.Exit(1);
    }
  }

  private static void printError(string message)
  {
    Console.WriteLine($"Error: {message}");
  }

  private static void printUsage()
  {
    Console.WriteLine($"Usage: cplus <input>");
  }

  private static string loadSource(string path)
  {
    string source = "";
    try {
      source = File.ReadAllText(path);
    } catch (FileNotFoundException) {
      printError($"File not found: {path}");
      Environment.Exit(1);
    }
    return source;
  }

  private static void compile(string source)
  {
    var parser = new Parser(source);
    parser.Parse();

    var binder = new Binder(parser.Ast, parser.Diagnostics);
    binder.Bind();

    binder.Diagnostics.Print();
    binder.Ast.Print(0);
  }
}

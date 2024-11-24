
internal static class Program
{
  private static int Main(string[] args)
  {
    checkArgs(args);
    var file = args[0];
    compile(file);
    return 0;
  }

  private static void checkArgs(string[] args)
  {
    if (args.Length < 1)
    {
      printError("No source file provided");
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
    Console.WriteLine($"Usage: cplus <source-file>");
  }

  private static string loadSource(string file)
  {
    string source = "";
    try {
      source = File.ReadAllText(file);
    } catch (FileNotFoundException) {
      printError($"File not found: {file}");
      Environment.Exit(1);
    }
    return source;
  }

  private static void compile(string file)
  {
    var source = loadSource(file);

    var parser = new Parser(file, source);
    parser.Parse();

    var diagnostics = parser.Diagnostics;

    if (diagnostics.IsFatal())
    {
      diagnostics.Print();
      Environment.Exit(1);
    }

    var ast = parser.Ast;

    var binder = new Binder(file, ast, diagnostics);
    binder.Bind();

    if (diagnostics.HasErrors())
    {
      diagnostics.Print();
      Environment.Exit(1);
    }

    diagnostics.Print();
    ast.Print(0);
  }
}

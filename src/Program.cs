
internal static class Program
{
  private static int Main(string[] args)
  {
    checkArgs(args);

    var path = args[0];
    var source = loadSource(path);

    var diagnostics = compile(source);

    diagnostics.Print();
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
    return File.ReadAllText(path);
  }

  private static Diagnostics compile(string source)
  {
    var compiler = new Compiler(source);
    compiler.Compile();
    return compiler.Diagnostics;
  }
}

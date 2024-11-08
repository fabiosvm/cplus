
public class Compiler
{
  public Lexer Lexer { get; }
  public Diagnostics Diagnostics { get; }

  public Compiler(string source)
  {
    Lexer = new Lexer(source);
    Diagnostics = Lexer.Diagnostics;
  }

  public void Compile()
  {
    compileModule();
    if (isFatal()) return;
    Diagnostics.Report(MessageKind.Note, "Compilation finished successfully");
  }

  private Token currentToken() => Lexer.CurrentToken;

  private bool match(TokenKind kind) => Lexer.Match(kind);

  private void nextToken() => Lexer.NextToken();

  private void consume(TokenKind kind)
  {
    if (!match(kind))
    {
      reportUnexpectedToken();
      return;
    }
    nextToken();
  }

  private bool isFatal() => Diagnostics.IsFatal();

  private void reportUnexpectedToken()
  {
    var line = currentToken().Line;
    var column = currentToken().Column;
    var lexeme = currentToken().Lexeme;
    var text = match(TokenKind.Eof) ? "end of file" : $"token '{lexeme}'";
    Diagnostics.Report(MessageKind.Fatal, $"Unexpected {text} [{line}:{column}]");
  }

  private void compileModule()
  {
    while (!match(TokenKind.Eof))
    {
      compileDecl();
      if (isFatal()) return;
    }
  }

  private void compileDecl()
  {
    if (match(TokenKind.VarKW))
    {
      compileVarDecl();
      return;
    }

    if (match(TokenKind.FuncKW))
    {
      compileFuncDecl();
      return;
    }

    reportUnexpectedToken();
  }

  private void compileVarDecl()
  {
    nextToken();
    if (isFatal()) return;
  
    compileType();
    if (isFatal()) return;
  
    if (!match(TokenKind.Ident))
    {
      reportUnexpectedToken();
      return;
    }
    nextToken();
    if (isFatal()) return;
  
    if (match(TokenKind.Eq))
    {
      nextToken();
      if (isFatal()) return;
  
      compileExpr();
      if (isFatal()) return;
    }
  
    consume(TokenKind.Semicolon);
  }

  private void compileType()
  {
    if (match(TokenKind.IntKW))
    {
      nextToken();
      return;
    }

    if (match(TokenKind.FloatKW))
    {
      nextToken();
      return;
    }

    reportUnexpectedToken();
  }

  private void compileFuncDecl()
  {
    nextToken();
    if (isFatal()) return;

    compileRetType();
    if (isFatal()) return;

    if (!match(TokenKind.Ident))
    {
      reportUnexpectedToken();
      return;
    }
    nextToken();
    if (isFatal()) return;

    compileParamList();
    if (isFatal()) return;

    compileBlock();
  }

  private void compileRetType()
  {
    if (match(TokenKind.VoidKW))
    {
      nextToken();
      return;
    }

    compileType();
  }

  private void compileParamList()
  {
    consume(TokenKind.LParen);
    if (isFatal()) return;

    if (match(TokenKind.RParen))
    {
      nextToken();
      return;
    }

    compileParam();
    if (isFatal()) return;

    while (match(TokenKind.Comma))
    {
      nextToken();
      if (isFatal()) return;

      compileParam();
      if (isFatal()) return;
    }

    consume(TokenKind.RParen);
  }

  private void compileParam()
  {
    compileType();
    if (isFatal()) return;

    if (!match(TokenKind.Ident))
    {
      reportUnexpectedToken();
      return;
    }
    nextToken();
  }

  private void compileBlock()
  {
    consume(TokenKind.LBrace);
    if (isFatal()) return;

    while (!match(TokenKind.RBrace))
    {
      compileStmt();
      if (isFatal()) return;
    }

    consume(TokenKind.RBrace);
  }

  private void compileStmt()
  {
    if (match(TokenKind.VarKW))
    {
      compileVarDecl();
      return;
    }
  
    if (match(TokenKind.Ident))
      if (compileAssignStmt() || isFatal()) return;

    if (match(TokenKind.ReturnKW))
    {
      compileReturnStmt();
      return;
    }

    compileExprStmt();
  }

  private bool compileAssignStmt()
  {
    LexerState state = Lexer.Save();

    nextToken();
    if (isFatal()) return false;

    if (!match(TokenKind.Eq))
    {
      Lexer.Restore(state);
      return false;
    }
    nextToken();
    if (isFatal()) return false;

    compileExpr();
    if (isFatal()) return false;

    consume(TokenKind.Semicolon);
    return true;
  }

  private void compileReturnStmt()
  {
    nextToken();
    if (isFatal()) return;

    if (match(TokenKind.Semicolon))
    {
      nextToken();
      return;
    }

    compileExpr();
    if (isFatal()) return;

    consume(TokenKind.Semicolon);
  }

  private void compileExprStmt()
  {
    compileExpr();
    if (isFatal()) return;

    consume(TokenKind.Semicolon);
  }

  private void compileExpr()
  {
    compileMulExpr();
    if (isFatal()) return;

    for (;;)
    {
      if (match(TokenKind.Plus))
      {
        nextToken();
        if (isFatal()) return;

        compileMulExpr();
        if (isFatal()) return;
        continue;
      }
    
      if (match(TokenKind.Minus))
      {
        nextToken();
        if (isFatal()) return;

        compileMulExpr();
        if (isFatal()) return;
        continue;
      }
    
      break;
    }
  }

  private void compileMulExpr()
  {
    compileUnaryExpr();
    if (isFatal()) return;

    for (;;)
    {
      if (match(TokenKind.Star))
      {
        nextToken();
        if (isFatal()) return;

        compileUnaryExpr();
        if (isFatal()) return;
        continue;
      }
    
      if (match(TokenKind.Slash))
      {
        nextToken();
        if (isFatal()) return;

        compileUnaryExpr();
        if (isFatal()) return;
        continue;
      }
    
      if (match(TokenKind.Percent))
      {
        nextToken();
        if (isFatal()) return;

        compileUnaryExpr();
        if (isFatal()) return;
        continue;
      }
    
      break;
    }
  }

  private void compileUnaryExpr()
  {
    if (match(TokenKind.Plus))
    {
      nextToken();
      if (isFatal()) return;

      compileUnaryExpr();
      return;
    }

    if (match(TokenKind.Minus))
    {
      nextToken();
      if (isFatal()) return;

      compileUnaryExpr();
      return;
    }

    compileCallExpr();
  }

  private void compileCallExpr()
  {
    compilePrimaryExpr();
    if (isFatal()) return;

    if (match(TokenKind.LParen))
    {
      nextToken();
      if (isFatal()) return;

      if (match(TokenKind.RParen))
      {
        nextToken();
        return;
      }

      compileExpr();
      if (isFatal()) return;

      while (match(TokenKind.Comma))
      {
        nextToken();
        if (isFatal()) return;

        compileExpr();
        if (isFatal()) return;
      }

      consume(TokenKind.RParen);
    }
  }

  private void compilePrimaryExpr()
  {
    if (match(TokenKind.IntLiteral))
    {
      nextToken();
      return;
    }

    if (match(TokenKind.FloatLiteral))
    {
      nextToken();
      return;
    }

    if (match(TokenKind.Ident))
    {
      nextToken();
      return;
    }

    if (match(TokenKind.LParen))
    {
      nextToken();
      if (isFatal()) return;

      compileExpr();
      if (isFatal()) return;

      consume(TokenKind.RParen);
      return;
    }

    reportUnexpectedToken();
  }
}

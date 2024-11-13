
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
    compilePrimitiveType();
    if (isFatal()) return;

    while (match(TokenKind.LBracket))
    {
      nextToken();
      if (isFatal()) return;

      consume(TokenKind.RBracket);
      if (isFatal()) return;
    }
  }

  private void compilePrimitiveType()
  {
    if (match(TokenKind.CharKW))
    {
      nextToken();
      return;
    }

    if (match(TokenKind.ShortKW))
    {
      nextToken();
      return;
    }

    if (match(TokenKind.IntKW))
    {
      nextToken();
      return;
    }

    if (match(TokenKind.LongKW))
    {
      nextToken();
      return;
    }

    if (match(TokenKind.FloatKW))
    {
      nextToken();
      return;
    }

    if (match(TokenKind.DoubleKW))
    {
      nextToken();
      return;
    }

    if (match(TokenKind.StringKW))
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

    if (!match(TokenKind.LBrace))
    {
      reportUnexpectedToken();
      return;
    }
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
    nextToken();
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

    if (match(TokenKind.IfKW))
    {
      compileIfStmt();
      return;
    }

    if (match(TokenKind.WhileKW))
    {
      compileWhileStmt();
      return;
    }

    if (match(TokenKind.DoKW))
    {
      compileDoWhileStmt();
      return;
    }

    if (match(TokenKind.BreakKW))
    {
      compileBreakStmt();
      return;
    }

    if (match(TokenKind.ContinueKW))
    {
      compileContinueStmt();
      return;
    }

    if (match(TokenKind.ReturnKW))
    {
      compileReturnStmt();
      return;
    }

    if (match(TokenKind.LBrace))
    {
      compileBlock();
      return;
    }

    compileExprStmt();
  }

  private bool compileAssignStmt()
  {
    LexerState state = Lexer.Save();

    nextToken();
    if (isFatal()) return false;

    while (match(TokenKind.LBracket))
    {
      compileSubscr();
      if (isFatal()) return false;
    }

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

  private void compileSubscr()
  {
    nextToken();
    if (isFatal()) return;

    compileExpr();
    if (isFatal()) return;

    consume(TokenKind.RBracket);
  }

  private void compileIfStmt()
  {
    nextToken();
    if (isFatal()) return;

    consume(TokenKind.LParen);
    if (isFatal()) return;

    compileExpr();
    if (isFatal()) return;

    consume(TokenKind.RParen);
    if (isFatal()) return;

    compileStmt();
    if (isFatal()) return;

    if (match(TokenKind.ElseKW))
    {
      nextToken();
      if (isFatal()) return;

      compileStmt();
    }
  }

  private void compileWhileStmt()
  {
    nextToken();
    if (isFatal()) return;

    consume(TokenKind.LParen);
    if (isFatal()) return;

    compileExpr();
    if (isFatal()) return;

    consume(TokenKind.RParen);
    if (isFatal()) return;

    compileStmt();
  }

  private void compileDoWhileStmt()
  {
    nextToken();
    if (isFatal()) return;

    compileStmt();
    if (isFatal()) return;

    consume(TokenKind.WhileKW);
    if (isFatal()) return;

    consume(TokenKind.LParen);
    if (isFatal()) return;

    compileExpr();
    if (isFatal()) return;

    consume(TokenKind.RParen);
    if (isFatal()) return;

    consume(TokenKind.Semicolon);
  }

  private void compileBreakStmt()
  {
    nextToken();
    consume(TokenKind.Semicolon);
  }

  private void compileContinueStmt()
  {
    nextToken();
    consume(TokenKind.Semicolon);
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
    compileAndExpr();
    if (isFatal()) return;

    while (match(TokenKind.PipePipe))
    {
      nextToken();
      if (isFatal()) return;

      compileAndExpr();
      if (isFatal()) return;
    }
  }

  private void compileAndExpr()
  {
    compileEqExpr();
    if (isFatal()) return;

    while (match(TokenKind.AmpAmp))
    {
      nextToken();
      if (isFatal()) return;

      compileEqExpr();
      if (isFatal()) return;
    }
  }

  private void compileEqExpr()
  {
    compileRelExpr();
    if (isFatal()) return;

    for (;;)
    {
      if (match(TokenKind.EqEq))
      {
        nextToken();
        if (isFatal()) return;

        compileRelExpr();
        if (isFatal()) return;
        continue;
      }

      if (match(TokenKind.NotEq))
      {
        nextToken();
        if (isFatal()) return;

        compileRelExpr();
        if (isFatal()) return;
        continue;
      }

      break;
    }
  }

  private void compileRelExpr()
  {
    compileAddExpr();
    if (isFatal()) return;

    for (;;)
    {
      if (match(TokenKind.Lt))
      {
        nextToken();
        if (isFatal()) return;

        compileAddExpr();
        if (isFatal()) return;
        continue;
      }

      if (match(TokenKind.LtEq))
      {
        nextToken();
        if (isFatal()) return;

        compileAddExpr();
        if (isFatal()) return;
        continue;
      }

      if (match(TokenKind.Gt))
      {
        nextToken();
        if (isFatal()) return;

        compileAddExpr();
        if (isFatal()) return;
        continue;
      }

      if (match(TokenKind.GtEq))
      {
        nextToken();
        if (isFatal()) return;

        compileAddExpr();
        if (isFatal()) return;
        continue;
      }

      break;
    }
  }

  private void compileAddExpr()
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
    if (match(TokenKind.Not))
    {
      nextToken();
      if (isFatal()) return;

      compileUnaryExpr();
      return;
    }

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

    for (;;)
    {
      if (match(TokenKind.LBracket))
      {
        compileSubscr();
        if (isFatal()) return;
        continue;
      }

      if (match(TokenKind.LParen))
      {
        compileCall();
        if (isFatal()) return;
        continue;
      }

      break;
    }
  }

  private void compileCall()
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

    if (match(TokenKind.CharLiteral))
    {
      nextToken();
      return;
    }

    if (match(TokenKind.StringLiteral))
    {
      nextToken();
      return;
    }

    if (match(TokenKind.LBracket))
    {
      compileArray();
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

  private void compileArray()
  {
    nextToken();
    if (isFatal()) return;

    if (match(TokenKind.RBracket))
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

    consume(TokenKind.RBracket);
  }
}

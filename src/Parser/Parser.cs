
using System.Collections;

public class Parser
{
  public Lexer Lexer { get; }
  public Diagnostics Diagnostics { get; }
  public Node Ast { get; private set; } = new InvalidNode();

  public Parser(string source)
  {
    Lexer = new Lexer(source);
    Diagnostics = Lexer.Diagnostics;
  }

  // TODO: Implement AST generation.
  public void Parse()
  {
    parseModule();
    if (isFatal()) return;
    Diagnostics.Report(MessageKind.Note, "Parsing completed successfully");
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

  private void parseModule()
  {
    while (!match(TokenKind.Eof))
    {
      parseDecl();
      if (isFatal()) return;
    }
  }

  private void parseDecl()
  {
    if (match(TokenKind.VarKW))
    {
      parseVarDecl();
      return;
    }

    if (match(TokenKind.FuncKW))
    {
      parseFuncDecl();
      return;
    }

    reportUnexpectedToken();
  }

  private void parseVarDecl()
  {
    nextToken();
    if (isFatal()) return;

    parseType();
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

      parseExpr();
      if (isFatal()) return;
    }

    consume(TokenKind.Semicolon);
  }

  private void parseType()
  {
    parsePrimitiveType();
    if (isFatal()) return;

    while (match(TokenKind.LBracket))
    {
      nextToken();
      if (isFatal()) return;

      consume(TokenKind.RBracket);
      if (isFatal()) return;
    }
  }

  private void parsePrimitiveType()
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

  private void parseFuncDecl()
  {
    nextToken();
    if (isFatal()) return;

    parseRetType();
    if (isFatal()) return;

    if (!match(TokenKind.Ident))
    {
      reportUnexpectedToken();
      return;
    }
    nextToken();
    if (isFatal()) return;

    parseParamList();
    if (isFatal()) return;

    if (!match(TokenKind.LBrace))
    {
      reportUnexpectedToken();
      return;
    }
    parseBlock();
  }

  private void parseRetType()
  {
    if (match(TokenKind.VoidKW))
    {
      nextToken();
      return;
    }

    parseType();
  }

  private void parseParamList()
  {
    consume(TokenKind.LParen);
    if (isFatal()) return;

    if (match(TokenKind.RParen))
    {
      nextToken();
      return;
    }

    parseParam();
    if (isFatal()) return;

    while (match(TokenKind.Comma))
    {
      nextToken();
      if (isFatal()) return;

      parseParam();
      if (isFatal()) return;
    }

    consume(TokenKind.RParen);
  }

  private void parseParam()
  {
    parseType();
    if (isFatal()) return;

    if (!match(TokenKind.Ident))
    {
      reportUnexpectedToken();
      return;
    }
    nextToken();
  }

  private void parseBlock()
  {
    nextToken();
    if (isFatal()) return;

    while (!match(TokenKind.RBrace))
    {
      parseStmt();
      if (isFatal()) return;
    }

    consume(TokenKind.RBrace);
  }

  private void parseStmt()
  {
    if (match(TokenKind.VarKW))
    {
      parseVarDecl();
      return;
    }
  
    if (match(TokenKind.Ident))
      if (parseAssignStmt() || isFatal()) return;

    if (match(TokenKind.IfKW))
    {
      parseIfStmt();
      return;
    }

    if (match(TokenKind.WhileKW))
    {
      parseWhileStmt();
      return;
    }

    if (match(TokenKind.DoKW))
    {
      parseDoWhileStmt();
      return;
    }

    if (match(TokenKind.BreakKW))
    {
      parseBreakStmt();
      return;
    }

    if (match(TokenKind.ContinueKW))
    {
      parseContinueStmt();
      return;
    }

    if (match(TokenKind.ReturnKW))
    {
      parseReturnStmt();
      return;
    }

    if (match(TokenKind.LBrace))
    {
      parseBlock();
      return;
    }

    parseExprStmt();
  }

  private bool parseAssignStmt()
  {
    LexerState state = Lexer.Save();

    nextToken();
    if (isFatal()) return false;

    while (match(TokenKind.LBracket))
    {
      parseSubscr();
      if (isFatal()) return false;
    }

    if (!match(TokenKind.Eq))
    {
      Lexer.Restore(state);
      return false;
    }
    nextToken();
    if (isFatal()) return false;

    parseExpr();
    if (isFatal()) return false;

    consume(TokenKind.Semicolon);
    return true;
  }

  private void parseSubscr()
  {
    nextToken();
    if (isFatal()) return;

    parseExpr();
    if (isFatal()) return;

    consume(TokenKind.RBracket);
  }

  private void parseIfStmt()
  {
    nextToken();
    if (isFatal()) return;

    consume(TokenKind.LParen);
    if (isFatal()) return;

    parseExpr();
    if (isFatal()) return;

    consume(TokenKind.RParen);
    if (isFatal()) return;

    parseStmt();
    if (isFatal()) return;

    if (match(TokenKind.ElseKW))
    {
      nextToken();
      if (isFatal()) return;

      parseStmt();
    }
  }

  private void parseWhileStmt()
  {
    nextToken();
    if (isFatal()) return;

    consume(TokenKind.LParen);
    if (isFatal()) return;

    parseExpr();
    if (isFatal()) return;

    consume(TokenKind.RParen);
    if (isFatal()) return;

    parseStmt();
  }

  private void parseDoWhileStmt()
  {
    nextToken();
    if (isFatal()) return;

    parseStmt();
    if (isFatal()) return;

    consume(TokenKind.WhileKW);
    if (isFatal()) return;

    consume(TokenKind.LParen);
    if (isFatal()) return;

    parseExpr();
    if (isFatal()) return;

    consume(TokenKind.RParen);
    if (isFatal()) return;

    consume(TokenKind.Semicolon);
  }

  private void parseBreakStmt()
  {
    nextToken();
    consume(TokenKind.Semicolon);
  }

  private void parseContinueStmt()
  {
    nextToken();
    consume(TokenKind.Semicolon);
  }

  private void parseReturnStmt()
  {
    nextToken();
    if (isFatal()) return;

    if (match(TokenKind.Semicolon))
    {
      nextToken();
      return;
    }

    parseExpr();
    if (isFatal()) return;

    consume(TokenKind.Semicolon);
  }

  private void parseExprStmt()
  {
    parseExpr();
    if (isFatal()) return;

    consume(TokenKind.Semicolon);
  }

  private void parseExpr()
  {
    parseAndExpr();
    if (isFatal()) return;

    while (match(TokenKind.PipePipe))
    {
      nextToken();
      if (isFatal()) return;

      parseAndExpr();
      if (isFatal()) return;
    }
  }

  private void parseAndExpr()
  {
    parseEqExpr();
    if (isFatal()) return;

    while (match(TokenKind.AmpAmp))
    {
      nextToken();
      if (isFatal()) return;

      parseEqExpr();
      if (isFatal()) return;
    }
  }

  private void parseEqExpr()
  {
    parseRelExpr();
    if (isFatal()) return;

    for (;;)
    {
      if (match(TokenKind.EqEq))
      {
        nextToken();
        if (isFatal()) return;

        parseRelExpr();
        if (isFatal()) return;
        continue;
      }

      if (match(TokenKind.NotEq))
      {
        nextToken();
        if (isFatal()) return;

        parseRelExpr();
        if (isFatal()) return;
        continue;
      }

      break;
    }
  }

  private void parseRelExpr()
  {
    parseAddExpr();
    if (isFatal()) return;

    for (;;)
    {
      if (match(TokenKind.Lt))
      {
        nextToken();
        if (isFatal()) return;

        parseAddExpr();
        if (isFatal()) return;
        continue;
      }

      if (match(TokenKind.LtEq))
      {
        nextToken();
        if (isFatal()) return;

        parseAddExpr();
        if (isFatal()) return;
        continue;
      }

      if (match(TokenKind.Gt))
      {
        nextToken();
        if (isFatal()) return;

        parseAddExpr();
        if (isFatal()) return;
        continue;
      }

      if (match(TokenKind.GtEq))
      {
        nextToken();
        if (isFatal()) return;

        parseAddExpr();
        if (isFatal()) return;
        continue;
      }

      break;
    }
  }

  private void parseAddExpr()
  {
    parseMulExpr();
    if (isFatal()) return;

    for (;;)
    {
      if (match(TokenKind.Plus))
      {
        nextToken();
        if (isFatal()) return;

        parseMulExpr();
        if (isFatal()) return;
        continue;
      }

      if (match(TokenKind.Minus))
      {
        nextToken();
        if (isFatal()) return;

        parseMulExpr();
        if (isFatal()) return;
        continue;
      }

      break;
    }
  }

  private void parseMulExpr()
  {
    parseUnaryExpr();
    if (isFatal()) return;

    for (;;)
    {
      if (match(TokenKind.Star))
      {
        nextToken();
        if (isFatal()) return;

        parseUnaryExpr();
        if (isFatal()) return;
        continue;
      }
    
      if (match(TokenKind.Slash))
      {
        nextToken();
        if (isFatal()) return;

        parseUnaryExpr();
        if (isFatal()) return;
        continue;
      }
    
      if (match(TokenKind.Percent))
      {
        nextToken();
        if (isFatal()) return;

        parseUnaryExpr();
        if (isFatal()) return;
        continue;
      }
    
      break;
    }
  }

  private void parseUnaryExpr()
  {
    if (match(TokenKind.Not))
    {
      nextToken();
      if (isFatal()) return;

      parseUnaryExpr();
      return;
    }

    if (match(TokenKind.Plus))
    {
      nextToken();
      if (isFatal()) return;

      parseUnaryExpr();
      return;
    }

    if (match(TokenKind.Minus))
    {
      nextToken();
      if (isFatal()) return;

      parseUnaryExpr();
      return;
    }

    parseCallExpr();
  }

  private void parseCallExpr()
  {
    parsePrimaryExpr();
    if (isFatal()) return;

    for (;;)
    {
      if (match(TokenKind.LBracket))
      {
        parseSubscr();
        if (isFatal()) return;
        continue;
      }

      if (match(TokenKind.LParen))
      {
        parseCall();
        if (isFatal()) return;
        continue;
      }

      break;
    }
  }

  private void parseCall()
  {
    nextToken();
    if (isFatal()) return;

    if (match(TokenKind.RParen))
    {
      nextToken();
      return;
    }

    parseExpr();
    if (isFatal()) return;

    while (match(TokenKind.Comma))
    {
      nextToken();
      if (isFatal()) return;

      parseExpr();
      if (isFatal()) return;
    }

    consume(TokenKind.RParen);
  }

  private void parsePrimaryExpr()
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
      parseArray();
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

      parseExpr();
      if (isFatal()) return;

      consume(TokenKind.RParen);
      return;
    }

    reportUnexpectedToken();
  }

  private void parseArray()
  {
    nextToken();
    if (isFatal()) return;

    if (match(TokenKind.RBracket))
    {
      nextToken();
      return;
    }

    parseExpr();
    if (isFatal()) return;

    while (match(TokenKind.Comma))
    {
      nextToken();
      if (isFatal()) return;

      parseExpr();
      if (isFatal()) return;
    }

    consume(TokenKind.RBracket);
  }
}

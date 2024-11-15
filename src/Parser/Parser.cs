
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

  public void Parse()
  {
    var ast = parseModule();
    if (isFatal()) return;

    Diagnostics.Report(MessageKind.Note, "Parsing completed successfully");
    Ast = ast;
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

  private Node parseModule()
  {
    var ast = new ModuleNode();
    while (!match(TokenKind.Eof))
    {
      var decl = parseDecl();
      if (isFatal()) return Node.Invalid;
      ast.Children.Add(decl);
    }
    return ast;
  }

  private Node parseDecl()
  {
    if (match(TokenKind.VarKW))
      return parseVarDecl();

    if (match(TokenKind.FuncKW))
      return parseFuncDecl();

    reportUnexpectedToken();
    return Node.Invalid;
  }

  private Node parseVarDecl()
  {
    var varToken = currentToken();
    nextToken();
    if (isFatal()) return Node.Invalid;

    // TODO: Append type to the VarDecl.
    parseType();
    if (isFatal()) return Node.Invalid;

    if (!match(TokenKind.Ident))
    {
      reportUnexpectedToken();
      return Node.Invalid;
    }
    var identToken = currentToken();
    nextToken();
    if (isFatal()) return Node.Invalid;

    var ident = new IdentNode(identToken);
    var varDecl = new VarDeclNode(varToken);
    varDecl.Children.Add(ident);

    if (match(TokenKind.Eq))
    {
      var eqToken = currentToken();
      nextToken();
      if (isFatal()) return Node.Invalid;

      var expr = parseExpr();
      if (isFatal()) return Node.Invalid;

      consume(TokenKind.Semicolon);
      if (isFatal()) return Node.Invalid;

      var assign = new AssignNode(eqToken);
      assign.Children.Add(varDecl);
      assign.Children.Add(expr);
      return assign;
    }

    consume(TokenKind.Semicolon);
    if (isFatal()) return Node.Invalid;

    return varDecl;
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

    reportUnexpectedToken();
  }

  private Node parseFuncDecl()
  {
    var funcToken = currentToken();
    nextToken();
    if (isFatal()) return Node.Invalid;

    // TODO: Append return type to the FuncDecl.
    parseRetType();
    if (isFatal()) return Node.Invalid;

    if (!match(TokenKind.Ident))
    {
      reportUnexpectedToken();
      return Node.Invalid;
    }
    var identToken = currentToken();
    nextToken();
    if (isFatal()) return Node.Invalid;

    var paramList = parseParamList();
    if (isFatal()) return Node.Invalid;

    if (!match(TokenKind.LBrace))
    {
      reportUnexpectedToken();
      return Node.Invalid;
    }
    var block = parseBlock();
    if (isFatal()) return Node.Invalid;

    var ident = new IdentNode(identToken);
    var funcDecl = new FuncDeclNode(funcToken);
    funcDecl.Children.Add(ident);
    funcDecl.Children.Add(paramList);
    funcDecl.Children.Add(block);

    return funcDecl;
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

  private Node parseParamList()
  {
    if (!match(TokenKind.LParen))
    {
      reportUnexpectedToken();
      return Node.Invalid;
    }
    var token = currentToken();
    nextToken();
    if (isFatal()) return Node.Invalid;

    var paramList = new ParamListNode(token);

    if (match(TokenKind.RParen))
    {
      nextToken();
      if (isFatal()) return Node.Invalid;
      return paramList;
    }

    var param = parseParam();
    if (isFatal()) return Node.Invalid;
    paramList.Children.Add(param);

    while (match(TokenKind.Comma))
    {
      nextToken();
      if (isFatal()) return Node.Invalid;

      param = parseParam();
      if (isFatal()) return Node.Invalid;
      paramList.Children.Add(param);
    }

    consume(TokenKind.RParen);
    if (isFatal()) return Node.Invalid;

    return paramList;
  }

  private Node parseParam()
  {
    // TODO: Append type to the Param.
    parseType();
    if (isFatal()) return Node.Invalid;

    if (!match(TokenKind.Ident))
    {
      reportUnexpectedToken();
      return Node.Invalid;
    }
    var identToken = currentToken();
    nextToken();
    if (isFatal()) return Node.Invalid;

    var ident = new IdentNode(identToken);
    var param = new ParamNode(identToken);
    param.Children.Add(ident);

    return param;
  }

  private Node parseBlock()
  {
    var token = currentToken();
    nextToken();
    if (isFatal()) return Node.Invalid;

    var block = new BlockNode(token);

    while (!match(TokenKind.RBrace))
    {
      var stmt = parseStmt();
      if (isFatal()) return Node.Invalid;
      block.Children.Add(stmt);
    }

    consume(TokenKind.RBrace);
    if (isFatal()) return Node.Invalid;

    return block;
  }

  private Node parseStmt()
  {
    if (match(TokenKind.VarKW))
      return parseVarDecl();

    if (match(TokenKind.Ident))
    {
      var assign = parseAssignStmt();
      if (isFatal()) return Node.Invalid;
      if (assign != null) return assign;
    }

    if (match(TokenKind.IfKW))
      return parseIfStmt();

    if (match(TokenKind.WhileKW))
      return parseWhileStmt();

    if (match(TokenKind.DoKW))
      return parseDoWhileStmt();

    if (match(TokenKind.BreakKW))
      return parseBreakStmt();

    if (match(TokenKind.ContinueKW))
      return parseContinueStmt();

    if (match(TokenKind.ReturnKW))
      return parseReturnStmt();

    if (match(TokenKind.LBrace))
      return parseBlock();

    return parseExprStmt();
  }

  private Node? parseAssignStmt()
  {
    LexerState state = Lexer.Save();

    var identToken = currentToken();
    nextToken();
    if (isFatal()) return null;

    Node lhs = new IdentNode(identToken);

    while (match(TokenKind.LBracket))
    {
      var subscr = parseSubscr(lhs);
      if (isFatal()) return null;
      lhs = subscr;
    }

    if (!match(TokenKind.Eq))
    {
      Lexer.Restore(state);
      return null;
    }
    var eqToken = currentToken();
    nextToken();
    if (isFatal()) return null;

    var rhs = parseExpr();
    if (isFatal()) return null;

    consume(TokenKind.Semicolon);
    if (isFatal()) return null;

    var assign = new AssignNode(eqToken);
    assign.Children.Add(lhs);
    assign.Children.Add(rhs);
    return assign;
  }

  private Node parseSubscr(Node lhs)
  {
    var token = currentToken();
    nextToken();
    if (isFatal()) return Node.Invalid;

    var rhs = parseExpr();
    if (isFatal()) return Node.Invalid;

    consume(TokenKind.RBracket);
    if (isFatal()) return Node.Invalid;

    var subscr = new ElementNode(token);
    subscr.Children.Add(lhs);
    subscr.Children.Add(rhs);
    return subscr;
  }

  private Node parseIfStmt()
  {
    var token = currentToken();
    nextToken();
    if (isFatal()) return Node.Invalid;

    consume(TokenKind.LParen);
    if (isFatal()) return Node.Invalid;

    var expr = parseExpr();
    if (isFatal()) return Node.Invalid;

    consume(TokenKind.RParen);
    if (isFatal()) return Node.Invalid;

    var thenStmt = parseStmt();
    if (isFatal()) return Node.Invalid;

    var ifStmt = new IfNode(token);
    ifStmt.Children.Add(expr);
    ifStmt.Children.Add(thenStmt);

    if (!match(TokenKind.ElseKW))
      return ifStmt;

    nextToken();
    if (isFatal()) return Node.Invalid;

    var elseStmt = parseStmt();
    if (isFatal()) return Node.Invalid;

    var ifElseStmt = new IfElseNode(token);
    ifElseStmt.Children.Add(ifStmt);
    ifElseStmt.Children.Add(elseStmt);

    return ifElseStmt;
  }

  private Node parseWhileStmt()
  {
    var token = currentToken();
    nextToken();
    if (isFatal()) return Node.Invalid;

    consume(TokenKind.LParen);
    if (isFatal()) return Node.Invalid;

    var expr = parseExpr();
    if (isFatal()) return Node.Invalid;

    consume(TokenKind.RParen);
    if (isFatal()) return Node.Invalid;

    var stmt = parseStmt();
    if (isFatal()) return Node.Invalid;

    var whileStmt = new WhileNode(token);
    whileStmt.Children.Add(expr);
    whileStmt.Children.Add(stmt);

    return whileStmt;
  }

  private Node parseDoWhileStmt()
  {
    var token = currentToken();
    nextToken();
    if (isFatal()) return Node.Invalid;

    var stmt = parseStmt();
    if (isFatal()) return Node.Invalid;

    consume(TokenKind.WhileKW);
    if (isFatal()) return Node.Invalid;

    consume(TokenKind.LParen);
    if (isFatal()) return Node.Invalid;

    var expr = parseExpr();
    if (isFatal()) return Node.Invalid;

    consume(TokenKind.RParen);
    if (isFatal()) return Node.Invalid;

    consume(TokenKind.Semicolon);
    if (isFatal()) return Node.Invalid;

    var doWhileStmt = new DoWhileNode(token);
    doWhileStmt.Children.Add(stmt);
    doWhileStmt.Children.Add(expr);

    return doWhileStmt;
  }

  private Node parseBreakStmt()
  {
    var token = currentToken();
    nextToken();
    if (isFatal()) return Node.Invalid;

    consume(TokenKind.Semicolon);
    if (isFatal()) return Node.Invalid;

    return new BreakNode(token);
  }

  private Node parseContinueStmt()
  {
    var token = currentToken();
    nextToken();
    if (isFatal()) return Node.Invalid;

    consume(TokenKind.Semicolon);
    if (isFatal()) return Node.Invalid;

    return new ContinueNode(token);
  }

  private Node parseReturnStmt()
  {
    var token = currentToken();
    nextToken();
    if (isFatal()) return Node.Invalid;

    var ret = new ReturnNode(token);
    if (match(TokenKind.Semicolon))
    {
      nextToken();
      if (isFatal()) return Node.Invalid;
      return ret; 
    }

    var expr = parseExpr();
    if (isFatal()) return Node.Invalid;

    consume(TokenKind.Semicolon);
    if (isFatal()) return Node.Invalid;

    ret.Children.Add(expr);
    return ret;
  }

  private Node parseExprStmt()
  {
    var expr = parseExpr();
    if (isFatal()) return Node.Invalid;

    consume(TokenKind.Semicolon);
    if (isFatal()) return Node.Invalid;

    return expr;
  }

  private Node parseExpr()
  {
    var lhs = parseAndExpr();
    if (isFatal()) return Node.Invalid;

    while (match(TokenKind.PipePipe))
    {
      var token = currentToken();
      nextToken();
      if (isFatal()) return Node.Invalid;

      var rhs = parseAndExpr();
      if (isFatal()) return Node.Invalid;

      var binOp = new AndNode(token);
      binOp.Children.Add(lhs);
      binOp.Children.Add(rhs);
      lhs = binOp;
    }

    return lhs;
  }

  private Node parseAndExpr()
  {
    var lhs = parseEqExpr();
    if (isFatal()) return Node.Invalid;

    while (match(TokenKind.AmpAmp))
    {
      var token = currentToken();
      nextToken();
      if (isFatal()) return Node.Invalid;

      var rhs = parseEqExpr();
      if (isFatal()) return Node.Invalid;

      var binOp = new AndNode(token);
      binOp.Children.Add(lhs);
      binOp.Children.Add(rhs);
      lhs = binOp;
    }

    return lhs;
  }

  private Node parseEqExpr()
  {
    var lhs = parseRelExpr();
    if (isFatal()) return Node.Invalid;

    for (;;)
    {
      if (match(TokenKind.EqEq))
      {
        var token = currentToken();
        nextToken();
        if (isFatal()) return Node.Invalid;

        var rhs = parseRelExpr();
        if (isFatal()) return Node.Invalid;

        var binOp = new EqNode(token);
        binOp.Children.Add(lhs);
        binOp.Children.Add(rhs);
        lhs = binOp;
        continue;
      }

      if (match(TokenKind.NotEq))
      {
        var token = currentToken();
        nextToken();
        if (isFatal()) return Node.Invalid;

        var rhs = parseRelExpr();
        if (isFatal()) return Node.Invalid;

        var binOp = new NotEqNode(token);
        binOp.Children.Add(lhs);
        binOp.Children.Add(rhs);
        lhs = binOp;
        continue;
      }

      break;
    }

    return lhs;
  }

  private Node parseRelExpr()
  {
    var lhs = parseAddExpr();
    if (isFatal()) return Node.Invalid;

    for (;;)
    {
      if (match(TokenKind.Lt))
      {
        var token = currentToken();
        nextToken();
        if (isFatal()) return Node.Invalid;

        var rhs = parseAddExpr();
        if (isFatal()) return Node.Invalid;

        var binOp = new LtNode(token);
        binOp.Children.Add(lhs);
        binOp.Children.Add(rhs);
        lhs = binOp;
        continue;
      }

      if (match(TokenKind.LtEq))
      {
        var token = currentToken();
        nextToken();
        if (isFatal()) return Node.Invalid;

        var rhs = parseAddExpr();
        if (isFatal()) return Node.Invalid;

        var binOp = new LeNode(token);
        binOp.Children.Add(lhs);
        binOp.Children.Add(rhs);
        lhs = binOp;
        continue;
      }

      if (match(TokenKind.Gt))
      {
        var token = currentToken();
        nextToken();
        if (isFatal()) return Node.Invalid;

        var rhs = parseAddExpr();
        if (isFatal()) return Node.Invalid;

        var binOp = new GtNode(token);
        binOp.Children.Add(lhs);
        binOp.Children.Add(rhs);
        lhs = binOp;
        continue;
      }

      if (match(TokenKind.GtEq))
      {
        var token = currentToken();
        nextToken();
        if (isFatal()) return Node.Invalid;

        var rhs = parseAddExpr();
        if (isFatal()) return Node.Invalid;

        var binOp = new GeNode(token);
        binOp.Children.Add(lhs);
        binOp.Children.Add(rhs);
        lhs = binOp;
        continue;
      }

      break;
    }

    return lhs;
  }

  private Node parseAddExpr()
  {
    var lhs = parseMulExpr();
    if (isFatal()) return Node.Invalid;

    for (;;)
    {
      if (match(TokenKind.Plus))
      {
        var token = currentToken();
        nextToken();
        if (isFatal()) return Node.Invalid;

        var rhs = parseMulExpr();
        if (isFatal()) return Node.Invalid;

        var binOp = new AddNode(token);
        binOp.Children.Add(lhs);
        binOp.Children.Add(rhs);
        lhs = binOp;
        continue;
      }

      if (match(TokenKind.Minus))
      {
        var token = currentToken();
        nextToken();
        if (isFatal()) return Node.Invalid;

        var rhs = parseMulExpr();
        if (isFatal()) return Node.Invalid;

        var binOp = new SubNode(token);
        binOp.Children.Add(lhs);
        binOp.Children.Add(rhs);
        lhs = binOp;
        continue;
      }

      break;
    }

    return lhs;
  }

  private Node parseMulExpr()
  {
    var lhs = parseUnaryExpr();
    if (isFatal()) return Node.Invalid;

    for (;;)
    {
      if (match(TokenKind.Star))
      {
        var token = currentToken();
        nextToken();
        if (isFatal()) return Node.Invalid;

        var rhs = parseUnaryExpr();
        if (isFatal()) return Node.Invalid;

        var binOp = new MulNode(token);
        binOp.Children.Add(lhs);
        binOp.Children.Add(rhs);
        lhs = binOp;
        continue;
      }
    
      if (match(TokenKind.Slash))
      {
        var token = currentToken();
        nextToken();
        if (isFatal()) return Node.Invalid;

        var rhs = parseUnaryExpr();
        if (isFatal()) return Node.Invalid;

        var binOp = new DivNode(token);
        binOp.Children.Add(lhs);
        binOp.Children.Add(rhs);
        lhs = binOp;
        continue;
      }
    
      if (match(TokenKind.Percent))
      {
        var token = currentToken();
        nextToken();
        if (isFatal()) return Node.Invalid;

        var rhs = parseUnaryExpr();
        if (isFatal()) return Node.Invalid;

        var binOp = new ModNode(token);
        binOp.Children.Add(lhs);
        binOp.Children.Add(rhs);
        lhs = binOp;
        continue;
      }
    
      break;
    }

    return lhs;
  }

  private Node parseUnaryExpr()
  {
    if (match(TokenKind.Not))
    {
      var token = currentToken();
      nextToken();
      if (isFatal()) return Node.Invalid;

      var expr = parseUnaryExpr();
      if (isFatal()) return Node.Invalid;

      var unaryOp = new NotNode(token);
      unaryOp.Children.Add(expr);
      return unaryOp;
    }

    if (match(TokenKind.Plus))
    {
      var token = currentToken();
      nextToken();
      if (isFatal()) return Node.Invalid;

      var expr = parseUnaryExpr();
      if (isFatal()) return Node.Invalid;

      var unaryOp = new UnaryPlusNode(token);
      unaryOp.Children.Add(expr);
      return unaryOp;
    }

    if (match(TokenKind.Minus))
    {
      var token = currentToken();
      nextToken();
      if (isFatal()) return Node.Invalid;

      var expr = parseUnaryExpr();
      if (isFatal()) return Node.Invalid;

      var unaryOp = new UnaryMinusNode(token);
      unaryOp.Children.Add(expr);
      return unaryOp;
    }

    return parseCallExpr();
  }

  private Node parseCallExpr()
  {
    var lhs = parsePrimaryExpr();
    if (isFatal()) return Node.Invalid;

    for (;;)
    {
      if (match(TokenKind.LBracket))
      {
        var rhs = parseSubscr(lhs);
        if (isFatal()) return Node.Invalid;
        lhs = rhs;
        continue;
      }

      if (match(TokenKind.LParen))
      {
        var rhs = parseCall(lhs);
        if (isFatal()) return Node.Invalid;
        lhs = rhs;
        continue;
      }

      break;
    }

    return lhs;
  }

  private Node parseCall(Node lhs)
  {
    var token = currentToken();
    nextToken();
    if (isFatal()) return Node.Invalid;

    var call = new CallNode(token);
    call.Children.Add(lhs);

    if (match(TokenKind.RParen))
    {
      nextToken();
      if (isFatal()) return Node.Invalid;
      return call;
    }

    var expr = parseExpr();
    if (isFatal()) return Node.Invalid;
    call.Children.Add(expr);

    while (match(TokenKind.Comma))
    {
      nextToken();
      if (isFatal()) return Node.Invalid;

      expr = parseExpr();
      if (isFatal()) return Node.Invalid;
      call.Children.Add(expr);
    }

    consume(TokenKind.RParen);
    if (isFatal()) return Node.Invalid;

    return call;
  }

  private Node parsePrimaryExpr()
  {
    if (match(TokenKind.IntLiteral))
    {
      var token = currentToken();
      nextToken();
      if (isFatal()) return Node.Invalid;
      return new IntLiteralNode(token);
    }

    if (match(TokenKind.FloatLiteral))
    {
      var token = currentToken();
      nextToken();
      if (isFatal()) return Node.Invalid;
      return new FloatLiteralNode(token);
    }

    if (match(TokenKind.CharLiteral))
    {
      var token = currentToken();
      nextToken();
      if (isFatal()) return Node.Invalid;
      return new CharLiteralNode(token);
    }

    if (match(TokenKind.StringLiteral))
    {
      var token = currentToken();
      nextToken();
      if (isFatal()) return Node.Invalid;
      return new StringLiteralNode(token);
    }

    if (match(TokenKind.LBracket))
      return parseArray();

    if (match(TokenKind.Ident))
    {
      var token = currentToken();
      nextToken();
      if (isFatal()) return Node.Invalid;
      return new IdentNode(token);
    }

    if (match(TokenKind.LParen))
    {
      nextToken();
      if (isFatal()) return Node.Invalid;

      var expr = parseExpr();
      if (isFatal()) return Node.Invalid;

      consume(TokenKind.RParen);
      if (isFatal()) return Node.Invalid;

      return expr;
    }

    reportUnexpectedToken();
    return Node.Invalid;
  }

  private Node parseArray()
  {
    var token = currentToken();
    nextToken();
    if (isFatal()) return Node.Invalid;

    var array = new ArrayNode(token);

    if (match(TokenKind.RBracket))
    {
      nextToken();
      if (isFatal()) return Node.Invalid;
      return array;
    }

    var expr = parseExpr();
    if (isFatal()) return Node.Invalid;
    array.Children.Add(expr);

    while (match(TokenKind.Comma))
    {
      nextToken();
      if (isFatal()) return Node.Invalid;

      expr = parseExpr();
      if (isFatal()) return Node.Invalid;
      array.Children.Add(expr);
    }

    consume(TokenKind.RBracket);
    if (isFatal()) return Node.Invalid;

    return array;
  }
}

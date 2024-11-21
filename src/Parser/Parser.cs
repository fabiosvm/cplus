
public class Parser
{
  public Lexer Lexer { get; }
  public Diagnostics Diagnostics { get; }
  public Node Ast { get; private set; } = Node.None;

  public Parser(string source)
  {
    Lexer = new Lexer(source);
    Diagnostics = Lexer.Diagnostics;
  }

  public void Parse()
  {
    var ast = parseModule();
    if (isFatal()) return;

    Diagnostics.Note("Parsing completed successfully");
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
    Diagnostics.Fatal($"Unexpected {text} [{line}:{column}]");
  }

  private Node parseModule()
  {
    var ast = new ModuleNode();
    while (!match(TokenKind.Eof))
    {
      var decl = parseDecl();
      if (isFatal()) return Node.None;
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
    return Node.None;
  }

  private Node parseVarDecl()
  {
    var varToken = currentToken();
    nextToken();
    if (isFatal()) return Node.None;

    var type = parseType();
    if (isFatal()) return Node.None;

    if (!match(TokenKind.Ident))
    {
      reportUnexpectedToken();
      return Node.None;
    }
    var identToken = currentToken();
    nextToken();
    if (isFatal()) return Node.None;

    var ident = new IdentNode(identToken);
    var varDecl = new VarDeclNode(varToken);
    varDecl.Children.Add(type);
    varDecl.Children.Add(ident);

    if (match(TokenKind.Eq))
    {
      var eqToken = currentToken();
      nextToken();
      if (isFatal()) return Node.None;

      var expr = parseExpr();
      if (isFatal()) return Node.None;

      consume(TokenKind.Semicolon);
      if (isFatal()) return Node.None;

      var assign = new AssignNode(eqToken);
      assign.Children.Add(varDecl);
      assign.Children.Add(expr);
      return assign;
    }

    consume(TokenKind.Semicolon);
    if (isFatal()) return Node.None;

    return varDecl;
  }

  private Node parseType()
  {
    var type = parsePrimitiveType();
    if (isFatal()) return Node.None;

    while (match(TokenKind.LBracket))
    {
      var token = currentToken();
      nextToken();
      if (isFatal()) return Node.None;

      consume(TokenKind.RBracket);
      if (isFatal()) return Node.None;

      var arrayType = new ArrayTypeNode(token);
      arrayType.Children.Add(type);
      type = arrayType;
    }

    return type;
  }

  private Node parsePrimitiveType()
  {
    if (match(TokenKind.BoolKW))
    {
      var token = currentToken();
      nextToken();
      if (isFatal()) return Node.None;
      return new BoolTypeNode(token);
    }

    if (match(TokenKind.FloatKW))
    {
      var token = currentToken();
      nextToken();
      if (isFatal()) return Node.None;
      return new FloatTypeNode(token);
    }

    if (match(TokenKind.DoubleKW))
    {
      var token = currentToken();
      nextToken();
      if (isFatal()) return Node.None;
      return new DoubleTypeNode(token);
    }

    if (match(TokenKind.UnsignedKW))
    {
      var token = currentToken();
      nextToken();
      if (isFatal()) return Node.None;

      var intType = parseIntType();
      if (isFatal()) return Node.None;

      var unsignedType = new UnsignedTypeNode(token);
      unsignedType.Children.Add(intType);
      return unsignedType;
    }

    return parseIntType();
  }

  private Node parseIntType()
  {
    if (match(TokenKind.CharKW))
    {
      var token = currentToken();
      nextToken();
      if (isFatal()) return Node.None;
      return new CharTypeNode(token);
    }

    if (match(TokenKind.ShortKW))
    {
      var token = currentToken();
      nextToken();
      if (isFatal()) return Node.None;
      return new ShortTypeNode(token);
    }

    if (match(TokenKind.IntKW))
    {
      var token = currentToken();
      nextToken();
      if (isFatal()) return Node.None;
      return new IntTypeNode(token);
    }

    if (match(TokenKind.LongKW))
    {
      var token = currentToken();
      nextToken();
      if (isFatal()) return Node.None;
      return new LongTypeNode(token);
    }

    reportUnexpectedToken();
    return Node.None;
  }

  private Node parseFuncDecl()
  {
    var funcToken = currentToken();
    nextToken();
    if (isFatal()) return Node.None;

    var retType = parseRetType();
    if (isFatal()) return Node.None;

    if (!match(TokenKind.Ident))
    {
      reportUnexpectedToken();
      return Node.None;
    }
    var identToken = currentToken();
    nextToken();
    if (isFatal()) return Node.None;

    var paramList = parseParamList();
    if (isFatal()) return Node.None;

    if (!match(TokenKind.LBrace))
    {
      reportUnexpectedToken();
      return Node.None;
    }
    var block = parseBlock(false);
    if (isFatal()) return Node.None;

    var ident = new IdentNode(identToken);
    var funcDecl = new FuncDeclNode(funcToken);
    funcDecl.Children.Add(retType);
    funcDecl.Children.Add(ident);
    funcDecl.Children.Add(paramList);
    funcDecl.Children.Add(block);

    return funcDecl;
  }

  private Node parseRetType()
  {
    if (match(TokenKind.VoidKW))
    {
      var token = currentToken();
      nextToken();
      if (isFatal()) return Node.None;

      return new VoidTypeNode(token);
    }

    return parseType();
  }

  private Node parseParamList()
  {
    if (!match(TokenKind.LParen))
    {
      reportUnexpectedToken();
      return Node.None;
    }
    var token = currentToken();
    nextToken();
    if (isFatal()) return Node.None;

    var paramList = new ParamListNode(token);

    if (match(TokenKind.RParen))
    {
      nextToken();
      if (isFatal()) return Node.None;
      return paramList;
    }

    var param = parseParam();
    if (isFatal()) return Node.None;
    paramList.Children.Add(param);

    while (match(TokenKind.Comma))
    {
      nextToken();
      if (isFatal()) return Node.None;

      param = parseParam();
      if (isFatal()) return Node.None;
      paramList.Children.Add(param);
    }

    consume(TokenKind.RParen);
    if (isFatal()) return Node.None;

    return paramList;
  }

  private Node parseParam()
  {
    Node inout = Node.None;
    if (match(TokenKind.InoutKW))
    {
      var token = currentToken();
      nextToken();
      if (isFatal()) return Node.None;
      inout = new InoutNode(token);
    }

    var type = parseType();
    if (isFatal()) return Node.None;

    if (!match(TokenKind.Ident))
    {
      reportUnexpectedToken();
      return Node.None;
    }
    var identToken = currentToken();
    nextToken();
    if (isFatal()) return Node.None;

    var ident = new IdentNode(identToken);
    var param = new ParamNode(identToken);
    param.Children.Add(inout);
    param.Children.Add(type);
    param.Children.Add(ident);

    return param;
  }

  private Node parseBlock(bool scoped)
  {
    var token = currentToken();
    nextToken();
    if (isFatal()) return Node.None;

    NonLeafNode block = scoped ? new ScopeBlockNode(token) : new BlockNode(token);

    while (!match(TokenKind.RBrace))
    {
      var stmt = parseStmt();
      if (isFatal()) return Node.None;
      block.Children.Add(stmt);
    }

    consume(TokenKind.RBrace);
    if (isFatal()) return Node.None;

    return block;
  }

  private Node parseStmt()
  {
    if (match(TokenKind.VarKW))
      return parseVarDecl();

    if (match(TokenKind.Ident))
    {
      var assign = parseAssignStmt();
      if (isFatal()) return Node.None;
      if (assign != null) return assign;
    }

    if (match(TokenKind.IfKW))
      return parseIfStmt();

    if (match(TokenKind.WhileKW))
      return parseWhileStmt();

    if (match(TokenKind.DoKW))
      return parseDoWhileStmt();

    if (match(TokenKind.ForKW))
      return parseForStmt();

    if (match(TokenKind.BreakKW))
      return parseBreakStmt();

    if (match(TokenKind.ContinueKW))
      return parseContinueStmt();

    if (match(TokenKind.ReturnKW))
      return parseReturnStmt();

    if (match(TokenKind.LBrace))
      return parseBlock(true);

    return parseExprStmt();
  }

  private Node? parseAssignStmt()
  {
    LexerState state = Lexer.Save();

    var identToken = currentToken();
    nextToken();
    if (isFatal()) return null;

    Node lhs = new SymbolNode(identToken);

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
    if (isFatal()) return Node.None;

    var rhs = parseExpr();
    if (isFatal()) return Node.None;

    consume(TokenKind.RBracket);
    if (isFatal()) return Node.None;

    var subscr = new ElementNode(token);
    subscr.Children.Add(lhs);
    subscr.Children.Add(rhs);
    return subscr;
  }

  private Node parseIfStmt()
  {
    var token = currentToken();
    nextToken();
    if (isFatal()) return Node.None;

    consume(TokenKind.LParen);
    if (isFatal()) return Node.None;

    var expr = parseExpr();
    if (isFatal()) return Node.None;

    consume(TokenKind.RParen);
    if (isFatal()) return Node.None;

    var thenStmt = parseStmt();
    if (isFatal()) return Node.None;

    var ifStmt = new IfNode(token);
    ifStmt.Children.Add(expr);
    ifStmt.Children.Add(thenStmt);

    if (!match(TokenKind.ElseKW))
      return ifStmt;

    nextToken();
    if (isFatal()) return Node.None;

    var elseStmt = parseStmt();
    if (isFatal()) return Node.None;

    var ifElseStmt = new IfElseNode(token);
    ifElseStmt.Children.Add(ifStmt);
    ifElseStmt.Children.Add(elseStmt);

    return ifElseStmt;
  }

  private Node parseWhileStmt()
  {
    var token = currentToken();
    nextToken();
    if (isFatal()) return Node.None;

    consume(TokenKind.LParen);
    if (isFatal()) return Node.None;

    var expr = parseExpr();
    if (isFatal()) return Node.None;

    consume(TokenKind.RParen);
    if (isFatal()) return Node.None;

    var stmt = parseStmt();
    if (isFatal()) return Node.None;

    var whileStmt = new WhileNode(token);
    whileStmt.Children.Add(expr);
    whileStmt.Children.Add(stmt);

    return whileStmt;
  }

  private Node parseDoWhileStmt()
  {
    var token = currentToken();
    nextToken();
    if (isFatal()) return Node.None;

    var stmt = parseStmt();
    if (isFatal()) return Node.None;

    consume(TokenKind.WhileKW);
    if (isFatal()) return Node.None;

    consume(TokenKind.LParen);
    if (isFatal()) return Node.None;

    var expr = parseExpr();
    if (isFatal()) return Node.None;

    consume(TokenKind.RParen);
    if (isFatal()) return Node.None;

    consume(TokenKind.Semicolon);
    if (isFatal()) return Node.None;

    var doWhileStmt = new DoWhileNode(token);
    doWhileStmt.Children.Add(stmt);
    doWhileStmt.Children.Add(expr);

    return doWhileStmt;
  }

  private Node parseForStmt()
  {
    var token = currentToken();
    nextToken();
    if (isFatal()) return Node.None;

    consume(TokenKind.LParen);
    if (isFatal()) return Node.None;

    var type = parseType();
    if (isFatal()) return Node.None;

    if (!match(TokenKind.Ident))
    {
      reportUnexpectedToken();
      return Node.None;
    }
    var identToken = currentToken();
    nextToken();
    if (isFatal()) return Node.None;

    var ident = new IdentNode(identToken);
    var varDecl = new VarDeclNode(token);
    varDecl.Children.Add(type);
    varDecl.Children.Add(ident);

    consume(TokenKind.InKW);
    if (isFatal()) return Node.None;

    var expr = parseExpr();
    if (isFatal()) return Node.None;

    consume(TokenKind.RParen);
    if (isFatal()) return Node.None;

    var stmt = parseStmt();
    if (isFatal()) return Node.None;

    var forStmt = new ForNode(token);
    forStmt.Children.Add(varDecl);
    forStmt.Children.Add(expr);
    forStmt.Children.Add(stmt);

    return forStmt;
  }

  private Node parseBreakStmt()
  {
    var token = currentToken();
    nextToken();
    if (isFatal()) return Node.None;

    consume(TokenKind.Semicolon);
    if (isFatal()) return Node.None;

    return new BreakNode(token);
  }

  private Node parseContinueStmt()
  {
    var token = currentToken();
    nextToken();
    if (isFatal()) return Node.None;

    consume(TokenKind.Semicolon);
    if (isFatal()) return Node.None;

    return new ContinueNode(token);
  }

  private Node parseReturnStmt()
  {
    var token = currentToken();
    nextToken();
    if (isFatal()) return Node.None;

    var ret = new ReturnNode(token);
    if (match(TokenKind.Semicolon))
    {
      nextToken();
      if (isFatal()) return Node.None;
      return ret; 
    }

    var expr = parseExpr();
    if (isFatal()) return Node.None;

    consume(TokenKind.Semicolon);
    if (isFatal()) return Node.None;

    ret.Children.Add(expr);
    return ret;
  }

  private Node parseExprStmt()
  {
    var expr = parseExpr();
    if (isFatal()) return Node.None;

    consume(TokenKind.Semicolon);
    if (isFatal()) return Node.None;

    return expr;
  }

  private Node parseExpr()
  {
    var lhs = parseAndExpr();
    if (isFatal()) return Node.None;

    while (match(TokenKind.PipePipe))
    {
      var token = currentToken();
      nextToken();
      if (isFatal()) return Node.None;

      var rhs = parseAndExpr();
      if (isFatal()) return Node.None;

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
    if (isFatal()) return Node.None;

    while (match(TokenKind.AmpAmp))
    {
      var token = currentToken();
      nextToken();
      if (isFatal()) return Node.None;

      var rhs = parseEqExpr();
      if (isFatal()) return Node.None;

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
    if (isFatal()) return Node.None;

    for (;;)
    {
      if (match(TokenKind.EqEq))
      {
        var token = currentToken();
        nextToken();
        if (isFatal()) return Node.None;

        var rhs = parseRelExpr();
        if (isFatal()) return Node.None;

        var binOp = new EqNode(token);
        binOp.Children.Add(lhs);
        binOp.Children.Add(rhs);
        lhs = binOp;
        continue;
      }

      if (match(TokenKind.BangEq))
      {
        var token = currentToken();
        nextToken();
        if (isFatal()) return Node.None;

        var rhs = parseRelExpr();
        if (isFatal()) return Node.None;

        var binOp = new NeNode(token);
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
    if (isFatal()) return Node.None;

    for (;;)
    {
      if (match(TokenKind.Lt))
      {
        var token = currentToken();
        nextToken();
        if (isFatal()) return Node.None;

        var rhs = parseAddExpr();
        if (isFatal()) return Node.None;

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
        if (isFatal()) return Node.None;

        var rhs = parseAddExpr();
        if (isFatal()) return Node.None;

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
        if (isFatal()) return Node.None;

        var rhs = parseAddExpr();
        if (isFatal()) return Node.None;

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
        if (isFatal()) return Node.None;

        var rhs = parseAddExpr();
        if (isFatal()) return Node.None;

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
    if (isFatal()) return Node.None;

    for (;;)
    {
      if (match(TokenKind.Plus))
      {
        var token = currentToken();
        nextToken();
        if (isFatal()) return Node.None;

        var rhs = parseMulExpr();
        if (isFatal()) return Node.None;

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
        if (isFatal()) return Node.None;

        var rhs = parseMulExpr();
        if (isFatal()) return Node.None;

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
    if (isFatal()) return Node.None;

    for (;;)
    {
      if (match(TokenKind.Star))
      {
        var token = currentToken();
        nextToken();
        if (isFatal()) return Node.None;

        var rhs = parseUnaryExpr();
        if (isFatal()) return Node.None;

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
        if (isFatal()) return Node.None;

        var rhs = parseUnaryExpr();
        if (isFatal()) return Node.None;

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
        if (isFatal()) return Node.None;

        var rhs = parseUnaryExpr();
        if (isFatal()) return Node.None;

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
    if (match(TokenKind.Bang))
    {
      var token = currentToken();
      nextToken();
      if (isFatal()) return Node.None;

      var expr = parseUnaryExpr();
      if (isFatal()) return Node.None;

      var unaryOp = new NotNode(token);
      unaryOp.Children.Add(expr);
      return unaryOp;
    }

    if (match(TokenKind.Plus))
    {
      var token = currentToken();
      nextToken();
      if (isFatal()) return Node.None;

      var expr = parseUnaryExpr();
      if (isFatal()) return Node.None;

      var unaryOp = new UnaryPlusNode(token);
      unaryOp.Children.Add(expr);
      return unaryOp;
    }

    if (match(TokenKind.Minus))
    {
      var token = currentToken();
      nextToken();
      if (isFatal()) return Node.None;

      var expr = parseUnaryExpr();
      if (isFatal()) return Node.None;

      var unaryOp = new UnaryMinusNode(token);
      unaryOp.Children.Add(expr);
      return unaryOp;
    }

    return parseCallExpr();
  }

  private Node parseCallExpr()
  {
    var lhs = parsePrimaryExpr();
    if (isFatal()) return Node.None;

    for (;;)
    {
      if (match(TokenKind.LBracket))
      {
        var rhs = parseSubscr(lhs);
        if (isFatal()) return Node.None;
        lhs = rhs;
        continue;
      }

      if (match(TokenKind.LParen))
      {
        var rhs = parseCall(lhs);
        if (isFatal()) return Node.None;
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
    if (isFatal()) return Node.None;

    var call = new CallNode(token);
    call.Children.Add(lhs);

    if (match(TokenKind.RParen))
    {
      nextToken();
      if (isFatal()) return Node.None;
      return call;
    }

    var expr = parseExpr();
    if (isFatal()) return Node.None;
    call.Children.Add(expr);

    while (match(TokenKind.Comma))
    {
      nextToken();
      if (isFatal()) return Node.None;

      expr = parseExpr();
      if (isFatal()) return Node.None;
      call.Children.Add(expr);
    }

    consume(TokenKind.RParen);
    if (isFatal()) return Node.None;

    return call;
  }

  private Node parsePrimaryExpr()
  {
    if (match(TokenKind.FalseKW))
    {
      var token = currentToken();
      nextToken();
      if (isFatal()) return Node.None;
      return new FalseNode(token);
    }

    if (match(TokenKind.TrueKW))
    {
      var token = currentToken();
      nextToken();
      if (isFatal()) return Node.None;
      return new TrueNode(token);
    }

    if (match(TokenKind.IntLiteral))
    {
      var token = currentToken();
      nextToken();
      if (isFatal()) return Node.None;
      return new IntLiteralNode(token);
    }

    if (match(TokenKind.FloatLiteral))
    {
      var token = currentToken();
      nextToken();
      if (isFatal()) return Node.None;
      return new FloatLiteralNode(token);
    }

    if (match(TokenKind.CharLiteral))
    {
      var token = currentToken();
      nextToken();
      if (isFatal()) return Node.None;
      return new CharLiteralNode(token);
    }

    if (match(TokenKind.StringLiteral))
    {
      var token = currentToken();
      nextToken();
      if (isFatal()) return Node.None;
      return new StringLiteralNode(token);
    }

    if (match(TokenKind.NewKW))
      return parseNewExpr();

    if (match(TokenKind.LBrace))
      return parseInitList();

    if (match(TokenKind.Amp))
      return parseRef();

    if (match(TokenKind.Ident))
    {
      var token = currentToken();
      nextToken();
      if (isFatal()) return Node.None;
      return new SymbolNode(token);
    }

    if (match(TokenKind.LParen))
    {
      nextToken();
      if (isFatal()) return Node.None;

      var expr = parseExpr();
      if (isFatal()) return Node.None;

      consume(TokenKind.RParen);
      if (isFatal()) return Node.None;

      return expr;
    }

    reportUnexpectedToken();
    return Node.None;
  }

  private Node parseNewExpr()
  {
    var newToken = currentToken();
    nextToken();
    if (isFatal()) return Node.None;

    var type = parseType();
    if (isFatal()) return Node.None;

    Node initList = Node.None;
    if (match(TokenKind.LBrace))
    {
      initList = parseInitList();
      if (isFatal()) return Node.None;
    }

    var newNode = new NewNode(newToken);
    newNode.Children.Add(type);
    newNode.Children.Add(initList);

    return newNode;
  }

  private Node parseInitList()
  {
    var token = currentToken();
    nextToken();
    if (isFatal()) return Node.None;

    var initList = new InitListNode(token);

    if (match(TokenKind.RBrace))
    {
      nextToken();
      if (isFatal()) return Node.None;
      return initList;
    }

    var expr = parseExpr();
    if (isFatal()) return Node.None;
    initList.Children.Add(expr);

    while (match(TokenKind.Comma))
    {
      nextToken();
      if (isFatal()) return Node.None;

      expr = parseExpr();
      if (isFatal()) return Node.None;
      initList.Children.Add(expr);
    }

    consume(TokenKind.RBrace);
    if (isFatal()) return Node.None;

    return initList;
  }

  private Node parseRef()
  {
    var refToken = currentToken();
    nextToken();
    if (isFatal()) return Node.None;

    if (!match(TokenKind.Ident))
    {
      reportUnexpectedToken();
      return Node.None;
    }
    var identToken = currentToken();
    nextToken();
    if (isFatal()) return Node.None;

    Node lhs = new SymbolNode(identToken);

    while (match(TokenKind.LBracket))
    {
      var subscr = parseSubscr(lhs);
      if (isFatal()) return Node.None;
      lhs = subscr;
    }

    var refNode = new RefNode(refToken);
    refNode.Children.Add(lhs);
    return refNode;
  }
}

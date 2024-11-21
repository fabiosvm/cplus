
# Lexer

## Tokens

| Token | Pattern |
|---|---|
| Eof | `\0` |
| Comma | `,` |
| Semicolon | `;` |
| LParen | `(` |
| RParen | `)` |
| LBracket | `[` |
| RBracket | `]` |
| LBrace | `{` |
| RBrace | `}` |
| PipePipe | `\|\|` |
| AmpAmp | `&&` |
| Amp | `&` |
| EqEq | `==` |
| Eq | `=` |
| LtEq | `<=` |
| Lt | `<` |
| GtEq | `>=` |
| Gt | `>` |
| BangEq | `!=` |
| Bang | `!` |
| Plus | `+` |
| Minus | `-` |
| Star | `*` |
| Slash | `/` |
| Percent | `%` |
| IntLiteral | `[0-9]+` |
| FloatLiteral | `[0-9]+\.[0-9]+` |
| CharLiteral | `'.'` |
| StringLiteral | `".*"` |
| BoolKW | `bool` |
| BreakKW | `break` |
| CharKW | `char` |
| ContinueKW | `continue` |
| DoKW | `do` |
| DoubleKW | `double` |
| ElseKW | `else` |
| FalseKW | `false` |
| FloatKW | `float` |
| ForKW | `for` |
| FuncKW | `func` |
| IfKW | `if` |
| InKW | `in` |
| InoutKW | `inout` |
| IntKW | `int` |
| LongKW | `long` |
| NewKW | `new` |
| ReturnKW | `return` |
| ShortKW | `short` |
| TrueKW | `true` |
| UnsignedKW | `unsigned` |
| VarKW | `var` |
| VoidKW | `void` |
| WhileKW | `while` |
| Ident | `[a-zA-Z_][a-zA-Z0-9_]*` |

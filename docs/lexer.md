
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
| EqEq | `==` |
| Eq | `=` |
| LtEq | `<=` |
| Lt | `<` |
| GtEq | `>=` |
| Gt | `>` |
| NotEq | `!=` |
| Not | `!` |
| Plus | `+` |
| Minus | `-` |
| Star | `*` |
| Slash | `/` |
| Percent | `%` |
| IntLiteral | `[0-9]+` |
| FloatLiteral | `[0-9]+\.[0-9]+` |
| CharLiteral | `'.'` |
| StringLiteral | `".*"` |
| BreakKW | `break` |
| CharKW | `char` |
| ContinueKW | `continue` |
| DoKW | `do` |
| DoubleKW | `double` |
| ElseKW | `else` |
| FloatKW | `float` |
| FuncKW | `func` |
| IfKW | `if` |
| IntKW | `int` |
| LongKW | `long` |
| ReturnKW | `return` |
| ShortKW | `short` |
| StringKW | `String` |
| VarKW | `var` |
| VoidKW | `void` |
| WhileKW | `while` |
| Ident | `[a-zA-Z_][a-zA-Z0-9_]*` |


# Lexer

## Tokens

| Token | Pattern |
|---|---|
| Eof | `\0` |
| Comma | `,` |
| Semicolon | `;` |
| LParen | `(` |
| RParen | `)` |
| LBrace | `{` |
| RBrace | `}` |
| Eq | `=` |
| Plus | `+` |
| Minus | `-` |
| Star | `*` |
| Slash | `/` |
| Percent | `%` |
| IntLiteral | `[0-9]+` |
| FloatLiteral | `[0-9]+\.[0-9]+` |
| FloatKW | `float` |
| FuncKW | `func` |
| IntKW | `int` |
| ReturnKW | `return` |
| VarKW | `var` |
| VoidKW | `void` |
| Ident | `[a-zA-Z_][a-zA-Z0-9_]*` |

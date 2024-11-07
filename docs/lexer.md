
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
| Plus | `+` |
| Minus | `-` |
| Star | `*` |
| Slash | `/` |
| Percent | `%` |
| IntLiteral | `[0-9]+` |
| FloatLiteral | `[0-9]+\.[0-9]+` |
| FloatKW | `float` |
| FunctionKW | `function` |
| IntKW | `int` |
| ReturnKW | `return` |
| VoidKW | `void` |
| Ident | `[a-zA-Z_][a-zA-Z0-9_]*` |

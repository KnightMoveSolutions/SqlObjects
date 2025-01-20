using System;

namespace KnightMoves.SqlObjects.SqlCode;

public interface IFluentSqlHaving
{
    SqlStatement HAVING();
    SqlStatement HAVING(ISqlLiteral leftLiteral);
    SqlStatement HAVING(string operand);
    SqlStatement HAVING(string multiPartIdentifier, string operand);
    SqlStatement HAVING(int value);
    SqlStatement HAVING(DateTime value);
    SqlStatement HAVING(bool value);
    SqlStatement HAVING(long value);
    SqlStatement HAVING(Guid value);
    SqlStatement HAVING(char value);
    SqlStatement HAVING(decimal value);
}

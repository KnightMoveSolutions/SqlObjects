using System;

namespace KnightMoves.SqlObjects.SqlCode;

public interface IFluentSqlBetween
{
    SqlStatement BETWEEN(string value);
    SqlStatement BETWEEN(string multipartIdentifier, string value);
    SqlStatement BETWEEN(int value);
    SqlStatement BETWEEN(DateTime value);
    SqlStatement BETWEEN(long value);
    SqlStatement BETWEEN(char value);
    SqlStatement BETWEEN(decimal value);
}

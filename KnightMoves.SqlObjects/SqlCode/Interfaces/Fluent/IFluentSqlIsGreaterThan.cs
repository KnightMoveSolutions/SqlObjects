using System;

namespace KnightMoves.SqlObjects.SqlCode;

public interface IFluentSqlIsGreaterThan
{
    SqlStatement IsGreaterThan();
    SqlStatement IsGreaterThan(string value);
    SqlStatement IsGreaterThan(string multiPartIdentifier, string value);
    SqlStatement IsGreaterThan(int value);
    SqlStatement IsGreaterThan(DateTime value);
    SqlStatement IsGreaterThan(bool value);
    SqlStatement IsGreaterThan(long value);
    SqlStatement IsGreaterThan(Guid value);
    SqlStatement IsGreaterThan(char value);
    SqlStatement IsGreaterThan(decimal value);
}

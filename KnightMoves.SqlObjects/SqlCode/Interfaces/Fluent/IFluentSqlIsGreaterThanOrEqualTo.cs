using System;

namespace KnightMoves.SqlObjects.SqlCode;

public interface IFluentSqlIsGreaterThanOrEqualTo
{
    SqlStatement IsGreaterThanOrEqualTo();
    SqlStatement IsGreaterThanOrEqualTo(string value);
    SqlStatement IsGreaterThanOrEqualTo(string multiPartIdentifier, string value);
    SqlStatement IsGreaterThanOrEqualTo(int value);
    SqlStatement IsGreaterThanOrEqualTo(DateTime value);
    SqlStatement IsGreaterThanOrEqualTo(bool value);
    SqlStatement IsGreaterThanOrEqualTo(long value);
    SqlStatement IsGreaterThanOrEqualTo(Guid value);
    SqlStatement IsGreaterThanOrEqualTo(char value);
    SqlStatement IsGreaterThanOrEqualTo(decimal value);
}

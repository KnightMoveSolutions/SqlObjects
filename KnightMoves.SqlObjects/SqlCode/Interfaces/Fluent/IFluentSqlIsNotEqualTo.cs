using System;

namespace KnightMoves.SqlObjects.SqlCode;

public interface IFluentSqlIsNotEqualTo
{
    SqlStatement IsNotEqualTo();
    SqlStatement IsNotEqualTo(string value);
    SqlStatement IsNotEqualTo(string multiPartIdentifier, string value);
    SqlStatement IsNotEqualTo(int value);
    SqlStatement IsNotEqualTo(DateTime value);
    SqlStatement IsNotEqualTo(bool value);
    SqlStatement IsNotEqualTo(long value);
    SqlStatement IsNotEqualTo(Guid value);
    SqlStatement IsNotEqualTo(char value);
    SqlStatement IsNotEqualTo(decimal value);
}

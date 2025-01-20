using System;

namespace KnightMoves.SqlObjects.SqlCode;

public interface IFluentSqlIsEqualTo
{
    SqlStatement IsEqualTo();
    SqlStatement IsEqualTo(string value);
    SqlStatement IsEqualTo(string multiPartIdentifier, string value);
    SqlStatement IsEqualTo(int value);
    SqlStatement IsEqualTo(DateTime value);
    SqlStatement IsEqualTo(bool value);
    SqlStatement IsEqualTo(long value);
    SqlStatement IsEqualTo(Guid value);
    SqlStatement IsEqualTo(char value);
    SqlStatement IsEqualTo(decimal value);
}

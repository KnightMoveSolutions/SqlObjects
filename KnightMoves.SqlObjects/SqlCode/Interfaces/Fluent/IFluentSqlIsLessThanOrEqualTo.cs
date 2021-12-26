using System;

namespace KnightMoves.SqlObjects.SqlCode
{
    public interface IFluentSqlIsLessThanOrEqualTo
    {
        SqlStatement IsLessThanOrEqualTo();
        SqlStatement IsLessThanOrEqualTo(string value);
        SqlStatement IsLessThanOrEqualTo(string multiPartIdentifier, string value);
        SqlStatement IsLessThanOrEqualTo(int value);
        SqlStatement IsLessThanOrEqualTo(DateTime value);
        SqlStatement IsLessThanOrEqualTo(bool value);
        SqlStatement IsLessThanOrEqualTo(long value);
        SqlStatement IsLessThanOrEqualTo(Guid value);
        SqlStatement IsLessThanOrEqualTo(char value);
        SqlStatement IsLessThanOrEqualTo(decimal value);
    }
}

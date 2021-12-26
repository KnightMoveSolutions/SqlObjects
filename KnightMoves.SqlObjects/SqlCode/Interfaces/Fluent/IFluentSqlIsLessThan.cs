using System;

namespace KnightMoves.SqlObjects.SqlCode
{
    public interface IFluentSqlIsLessThan
    {
        SqlStatement IsLessThan();
        SqlStatement IsLessThan(string value);
        SqlStatement IsLessThan(string multiPartIdentifier, string value);
        SqlStatement IsLessThan(int value);
        SqlStatement IsLessThan(DateTime value);
        SqlStatement IsLessThan(bool value);
        SqlStatement IsLessThan(long value);
        SqlStatement IsLessThan(Guid value);
        SqlStatement IsLessThan(char value);
        SqlStatement IsLessThan(decimal value);
    }
}

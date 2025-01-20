using System;

namespace KnightMoves.SqlObjects.SqlCode;

public abstract partial class SqlStatement : IFluentSqlIsLessThan
{
    public abstract SqlStatement IsLessThan();
    public SqlStatement IsLessThan(string value) => IsLessThan(string.Empty, value);
    public abstract SqlStatement IsLessThan(string multiPartIdentifier, string value);
    public abstract SqlStatement IsLessThan(int value);
    public abstract SqlStatement IsLessThan(DateTime value);
    public abstract SqlStatement IsLessThan(bool value);
    public abstract SqlStatement IsLessThan(long value);
    public abstract SqlStatement IsLessThan(Guid value);
    public abstract SqlStatement IsLessThan(char value);
    public abstract SqlStatement IsLessThan(decimal value);

}

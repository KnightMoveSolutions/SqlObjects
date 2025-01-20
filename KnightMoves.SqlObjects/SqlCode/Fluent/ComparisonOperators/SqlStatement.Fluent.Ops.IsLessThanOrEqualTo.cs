using System;

namespace KnightMoves.SqlObjects.SqlCode;

public abstract partial class SqlStatement : IFluentSqlIsLessThanOrEqualTo
{
    public abstract SqlStatement IsLessThanOrEqualTo();
    public SqlStatement IsLessThanOrEqualTo(string value) => IsLessThanOrEqualTo(string.Empty, value);
    public abstract SqlStatement IsLessThanOrEqualTo(string multiPartIdentifier, string value);
    public abstract SqlStatement IsLessThanOrEqualTo(int value);
    public abstract SqlStatement IsLessThanOrEqualTo(DateTime value);
    public abstract SqlStatement IsLessThanOrEqualTo(bool value);
    public abstract SqlStatement IsLessThanOrEqualTo(long value);
    public abstract SqlStatement IsLessThanOrEqualTo(Guid value);
    public abstract SqlStatement IsLessThanOrEqualTo(char value);
    public abstract SqlStatement IsLessThanOrEqualTo(decimal value);

}

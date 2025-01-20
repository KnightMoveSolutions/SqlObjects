using System;

namespace KnightMoves.SqlObjects.SqlCode;

public abstract partial class SqlStatement : IFluentSqlIsGreaterThanOrEqualTo
{
    public abstract SqlStatement IsGreaterThanOrEqualTo();
    public SqlStatement IsGreaterThanOrEqualTo(string value) => IsGreaterThanOrEqualTo(string.Empty, value);
    public abstract SqlStatement IsGreaterThanOrEqualTo(string multiPartIdentifier, string value);
    public abstract SqlStatement IsGreaterThanOrEqualTo(int value);
    public abstract SqlStatement IsGreaterThanOrEqualTo(DateTime value);
    public abstract SqlStatement IsGreaterThanOrEqualTo(bool value);
    public abstract SqlStatement IsGreaterThanOrEqualTo(long value);
    public abstract SqlStatement IsGreaterThanOrEqualTo(Guid value);
    public abstract SqlStatement IsGreaterThanOrEqualTo(char value);
    public abstract SqlStatement IsGreaterThanOrEqualTo(decimal value);

}

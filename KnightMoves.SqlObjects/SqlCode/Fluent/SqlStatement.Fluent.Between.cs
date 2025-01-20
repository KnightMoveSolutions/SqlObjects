using System;

namespace KnightMoves.SqlObjects.SqlCode;

public abstract partial class SqlStatement : IFluentSqlBetween
{
    public SqlStatement BETWEEN(string value) => BETWEEN(string.Empty, value);
    public abstract SqlStatement BETWEEN(string multipartIdentifier, string value);
    public abstract SqlStatement BETWEEN(int value);
    public abstract SqlStatement BETWEEN(DateTime value);
    public abstract SqlStatement BETWEEN(long value);
    public abstract SqlStatement BETWEEN(char value);
    public abstract SqlStatement BETWEEN(decimal value);

}

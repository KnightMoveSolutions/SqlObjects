using System;

namespace KnightMoves.SqlObjects.SqlCode;

public abstract partial class SqlStatement : IFluentSqlInList
{
    public abstract SqlStatement IN(params string[] values);
    public abstract SqlStatement IN(params int[] values);
    public abstract SqlStatement IN(params DateTime[] values);
    public abstract SqlStatement IN(params long[] values);
    public abstract SqlStatement IN(params char[] values);
    public abstract SqlStatement IN(params decimal[] values);

}

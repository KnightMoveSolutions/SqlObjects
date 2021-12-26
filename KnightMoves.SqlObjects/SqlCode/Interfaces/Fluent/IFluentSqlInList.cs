using System;

namespace KnightMoves.SqlObjects.SqlCode
{
    public interface IFluentSqlInList
    {
        SqlStatement IN(params string[] values);
        SqlStatement IN(params int[] values);
        SqlStatement IN(params DateTime[] values);
        SqlStatement IN(params long[] values);
        SqlStatement IN(params char[] values);
        SqlStatement IN(params decimal[] values);
    }
}

using System.Collections.Generic;

namespace KnightMoves.SqlObjects.SqlCode
{
    public interface IFluentSqlGroupBy
    {
        SqlStatement GROUPBY();
        SqlStatement GROUPBY(IEnumerable<ISqlColumn> columns);
        SqlStatement GROUPBY(IEnumerable<ISqlQueryExpression> groupItems);
    }
}

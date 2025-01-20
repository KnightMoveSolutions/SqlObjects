using System.Collections.Generic;

namespace KnightMoves.SqlObjects.SqlCode;

public abstract partial class SqlStatement : IFluentSqlGroupBy
{
    public abstract SqlStatement GROUPBY();
    public abstract SqlStatement GROUPBY(IEnumerable<ISqlColumn> columns);
    public abstract SqlStatement GROUPBY(IEnumerable<ISqlQueryExpression> groupItems);
}

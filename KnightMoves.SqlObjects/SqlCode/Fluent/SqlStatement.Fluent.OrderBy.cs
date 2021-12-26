using System.Collections.Generic;

namespace KnightMoves.SqlObjects.SqlCode
{
    public abstract partial class SqlStatement : IFluentSqlOrderBy
    {
        public abstract SqlStatement ORDERBY();
        public abstract SqlStatement ORDERBY(string orderByExpression);
        public abstract SqlStatement ORDERBY(string multipartIdentifier, string column);
        public abstract SqlStatement ORDERBY(ISqlQueryExpression orderByExpression);
        public abstract SqlStatement ORDERBY(IEnumerable<ISqlOrderByExpression> orderByExpressions);
        public abstract SqlStatement ASC();
        public abstract SqlStatement DESC();
    }
}

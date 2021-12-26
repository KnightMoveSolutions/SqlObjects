using System;

namespace KnightMoves.SqlObjects.SqlCode
{
    public abstract partial class SqlStatement : IFluentSqlYear
    {
        public abstract SqlStatement YEAR(DateTime dateExpression);
        public abstract SqlStatement YEAR(string dateExpression);
        public abstract SqlStatement YEAR(string multipartIdentifier, string column);
    }
}

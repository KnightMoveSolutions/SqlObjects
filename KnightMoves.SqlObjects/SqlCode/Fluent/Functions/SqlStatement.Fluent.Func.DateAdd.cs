using System;

namespace KnightMoves.SqlObjects.SqlCode
{
    public abstract partial class SqlStatement : IFluentSqlDateAdd
    {
        public abstract SqlStatement DATEADD(DateParts datePart, int increment, DateTime dateExpression);
        public abstract SqlStatement DATEADD(DateParts datePart, int increment, string dateExpression);
        public abstract SqlStatement DATEADD(DateParts datePart, int increment, string multipartIdentifier, string column);
    }
}

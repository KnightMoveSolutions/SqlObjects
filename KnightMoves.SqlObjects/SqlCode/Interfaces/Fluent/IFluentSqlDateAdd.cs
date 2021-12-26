using System;

namespace KnightMoves.SqlObjects.SqlCode
{
    public interface IFluentSqlDateAdd
    {
        SqlStatement DATEADD(DateParts datePart, int increment, DateTime dateExpression);
        SqlStatement DATEADD(DateParts datePart, int increment, string dateExpression);
        SqlStatement DATEADD(DateParts datePart, int increment, string multipartIdentifier, string column);
    }
}

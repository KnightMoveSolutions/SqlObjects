using System;

namespace KnightMoves.SqlObjects.SqlCode;

public abstract partial class SqlStatement : IFluentSqlDateDiff
{
    public abstract SqlStatement DATEDIFF(DateParts datePart, DateTime startDate, DateTime endDate);
    public abstract SqlStatement DATEDIFF(DateParts datePart, string startDateExpression, DateTime endDate);
    public abstract SqlStatement DATEDIFF(DateParts datePart, DateTime startDate, string endDateExpression);
    public abstract SqlStatement DATEDIFF(DateParts datePart, string startDateExpression, string endDateExpression);
    public abstract SqlStatement DATEDIFF(DateParts datePart, string multipartIdentifier, string startDateColumn, DateTime endDate);
    public abstract SqlStatement DATEDIFF(DateParts datePart, string multipartIdentifier, string startDateColumn, string endDateExpression);
    public abstract SqlStatement DATEDIFF(DateParts datePart, DateTime startDate, string multipartIdentifier, string endDateColumn);
    public abstract SqlStatement DATEDIFF(DateParts datePart, string multipartIdentifierStart, string startDateColumn, string multipartIdentifierEnd, string endDateColumn);
}

using System;

namespace KnightMoves.SqlObjects.SqlCode;

public interface IFluentSqlDateDiff
{
    SqlStatement DATEDIFF(DateParts datePart, DateTime startDate, DateTime endDate);
    SqlStatement DATEDIFF(DateParts datePart, string startDateExpression, DateTime endDate);
    SqlStatement DATEDIFF(DateParts datePart, DateTime startDate, string endDateExpression);
    SqlStatement DATEDIFF(DateParts datePart, string startDateExpression, string endDateExpression);
    SqlStatement DATEDIFF(DateParts datePart, string multipartIdentifier, string startDateColumn, DateTime endDate);
    SqlStatement DATEDIFF(DateParts datePart, string multipartIdentifier, string startDateColumn, string endDateExpression);
    SqlStatement DATEDIFF(DateParts datePart, DateTime startDate, string multipartIdentifier, string endDateColumn);
    SqlStatement DATEDIFF(DateParts datePart, string multipartIdentifierStart, string startDateColumn, string multipartIdentifierEnd, string endDateColumn);

}

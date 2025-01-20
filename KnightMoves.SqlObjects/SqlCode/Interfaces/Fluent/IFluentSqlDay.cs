using System;

namespace KnightMoves.SqlObjects.SqlCode;

public interface IFluentSqlDay
{
    SqlStatement DAY(DateTime dateExpression);
    SqlStatement DAY(string dateExpression);
    SqlStatement DAY(string multipartIdentifier, string column);
}

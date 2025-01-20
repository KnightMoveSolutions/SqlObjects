using System;

namespace KnightMoves.SqlObjects.SqlCode;

public interface IFluentSqlMonth
{
    SqlStatement MONTH(DateTime dateExpression);
    SqlStatement MONTH(string dateExpression);
    SqlStatement MONTH(string multipartIdentifier, string column);
}

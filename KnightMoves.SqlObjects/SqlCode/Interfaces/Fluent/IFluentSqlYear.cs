using System;

namespace KnightMoves.SqlObjects.SqlCode
{
    public interface IFluentSqlYear
    {
        SqlStatement YEAR(DateTime dateExpression);
        SqlStatement YEAR(string dateExpression);
        SqlStatement YEAR(string multipartIdentifier, string column);
    }
}

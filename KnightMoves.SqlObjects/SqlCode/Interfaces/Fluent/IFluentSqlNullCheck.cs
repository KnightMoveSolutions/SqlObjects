using System;

namespace KnightMoves.SqlObjects.SqlCode;

public interface IFluentSqlNullCheck
{
    SqlStatement IS_NULL();
    SqlStatement IS_NOT_NULL();

    SqlStatement ISNULL(string multipartIdentifier, string column, string replaceExpression);
    SqlStatement ISNULL(string multipartIdentifier, string column, int replaceExpression);
    SqlStatement ISNULL(string multipartIdentifier, string column, DateTime replaceExpression);
    SqlStatement ISNULL(string multipartIdentifier, string column, bool replaceExpression);
    SqlStatement ISNULL(string multipartIdentifier, string column, long replaceExpression);
    SqlStatement ISNULL(string multipartIdentifier, string column, Guid replaceExpression);
    SqlStatement ISNULL(string multipartIdentifier, string column, char replaceExpression);
    SqlStatement ISNULL(string multipartIdentifier, string column, decimal replaceExpression);

    SqlStatement ISNULL(string checkExpression, string replaceExpression);
    SqlStatement ISNULL(string checkExpression, int replaceExpression);
    SqlStatement ISNULL(string checkExpression, DateTime replaceExpression);
    SqlStatement ISNULL(string checkExpression, bool replaceExpression);
    SqlStatement ISNULL(string checkExpression, long replaceExpression);
    SqlStatement ISNULL(string checkExpression, Guid replaceExpression);
    SqlStatement ISNULL(string checkExpression, char replaceExpression);
    SqlStatement ISNULL(string checkExpression, decimal replaceExpression);

    SqlStatement ISNULL(ISqlQueryExpression checkExpression, string replaceExpression);
    SqlStatement ISNULL(ISqlQueryExpression checkExpression, int replaceExpression);
    SqlStatement ISNULL(ISqlQueryExpression checkExpression, DateTime replaceExpression);
    SqlStatement ISNULL(ISqlQueryExpression checkExpression, bool replaceExpression);
    SqlStatement ISNULL(ISqlQueryExpression checkExpression, long replaceExpression);
    SqlStatement ISNULL(ISqlQueryExpression checkExpression, Guid replaceExpression);
    SqlStatement ISNULL(ISqlQueryExpression checkExpression, char replaceExpression);
    SqlStatement ISNULL(ISqlQueryExpression checkExpression, decimal replaceExpression);

    SqlStatement ISNULL(ISqlQueryExpression checkExpression, ISqlQueryExpression replaceExpression);
}

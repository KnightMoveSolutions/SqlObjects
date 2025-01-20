using System.Collections.Generic;

namespace KnightMoves.SqlObjects.SqlCode;

public interface IFluentSqlOrderBy
{
    SqlStatement ORDERBY();
    SqlStatement ORDERBY(string orderByExpression);
    SqlStatement ORDERBY(string multipartIdentifier, string column);
    SqlStatement ORDERBY(ISqlQueryExpression orderByExpression);
    SqlStatement ORDERBY(IEnumerable<ISqlOrderByExpression> orderByExpressions);
    SqlStatement ASC();
    SqlStatement DESC();
}

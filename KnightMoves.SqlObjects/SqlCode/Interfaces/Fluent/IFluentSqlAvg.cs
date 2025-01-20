namespace KnightMoves.SqlObjects.SqlCode;

public interface IFluentSqlAvg
{
    SqlStatement AVG(decimal aggregateExpression);
    SqlStatement AVG(string aggregateExpression);
    SqlStatement AVG(string multipartIdentifier, string column);
}

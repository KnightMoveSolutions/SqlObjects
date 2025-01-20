namespace KnightMoves.SqlObjects.SqlCode;

public abstract partial class SqlStatement : IFluentSqlAvg
{
    public abstract SqlStatement AVG(decimal aggregateExpression);
    public abstract SqlStatement AVG(string aggregateExpression);
    public abstract SqlStatement AVG(string multipartIdentifier, string column);
}

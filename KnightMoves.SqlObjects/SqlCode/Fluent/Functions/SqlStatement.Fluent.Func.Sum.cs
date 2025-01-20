namespace KnightMoves.SqlObjects.SqlCode;

public abstract partial class SqlStatement : IFluentSqlSum
{
    public abstract SqlStatement SUM(string aggregateExpression);
    public abstract SqlStatement SUM(string multipartIdentifier, string column);
}

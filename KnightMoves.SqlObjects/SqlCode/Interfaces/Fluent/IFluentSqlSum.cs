namespace KnightMoves.SqlObjects.SqlCode
{
    public interface IFluentSqlSum
    {
        SqlStatement SUM(string aggregateExpression);
        SqlStatement SUM(string multipartIdentifier, string column);
    }
}

namespace KnightMoves.SqlObjects.SqlCode
{
    public interface IFluentSqlMin
    {
        SqlStatement MIN(string aggregateExpression);
        SqlStatement MIN(string multipartIdentifier, string column);
    }
}

namespace KnightMoves.SqlObjects.SqlCode
{
    public interface IFluentSqlMax
    {
        SqlStatement MAX(string aggregateExpression);
        SqlStatement MAX(string multipartIdentifier, string column);
    }
}

namespace KnightMoves.SqlObjects.SqlCode
{
    public interface IFluentSqlCount
    {
        SqlStatement COUNT(string aggregateExpression);
        SqlStatement COUNT(string multipartIdentifier, string column);
    }
}

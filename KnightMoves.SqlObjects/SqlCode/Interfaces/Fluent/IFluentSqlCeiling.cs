namespace KnightMoves.SqlObjects.SqlCode
{
    public interface IFluentSqlCeiling
    {
        SqlStatement CEILING(decimal numericExpression);
        SqlStatement CEILING(string aggregateExpression);
        SqlStatement CEILING(string multipartIdentifier, string column);
    }
}

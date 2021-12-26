namespace KnightMoves.SqlObjects.SqlCode
{
    public interface IFluentSqlFloor
    {
        SqlStatement FLOOR(decimal numericExpression);
        SqlStatement FLOOR(string numericExpression);
        SqlStatement FLOOR(string multipartIdentifier, string column);
    }
}

namespace KnightMoves.SqlObjects.SqlCode;

public interface IFluentSqlAbs
{
    SqlStatement ABS(decimal numericExpression);
    SqlStatement ABS(string numericExpression);
    SqlStatement ABS(string multipartIdentifier, string column);
}

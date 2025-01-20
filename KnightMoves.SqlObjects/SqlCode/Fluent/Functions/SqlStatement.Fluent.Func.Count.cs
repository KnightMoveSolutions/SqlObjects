namespace KnightMoves.SqlObjects.SqlCode;

public abstract partial class SqlStatement : IFluentSqlCount
{
    public abstract SqlStatement COUNT(string aggregateExpression);
    public abstract SqlStatement COUNT(string multipartIdentifier, string column);
}

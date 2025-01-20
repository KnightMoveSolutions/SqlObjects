namespace KnightMoves.SqlObjects.SqlCode;

public abstract partial class SqlStatement : IFluentSqlCeiling
{
    public abstract SqlStatement CEILING(decimal numericExpression);
    public abstract SqlStatement CEILING(string aggregateExpression);
    public abstract SqlStatement CEILING(string multipartIdentifier, string column);
}

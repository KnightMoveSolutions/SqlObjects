namespace KnightMoves.SqlObjects.SqlCode;

public abstract partial class SqlStatement : IFluentSqlFloor
{
    public abstract SqlStatement FLOOR(decimal numericExpression);
    public abstract SqlStatement FLOOR(string numericExpression);
    public abstract SqlStatement FLOOR(string multipartIdentifier, string column);
}

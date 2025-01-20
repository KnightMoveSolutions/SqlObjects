namespace KnightMoves.SqlObjects.SqlCode;

public abstract partial class SqlStatement : IFluentSqlUnion
{
    public abstract SqlStatement UNION();
}

namespace KnightMoves.SqlObjects.SqlCode;

public abstract partial class SqlStatement : IFluentSqlLike
{
    public abstract SqlStatement LIKE(string pattern);
}

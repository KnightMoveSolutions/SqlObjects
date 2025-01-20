namespace KnightMoves.SqlObjects.SqlCode;

public interface IFluentSqlLike
{
    SqlStatement LIKE(string pattern);
}

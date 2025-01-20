namespace KnightMoves.SqlObjects.SqlCode;

public interface IFluentSqlJoin
{
    SqlStatement ON(string column);
    SqlStatement ON(string multiPartIdentifier, string column);
}

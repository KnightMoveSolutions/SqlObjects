namespace KnightMoves.SqlObjects.SqlCode;

public interface IFluentSqlFrom
{
    SqlStatement FROM();
    SqlStatement FROM(string table);
    SqlStatement FROM(string schema, string table);
    SqlStatement FROM(string schema, string table, string alias);
}

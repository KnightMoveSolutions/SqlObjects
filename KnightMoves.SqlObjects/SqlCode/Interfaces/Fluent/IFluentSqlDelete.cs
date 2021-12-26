namespace KnightMoves.SqlObjects.SqlCode
{
    public interface IFluentSqlDelete
    {
        SqlStatement DELETE();
        SqlStatement FROM(string table);
        SqlStatement FROM(string schema, string table);
    }
}

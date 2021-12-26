namespace KnightMoves.SqlObjects.SqlCode
{
    public interface IFluentSqlUpdate
    {
        SqlStatement UPDATE(string table);
        SqlStatement UPDATE(string schema, string table);
        SqlStatement SET();
    }
}

namespace KnightMoves.SqlObjects.SqlCode
{
    public abstract partial class SqlStatement : IFluentSqlUpdate
    {
        public abstract SqlStatement UPDATE(string table);
        public abstract SqlStatement UPDATE(string schema, string table);
        public abstract SqlStatement SET();
    }
}

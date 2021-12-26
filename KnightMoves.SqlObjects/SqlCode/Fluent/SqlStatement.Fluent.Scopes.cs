namespace KnightMoves.SqlObjects.SqlCode
{
    public abstract partial class SqlStatement : IFluentSqlScopes
    {
        public abstract SqlStatement StartConditionScope();
        public abstract SqlStatement EndConditionScope();
        public abstract SqlStatement SubQueryStart();
        public abstract SqlStatement SubQueryEnd();
    }
}

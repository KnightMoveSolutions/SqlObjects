namespace KnightMoves.SqlObjects.SqlCode
{
    public abstract partial class SqlStatement : IFluentSqlNullCheck
    {
        public abstract SqlStatement IS_NULL();
        public abstract SqlStatement IS_NOT_NULL();

        // The rest of IFluentSqlNullCheck implemented in  ..\Functions\SqlStatement.Fluent.Func.ISNULL.cs
    }
}

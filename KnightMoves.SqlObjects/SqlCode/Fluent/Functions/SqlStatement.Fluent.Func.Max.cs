namespace KnightMoves.SqlObjects.SqlCode
{
    public abstract partial class SqlStatement : IFluentSqlMax
    {
        public abstract SqlStatement MAX(string aggregateExpression);
        public abstract SqlStatement MAX(string multipartIdentifier, string column);
    }
}

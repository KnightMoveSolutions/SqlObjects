namespace KnightMoves.SqlObjects.SqlCode
{
    public abstract partial class SqlStatement : IFluentSqlMin
    {
        public abstract SqlStatement MIN(string aggregateExpression);
        public abstract SqlStatement MIN(string multipartIdentifier, string column);
    }
}

namespace KnightMoves.SqlObjects.SqlCode
{
    public abstract partial class SqlStatement : IFluentSqlAbs
    {
        public abstract SqlStatement ABS(decimal numericExpression);
        public abstract SqlStatement ABS(string numericExpression);
        public abstract SqlStatement ABS(string multipartIdentifier, string column);
    }
}

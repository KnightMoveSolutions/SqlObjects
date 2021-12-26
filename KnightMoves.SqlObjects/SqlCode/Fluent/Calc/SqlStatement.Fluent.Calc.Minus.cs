namespace KnightMoves.SqlObjects.SqlCode
{
    public abstract partial class SqlStatement : IFluentSqlMinus
    {
        public SqlStatement Minus(string column) => Minus(string.Empty, column);
        public abstract SqlStatement Minus(string multipartIdentifier, string column);
        public abstract SqlStatement Minus(int operand);
        public abstract SqlStatement Minus(decimal operand);
        public abstract SqlStatement Minus(long operand);
    }
}

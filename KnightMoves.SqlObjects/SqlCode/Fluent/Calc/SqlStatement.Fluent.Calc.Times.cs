namespace KnightMoves.SqlObjects.SqlCode
{
    public abstract partial class SqlStatement : IFluentSqlTimes
    {
        public SqlStatement Times(string column) => Times(string.Empty, column);
        public abstract SqlStatement Times(string multipartIdentifier, string column);
        public abstract SqlStatement Times(int operand);
        public abstract SqlStatement Times(decimal operand);
        public abstract SqlStatement Times(long operand);
    }
}

namespace KnightMoves.SqlObjects.SqlCode;

public abstract partial class SqlStatement : IFluentSqlDividedBy
{
    public SqlStatement DividedBy(string column) => DividedBy(string.Empty, column);
    public abstract SqlStatement DividedBy(string multipartIdentifier, string column);
    public abstract SqlStatement DividedBy(int operand);
    public abstract SqlStatement DividedBy(decimal operand);
    public abstract SqlStatement DividedBy(long operand);
}

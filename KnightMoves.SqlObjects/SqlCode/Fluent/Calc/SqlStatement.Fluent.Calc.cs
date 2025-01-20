namespace KnightMoves.SqlObjects.SqlCode;

public abstract partial class SqlStatement : IFluentSqlCalc
{
    public SqlStatement Calculate(string column) => Calculate(string.Empty, column);
    public abstract SqlStatement Calculate(string multipartIdentifier, string column);
    public abstract SqlStatement Calculate(int operand);
    public abstract SqlStatement Calculate(decimal operand);
    public abstract SqlStatement Calculate(long operand);
}

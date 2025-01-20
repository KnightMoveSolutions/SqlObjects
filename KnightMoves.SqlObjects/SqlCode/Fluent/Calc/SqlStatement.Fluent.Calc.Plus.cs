namespace KnightMoves.SqlObjects.SqlCode;

public abstract partial class SqlStatement : IFluentSqlPlus
{
    public SqlStatement Plus(string column) => Plus(string.Empty, column);
    public abstract SqlStatement Plus(string multipartIdentifier, string column);
    public abstract SqlStatement Plus(int operand);
    public abstract SqlStatement Plus(decimal operand);
    public abstract SqlStatement Plus(long operand);
}

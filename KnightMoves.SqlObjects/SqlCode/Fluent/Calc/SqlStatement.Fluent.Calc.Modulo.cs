namespace KnightMoves.SqlObjects.SqlCode;

public abstract partial class SqlStatement : IFluentSqlModulo
{
    public SqlStatement Modulo(string column) => Modulo(string.Empty, column);
    public abstract SqlStatement Modulo(string multipartIdentifier, string column);
    public abstract SqlStatement Modulo(int operand);
    public abstract SqlStatement Modulo(decimal operand);
    public abstract SqlStatement Modulo(long operand);
}

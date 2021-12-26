namespace KnightMoves.SqlObjects.SqlCode
{
    public interface IFluentSqlModulo
    {
        SqlStatement Modulo(string column);
        SqlStatement Modulo(string multipartIdentifier, string column);
        SqlStatement Modulo(int operand);
        SqlStatement Modulo(decimal operand);
        SqlStatement Modulo(long operand);
    }
}

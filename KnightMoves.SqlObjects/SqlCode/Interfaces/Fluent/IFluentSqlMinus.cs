namespace KnightMoves.SqlObjects.SqlCode;

public interface IFluentSqlMinus
{
    SqlStatement Minus(string column);
    SqlStatement Minus(string multipartIdentifier, string column);
    SqlStatement Minus(int operand);
    SqlStatement Minus(decimal operand);
    SqlStatement Minus(long operand);
}

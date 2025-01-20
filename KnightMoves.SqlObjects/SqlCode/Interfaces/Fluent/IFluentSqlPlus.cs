namespace KnightMoves.SqlObjects.SqlCode;

public interface IFluentSqlPlus
{
    SqlStatement Plus(string column);
    SqlStatement Plus(string multipartIdentifier, string column);
    SqlStatement Plus(int operand);
    SqlStatement Plus(decimal operand);
    SqlStatement Plus(long operand);
}

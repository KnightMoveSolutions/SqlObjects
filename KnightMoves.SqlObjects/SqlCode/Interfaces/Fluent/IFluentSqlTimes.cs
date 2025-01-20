namespace KnightMoves.SqlObjects.SqlCode;

public interface IFluentSqlTimes
{
    SqlStatement Times(string column);
    SqlStatement Times(string multipartIdentifier, string column);
    SqlStatement Times(int operand);
    SqlStatement Times(decimal operand);
    SqlStatement Times(long operand);
}

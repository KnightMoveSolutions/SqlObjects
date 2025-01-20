namespace KnightMoves.SqlObjects.SqlCode;

public interface IFluentSqlCalc :
    IFluentSqlPlus,
    IFluentSqlMinus,
    IFluentSqlTimes,
    IFluentSqlDividedBy,
    IFluentSqlModulo
{
    SqlStatement Calculate(string column);
    SqlStatement Calculate(string multipartIdentifier, string column);
    SqlStatement Calculate(int operand);
    SqlStatement Calculate(decimal operand);
    SqlStatement Calculate(long operand);
}

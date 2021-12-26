namespace KnightMoves.SqlObjects.SqlCode
{
    public interface IFluentSqlDividedBy
    {
        SqlStatement DividedBy(string column);
        SqlStatement DividedBy(string multipartIdentifier, string column);
        SqlStatement DividedBy(int operand);
        SqlStatement DividedBy(decimal operand);
        SqlStatement DividedBy(long operand);
    }
}

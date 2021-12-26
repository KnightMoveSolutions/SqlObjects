namespace KnightMoves.SqlObjects.SqlCode
{
    public interface IFluentSqlScopes
    {
        SqlStatement StartConditionScope();
        SqlStatement EndConditionScope();
        SqlStatement SubQueryStart();
        SqlStatement SubQueryEnd();
    }
}

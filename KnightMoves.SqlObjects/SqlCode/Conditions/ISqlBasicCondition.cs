namespace KnightMoves.SqlObjects.SqlCode
{
    public interface ISqlBasicCondition : ISqlCondition
    {
        SqlComparisonOperators Operator { get; set; }
    }
}
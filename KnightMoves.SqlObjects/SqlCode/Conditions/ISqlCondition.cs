namespace KnightMoves.SqlObjects.SqlCode
{
    public interface ISqlCondition
    {
        SqlLogicalOperators LogicalOperator { get; set; }
        ISqlQueryExpression LeftOperand { get; set; }
    }
}
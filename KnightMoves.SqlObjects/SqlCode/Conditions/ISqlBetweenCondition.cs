namespace KnightMoves.SqlObjects.SqlCode
{
    public interface ISqlBetweenCondition : ISqlCondition
    {
        ISqlQueryExpression StartVal { get; set; }
        ISqlQueryExpression EndVal { get; set; }
    }
}
namespace KnightMoves.SqlObjects.SqlCode;

public interface ISqlCalculation : ISqlQueryExpression
{
    ISqlQueryExpression LeftOperand { get; set; }
    SqlArithmeticOperators Operator { get; set; }
    ISqlQueryExpression RightOperand { get; set; }
}
using System;
using System.Linq;

namespace KnightMoves.SqlObjects.SqlCode;

public abstract partial class SqlStatement : IFluentSqlLogicalOperators
{
    public SqlStatement AND()
    {
        if (IsCondition)
        {
            var condition = Children.LastOrDefault(c => c is ISqlCondition) as ISqlCondition;

            if (condition != null)
                condition.LogicalOperator = SqlLogicalOperators.AND;

            return this;
        }

        LogicalOperator = SqlLogicalOperators.AND;

        return this;
    }

    public virtual SqlStatement AND(ISqlLiteral leftLiteral)
    {
        LogicalOperator = SqlLogicalOperators.AND;
        LeftOperand = leftLiteral;
        return this;
    }

    public SqlStatement AND(string operand)
    {
        return AND(string.Empty, operand);
    }

    public virtual SqlStatement AND(string multiPartIdentifier, string operand)
    {
        LogicalOperator = SqlLogicalOperators.AND;
        LeftMultiPartIdentifier = multiPartIdentifier;
        LeftOperandValue = operand;
        return this;
    }

    public abstract SqlStatement AND(int operand);
    public abstract SqlStatement AND(DateTime operand);
    public abstract SqlStatement AND(bool operand);
    public abstract SqlStatement AND(long operand);
    public abstract SqlStatement AND(Guid operand);
    public abstract SqlStatement AND(char operand);
    public abstract SqlStatement AND(decimal operand);


    public SqlStatement OR()
    {
        if (IsConditionGroup)
        {
            var condition = Children.Last(c => c is ISqlCondition) as ISqlCondition;

            if (condition != null)
                condition.LogicalOperator = SqlLogicalOperators.OR;

            return this;
        }

        LogicalOperator = SqlLogicalOperators.OR;

        return this;
    }

    public SqlStatement OR(ISqlLiteral leftLiteral)
    {
        LogicalOperator = SqlLogicalOperators.OR;
        LeftOperand = leftLiteral;
        return this;
    }

    public SqlStatement OR(string operand)
    {
        return OR(string.Empty, operand);
    }

    public SqlStatement OR(string multiPartIdentifier, string operand)
    {
        LogicalOperator = SqlLogicalOperators.OR;
        LeftMultiPartIdentifier = multiPartIdentifier;
        LeftOperandValue = operand;
        return this;
    }

}

/*
    THE LICENSED WORK IS PROVIDED UNDER THE TERMS OF THE DEVELOPER TOOL LIMITED 
    LICENSE (“LICENSE”) AS FIRST COMPLETED BY THE ORIGINAL AUTHOR. ANY USE, PUBLIC 
    DISPLAY, PUBLIC PERFORMANCE, REPRODUCTION OR DISTRIBUTION OF, OR PREPARATION OF 
    DERIVATIVE WORKS BASED ON THE LICENSED WORK CONSTITUTES RECIPIENT’S ACCEPTANCE 
    OF THIS LICENSE AND ITS TERMS, WHETHER OR NOT SUCH RECIPIENT READS THE TERMS OF THE 
    LICENSE. “LICENSED WORK” AND “RECIPIENT” ARE DEFINED IN THE LICENSE. A COPY OF THE 
    LICENSE IS LOCATED IN THE TEXT FILE ENTITLED “LICENSE.TXT” ACCOMPANYING THE CONTENTS 
    OF THIS FILE. IF A COPY OF THE LICENSE DOES NOT ACCOMPANY THIS FILE, A COPY OF THE 
    LICENSE MAY ALSO BE OBTAINED AT THE FOLLOWING WEB SITE:  
 
    https://docs.knightmovesolutions.com/sql-objects/license.html
*/

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

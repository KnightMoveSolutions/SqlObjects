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

namespace KnightMoves.SqlObjects.SqlCode.TSQL;

public abstract partial class TSQLStatement : SqlStatement
{
    /// <summary>
    /// Creates a condition scope that when rendered into SQL is represented by a pair of parentheses 
    /// that is abile to contain SQL conditions
    /// <code>
    /// ( /* Scope is here where conditions are defined */ )
    /// </code>
    /// </summary>
    public override SqlStatement StartConditionScope()
    {
        var conditionGroup = new TSQLConditionGroup { LogicalOperator = LogicalOperator };

        var parent = GetConditionParent();

        parent.Children.Add(conditionGroup);

        return conditionGroup;
    }

    /// <summary>
    /// Signifies the termination of a condition scope (i.e. closing parentheses)
    /// </summary>
    public override SqlStatement EndConditionScope() => Parent;

    /// <summary>
    /// Creates a sub-query scope object intended to contain a query inside of it
    /// </summary>
    public override SqlStatement SubQueryStart()
    {
        var subQuery = new TSQLSubQuery { LogicalOperator = LogicalOperator };

        var parent = GetExpressionParent();

        var leftOperand = GetLeftOperand() as SqlStatement;

        // If leftOperand exists then this sub-query should be on the right side of a condition
        // so we build the condition and then return the sub-query. Otherwise proceed normally.
        if (leftOperand != null && leftOperand.ComparisonOperator.HasValue)
        {
            var basicCondition = new TSQLBasicCondition { Operator = leftOperand.ComparisonOperator.Value };

            parent.Children.Add(basicCondition);

            basicCondition.Children.Add(leftOperand);
            basicCondition.Children.Add(subQuery);

            ClearTempValues();

            return subQuery;
        }

        if (parent == null)
            parent = GetParentScope();

        parent.Children.Add(subQuery);

        return subQuery;
    }

    /// <summary>
    /// Signifies the termination of a sub-query (i.e. closing parentheses)
    /// </summary>
    public override SqlStatement SubQueryEnd() => GetParentScope();

}

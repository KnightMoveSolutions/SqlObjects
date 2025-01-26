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

using System.Data;

namespace KnightMoves.SqlObjects.SqlCode.TSQL;

public abstract partial class TSQLStatement : SqlStatement
{
    /// <summary>
    /// Finish a calculation or condition with IS NULL
    /// </summary>
    public override SqlStatement IS_NULL()
    {
        var parent = IsColumn || this is ITSQLCase ? GetExpressionParent() : GetConditionParent();
        var basicCondition = MakeBasicCondition(LeftMultiPartIdentifier, LeftOperandValue, null, SqlComparisonOperators.IS, null, "NULL", SqlDbType.Variant);
        parent.Children.Add(basicCondition);
        return parent;
    }

    /// <summary>
    /// Finish a calculation or condition with IS NOT NULL
    /// </summary>
    public override SqlStatement IS_NOT_NULL()
    {
        var parent = IsColumn || this is ITSQLCase ? GetExpressionParent() : GetConditionParent();
        var basicCondition = MakeBasicCondition(LeftMultiPartIdentifier, LeftOperandValue, null, SqlComparisonOperators.ISNOT, null, "NULL", SqlDbType.Variant);
        parent.Children.Add(basicCondition);
        return parent;
    }
}

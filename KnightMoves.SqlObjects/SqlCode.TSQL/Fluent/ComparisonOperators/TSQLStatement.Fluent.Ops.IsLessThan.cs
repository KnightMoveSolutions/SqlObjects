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
using System.Data;

namespace KnightMoves.SqlObjects.SqlCode.TSQL;

public abstract partial class TSQLStatement : SqlStatement
{
    /// <summary>
    /// Continue a calculation or a condition with the '<' sign
    /// </summary>
    public override SqlStatement IsLessThan()
    {
        ComparisonOperator = SqlComparisonOperators.IsLessThan;
        return this;
    }

    /// <summary>
    /// Finish a calculation or a condition as '<' to a column as the right operand 
    /// </summary>
    public override SqlStatement IsLessThan(string multiPartIdentifier, string value)
    {
        SqlDbType? rightDbType = null;
        if (string.IsNullOrEmpty(multiPartIdentifier)) rightDbType = SqlDbType.VarChar;
        var parent = IsColumn || this is ITSQLCase ? GetExpressionParent() : GetConditionParent();
        var basicCondition = MakeBasicCondition(LeftMultiPartIdentifier, LeftOperandValue, null, SqlComparisonOperators.IsLessThan, multiPartIdentifier, value, rightDbType);
        parent.Children.Add(basicCondition);
        return parent;
    }

    /// <summary>
    /// Finish a calculation or a condition as '<' to an <see cref="int"/> as the right operand
    /// </summary>
    public override SqlStatement IsLessThan(int value)
    {
        var parent = IsColumn || this is ITSQLCase ? GetExpressionParent() : GetConditionParent();
        var basicCondition = MakeBasicCondition(LeftMultiPartIdentifier, LeftOperandValue, null, SqlComparisonOperators.IsLessThan, null, value.ToString(), SqlDbType.Int);
        parent.Children.Add(basicCondition);
        return parent;
    }

    /// <summary>
    /// Finish a calculation or a condition as '<' to an <see cref="DateTime"/> as the right operand
    /// </summary>
    public override SqlStatement IsLessThan(DateTime value)
    {
        var parent = IsColumn || this is ITSQLCase ? GetExpressionParent() : GetConditionParent();
        var basicCondition = MakeBasicCondition(LeftMultiPartIdentifier, LeftOperandValue, null, SqlComparisonOperators.IsLessThan, null, value.ToString(SqlDataType.SQL_DATE_STRING_FORMAT), SqlDbType.DateTime);
        parent.Children.Add(basicCondition);
        return parent;
    }

    /// <summary>
    /// Finish a calculation or a condition as '<' to an <see cref="bool"/> as the right operand
    /// </summary>
    public override SqlStatement IsLessThan( bool value)
    {
        var parent = IsColumn || this is ITSQLCase ? GetExpressionParent() : GetConditionParent();
        var basicCondition = MakeBasicCondition(LeftMultiPartIdentifier, LeftOperandValue, null, SqlComparisonOperators.IsLessThan, null, value ? "1" : "0", SqlDbType.Bit);
        parent.Children.Add(basicCondition);
        return parent;
    }

    /// <summary>
    /// Finish a calculation or a condition as '<' to an <see cref="long"/> as the right operand
    /// </summary>
    public override SqlStatement IsLessThan(long value)
    {
        var parent = IsColumn || this is ITSQLCase ? GetExpressionParent() : GetConditionParent();
        var basicCondition = MakeBasicCondition(LeftMultiPartIdentifier, LeftOperandValue, null, SqlComparisonOperators.IsLessThan, null, value.ToString(), SqlDbType.BigInt);
        parent.Children.Add(basicCondition);
        return parent;
    }

    /// <summary>
    /// Finish a calculation or a condition as '<' to an <see cref="Guid"/> as the right operand
    /// </summary>
    public override SqlStatement IsLessThan(Guid value)
    {
        var parent = IsColumn || this is ITSQLCase ? GetExpressionParent() : GetConditionParent();
        var basicCondition = MakeBasicCondition(LeftMultiPartIdentifier, LeftOperandValue, null, SqlComparisonOperators.IsLessThan, null, value.ToString(), SqlDbType.UniqueIdentifier);
        parent.Children.Add(basicCondition);
        return parent;
    }

    /// <summary>
    /// Finish a calculation or a condition as '<' to an <see cref="char"/> as the right operand
    /// </summary>
    public override SqlStatement IsLessThan(char value)
    {
        var parent = IsColumn || this is ITSQLCase ? GetExpressionParent() : GetConditionParent();
        var basicCondition = MakeBasicCondition(LeftMultiPartIdentifier, LeftOperandValue, null, SqlComparisonOperators.IsLessThan, null, value.ToString(), SqlDbType.Char);
        parent.Children.Add(basicCondition);
        return parent;
    }

    /// <summary>
    /// Finish a calculation or a condition as '<' to an <see cref="decimal"/> as the right operand
    /// </summary>
    public override SqlStatement IsLessThan(decimal value)
    {
        var parent = IsColumn || this is ITSQLCase ? GetExpressionParent() : GetConditionParent();
        var basicCondition = MakeBasicCondition(LeftMultiPartIdentifier, LeftOperandValue, null, SqlComparisonOperators.IsLessThan, null, value.ToString(), SqlDbType.Decimal);
        parent.Children.Add(basicCondition);
        return parent;
    }

}

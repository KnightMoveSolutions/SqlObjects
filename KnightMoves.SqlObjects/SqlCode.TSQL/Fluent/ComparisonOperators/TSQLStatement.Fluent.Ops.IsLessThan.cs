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

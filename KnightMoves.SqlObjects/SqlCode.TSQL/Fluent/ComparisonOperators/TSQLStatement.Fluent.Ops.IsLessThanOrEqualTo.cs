using System;
using System.Data;

namespace KnightMoves.SqlObjects.SqlCode.TSQL
{
    public abstract partial class TSQLStatement : SqlStatement
    {
        /// <summary>
        /// Continue a calculation or a condition with the '<=' sign
        /// </summary>
        public override SqlStatement IsLessThanOrEqualTo()
        {
            ComparisonOperator = SqlComparisonOperators.IsLessThanOrEqualTo;
            return this;
        }

        /// <summary>
        /// Finish a calculation or a condition as '<=' to a column as the right operand 
        /// </summary>
        public override SqlStatement IsLessThanOrEqualTo(string multiPartIdentifier, string value)
        {
            SqlDbType? rightDbType = null;
            if (string.IsNullOrEmpty(multiPartIdentifier)) rightDbType = SqlDbType.VarChar;
            var parent = IsColumn || this is ITSQLCase ? GetExpressionParent() : GetConditionParent();
            var basicCondition = MakeBasicCondition(LeftMultiPartIdentifier, LeftOperandValue, null, SqlComparisonOperators.IsLessThanOrEqualTo, multiPartIdentifier, value, rightDbType);
            parent.Children.Add(basicCondition);
            return parent;
        }

        /// <summary>
        /// Finish a calculation or a condition as '<=' to an <see cref="int"/> as the right operand
        /// </summary>
        public override SqlStatement IsLessThanOrEqualTo(int value)
        {
            var parent = IsColumn || this is ITSQLCase ? GetExpressionParent() : GetConditionParent();
            var basicCondition = MakeBasicCondition(LeftMultiPartIdentifier, LeftOperandValue, null, SqlComparisonOperators.IsLessThanOrEqualTo, null, value.ToString(), SqlDbType.Int);
            parent.Children.Add(basicCondition);
            return parent;
        }

        /// <summary>
        /// Finish a calculation or a condition as '<=' to a <see cref="DateTime"/> as the right operand
        /// </summary>
        public override SqlStatement IsLessThanOrEqualTo(DateTime value)
        {
            var parent = IsColumn || this is ITSQLCase ? GetExpressionParent() : GetConditionParent();
            var basicCondition = MakeBasicCondition(LeftMultiPartIdentifier, LeftOperandValue, null, SqlComparisonOperators.IsLessThanOrEqualTo, null, value.ToString(SqlDataType.SQL_DATE_STRING_FORMAT), SqlDbType.DateTime);
            parent.Children.Add(basicCondition);
            return parent;
        }

        /// <summary>
        /// Finish a calculation or a condition as '<=' to a <see cref="bool"/> as the right operand
        /// </summary>
        public override SqlStatement IsLessThanOrEqualTo(bool value)
        {
            var parent = IsColumn || this is ITSQLCase ? GetExpressionParent() : GetConditionParent();
            var basicCondition = MakeBasicCondition(LeftMultiPartIdentifier, LeftOperandValue, null, SqlComparisonOperators.IsLessThanOrEqualTo, null, value ? "1" : "0", SqlDbType.Bit);
            parent.Children.Add(basicCondition);
            return parent;
        }

        /// <summary>
        /// Finish a calculation or a condition as '<=' to a <see cref="long"/> as the right operand
        /// </summary>
        public override SqlStatement IsLessThanOrEqualTo(long value)
        {
            var parent = IsColumn || this is ITSQLCase ? GetExpressionParent() : GetConditionParent();
            var basicCondition = MakeBasicCondition(LeftMultiPartIdentifier, LeftOperandValue, null, SqlComparisonOperators.IsLessThanOrEqualTo, null, value.ToString(), SqlDbType.BigInt);
            parent.Children.Add(basicCondition);
            return parent;
        }

        /// <summary>
        /// Finish a calculation or a condition as '<=' to a <see cref="Guid"/> as the right operand
        /// </summary>
        public override SqlStatement IsLessThanOrEqualTo(Guid value)
        {
            var parent = IsColumn || this is ITSQLCase ? GetExpressionParent() : GetConditionParent();
            var basicCondition = MakeBasicCondition(LeftMultiPartIdentifier, LeftOperandValue, null, SqlComparisonOperators.IsLessThanOrEqualTo, null, value.ToString(), SqlDbType.UniqueIdentifier);
            parent.Children.Add(basicCondition);
            return parent;
        }

        /// <summary>
        /// Finish a calculation or a condition as '<=' to a <see cref="char"/> as the right operand
        /// </summary>
        public override SqlStatement IsLessThanOrEqualTo(char value)
        {
            var parent = IsColumn || this is ITSQLCase ? GetExpressionParent() : GetConditionParent();
            var basicCondition = MakeBasicCondition(LeftMultiPartIdentifier, LeftOperandValue, null, SqlComparisonOperators.IsLessThanOrEqualTo, null, value.ToString(), SqlDbType.Char);
            parent.Children.Add(basicCondition);
            return parent;
        }

        /// <summary>
        /// Finish a calculation or a condition as '<=' to a <see cref="decimal"/> as the right operand
        /// </summary>
        public override SqlStatement IsLessThanOrEqualTo(decimal value)
        {
            var parent = IsColumn || this is ITSQLCase ? GetExpressionParent() : GetConditionParent();
            var basicCondition = MakeBasicCondition(LeftMultiPartIdentifier, LeftOperandValue, null, SqlComparisonOperators.IsLessThanOrEqualTo, null, value.ToString(), SqlDbType.Decimal);
            parent.Children.Add(basicCondition);
            return parent;
        }

    }
}

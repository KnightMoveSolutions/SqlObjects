using System.Data;

namespace KnightMoves.SqlObjects.SqlCode.TSQL
{
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
}

using System.Linq;
using System.Text;

namespace KnightMoves.SqlObjects.SqlCode.TSQL
{
    /// <summary>
    /// This class implements <see cref="ISqlBasicCondition"/> and builds a basic condition 
    /// </summary>
    public class TSQLBasicCondition : TSQLStatement, ISqlBasicCondition
    {
        /// <summary>
        /// A <see cref="SqlComparisonOperators"/> enum value that represents the operator between the 
        /// left and right operands of a condition. Comparison operators are =, &lt;=, &gt;=, &lt;&gt;, and %
        /// </summary>
        public SqlComparisonOperators Operator { get; set; }

        /// <summary>
        /// <para>
        /// Returns a basic condition for use in a T-SQL WHERE clause of the following form of 
        /// </para>
        /// <para>
        /// [LeftOperand] [ComparisonOperator] [RightOperand]
        /// </para>
        /// </summary>
        public override string SQL()
        {
            var sql = new StringBuilder();

            var leftOperand = Children.FirstOrDefault(c => c is ISqlQueryExpression);
            var rightOperand = Children.LastOrDefault(c => c is ISqlQueryExpression);

            sql.Append(leftOperand.ToString().Trim());
            sql.Append($" {GetOperatorString(Operator)} ");
            sql.Append(rightOperand.ToString().Trim());

            return sql.ToString();
        }
    }
}

using KnightMoves.Hierarchical;
using Newtonsoft.Json;

namespace KnightMoves.SqlObjects.SqlCode.TSQL
{
    /// <summary>
    /// This class implements <see cref="ISqlBetweenCondition"/> and builds a BETWEEN clause 
    /// <code>
    /// [LeftOperand] BETWEEN [StartVal] AND [EndVal]
    /// </code>
    /// </summary>
    public class TSQLBetweenCondition : TSQLStatement, ISqlBetweenCondition
    {
        /// <summary>
        /// A <see cref="ISqlQueryExpression"/> object that represents the beginning value of a BETWEEN SQL clause.
        /// </summary>
        [JsonConverter(typeof(TreeNodeJsonConverter))]
        public ISqlQueryExpression StartVal { get; set; }

        /// <summary>
        /// A <see cref="ISqlQueryExpression"/> object that represents the end value of a BETWEEN SQL clause.
        /// </summary>
        [JsonConverter(typeof(TreeNodeJsonConverter))]
        public ISqlQueryExpression EndVal { get; set; }

        /// <summary>
        /// Returns a BETWEEN clause using the LeftOperand property as the left operand, 
        /// the StartVal property as the left range value (inclusive) and the EndVal property 
        /// as the right range value (inclusive).
        /// </summary>
        public override string SQL()
        {
            return $"{LeftOperand} BETWEEN {StartVal} AND {EndVal}";
        }
    }
}

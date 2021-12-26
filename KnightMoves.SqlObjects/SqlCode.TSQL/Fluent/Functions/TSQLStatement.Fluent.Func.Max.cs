using System.Collections.Generic;
using System.Data;

namespace KnightMoves.SqlObjects.SqlCode.TSQL
{
    public abstract partial class TSQLStatement : SqlStatement
    {
        /// <summary>
        /// Outputs Max(aggregateExpression) using a literal <see cref="string"/> for the <paramref name="aggregateExpression"/>
        /// </summary>
        public override SqlStatement MAX(string aggregateExpression)
        {
            return MakeFunction<TSQLFuncMax>(new List<FunctionParamInfo>
            {
                new FunctionParamInfo { DataType = SqlDbType.Variant, ParamName = "aggregateExpression", ParamValue = aggregateExpression }
            });
        }

        /// <summary>
        /// Outputs Max(aggregateExpression) using a column for the <paramref name="aggregateExpression"/>
        /// </summary>
        public override SqlStatement MAX(string multipartIdentifier, string column)
        {
            return MakeFunction<TSQLFuncMax>(new List<FunctionParamInfo>
            {
                new FunctionParamInfo { DataType = SqlDbType.Variant, ParamName = "aggregateExpression", MultipartIdentifier = multipartIdentifier, ParamValue = column }
            });
        }
    }
}

using System.Collections.Generic;
using System.Data;

namespace KnightMoves.SqlObjects.SqlCode.TSQL
{
    public abstract partial class TSQLStatement : SqlStatement
    {
        /// <summary>
        /// Outputs CEILING(numericExpression) using a <see cref="decimal"/> for the <paramref name="numericExpression"/>
        /// </summary>
        public override SqlStatement CEILING(decimal numericExpression)
        {
            return MakeFunction<TSQLFuncCeiling>(new List<FunctionParamInfo>
            {
                new FunctionParamInfo { DataType = SqlDbType.Decimal, ParamName = "numericExpression", ParamValue = numericExpression.ToString() }
            });
        }

        /// <summary>
        /// Outputs CEILING(numericExpression) using a literal <see cref="string"/> for the <paramref name="numericExpression"/>
        /// </summary>
        public override SqlStatement CEILING(string numericExpression)
        {
            return MakeFunction<TSQLFuncCeiling>(new List<FunctionParamInfo>
            {
                new FunctionParamInfo { DataType = SqlDbType.Variant, ParamName = "numericExpression", ParamValue = numericExpression.ToString() }
            });
        }

        /// <summary>
        /// Outputs CEILING(numericExpression) using a column for the numericExpression
        /// </summary>
        public override SqlStatement CEILING(string multipartIdentifier, string column)
        {
            return MakeFunction<TSQLFuncCeiling>(new List<FunctionParamInfo>
            {
                new FunctionParamInfo { DataType = SqlDbType.Variant, ParamName = "numericExpression", MultipartIdentifier = multipartIdentifier, ParamValue = column }
            });
        }
    }
}

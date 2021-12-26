using System;
using System.Collections.Generic;
using System.Data;

namespace KnightMoves.SqlObjects.SqlCode.TSQL
{
    public abstract partial class TSQLStatement : SqlStatement
    {
        /// <summary>
        /// Outputs Month(dateExpression) using a <see cref="DateTime"/> for the <paramref name="dateExpression"/>
        /// </summary>
        public override SqlStatement MONTH(DateTime dateExpression)
        {
            return MakeFunction<TSQLFuncMonth>(new List<FunctionParamInfo>
            {
                new FunctionParamInfo { DataType = SqlDbType.DateTime, ParamName = "dateExpression", ParamValue = dateExpression.ToString(SqlDataType.SQL_DATE_STRING_FORMAT) }
            });
        }

        /// <summary>
        /// Outputs Month(dateExpression) using a literal <see cref="string"/> for the <paramref name="dateExpression"/>
        /// </summary>
        public override SqlStatement MONTH(string dateExpression)
        {
            return MakeFunction<TSQLFuncMonth>(new List<FunctionParamInfo>
            {
                new FunctionParamInfo { DataType = SqlDbType.Variant, ParamName = "dateExpression", ParamValue = dateExpression }
            });
        }

        /// <summary>
        /// Outputs Month(dateExpression) using a column for the <paramref name="dateExpression"/>
        /// </summary>
        public override SqlStatement MONTH(string multipartIdentifier, string column)
        {
            return MakeFunction<TSQLFuncMonth>(new List<FunctionParamInfo>
            {
                new FunctionParamInfo { DataType = SqlDbType.Variant, ParamName = "dateExpression", MultipartIdentifier = multipartIdentifier, ParamValue = column }
            });
        }
    }
}

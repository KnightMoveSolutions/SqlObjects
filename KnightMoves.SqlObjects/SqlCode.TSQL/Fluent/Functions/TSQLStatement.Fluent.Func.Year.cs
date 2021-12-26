using System;
using System.Collections.Generic;
using System.Data;

namespace KnightMoves.SqlObjects.SqlCode.TSQL
{
    public abstract partial class TSQLStatement : SqlStatement
    {
        /// <summary>
        /// Outputs Year(dateExpression) using a <see cref="DateTime"/> for the <paramref name="dateExpression"/>
        /// </summary>
        public override SqlStatement YEAR(DateTime dateExpression)
        {
            return MakeFunction<TSQLFuncYear>(new List<FunctionParamInfo>
            {
                new FunctionParamInfo { DataType = SqlDbType.DateTime, ParamName = "dateExpression", ParamValue = dateExpression.ToString(SqlDataType.SQL_DATE_STRING_FORMAT) }
            });
        }

        /// <summary>
        /// Outputs Year(dateExpression) using a literal <see cref="string"/> for the <paramref name="dateExpression"/>
        /// </summary>
        public override SqlStatement YEAR(string dateExpression)
        {
            return MakeFunction<TSQLFuncYear>(new List<FunctionParamInfo>
            {
                new FunctionParamInfo { DataType = SqlDbType.Variant, ParamName = "dateExpression", ParamValue = dateExpression }
            });
        }

        /// <summary>
        /// Outputs Year(dateExpression) using a column for the <paramref name="dateExpression"/>
        /// </summary>
        public override SqlStatement YEAR(string multipartIdentifier, string column)
        {
            return MakeFunction<TSQLFuncYear>(new List<FunctionParamInfo>
            {
                new FunctionParamInfo { DataType = SqlDbType.Variant, ParamName = "dateExpression", MultipartIdentifier = multipartIdentifier, ParamValue = column }
            });
        }
    }
}

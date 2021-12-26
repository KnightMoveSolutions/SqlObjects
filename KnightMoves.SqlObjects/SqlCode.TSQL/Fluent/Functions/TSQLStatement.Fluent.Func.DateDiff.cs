using System;
using System.Collections.Generic;
using System.Data;

namespace KnightMoves.SqlObjects.SqlCode.TSQL
{
    public abstract partial class TSQLStatement : SqlStatement
    {
        /// <summary>
        /// Outputs DateDiff(datePart,startDate,endDate) using a <see cref="DateTime"/> for both the <paramref name="startDate"/> and <paramref name="endDate"/>
        /// </summary>
        public override SqlStatement DATEDIFF(DateParts datePart, DateTime startDate, DateTime endDate)
        {
            return MakeFunction<TSQLFuncDateDiff>(new List<FunctionParamInfo>
            {
                new FunctionParamInfo { DataType = SqlDbType.Variant, ParamName = "datePart", ParamValue = datePart.ToString() },
                new FunctionParamInfo { DataType = SqlDbType.DateTime, ParamName = "startDate", ParamValue = startDate.ToString(SqlDataType.SQL_DATE_STRING_FORMAT) },
                new FunctionParamInfo { DataType = SqlDbType.DateTime, ParamName = "endDate", ParamValue = endDate.ToString(SqlDataType.SQL_DATE_STRING_FORMAT)}
            });
        }

        /// <summary>
        /// Outputs DateDiff(datePart,startDate,endDate) using a <see cref="string"/> for the <paramref name="startDate"/> 
        /// and a <see cref="DateTime"/> for the <paramref name="endDate"/>
        /// </summary>
        public override SqlStatement DATEDIFF(DateParts datePart, string startDateExpression, DateTime endDate)
        {
            return MakeFunction<TSQLFuncDateDiff>(new List<FunctionParamInfo>
            {
                new FunctionParamInfo { DataType = SqlDbType.Variant, ParamName = "datePart", ParamValue = datePart.ToString() },
                new FunctionParamInfo { DataType = SqlDbType.Variant, ParamName = "startDate", ParamValue = startDateExpression  },
                new FunctionParamInfo { DataType = SqlDbType.DateTime, ParamName = "endDate", ParamValue = endDate.ToString(SqlDataType.SQL_DATE_STRING_FORMAT)}
            });
        }

        /// <summary>
        /// Outputs DateDiff(datePart,startDate,endDate) using a <see cref="DateTime"/> for the <paramref name="startDate"/> 
        /// and a <see cref="string"/> for the <paramref name="endDate"/>
        /// </summary>
        public override SqlStatement DATEDIFF(DateParts datePart, DateTime startDate, string endDateExpression)
        {
            return MakeFunction<TSQLFuncDateDiff>(new List<FunctionParamInfo>
            {
                new FunctionParamInfo { DataType = SqlDbType.Variant, ParamName = "datePart", ParamValue = datePart.ToString() },
                new FunctionParamInfo { DataType = SqlDbType.DateTime, ParamName = "startDate", ParamValue = startDate.ToString(SqlDataType.SQL_DATE_STRING_FORMAT) },
                new FunctionParamInfo { DataType = SqlDbType.Variant, ParamName = "endDate", ParamValue = endDateExpression}
            });
        }

        /// <summary>
        /// Outputs DateDiff(datePart,startDate,endDate) using a <see cref="string"/> for the <paramref name="startDate"/> 
        /// and a <see cref="string"/> for the <paramref name="endDate"/>
        /// </summary>
        public override SqlStatement DATEDIFF(DateParts datePart, string startDateExpression, string endDateExpression)
        {
            return MakeFunction<TSQLFuncDateDiff>(new List<FunctionParamInfo>
            {
                new FunctionParamInfo { DataType = SqlDbType.Variant, ParamName = "datePart", ParamValue = datePart.ToString() },
                new FunctionParamInfo { DataType = SqlDbType.Variant, ParamName = "startDate", ParamValue = startDateExpression },
                new FunctionParamInfo { DataType = SqlDbType.Variant, ParamName = "endDate", ParamValue = endDateExpression }
            });
        }

        /// <summary>
        /// Outputs DateDiff(datePart,startDate,endDate) using a column for the <paramref name="startDate"/> 
        /// and a <see cref="DateTime"/> for the <paramref name="endDate"/>
        /// </summary>
        public override SqlStatement DATEDIFF(DateParts datePart, string multipartIdentifier, string startDateColumn, DateTime endDate)
        {
            return MakeFunction<TSQLFuncDateDiff>(new List<FunctionParamInfo>
            {
                new FunctionParamInfo { DataType = SqlDbType.Variant, ParamName = "datePart", ParamValue = datePart.ToString() },
                new FunctionParamInfo { DataType = SqlDbType.Variant, ParamName = "startDate", MultipartIdentifier = multipartIdentifier, ParamValue = startDateColumn },
                new FunctionParamInfo { DataType = SqlDbType.DateTime, ParamName = "endDate", ParamValue = endDate.ToString(SqlDataType.SQL_DATE_STRING_FORMAT)}
            });
        }

        /// <summary>
        /// Outputs DateDiff(datePart,startDate,endDate) using a column for the <paramref name="startDate"/> 
        /// and a <see cref="string"/> for the <paramref name="endDate"/>
        /// </summary>
        public override SqlStatement DATEDIFF(DateParts datePart, string multipartIdentifier, string startDateColumn, string endDateExpression)
        {
            return MakeFunction<TSQLFuncDateDiff>(new List<FunctionParamInfo>
            {
                new FunctionParamInfo { DataType = SqlDbType.Variant, ParamName = "datePart", ParamValue = datePart.ToString() },
                new FunctionParamInfo { DataType = SqlDbType.Variant, ParamName = "startDate", MultipartIdentifier = multipartIdentifier, ParamValue = startDateColumn },
                new FunctionParamInfo { DataType = SqlDbType.Variant, ParamName = "endDate", ParamValue = endDateExpression }
            });
        }

        /// <summary>
        /// Outputs DateDiff(datePart,startDate,endDate) using a <see cref="DateTime"/> for the <paramref name="startDate"/> 
        /// and a column for the <paramref name="endDate"/>
        /// </summary>
        public override SqlStatement DATEDIFF(DateParts datePart, DateTime startDate, string multipartIdentifier, string endDateColumn)
        {
            return MakeFunction<TSQLFuncDateDiff>(new List<FunctionParamInfo>
            {
                new FunctionParamInfo { DataType = SqlDbType.Variant, ParamName = "datePart", ParamValue = datePart.ToString() },
                new FunctionParamInfo { DataType = SqlDbType.DateTime, ParamName = "startDate", ParamValue = startDate.ToString(SqlDataType.SQL_DATE_STRING_FORMAT) },
                new FunctionParamInfo { DataType = SqlDbType.Variant, ParamName = "endDate", MultipartIdentifier = multipartIdentifier, ParamValue = endDateColumn }
            });
        }

        /// <summary>
        /// Outputs DateDiff(datePart,startDate,endDate) using a column for the <paramref name="startDate"/> 
        /// and a column for the <paramref name="endDate"/>
        /// </summary>
        public override SqlStatement DATEDIFF(DateParts datePart, string multipartIdentifierStart, string startDateColumn, string multipartIdentifierEnd, string endDateColumn)
        {
            return MakeFunction<TSQLFuncDateDiff>(new List<FunctionParamInfo>
            {
                new FunctionParamInfo { DataType = SqlDbType.Variant, ParamName = "datePart", ParamValue = datePart.ToString() },
                new FunctionParamInfo { DataType = SqlDbType.Variant, ParamName = "startDate", MultipartIdentifier = multipartIdentifierStart, ParamValue = startDateColumn },
                new FunctionParamInfo { DataType = SqlDbType.Variant, ParamName = "endDate", MultipartIdentifier = multipartIdentifierEnd, ParamValue = endDateColumn }
            });
        }

    }
}

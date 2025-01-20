using System;
using System.Collections.Generic;
using System.Data;

namespace KnightMoves.SqlObjects.SqlCode.TSQL;

public abstract partial class TSQLStatement : SqlStatement
{
    /// <summary>
    /// Outputs DateAdd(datePart,increment,dateExpression) using <see cref="DateTime"/> for the <paramref name="dateExpression"/>
    /// </summary>
    public override SqlStatement DATEADD(DateParts datePart, int increment, DateTime dateExpression)
    {
        return MakeFunction<TSQLFuncDateAdd>(new List<FunctionParamInfo>
        {
            new FunctionParamInfo { DataType = SqlDbType.Variant, ParamName = "datePart", ParamValue = datePart.ToString() },
            new FunctionParamInfo { DataType = SqlDbType.Int, ParamName = "increment", ParamValue = increment.ToString() },
            new FunctionParamInfo { DataType = SqlDbType.DateTime, ParamName = "dateExpression", ParamValue = dateExpression.ToString(SqlDataType.SQL_DATE_STRING_FORMAT) }
        });
    }

    /// <summary>
    /// Outputs DateAdd(datePart,increment,dateExpression) using a literal <see cref="string"/> for the <paramref name="dateExpression"/>
    /// </summary>
    public override SqlStatement DATEADD(DateParts datePart, int increment, string dateExpression)
    {
        return MakeFunction<TSQLFuncDateAdd>(new List<FunctionParamInfo>
        {
            new FunctionParamInfo { DataType = SqlDbType.Variant, ParamName = "datePart", ParamValue = datePart.ToString() },
            new FunctionParamInfo { DataType = SqlDbType.Int, ParamName = "increment", ParamValue = increment.ToString() },
            new FunctionParamInfo { DataType = SqlDbType.Variant, ParamName = "dateExpression", ParamValue = dateExpression }
        });
    }

    /// <summary>
    /// Outputs DateAdd(datePart,increment,dateExpression) using a column for the <paramref name="dateExpression"/>
    /// </summary>
    public override SqlStatement DATEADD(DateParts datePart, int increment, string multipartIdentifier, string column)
    {
        return MakeFunction<TSQLFuncDateAdd>(new List<FunctionParamInfo>
        {
            new FunctionParamInfo { DataType = SqlDbType.Variant, ParamName = "datePart", ParamValue = datePart.ToString() },
            new FunctionParamInfo { DataType = SqlDbType.Int, ParamName = "increment", ParamValue = increment.ToString() },
            new FunctionParamInfo { DataType = SqlDbType.Variant, ParamName = "dateExpression", MultipartIdentifier = multipartIdentifier, ParamValue = column }
        });
    }
}

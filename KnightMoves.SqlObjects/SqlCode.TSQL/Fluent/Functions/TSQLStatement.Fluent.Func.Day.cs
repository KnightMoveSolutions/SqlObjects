using System;
using System.Collections.Generic;
using System.Data;

namespace KnightMoves.SqlObjects.SqlCode.TSQL;

public abstract partial class TSQLStatement : SqlStatement
{
    /// <summary>
    /// Outputs Day(dateExpression) using a <see cref="DateTime"/> for the <paramref name="dateExpression"/>
    /// </summary>
    public override SqlStatement DAY(DateTime dateExpression)
    {
        return MakeFunction<TSQLFuncDay>(new List<FunctionParamInfo>
        {
            new FunctionParamInfo { DataType = SqlDbType.DateTime, ParamName = "dateExpression", ParamValue = dateExpression.ToString(SqlDataType.SQL_DATE_STRING_FORMAT) }
        });
    }

    /// <summary>
    /// Outputs Day(dateExpression) using a literal <see cref="string"/> for the <paramref name="dateExpression"/>
    /// </summary>
    public override SqlStatement DAY(string dateExpression)
    {
        return MakeFunction<TSQLFuncDay>(new List<FunctionParamInfo>
        {
            new FunctionParamInfo { DataType = SqlDbType.Variant, ParamName = "dateExpression", ParamValue = dateExpression }
        });
    }

    /// <summary>
    /// Outputs Day(dateExpression) using a column for the <paramref name="dateExpression"/>
    /// </summary>
    public override SqlStatement DAY(string multipartIdentifier, string column)
    {
        return MakeFunction<TSQLFuncDay>(new List<FunctionParamInfo>
        {
            new FunctionParamInfo { DataType = SqlDbType.Variant, ParamName = "dateExpression", MultipartIdentifier = multipartIdentifier, ParamValue = column }
        });
    }
}

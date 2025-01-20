using System.Collections.Generic;
using System.Data;

namespace KnightMoves.SqlObjects.SqlCode.TSQL;

public abstract partial class TSQLStatement : SqlStatement
{
    /// <summary>
    /// Outputs Min(aggregateExpression) using a literal <see cref="string"/> for the <paramref name="aggregateExpression"/>
    /// </summary>
    public override SqlStatement MIN(string aggregateExpression)
    {
        return MakeFunction<TSQLFuncMin>(new List<FunctionParamInfo>
        {
            new FunctionParamInfo { DataType = SqlDbType.Variant, ParamName = "aggregateExpression", ParamValue = aggregateExpression }
        });
    }

    /// <summary>
    /// Outputs Min(aggregateExpression) using a column for the <paramref name="aggregateExpression"/>
    /// </summary>
    public override SqlStatement MIN(string multipartIdentifier, string column)
    {
        return MakeFunction<TSQLFuncMin>(new List<FunctionParamInfo>
        {
            new FunctionParamInfo { DataType = SqlDbType.Variant, ParamName = "aggregateExpression", MultipartIdentifier = multipartIdentifier, ParamValue = column }
        });
    }
}

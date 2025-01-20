using System.Collections.Generic;
using System.Data;

namespace KnightMoves.SqlObjects.SqlCode.TSQL;

public abstract partial class TSQLStatement : SqlStatement
{
    /// <summary>
    /// Outputs COUNT(aggregateExpression) using a literal <see cref="string"/> for the <paramref name="aggregateExpression"/>
    /// </summary>

    public override SqlStatement COUNT(string aggregateExpression)
    {
        return MakeFunction<TSQLFuncCount>(new List<FunctionParamInfo>
        {
            new FunctionParamInfo { DataType = SqlDbType.Variant, ParamName = "aggregateExpression", ParamValue = aggregateExpression.ToString() }
        });
    }

    /// <summary>
    /// Outputs COUNT(aggregateExpression) using a column for the aggregateExpression
    /// </summary>
    public override SqlStatement COUNT(string multipartIdentifier, string column)
    {
        return MakeFunction<TSQLFuncCount>(new List<FunctionParamInfo>
        {
            new FunctionParamInfo { DataType = SqlDbType.Variant, ParamName = "aggregateExpression", MultipartIdentifier = multipartIdentifier, ParamValue = column }
        });
    }
}

using System.Collections.Generic;
using System.Data;

namespace KnightMoves.SqlObjects.SqlCode.TSQL;

public abstract partial class TSQLStatement : SqlStatement
{
    /// <summary>
    /// Outputs Sum(aggregateExpression) using a literal <see cref="string"/> for the <paramref name="aggregateExpression"/>
    /// </summary>
    public override SqlStatement SUM(string aggregateExpression)
    {
        return MakeFunction<TSQLFuncSum>(new List<FunctionParamInfo>
        {
            new FunctionParamInfo { DataType = SqlDbType.Variant, ParamName = "aggregateExpression", ParamValue = aggregateExpression }
        });
    }

    /// <summary>
    /// Outputs Sum(aggregateExpression) using a column for the <paramref name="aggregateExpression"/>
    /// </summary>
    public override SqlStatement SUM(string multipartIdentifier, string column)
    {
        return MakeFunction<TSQLFuncSum>(new List<FunctionParamInfo>
        {
            new FunctionParamInfo { DataType = SqlDbType.Variant, ParamName = "aggregateExpression", MultipartIdentifier = multipartIdentifier, ParamValue = column }
        });
    }
}

using System.Collections.Generic;
using System.Data;

namespace KnightMoves.SqlObjects.SqlCode.TSQL;

public abstract partial class TSQLStatement : SqlStatement
{
    /// <summary>
    /// Outputs AVG(aggregateExpression) using a <see cref="decimal"/> for the <paramref name="aggregateExpression"/>
    /// </summary>
    public override SqlStatement AVG(decimal aggregateExpression)
    {
        return MakeFunction<TSQLFuncAvg>(new List<FunctionParamInfo>
        {
            new FunctionParamInfo{ DataType = SqlDbType.Decimal, ParamName = "aggregateExpression", ParamValue = aggregateExpression.ToString() }
        });
    }

    /// <summary>
    /// Outputs AVG(aggregateExpression) using a literal <see cref="string"/> for the <paramref name="aggregateExpression"/>
    /// </summary>
    public override SqlStatement AVG(string aggregateExpression)
    {
        return MakeFunction<TSQLFuncAvg>(new List<FunctionParamInfo>
        {
            new FunctionParamInfo { DataType = SqlDbType.Variant, ParamName = "aggregateExpression", ParamValue = aggregateExpression.ToString() }
        });
    }

    /// <summary>
    /// Outputs AVG(numericExpression) using a column for the numericExpression
    /// </summary>
    public override SqlStatement AVG(string multipartIdentifier, string column)
    {
        return MakeFunction<TSQLFuncAvg>(new List<FunctionParamInfo>
        {
            new FunctionParamInfo { DataType = SqlDbType.Variant, ParamName = "aggregateExpression", MultipartIdentifier = multipartIdentifier, ParamValue = column }
        });
    }
}

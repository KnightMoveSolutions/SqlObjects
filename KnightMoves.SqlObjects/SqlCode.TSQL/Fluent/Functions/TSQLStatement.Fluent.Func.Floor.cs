using System.Collections.Generic;
using System.Data;

namespace KnightMoves.SqlObjects.SqlCode.TSQL;

public abstract partial class TSQLStatement : SqlStatement
{
    /// <summary>
    /// Outputs Floor(numericExpression) using a <see cref="decimal"/> for the <paramref name="numericExpression"/>
    /// </summary>
    public override SqlStatement FLOOR(decimal numericExpression)
    {
        return MakeFunction<TSQLFuncFloor>(new List<FunctionParamInfo>
        {
            new FunctionParamInfo { DataType = SqlDbType.Decimal, ParamName = "numericExpression", ParamValue = numericExpression.ToString() }
        });
    }

    /// <summary>
    /// Outputs Floor(numericExpression) using a literal <see cref="string"/> for the <paramref name="numericExpression"/>
    /// </summary>
    public override SqlStatement FLOOR(string numericExpression)
    {
        return MakeFunction<TSQLFuncFloor>(new List<FunctionParamInfo>
        {
            new FunctionParamInfo { DataType = SqlDbType.Variant, ParamName = "numericExpression", ParamValue = numericExpression }
        });
    }

    /// <summary>
    /// Outputs Floor(numericExpression) using a column for the <paramref name="numericExpression"/>
    /// </summary>
    public override SqlStatement FLOOR(string multipartIdentifier, string column)
    {
        return MakeFunction<TSQLFuncFloor>(new List<FunctionParamInfo>
        {
            new FunctionParamInfo { DataType = SqlDbType.Variant, ParamName = "numericExpression", MultipartIdentifier = multipartIdentifier, ParamValue = column }
        });
    }
}

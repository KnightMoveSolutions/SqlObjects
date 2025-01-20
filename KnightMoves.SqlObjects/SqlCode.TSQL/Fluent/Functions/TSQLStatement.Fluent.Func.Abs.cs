using System.Collections.Generic;
using System.Data;

namespace KnightMoves.SqlObjects.SqlCode.TSQL;

public abstract partial class TSQLStatement : SqlStatement
{
    /// <summary>
    /// Outputs ABS(numericExpression) using a <see cref="decimal"/> for the <paramref name="numericExpression"/>
    /// </summary>
    public override SqlStatement ABS(decimal numericExpression)
    {
        return MakeFunction<TSQLFuncAbs>(new List<FunctionParamInfo>
        {
            new FunctionParamInfo { DataType = SqlDbType.Decimal, ParamName = "numericExpression", ParamValue = numericExpression.ToString() }
        });
    }

    /// <summary>
    /// Outputs ABS(numericExpression) using a literal <see cref="string"/> for the <paramref name="numericExpression"/>
    /// </summary>
    public override SqlStatement ABS(string numericExpression)
    {
        return MakeFunction<TSQLFuncAbs>(new List<FunctionParamInfo>
        {
            new FunctionParamInfo { DataType = SqlDbType.Variant, ParamName = "numericExpression", ParamValue = numericExpression.ToString() }
        });
    }

    /// <summary>
    /// Outputs ABS(numericExpression) using a column for the numericExpression
    /// </summary>
    public override SqlStatement ABS(string multipartIdentifier, string column)
    {
        return MakeFunction<TSQLFuncAbs>(new List<FunctionParamInfo>
        {
            new FunctionParamInfo { DataType = SqlDbType.Variant, ParamName = "numericExpression", MultipartIdentifier = multipartIdentifier, ParamValue = column }
        });
    }
}

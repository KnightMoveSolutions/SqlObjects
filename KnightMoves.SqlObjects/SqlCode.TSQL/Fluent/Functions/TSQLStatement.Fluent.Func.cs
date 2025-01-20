using System.Collections.Generic;
using System.Data;

namespace KnightMoves.SqlObjects.SqlCode.TSQL;

public abstract partial class TSQLStatement : SqlStatement
{
    private class FunctionParamInfo
    {
        public SqlDbType? DataType { get; set; }
        public string ParamName { get; set; }
        public string MultipartIdentifier { get; set; }
        public string ParamValue { get; set; }
    }

    private SqlStatement MakeFunction<T>(List<FunctionParamInfo> paramInfo) where T : ISqlFunction, new()
    {
        var func = new T();

        paramInfo.ForEach(p =>
        {
            if (p.DataType.HasValue && p.DataType.Value != SqlDbType.Variant)
            {
                func.SetParameterValue(p.ParamName, new TSQLLiteral
                {
                    DataType = new TSQLDataType(p.DataType.Value),
                    Value = p.ParamValue
                });
            }
            else if (string.IsNullOrEmpty(p.MultipartIdentifier))
            {
                func.SetParameterValue(p.ParamName, new TSQLLiteral
                {
                    DataType = new TSQLDataType(SqlDbType.Variant),
                    Value = p.ParamValue
                });
            }
            else
            {
                func.SetParameterValue(p.ParamName, new TSQLColumn
                {
                    DataType = new TSQLDataType(SqlDbType.Variant),
                    MultiPartIdentifier = p.MultipartIdentifier,
                    ColumnName = p.ParamValue
                });
            }
        });

        var parent = GetExpressionParent();

        parent.Children.Add(func as SqlStatement);

        return func as SqlStatement;
    }
}

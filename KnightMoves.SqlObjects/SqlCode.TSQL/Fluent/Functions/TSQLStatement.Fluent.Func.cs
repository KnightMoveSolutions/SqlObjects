/*
    THE LICENSED WORK IS PROVIDED UNDER THE TERMS OF THE DEVELOPER TOOL LIMITED 
    LICENSE (“LICENSE”) AS FIRST COMPLETED BY THE ORIGINAL AUTHOR. ANY USE, PUBLIC 
    DISPLAY, PUBLIC PERFORMANCE, REPRODUCTION OR DISTRIBUTION OF, OR PREPARATION OF 
    DERIVATIVE WORKS BASED ON THE LICENSED WORK CONSTITUTES RECIPIENT’S ACCEPTANCE 
    OF THIS LICENSE AND ITS TERMS, WHETHER OR NOT SUCH RECIPIENT READS THE TERMS OF THE 
    LICENSE. “LICENSED WORK” AND “RECIPIENT” ARE DEFINED IN THE LICENSE. A COPY OF THE 
    LICENSE IS LOCATED IN THE TEXT FILE ENTITLED “LICENSE.TXT” ACCOMPANYING THE CONTENTS 
    OF THIS FILE. IF A COPY OF THE LICENSE DOES NOT ACCOMPANY THIS FILE, A COPY OF THE 
    LICENSE MAY ALSO BE OBTAINED AT THE FOLLOWING WEB SITE:  
 
    https://docs.knightmovesolutions.com/sql-objects/license.html
*/

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

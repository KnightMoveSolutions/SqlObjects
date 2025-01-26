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

using System;
using System.Collections.Generic;
using System.Data;

namespace KnightMoves.SqlObjects.SqlCode.TSQL;

public abstract partial class TSQLStatement : SqlStatement
{
    /// <summary>
    /// Outputs Month(dateExpression) using a <see cref="DateTime"/> for the <paramref name="dateExpression"/>
    /// </summary>
    public override SqlStatement MONTH(DateTime dateExpression)
    {
        return MakeFunction<TSQLFuncMonth>(new List<FunctionParamInfo>
        {
            new FunctionParamInfo { DataType = SqlDbType.DateTime, ParamName = "dateExpression", ParamValue = dateExpression.ToString(SqlDataType.SQL_DATE_STRING_FORMAT) }
        });
    }

    /// <summary>
    /// Outputs Month(dateExpression) using a literal <see cref="string"/> for the <paramref name="dateExpression"/>
    /// </summary>
    public override SqlStatement MONTH(string dateExpression)
    {
        return MakeFunction<TSQLFuncMonth>(new List<FunctionParamInfo>
        {
            new FunctionParamInfo { DataType = SqlDbType.Variant, ParamName = "dateExpression", ParamValue = dateExpression }
        });
    }

    /// <summary>
    /// Outputs Month(dateExpression) using a column for the <paramref name="dateExpression"/>
    /// </summary>
    public override SqlStatement MONTH(string multipartIdentifier, string column)
    {
        return MakeFunction<TSQLFuncMonth>(new List<FunctionParamInfo>
        {
            new FunctionParamInfo { DataType = SqlDbType.Variant, ParamName = "dateExpression", MultipartIdentifier = multipartIdentifier, ParamValue = column }
        });
    }
}

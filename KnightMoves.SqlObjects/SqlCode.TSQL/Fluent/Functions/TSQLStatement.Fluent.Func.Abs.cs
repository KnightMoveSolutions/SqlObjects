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

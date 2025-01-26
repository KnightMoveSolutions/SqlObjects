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
using KnightMoves.Hierarchical;
using Newtonsoft.Json;

namespace KnightMoves.SqlObjects.SqlCode.TSQL;

/// <summary>
/// This class implements <see cref="ITSQLCaseExpression"/> and produces the ELSE fragment of a T-SQL CASE statement
/// </summary>
public class TSQLCaseElse : TSQLStatement, ITSQLCaseExpression
{
    /// <summary>
    /// The alias of the SQL expression so it can be referenced easily in other SQL fragments.
    /// </summary>
    public string Alias { get; set; }
    
    /// <summary>
    /// The data type of the value returned by the ELSE clause
    /// </summary>
    public SqlDataType DataType { get; set; }

    /// <summary>
    /// An <see cref="ISqlQueryExpression"/> object representing the result of the ELSE clause. 
    /// The data type of this <see cref="ISqlQueryExpression"/> object must match the 
    /// <see cref="DataType"/> property.
    /// </summary>
    [JsonConverter(typeof(TreeNodeJsonConverter))]
    public ISqlQueryExpression Result { get; set; }

    /// <summary>
    /// Returns the ELSE clause for a T-SQL CASE statement. 
    /// </summary>
    public override string SQL()
    {
        if (Result.DataType != ((TSQLCase)Parent).DataType)
            throw new InvalidOperationException("The DataType of the CASE ELSE clause is invalid.  It must match the DataType of its enclosing CASE statement. SQL Fragment Name='" + Name + "', SQL Fragment ID='" + Id + "'");

        var sql = $"{IndentString}ELSE {Result}";

        if (!string.IsNullOrEmpty(Alias))
            sql += $" AS [{Alias.SanitizeDelimitedValue()}]";

        return sql;
    }
}

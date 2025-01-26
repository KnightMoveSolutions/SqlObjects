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
using System.Text;

namespace KnightMoves.SqlObjects.SqlCode.TSQL;

/// <summary>
/// This class implements <see cref="ISqlFrom"/> and builds a FROM [Schema].[Table] clause
/// to be used in conjunction with a SELECT statement.
/// </summary>
public class TSQLFrom : TSQLStatement, ISqlFrom
{
    /// <summary>
    /// The schema of the Table
    /// </summary>
    public string Schema { get; set; }

    /// <summary>
    /// The name of the Table
    /// </summary>
    public string Table { get; set; }

    /// <summary>
    /// The table's Multipart Identifier alias value
    /// </summary>
    public string MultiPartIdentifier { get; set; }

    /// <summary>
    /// Builds a FROM clause in the following format where "t" is the MultiPartIdentifier
    /// <code>
    /// FROM [Schema].[Table] t
    /// </code>
    /// </summary>
    public override string SQL()
    {
        var sql = new StringBuilder();

        sql.Append($"{IndentString}FROM ");

        if(!string.IsNullOrEmpty(Table))
        {
            if (!string.IsNullOrEmpty(Schema))
                sql.Append($"[{Schema.SanitizeDelimitedValue()}].");

            sql.Append($"[{Table.SanitizeDelimitedValue()}]");
        }

        if(!string.IsNullOrEmpty(MultiPartIdentifier))
        {
            sql.Append($" {MultiPartIdentifier}");
        }

        sql.Append(Environment.NewLine);

        return sql.ToString();
    }
}

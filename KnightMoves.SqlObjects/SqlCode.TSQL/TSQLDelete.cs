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
/// This class implements <see cref="ISqlDelete"/> and builds a SQL DELETE FROM statement
/// </summary>
public class TSQLDelete : TSQLStatement, ISqlDelete
{
    /// <summary>
    /// The schema of the Table to delete from
    /// </summary>
    public string Schema { get; set; }

    /// <summary>
    /// The name of the Table to delete from
    /// </summary>
    public string Table { get; set; }

    public TSQLDelete() { }

    public TSQLDelete(string table) : this(string.Empty, table) { }

    public TSQLDelete(string schema, string table)
    {
        Schema = schema;
        Table = table;
    }

    /// <summary>
    /// Builds the SQL DELETE FROM statement using the Schema (if provided) and Table 
    /// properties to specify the table to delete from.
    /// </summary>
    public override string SQL()
    {
        var sql = new StringBuilder();

        sql.Append($"{IndentString}DELETE FROM ");

        if (!string.IsNullOrEmpty(Schema))
            sql.Append($"[{Schema.SanitizeDelimitedValue()}].");

        sql.Append($"[{Table.SanitizeDelimitedValue()}]{Environment.NewLine}");

        return sql.ToString();
    }
}

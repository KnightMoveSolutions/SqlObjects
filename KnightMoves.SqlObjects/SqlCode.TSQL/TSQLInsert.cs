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
using System.Linq;
using System.Text;

namespace KnightMoves.SqlObjects.SqlCode.TSQL;

/// <summary>
/// This class implements <see cref="ISqlInsert"/> and builds a SQL INSERT INTO statement
/// </summary>
public class TSQLInsert : TSQLStatement, ISqlInsert
{
    /// <summary>
    /// The Schema of the Table being affected
    /// </summary>
    public string Schema { get; set; }

    /// <summary>
    /// The Table name
    /// </summary>
    public string Table { get; set; }

    public TSQLInsert() { }

    public TSQLInsert(string table) : this(string.Empty, table) { }

    public TSQLInsert(string schema, string table) 
    {
        Schema = schema;
        Table = table;
    }

    /// <summary>
    /// Builds a SQL INSERT INTO statement using any <see cref="ISqlColumn"/> objects added to it 
    /// to build the list of columns. If an <see cref="ISqlSelect"/> was added to it then it will 
    /// use the SELECT statement in lieu of VALUES. If no SELECT is found it will build a VALUES 
    /// list from any non-columns it finds were added to it.
    /// <code>INSERT INTO [Schema].[Table] (</code>
    /// <code>-- Column list here</code>
    /// <code>) VALUES ( -- Or SELECT statement if it exists</code>
    /// <code>-- Value expression list here if no SELECT statement exists</code>
    /// <code>)</code>
    /// </summary>
    public override string SQL()
    {
        var sql = new StringBuilder();

        sql.Append($"{IndentString}INSERT INTO{Environment.NewLine}");

        sql.Append($"{IndentString}({Environment.NewLine}");

        foreach (var column in Children.Where(c => c.IsColumn))
            sql.Append($"{column},{Environment.NewLine}");

        sql.Remove(sql.Length - 3, 1);

        sql.Append($"{IndentString})");

        if (Siblings.Select(s => s as SqlStatement).Any(c => c.IsSelect))
        {
            sql.Append(Environment.NewLine);
            return sql.ToString();
        }

        sql.Append($" VALUES ({Environment.NewLine}");

        foreach (var sqlStatement in Children.Where(c => !c.IsColumn))
            sql.Append($"{sqlStatement},{Environment.NewLine}");

        sql.Remove(sql.Length - 3, 1);

        sql.Append($"{IndentString}){Environment.NewLine}");

        return sql.ToString();
    }
}

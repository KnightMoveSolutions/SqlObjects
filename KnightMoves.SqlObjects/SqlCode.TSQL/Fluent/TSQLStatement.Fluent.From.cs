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

namespace KnightMoves.SqlObjects.SqlCode.TSQL;

public abstract partial class TSQLStatement : SqlStatement
{
    /// <summary>
    /// Starts a SQL FROM statement
    /// </summary>
    public override SqlStatement FROM()
    {
        return FROM(string.Empty);
    }

    /// <summary>
    /// Outputs a SQL FROM [Table] statement using <paramref name="table"/> as the [Table] specification
    /// </summary>
    public override SqlStatement FROM(string table)
    {
        if (IsDelete)
        {
            (this as ISqlDelete).Table = table;
            return this;
        }

        var from = new TSQLFrom { Table = table };

        var parent = GetParentScope();

        parent.Children.Add(from);

        return from;
    }

    /// <summary>
    /// Outputs a SQL FROM [Schema].[Table] statement using <paramref name="schema"/> and <paramref name="table"/> 
    /// as the [Schema] and [Table] specifications respectively
    /// <code>
    /// Example: FROM [dbo].[MyTable]
    /// </code>
    /// </summary>
    public override SqlStatement FROM(string schema, string table)
    {
        if (IsDelete)
        {
            var deleteStmt = this as ISqlDelete;
            deleteStmt.Schema = schema;
            deleteStmt.Table = table;
            return this;
        }

        var from = new TSQLFrom { Schema = schema, Table = table };

        var parent = GetParentScope();

        parent.Children.Add(from);

        return from;
    }

    /// <summary>
    /// Outputs a SQL "FROM [Schema].[Table] i" statement using <paramref name="schema"/>, <paramref name="table"/>,
    /// and <paramref name="multipartIdentifier"/> as the [Schema], [Table], and i specifications respectively.
    /// <code>
    /// Example: FROM [dbo].[MyTable] t
    /// </code>
    /// </summary>
    public override SqlStatement FROM(string schema, string table, string multipartIdentifier)
    {
        var from = new TSQLFrom { Schema = schema, Table = table, MultiPartIdentifier = multipartIdentifier };

        var parent = GetParentScope();

        parent.Children.Add(from);

        return from;
    }
}

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

namespace KnightMoves.SqlObjects.SqlCode.TSQL;

public abstract partial class TSQLStatement : SqlStatement
{
    /// <summary>
    /// Starts a SQL UPDATE statement using the specified <paramref name="table"/> as the target
    /// </summary>
    public override SqlStatement UPDATE(string table)
    {
        return UPDATE(string.Empty, table);
    }

    /// <summary>
    /// Starts a SQL UPDATE statement using the specified [<paramref name="schema"/>].[<paramref name="table"/>] as the target
    /// </summary>
    public override SqlStatement UPDATE(string schema, string table)
    {
        return new TSQLUpdate(schema, table);
    }

    /// <summary>
    /// Signifies the SET keyword immediately after an UPDATE clause that marks the start of the update expression list
    /// </summary>
    public override SqlStatement SET()
    {
        if (IsUpdate == false)
            throw new InvalidOperationException(".SET() can only be called after .UPDATE(). To set variables use .Script()");

        return this;
    }
}

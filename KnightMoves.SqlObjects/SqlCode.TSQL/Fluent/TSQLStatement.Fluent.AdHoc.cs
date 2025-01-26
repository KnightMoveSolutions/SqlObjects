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

using System.Data;

namespace KnightMoves.SqlObjects.SqlCode.TSQL;

public abstract partial class TSQLStatement : SqlStatement
{
    /// <summary>
    /// Creates an object containing <paramref name="scriptCode"/>
    /// </summary>
    public override SqlStatement Script(string scriptCode)
    {
        var scope = GetParentScope();

        var script = new TSQLScript();

        script.Children.Add(new TSQLLiteral
        {
            DataType = new TSQLDataType(SqlDbType.Variant),
            Value = scriptCode
        });

        scope.Children.Add(script);

        return scope;
    }

    /// <summary>
    /// Adds <paramref name="literal"/> as-is to the preceding SQL object
    /// </summary>
    public override SqlStatement Literal(string literal)
    {
        Children.Add(new TSQLLiteral
        {
            DataType = new TSQLDataType(SqlDbType.Variant),
            Value = literal
        });

        return this;
    }

}

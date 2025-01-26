﻿/*
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
    /// Outputs a SQL LIKE clause using <paramref name="pattern"/> as the filtering specification
    /// </summary>
    public override SqlStatement LIKE(string pattern)
    {
        var parent = GetConditionParent();

        var like = new TSQLLikeCondition { Pattern = pattern };

        var leftOperand = GetLeftOperand();

        like.Children.Add(leftOperand as SqlStatement);

        parent.Children.Add(like);

        return parent;
    }
}

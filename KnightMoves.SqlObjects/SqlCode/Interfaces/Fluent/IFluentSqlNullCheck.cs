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

namespace KnightMoves.SqlObjects.SqlCode;

public interface IFluentSqlNullCheck
{
    SqlStatement IS_NULL();
    SqlStatement IS_NOT_NULL();

    SqlStatement ISNULL(string multipartIdentifier, string column, string replaceExpression);
    SqlStatement ISNULL(string multipartIdentifier, string column, int replaceExpression);
    SqlStatement ISNULL(string multipartIdentifier, string column, DateTime replaceExpression);
    SqlStatement ISNULL(string multipartIdentifier, string column, bool replaceExpression);
    SqlStatement ISNULL(string multipartIdentifier, string column, long replaceExpression);
    SqlStatement ISNULL(string multipartIdentifier, string column, Guid replaceExpression);
    SqlStatement ISNULL(string multipartIdentifier, string column, char replaceExpression);
    SqlStatement ISNULL(string multipartIdentifier, string column, decimal replaceExpression);

    SqlStatement ISNULL(string checkExpression, string replaceExpression);
    SqlStatement ISNULL(string checkExpression, int replaceExpression);
    SqlStatement ISNULL(string checkExpression, DateTime replaceExpression);
    SqlStatement ISNULL(string checkExpression, bool replaceExpression);
    SqlStatement ISNULL(string checkExpression, long replaceExpression);
    SqlStatement ISNULL(string checkExpression, Guid replaceExpression);
    SqlStatement ISNULL(string checkExpression, char replaceExpression);
    SqlStatement ISNULL(string checkExpression, decimal replaceExpression);

    SqlStatement ISNULL(ISqlQueryExpression checkExpression, string replaceExpression);
    SqlStatement ISNULL(ISqlQueryExpression checkExpression, int replaceExpression);
    SqlStatement ISNULL(ISqlQueryExpression checkExpression, DateTime replaceExpression);
    SqlStatement ISNULL(ISqlQueryExpression checkExpression, bool replaceExpression);
    SqlStatement ISNULL(ISqlQueryExpression checkExpression, long replaceExpression);
    SqlStatement ISNULL(ISqlQueryExpression checkExpression, Guid replaceExpression);
    SqlStatement ISNULL(ISqlQueryExpression checkExpression, char replaceExpression);
    SqlStatement ISNULL(ISqlQueryExpression checkExpression, decimal replaceExpression);

    SqlStatement ISNULL(ISqlQueryExpression checkExpression, ISqlQueryExpression replaceExpression);
}

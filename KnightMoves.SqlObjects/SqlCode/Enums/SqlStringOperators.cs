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

namespace KnightMoves.SqlObjects.SqlCode;

/// <summary>
/// Used to identify which wildcard characters to build into a SQL LIKE clause condition.
/// </summary>
public enum SqlStringOperators
{
    /// <summary>
    /// Indicates a % wildcard at the beginning of a string. Ex: Field1 LIKE '%Smith'
    /// </summary>
    StartsWith,

    /// <summary>
    /// Indicates a % wildcard at the end of a string. Ex: Field1 LIKE 'John%'
    /// </summary>
    EndsWith,

    /// <summary>
    /// Indicates a wildcard at the beginning and end of a string. Ex: Field1 LIKE '%ith%'
    /// </summary>
    Contains
}

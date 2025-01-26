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

using System.Collections.Generic;

namespace KnightMoves.SqlObjects.SqlCode.Security;

/// <summary>
/// Implementations of this interface do the job of validating a string by checking for 
/// the existence of SQL injection signatures. 
/// </summary>
public interface ISQLInjectionSignatureChecker
{
    /// <summary>
    /// A collection of messages describing which SQL injection signatures were detected in the string.
    /// If no SQL injection signatures were found in the string then this collection is empty.
    /// </summary>
    IList<string> SignaturesDetected { get; }

    /// <summary>
    /// Returns true if SQL injection is detected in the string with a collection of 
    /// strings representing the types of SQL injection signatures that were detected
    /// </summary>
    (bool, IEnumerable<string>) ContainsSqlInjection(string val);
}

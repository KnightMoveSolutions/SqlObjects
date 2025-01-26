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

namespace KnightMoves.SqlObjects.SqlCode.Security;

/// <summary>
/// Implementations of this interface should enforce some kind of SQL injection signature rule on the 
/// value passed to its methods.  
/// </summary>
public interface ISQLInjectionSignatureRule
{
    /// <summary>
    /// The error message that describes the SQL injection signature rule that was detected
    /// </summary>
    string ErrorMessage { get; }

    /// <summary>
    /// Returns true, if the SQL injection signature is not detected in the string. If the 
    /// SQL injection signature is found in the string it will return false.
    /// </summary>
    /// <param name="val">The string being examined for the existence of the SQL injection signature</param>
    /// <param name="sigsDetected">If the SQL injection signature is deteced in <paramref name="val"/> then the <see cref="ErrorMessage"/> will be added to this collection</param>
    /// <returns>True if the SQL injection signature is *not* found in <paramref name="val"/>, false it is *found* in <paramref name="val"/></returns>
    bool ContainsSqlInjection(string val);
}

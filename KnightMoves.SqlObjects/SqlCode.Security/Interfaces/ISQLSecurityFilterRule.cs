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
using System.Collections.Generic;

namespace KnightMoves.SqlObjects.SqlCode.Security;

/// <summary>
/// Implementations of this interface should enforce some kind of SQL security rule on the value passed 
/// to its methods based on the data type that is also passed to its methods.  
/// </summary>
public interface ISQLSecurityFilterRule
{
    /// <summary>
    /// This method checks the value passed to it for some security rule. 
    /// </summary>
    /// <typeparam name="T">The data type that will be used to determine how to sanitize <paramref name="val"/></typeparam>
    /// <param name="val">String value that will be used as part of building the dynamic SQL statement.</param>
    /// <returns>The <paramref name="val"/> as-is if there were no problems, or it will return a value that is guaranteed to be valid.</returns>
    string SanitizeInput<T>(string val);

    /// <summary>
    /// This method checks the value passed to it for some security rule. 
    /// </summary>
    /// <param name="T">The data type that will be used to determine how to sanitize <paramref name="val"/></param>
    /// <param name="val">String value that will be used as part of building the dynamic SQL statement.</param>
    /// <returns>The <paramref name="val"/> as-is if there were no problems, or it will return a value that is guaranteed to be valid.</returns>
    string SanitizeInput(Type T, string val);

    /// <summary>
    /// This method checks the value passed to it for some security rule. 
    /// </summary>
    /// <typeparam name="T">The data type that will be used to determine if <paramref name="val"/> is valid</typeparam>
    /// <param name="val">String value that will be used as part of building the dynamic SQL statement.</param>
    /// <param name="violations">Collection of strings that contain error messages describing how the <paramref name="val"/> violated a rule.</param>
    /// <returns></returns>
    (bool, IEnumerable<string>) CheckInput<T>(string val);

    /// <summary>
    /// This method checks the value passed to it for some security rule. 
    /// </summary>
    /// <param name="t">The data type that will be used to determine if <paramref name="val"/> is valid</param>
    /// <param name="val">String value that will be used as part of building the dynamic SQL statement.</param>
    /// <param name="violations">Collection of strings that contain error messages describing how the <paramref name="val"/> violated a rule.</param>
    /// <returns></returns>
    (bool, IEnumerable<string>) CheckInput(Type t, string val);
}

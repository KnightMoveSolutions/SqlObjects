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

using System;
using System.Collections.Generic;

namespace KnightMoves.SqlObjects.SqlCode.Security
{
    /// <summary>
    /// This interface defines the methods that concrete classes must implement in order to be used as SQL security filters. 
    /// The purpose of SQL security filters is to mitigate the risk of SQL injection.
    /// </summary>
    public interface ISQLSecurityFilter
    {
        /// <summary>
        /// This flag should cause the <see cref="SanitizeInput"/> and <see cref="CheckInput"/> methods to ignore security 
        /// checks.  This is to allow for situations when functions are set as values (e.g., GetDate()). Allowing SQL code in 
        /// values effectively turns off all security checks that the filter does. 
        /// Be very careful when setting this to true...you have been warned.
        /// </summary>
        bool DisableSecurityCheck { get; set; }

        /// <summary>
        /// Checks the value against SQL security rules that are based in part on the data type <typeparamref name="T"/> and 
        /// then return a value that is guaranteed to work. 
        /// </summary>
        /// <typeparam name="T">The data type that will be used to determine how to sanitize <paramref name="val"/></typeparam>
        /// <param name="val">String value that will be used as part of building the dynamic SQL statement.</param>
        /// <returns>The <paramref name="val"/> as-is if there were no problems, or it will return a value that is guaranteed to be valid.</returns>
        string SanitizeInput<T>(string val);

        /// <summary>
        /// Checks the value against SQL security rules that are based in part on the data type <paramref name="T"/> and then 
        /// return a value that is guaranteed to work. 
        /// </summary>
        /// <param name="T">The data type that will be used to determine how to sanitize <paramref name="val"/></param>
        /// <param name="val">String value that will be used as part of building the dynamic SQL statement.</param>
        /// <returns>The <paramref name="val"/> as-is if there were no problems, or it will return a value that is guaranteed to be valid.</returns>
        string SanitizeInput(Type T, string val);

        /// <summary>
        /// Checks the value against SQL security rules that are based in part on the data type "t" that is passed to it,
        /// however, its job is to return true if it satisfies *all* security rules and false if it fails *any one* of the 
        /// security rules. It will also fill the <see cref="Violations"/> collection with string values that describe the error 
        /// or security violation discovered in the inputted value. That way, if the method returns "false", the 
        /// <see cref="Violations"/> collection can be inspected for messages that describe why the value failed to satisfy 
        /// the security rules. 
        /// </summary>
        /// <typeparam name="T">The data type that will be used to determine how to sanitize <paramref name="val"/></typeparam>
        /// <param name="val">String value that will be used as part of building the dynamic SQL statement.</param>
        /// <returns>True if <paramref name="val"/> passes all security rules, false if it fails at least one.</returns>
        (bool, IEnumerable<string>) CheckInput<T>(string val);

        /// <summary>
        /// Checks the value against SQL security rules that are based in part on the data type "t" that is passed to it
        /// , however, its job is to return true if it satisfies *all* security rules and false if it fails *any one* of the 
        /// security rules. It will also fill the <see cref="Violations"/> collection with string values that describe the error 
        /// or security violation discovered in the inputted value. That way, if the method returns "false", the 
        /// <see cref="Violations"/> collection can be inspected for messages that describe why the value failed to satisfy 
        /// the security rules. 
        /// </summary>
        /// <param name="t">.NET Data Type</param>
        /// <param name="val">String value that will be used as part of building the dynamic SQL statement.</param>
        /// <returns>True if <paramref name="val"/> passes all security rules, false if it fails at least one.</returns>
        (bool, IEnumerable<string>) CheckInput(Type t, string val);
    }
}

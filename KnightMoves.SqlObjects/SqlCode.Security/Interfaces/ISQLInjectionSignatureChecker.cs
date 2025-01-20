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

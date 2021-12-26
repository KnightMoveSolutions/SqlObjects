namespace KnightMoves.SqlObjects.SqlCode.Security
{
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
}

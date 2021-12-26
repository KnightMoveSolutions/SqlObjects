using System.Text.RegularExpressions;

namespace KnightMoves.SqlObjects.SqlCode.Security
{
    /// <summary>
    /// Checks for the existence semi-colon (;) in the value to flag for a possible SQL injection attack.
    /// </summary>
    public class SQLInjSemiColonRule : ISQLInjectionSignatureRule
    {
        private Regex _signature;

        /// <summary>
        /// The message describing how this rule was violated.
        /// </summary>
        public string ErrorMessage { get; } = "SQL statement terminating character ';' found in string";

        public SQLInjSemiColonRule()
        {
            _signature = new Regex(";", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// Returns true if a semi-colon (;) is found in <paramref name="val"/> or false if it is NOT found.
        /// </summary>
        public bool ContainsSqlInjection(string val) => _signature.IsMatch(val);
    }
}

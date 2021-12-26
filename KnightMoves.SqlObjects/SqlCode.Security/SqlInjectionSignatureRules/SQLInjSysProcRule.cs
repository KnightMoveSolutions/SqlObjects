using System.Text.RegularExpressions;

namespace KnightMoves.SqlObjects.SqlCode.Security
{
    /// <summary>
    /// Checks for the existence of a system stored procedure call in the value to flag for a possible SQL injection attack.
    /// </summary>
    public class SQLInjSysProcRule : ISQLInjectionSignatureRule
    {
        private Regex _signature;

        /// <summary>
        /// The message describing how this rule was violated.
        /// </summary>
        public string ErrorMessage { get; } = "Possible execution of system stored procedure detected in string";

        public SQLInjSysProcRule()
        {
            _signature = new Regex(@"(sp_|xp_)", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// Returns true if a system stored procedure call is found in <paramref name="val"/> or false if it is NOT found.
        /// </summary>
        public bool ContainsSqlInjection(string val) => _signature.IsMatch(val);
    }
}

using System.Text.RegularExpressions;

namespace KnightMoves.SqlObjects.SqlCode.Security
{
    /// <summary>
    /// Checks for the existence of a SQL 'UNION' statement in the value to flag for a possible SQL injection attack.
    /// </summary>
    public class SQLInjUnionRule : ISQLInjectionSignatureRule
    {
        private Regex _signature;

        /// <summary>
        /// The message describing how this rule was violated.
        /// </summary>
        public string ErrorMessage { get; } = "Possible UNION statement detected in string";

        public SQLInjUnionRule()
        {
            _signature = new Regex(@"union", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// Returns true if a SQL 'UNION' clause is found in <paramref name="val"/> or false if it is NOT found.
        /// </summary>
        public bool ContainsSqlInjection(string val) => _signature.IsMatch(val);
    }
}

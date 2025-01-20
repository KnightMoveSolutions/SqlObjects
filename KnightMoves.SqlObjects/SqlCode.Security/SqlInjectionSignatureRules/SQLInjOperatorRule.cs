using System.Text.RegularExpressions;

namespace KnightMoves.SqlObjects.SqlCode.Security;

/// <summary>
/// Checks for the existence of SQL operators (=, <, and >) in the value to flag for a possible SQL injection attack.
/// </summary>
public class SQLInjOperatorRule : ISQLInjectionSignatureRule
{
    private Regex _signature;

    /// <summary>
    /// The message describing how this rule was violated.
    /// </summary>
    public string ErrorMessage { get; } = "SQL operators detected in string";

    public SQLInjOperatorRule()
    {
        _signature = new Regex(@"[=<>]", RegexOptions.Compiled);
    }

    /// <summary>
    /// Returns true if any SQL operators (=, <, and >) are found in <paramref name="val"/> or false if it is NOT found.
    /// </summary>
    public bool ContainsSqlInjection(string val) => _signature.IsMatch(val);
}

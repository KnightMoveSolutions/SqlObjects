using System.Text.RegularExpressions;

namespace KnightMoves.SqlObjects.SqlCode.Security;

/// <summary>
/// Checks for the existence of the SQL 'BETWEEN' clause in the value to flag for a possible SQL injection attack.
/// </summary>
public class SQLInjBetweenRule : ISQLInjectionSignatureRule
{
    private Regex _signature;

    /// <summary>
    /// The message describing how this rule was violated.
    /// </summary>
    public string ErrorMessage { get; } = "Possible BETWEEN clause detected in string";

    public SQLInjBetweenRule()
    {
        _signature = new Regex(@"between", RegexOptions.Compiled | RegexOptions.IgnoreCase);
    }

    /// <summary>
    /// Returns true if the SQL 'BETWEEN' clause is found in <paramref name="val"/> or false if it is NOT found.
    /// </summary>
    public bool ContainsSqlInjection(string val) => _signature.IsMatch(val);
}

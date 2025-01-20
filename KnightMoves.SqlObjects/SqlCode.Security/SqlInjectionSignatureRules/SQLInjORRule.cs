using System.Text.RegularExpressions;

namespace KnightMoves.SqlObjects.SqlCode.Security;

/// <summary>
/// Checks for the existence of a SQL 'OR' in the value to flag for a possible SQL injection attack.
/// </summary>
public class SQLInjORRule : ISQLInjectionSignatureRule
{
    private Regex _signature;

    /// <summary>
    /// The message describing how this rule was violated.
    /// </summary>
    public string ErrorMessage { get; } = "Possible OR statement detected in string";

    public SQLInjORRule()
    {
        _signature = new Regex(@"((\s+)(or)(\s+))|((\s+)(or)$)|(^(or)(\s+))", RegexOptions.Compiled | RegexOptions.IgnoreCase);
    }

    /// <summary>
    /// Returns true if a SQL 'OR' is found in <paramref name="val"/> or false if it is NOT found.
    /// </summary>
    public bool ContainsSqlInjection(string val) => _signature.IsMatch(val);
}

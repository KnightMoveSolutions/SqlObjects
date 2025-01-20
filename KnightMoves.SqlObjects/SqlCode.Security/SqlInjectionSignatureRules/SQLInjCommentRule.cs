using System.Text.RegularExpressions;

namespace KnightMoves.SqlObjects.SqlCode.Security;

/// <summary>
/// Checks for the existence of the SQL 'comment' in the value to flag for a possible SQL injection attack.
/// </summary>
public class SQLInjCommentRule : ISQLInjectionSignatureRule
{
    private Regex _signature;

    /// <summary>
    /// The message describing how this rule was violated.
    /// </summary>
    public string ErrorMessage { get; } = "SQL Comment detected in string";

    public SQLInjCommentRule()
    {
        _signature = new Regex(@"(--|\/\*|\*\/)", RegexOptions.Compiled);
    }

    /// <summary>
    /// Returns true if a SQL 'comment' is found in <paramref name="val"/> or false if it is NOT found.
    /// </summary>
    public bool ContainsSqlInjection(string val) => _signature.IsMatch(val);
}

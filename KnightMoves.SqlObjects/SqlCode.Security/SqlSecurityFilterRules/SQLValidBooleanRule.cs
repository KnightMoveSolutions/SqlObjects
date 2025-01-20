using System;
using System.Collections.Generic;

namespace KnightMoves.SqlObjects.SqlCode.Security;

/// <summary>
/// Checks values for valid boolean
/// </summary>
public class SQLValidBooleanRule : ISQLSecurityFilterRule
{
    private IEnumerable<string> ERROR_MESSAGE = new List<string> { "Invalid boolean value.  Must be \"1\" or \"0\" or \"true\" or \"false\"" };

    /// <summary>
    /// If <typeparamref name="T"/> is <see cref="bool"/> it will check <paramref name="val"/> for a valid boolean value and 
    /// return 0 if it is invalid. If <typeparamref name="T"/> is not <see cref="bool"/> then it will ignore
    /// the value and return it as-is.
    /// </summary>
    public string SanitizeInput<T>(string val)
    {
        return SanitizeInput(typeof(T), val);
    }

    /// <summary>
    /// If <typeparamref name="T"/> is <see cref="bool"/> it will check <paramref name="val"/> for a valid boolean value and 
    /// return 0 if it is invalid. If <typeparamref name="T"/> is not <see cref="bool"/> then it will ignore
    /// the value and return it as-is.
    /// </summary>
    public string SanitizeInput(Type T, string val)
    {
        if (T != typeof(bool))
            return val;

        if (!ParseValue(T, val))
            return "0";

        return (val.ToLower() == "true" || val == "1") ? "1" : "0"; ;
    }

    /// <summary>
    /// If <typeparamref name="T"/> is <see cref="bool"/> it will return true if <paramref name="val"/> is 
    /// a valid boolean value. If not it returns false with a collection of error strings.
    /// </summary>
    public (bool, IEnumerable<string>) CheckInput<T>(string val)
    {
        return CheckInput(typeof(T), val);
    }

    /// <summary>
    /// If <typeparamref name="T"/> is <see cref="bool"/> it will return true if <paramref name="val"/> is 
    /// a valid boolean value. If not it returns false with a collection of error strings.
    /// </summary>
    public (bool, IEnumerable<string>) CheckInput(Type T, string val)
    {
        bool okay = T != typeof(bool) || ParseValue(T, val);

        return (okay, okay ? new List<string>() : ERROR_MESSAGE);
    }

    private bool ParseValue(Type t, string val) =>
        val == "1" ||
        val == "0" ||
        val.ToLower() == "true" ||
        val.ToLower() == "false";
}

using System;
using System.Collections.Generic;

namespace KnightMoves.SqlObjects.SqlCode.Security;

/// <summary>
/// Checks values for valid Guid
/// </summary>
public class SQLValidGuidRule : ISQLSecurityFilterRule
{
    private IEnumerable<string> ERROR_MESSAGE = new List<string> { "Invalid GUID value" };

    /// <summary>
    /// If <typeparamref name="T"/> is <see cref="Guid"/> it will check <paramref name="val"/> for a valid <see cref="Guid"/> value and 
    /// return 00000000-0000-0000-0000-000000000000 if it is invalid. If <typeparamref name="T"/> is not <see cref="Guid"/> then it will ignore
    /// the value and return it as-is.
    /// </summary>
    public string SanitizeInput<T>(string val)
    {
        return SanitizeInput(typeof(T), val);
    }

    /// <summary>
    /// If <typeparamref name="T"/> is <see cref="Guid"/> it will check <paramref name="val"/> for a valid <see cref="Guid"/> value and 
    /// return 00000000-0000-0000-0000-000000000000 if it is invalid. If <typeparamref name="T"/> is not <see cref="Guid"/> then it will ignore
    /// the value and return it as-is.
    /// </summary>
    public string SanitizeInput(Type T, string val)
    {
        if (T == typeof(Guid) && !Guid.TryParse(val, out _))
            return Guid.Empty.ToString();

        return val;
    }

    /// <summary>
    /// If <typeparamref name="T"/> is <see cref="Guid"/> it will return true if <paramref name="val"/> is 
    /// a valid <see cref="Guid"/> value. If not it returns false with a collection of error strings.
    /// </summary>
    public (bool, IEnumerable<string>) CheckInput<T>(string val)
    {
        return CheckInput(typeof(T), val);
    }

    /// <summary>
    /// If <typeparamref name="T"/> is <see cref="Guid"/> it will return true if <paramref name="val"/> is 
    /// a valid <see cref="Guid"/> value. If not it returns false with a collection of error strings.
    /// </summary>
    public (bool, IEnumerable<string>) CheckInput(Type T, string val)
    {
        bool okay = T != typeof(Guid) || Guid.TryParse(val, out _);

        return (okay, okay ? new List<string>() : ERROR_MESSAGE);
    }
}

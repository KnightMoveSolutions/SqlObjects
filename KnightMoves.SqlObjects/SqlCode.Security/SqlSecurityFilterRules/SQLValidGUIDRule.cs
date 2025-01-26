/*
    THE LICENSED WORK IS PROVIDED UNDER THE TERMS OF THE DEVELOPER TOOL LIMITED 
    LICENSE (“LICENSE”) AS FIRST COMPLETED BY THE ORIGINAL AUTHOR. ANY USE, PUBLIC 
    DISPLAY, PUBLIC PERFORMANCE, REPRODUCTION OR DISTRIBUTION OF, OR PREPARATION OF 
    DERIVATIVE WORKS BASED ON THE LICENSED WORK CONSTITUTES RECIPIENT’S ACCEPTANCE 
    OF THIS LICENSE AND ITS TERMS, WHETHER OR NOT SUCH RECIPIENT READS THE TERMS OF THE 
    LICENSE. “LICENSED WORK” AND “RECIPIENT” ARE DEFINED IN THE LICENSE. A COPY OF THE 
    LICENSE IS LOCATED IN THE TEXT FILE ENTITLED “LICENSE.TXT” ACCOMPANYING THE CONTENTS 
    OF THIS FILE. IF A COPY OF THE LICENSE DOES NOT ACCOMPANY THIS FILE, A COPY OF THE 
    LICENSE MAY ALSO BE OBTAINED AT THE FOLLOWING WEB SITE:  
 
    https://docs.knightmovesolutions.com/sql-objects/license.html
*/

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

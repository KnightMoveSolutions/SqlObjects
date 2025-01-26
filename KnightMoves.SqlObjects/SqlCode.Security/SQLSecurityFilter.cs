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
using System.Linq;

namespace KnightMoves.SqlObjects.SqlCode.Security;

/// <summary>
/// This is the default implementation of the <see cref="ISQLSecurityFilter"/> interface. This class uses a 
/// collection of <see cref="ISQLSecurityFilterRule"/> objects to determine whether or not a value is okay. 
/// </summary>
public class SQLSecurityFilter : ISQLSecurityFilter
{
    protected IList<ISQLSecurityFilterRule> _securityFilterRules;

    /// <summary>
    /// When set to true, it will cause <see cref="SanitizeInput"/> and <see cref="CheckInput"/> to ignore 
    /// security rules and return the original value and true respectively.
    /// </summary>
    public bool DisableSecurityCheck { get; set; }

    /// <summary>
    /// This implementation of <see cref="ISQLSecurityFilter"/> requires a collection of 
    /// <see cref="ISQLSecurityFilterRule"/> objects.
    /// </summary>
    public SQLSecurityFilter(IList<ISQLSecurityFilterRule> securityFilterRules)
    {
        _securityFilterRules = securityFilterRules;
    }

    /// <summary>
    /// Checks <paramref name="val"/> against the <see cref="ISQLSecurityFilterRule"/> objects and returns 
    /// a sanitized value based on the type of <typeparamref name="T"/> if <paramref name="val"/> fails
    /// any of the rules. Otherwise it returns the value of <paramref name="val"/> as-is.
    /// </summary>
    public string SanitizeInput<T>(string val)
    {
        return SanitizeInput(typeof(T), val);
    }

    /// <summary>
    /// Checks <paramref name="val"/> against the <see cref="ISQLSecurityFilterRule"/> objects and returns 
    /// a sanitized value based on the type of <typeparamref name="T"/> if <paramref name="val"/> fails
    /// any of the rules. Otherwise it returns the value of <paramref name="val"/> as-is.
    /// </summary>
    public string SanitizeInput(Type T, string val)
    {
        if (DisableSecurityCheck)
            return val;

        foreach (var filterRule in _securityFilterRules)
            val = filterRule.SanitizeInput(T, val);

        return val;
    }

    /// <summary>
    /// Checks <paramref name="val"/> against the <see cref="ISQLSecurityFilterRule"/> objects based in part 
    /// on the type of <paramref name="T"/> and returns true if <paramref name="val"/> satisies all of the rules 
    /// implemented by the <see cref="ISQLSecurityFilterRule"/> objects or returns false if <paramref name="val"/> 
    /// fails any one of the rules. The collection of strings will contain descriptions of the failed rules if any.
    /// </summary>
    public (bool, IEnumerable<string>) CheckInput<T>(string val)
    {
        return CheckInput(typeof(T), val);
    }

    /// <summary>
    /// Checks <paramref name="val"/> against the <see cref="ISQLSecurityFilterRule"/> objects based in part 
    /// on the type of <paramref name="T"/> and returns true if <paramref name="val"/> satisies all of the rules 
    /// implemented by the <see cref="ISQLSecurityFilterRule"/> objects or returns false if <paramref name="val"/> 
    /// fails any one of the rules. The collection of strings will contain descriptions of the failed rules if any.
    /// </summary>
    public (bool, IEnumerable<string>) CheckInput(Type T, string val)
    {
        if (DisableSecurityCheck)
            return (true, new List<string>());

        var okay = true;
        var violations = new List<string>();

        _securityFilterRules
            .ToList()
            .ForEach(rule =>
            {
                (bool result, IEnumerable<string> errors) = rule.CheckInput(T, val);
                okay &= result;
                if (!okay && errors != null)
                    errors.ToList().ForEach(e => violations.Add(e));
            });

        return (okay, violations);
    }
}

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

using System.Collections.Generic;

namespace KnightMoves.SqlObjects.SqlCode.Security;

/// <summary>
/// Used to create an <see cref="ISQLSecurityFilter"/> object based on the 
/// default implementations of the various <see cref="ISQLInjectionSignatureRule"/>s
/// and <see cref="ISQLSecurityFilterRule"/>s. Currently used to provide an 
/// instance for the Fluent API since it cannot utilize an IoC container.
/// </summary>
public class SQLSecurityFactory
{
    /// <summary>
    /// Creates an <see cref="ISQLSecurityFilter"/> object based on the default 
    /// implementations of the various <see cref="ISQLInjectionSignatureRule"/>s
    /// and <see cref="ISQLSecurityFilterRule"/>s
    /// </summary>
    public static ISQLSecurityFilter Create()
    {
        var sqlInjectionRules = new List<ISQLInjectionSignatureRule>
        {
            new SQLInjBetweenRule(),
            new SQLInjCommentRule(),
            new SQLInjExecRule(),
            new SQLInjLikeRule(),
            new SQLInjOperatorRule(),
            new SQLInjORRule(),
            new SQLInjSelectRule(),
            new SQLInjSemiColonRule(),
            new SQLInjSysProcRule(),
            new SQLInjUnionRule()
        };

        var sqlInjectionChecker = new SQLInjectionSignatureChecker(sqlInjectionRules);

        var filterRules = new List<ISQLSecurityFilterRule>
        { 
            new SQLValidBooleanRule(),
            new SQLValidGuidRule(),
            new SQLValidNumericRule(),
            new SQLValidStringRule(sqlInjectionChecker)
        };

        var sqlSecurityFilter = new SQLSecurityFilter(filterRules);

        return sqlSecurityFilter;
    }
}

using System.Collections.Generic;

namespace KnightMoves.SqlObjects.SqlCode.Security
{
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
}

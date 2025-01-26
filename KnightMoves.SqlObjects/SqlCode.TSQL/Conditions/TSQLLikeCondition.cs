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

using KnightMoves.SqlObjects.SqlCode.Security;
using System.Linq;

namespace KnightMoves.SqlObjects.SqlCode.TSQL;

/// <summary>
/// <para>
/// This class implements <see cref="ISqlLikeCondition"/> and builds a T-SQL 'LIKE' clause in the following form
/// </para> 
/// <para>
/// [LeftOperand] LIKE [Pattern]
/// </para>
/// <remarks>
/// This class uses an instance of <see cref="ISQLSecurityFilter"/> from the <see cref="SQLSecurityFactory"/>.Create() factory method
/// </remarks>
/// </summary>
public class TSQLLikeCondition : TSQLStatement, ISqlLikeCondition
{
    private readonly ISQLSecurityFilter _sqlSecurityFilter;

    public TSQLLikeCondition()
    {
        _sqlSecurityFilter = SQLSecurityFactory.Create();
    }

    string _pattern;

    /// <summary>
    /// A string representing the pattern to match for and which typically includes SQL wildcard characters such as % and ?
    /// </summary>
    public string Pattern 
    { 
        get =>  _pattern.IsParameter() ? _pattern : $"'{_pattern}'";
        set {
            // Values that begin with @ should be a parameter for parameterized queries.
            // But if an input value starts with @ it could be confused with a parameter
            // so we don't want to trust it completely. We should remove things that 
            // would cause the value to be evaluated as SQL. Otherwise it would be a SQL
            // injection point.
            if (value.IsParameter())
                _pattern = value.Replace(";", string.Empty)
                                .Replace("-", string.Empty)
                                .Replace("\\", string.Empty)
                                .Replace('\t', ' ')
                                .Replace('\r', ' ')
                                .Replace('\n', ' ')
                                .Replace(" ", string.Empty);

            // We strip begin/end quotes to cover deserialization
            if (value != null && value.StartsWith("'") && value.EndsWith("'"))
                value = value[1..(value.Length - 1)];

            // Here we sanitize the value protecting the resulting SQL from 
            // SQL Injection attacks for values that are not parameters
            _pattern = _sqlSecurityFilter.SanitizeInput<string>(value);
        } 
    }

    /// <summary>
    /// <para>
    /// Returns a T-SQL 'LIKE' clause in the following form.
    /// </para> 
    /// <para>
    /// [LeftOperand] LIKE [Pattern]
    /// </para>
    /// </summary>
    public override string SQL()
    {
        var leftOperand = Children.FirstOrDefault(c => c is ISqlQueryExpression);

        return $"{leftOperand.ToString().Trim()} LIKE {Pattern}";
    }
}

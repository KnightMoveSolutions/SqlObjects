namespace KnightMoves.SqlObjects.SqlCode;

/// <summary>
/// Used to identity SQL comparison operators for <see cref="ISqlCondition"/>s in a SQL WHERE clause
/// </summary>
public enum SqlComparisonOperators
{
    /// <summary>
    /// =
    /// </summary>
    IsEqualTo,

    /// <summary>
    /// &lt;&gt;
    /// </summary>
    IsNotEqualTo,

    /// <summary>
    /// &gt;
    /// </summary>
    IsGreaterThan,

    /// <summary>
    /// &lt;
    /// </summary>
    IsLessThan,

    /// <summary>
    /// &gt;=
    /// </summary>
    IsGreaterThanOrEqualTo,

    /// <summary>
    /// &lt;=
    /// </summary>
    IsLessThanOrEqualTo,

    /// <summary>
    /// IS
    /// </summary>
    IS,

    /// <summary>
    /// IS NOT
    /// </summary>
    ISNOT
}

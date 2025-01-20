namespace KnightMoves.SqlObjects.SqlCode;

/// <summary>
/// Used to identify operators that separate SQL WHERE clause conditions, JOIN conditions, etc.
/// </summary>
public enum SqlLogicalOperators
{
    /// <summary>
    /// Ex: Field1 = 'A' AND Field1 = 'B'
    /// </summary>
    AND,

    /// <summary>
    /// Ex: Field1 = 'A' OR Field1 = 'B'
    /// </summary>
    OR
}

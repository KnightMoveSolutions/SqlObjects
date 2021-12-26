namespace KnightMoves.SqlObjects.SqlCode
{
    /// <summary>
    /// Used to identify which wildcard characters to build into a SQL LIKE clause condition.
    /// </summary>
    public enum SqlStringOperators
    {
        /// <summary>
        /// Indicates a % wildcard at the beginning of a string. Ex: Field1 LIKE '%Smith'
        /// </summary>
        StartsWith,

        /// <summary>
        /// Indicates a % wildcard at the end of a string. Ex: Field1 LIKE 'John%'
        /// </summary>
        EndsWith,

        /// <summary>
        /// Indicates a wildcard at the beginning and end of a string. Ex: Field1 LIKE '%ith%'
        /// </summary>
        Contains
    }
}

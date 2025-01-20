namespace KnightMoves.SqlObjects.SqlCode.TSQL;

public abstract partial class TSQLStatement : SqlStatement
{
    /// <summary>
    /// Starts a SQL DELETE statement
    /// </summary>
    public override SqlStatement DELETE()
    {
        return new TSQLDelete();
    }
}

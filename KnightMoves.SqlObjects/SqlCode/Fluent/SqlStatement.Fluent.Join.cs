namespace KnightMoves.SqlObjects.SqlCode;

public abstract partial class SqlStatement : IFluentSqlJoin
{
    public SqlStatement ON(string column)
    {
        return ON(string.Empty, column);
    }

    public SqlStatement ON(string multiPartIdentifier, string column)
    {
        LeftMultiPartIdentifier = multiPartIdentifier;
        LeftOperandValue = column;
        return this;
    }
}

namespace KnightMoves.SqlObjects.SqlCode
{
    public interface ISqlQueryExpression
    {
        string Alias { get; set; }
        SqlDataType DataType { get; set; }
    }
}
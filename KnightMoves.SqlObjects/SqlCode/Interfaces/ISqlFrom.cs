namespace KnightMoves.SqlObjects.SqlCode
{
    public interface ISqlFrom
    {
        string Schema { get; set; }
        string Table { get; set; }
        string MultiPartIdentifier { get; set; }
    }
}
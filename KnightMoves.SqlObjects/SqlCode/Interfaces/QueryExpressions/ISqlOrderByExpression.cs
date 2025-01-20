namespace KnightMoves.SqlObjects.SqlCode;

public interface ISqlOrderByExpression
{
    //ISqlQueryExpression Expression { get; set; }
    SqlOrderByDirections Direction { get; set; }
}

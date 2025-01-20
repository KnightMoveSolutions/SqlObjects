namespace KnightMoves.SqlObjects.SqlCode;

public abstract partial class SqlStatement : IFluentSqlDelete
{
    public abstract SqlStatement DELETE();

    // FROM implemented in SqlStatement.Fluent.From.cs
}

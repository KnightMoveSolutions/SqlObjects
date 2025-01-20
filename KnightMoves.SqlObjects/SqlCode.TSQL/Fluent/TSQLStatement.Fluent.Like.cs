namespace KnightMoves.SqlObjects.SqlCode.TSQL;

public abstract partial class TSQLStatement : SqlStatement
{
    /// <summary>
    /// Outputs a SQL LIKE clause using <paramref name="pattern"/> as the filtering specification
    /// </summary>
    public override SqlStatement LIKE(string pattern)
    {
        var parent = GetConditionParent();

        var like = new TSQLLikeCondition { Pattern = pattern };

        var leftOperand = GetLeftOperand();

        like.Children.Add(leftOperand as SqlStatement);

        parent.Children.Add(like);

        return parent;
    }
}

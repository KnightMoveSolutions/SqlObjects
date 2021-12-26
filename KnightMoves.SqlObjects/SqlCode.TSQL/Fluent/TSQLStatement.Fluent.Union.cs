namespace KnightMoves.SqlObjects.SqlCode.TSQL
{
    public abstract partial class TSQLStatement : SqlStatement
    {
        /// <summary>
        /// Outputs the SQL UNION keyword
        /// </summary>
        public override SqlStatement UNION()
        {
            var parent = GetParentScope();

            parent.Children.Add(new TSQLUnion());

            return parent;
        }
    }
}

using System;

namespace KnightMoves.SqlObjects.SqlCode.TSQL
{
    public abstract partial class TSQLStatement : SqlStatement
    {
        /// <summary>
        /// Starts a SQL UPDATE statement using the specified <paramref name="table"/> as the target
        /// </summary>
        public override SqlStatement UPDATE(string table)
        {
            return UPDATE(string.Empty, table);
        }

        /// <summary>
        /// Starts a SQL UPDATE statement using the specified [<paramref name="schema"/>].[<paramref name="table"/>] as the target
        /// </summary>
        public override SqlStatement UPDATE(string schema, string table)
        {
            return new TSQLUpdate(schema, table);
        }

        /// <summary>
        /// Signifies the SET keyword immediately after an UPDATE clause that marks the start of the update expression list
        /// </summary>
        public override SqlStatement SET()
        {
            if (IsUpdate == false)
                throw new InvalidOperationException(".SET() can only be called after .UPDATE(). To set variables use .Script()");

            return this;
        }
    }
}

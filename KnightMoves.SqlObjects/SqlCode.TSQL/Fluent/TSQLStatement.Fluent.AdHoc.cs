using System.Data;

namespace KnightMoves.SqlObjects.SqlCode.TSQL
{
    public abstract partial class TSQLStatement : SqlStatement
    {
        /// <summary>
        /// Creates an object containing <paramref name="scriptCode"/>
        /// </summary>
        public override SqlStatement Script(string scriptCode)
        {
            var scope = GetParentScope();

            var script = new TSQLScript();

            script.Children.Add(new TSQLLiteral
            {
                DataType = new TSQLDataType(SqlDbType.Variant),
                Value = scriptCode
            });

            scope.Children.Add(script);

            return scope;
        }

        /// <summary>
        /// Adds <paramref name="literal"/> as-is to the preceding SQL object
        /// </summary>
        public override SqlStatement Literal(string literal)
        {
            Children.Add(new TSQLLiteral
            {
                DataType = new TSQLDataType(SqlDbType.Variant),
                Value = literal
            });

            return this;
        }

    }
}

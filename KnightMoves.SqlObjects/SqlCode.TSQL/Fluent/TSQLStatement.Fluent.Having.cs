using System;

namespace KnightMoves.SqlObjects.SqlCode.TSQL
{
    public abstract partial class TSQLStatement : SqlStatement
    {
        /// <summary>
        /// Starts a SQL HAVING clause
        /// </summary>
        public override SqlStatement HAVING()
        {
            var having = new TSQLHaving();
            var scope = GetParentScope();
            scope.Children.Add(having);
            return having;
        }

        /// <summary>
        /// Starts a SQL HAVING clause using <paramref name="leftLiteral"/> object as the left operand of the first condition
        /// </summary>
        public override SqlStatement HAVING(ISqlLiteral leftLiteral)
        {
            if (leftLiteral.DataType == null)
                throw new ArgumentException($"The {nameof(leftLiteral.DataType)} property cannot be null for argument {nameof(leftLiteral)}");

            var having = new TSQLHaving();
            var scope = GetParentScope();
            scope.Children.Add(having);
            having.AND(leftLiteral);
            return having;
        }

        /// <summary>
        /// Starts a SQL HAVING clause using <paramref name="operand"/> string as the left operand of the first condition
        /// </summary>
        public override SqlStatement HAVING(string operand)
        {
            return HAVING(string.Empty, operand);
        }

        /// <summary>
        /// Starts a SQL HAVING clause using a column as the left operand of the first condition 
        /// </summary>
        public override SqlStatement HAVING(string multiPartIdentifier, string column)
        {
            var having = new TSQLHaving();
            var parent = GetParentScope();

            if (parent.IsUpdate || parent.IsDelete)
                return this;

            parent.Children.Add(having);
            having.AND(multiPartIdentifier, column);
            return having;
        }

        /// <summary>
        /// Starts a SQL HAVING clause using an <see cref="int"/> as the left operand of the first condition
        /// </summary>
        public override SqlStatement HAVING(int value)
        {
            var having = MakeHaving(value);
            having.AND(value);
            return having;
        }

        /// <summary>
        /// Starts a SQL HAVING clause using a <see cref="DateTime"/> as the left operand of the first condition
        /// </summary>
        public override SqlStatement HAVING(DateTime value)
        {
            var having = MakeHaving(value);
            having.AND(value);
            return having;
        }

        /// <summary>
        /// Starts a SQL HAVING clause using a <see cref="bool"/> as the left operand of the first condition
        /// </summary>
        public override SqlStatement HAVING(bool value)
        {
            var having = MakeHaving(value);
            having.AND(value);
            return having;
        }

        /// <summary>
        /// Starts a SQL HAVING clause using a <see cref="long"/> as the left operand of the first condition
        /// </summary>
        public override SqlStatement HAVING(long value)
        {
            var having = MakeHaving(value);
            having.AND(value);
            return having;
        }

        /// <summary>
        /// Starts a SQL HAVING clause using a <see cref="Guid"/> as the left operand of the first condition
        /// </summary>
        public override SqlStatement HAVING(Guid value)
        {
            var having = MakeHaving(value);
            having.AND(value);
            return having;
        }

        /// <summary>
        /// Starts a SQL HAVING clause using a <see cref="char"/> as the left operand of the first condition
        /// </summary>
        public override SqlStatement HAVING(char value)
        {
            var having = MakeHaving(value);
            having.AND(value);
            return having;
        }

        /// <summary>
        /// Starts a SQL HAVING clause using a <see cref="decimal"/> as the left operand of the first condition
        /// </summary>
        public override SqlStatement HAVING(decimal value)
        {
            var having = MakeHaving(value);
            having.AND(value);
            return having;
        }

        private TSQLHaving MakeHaving<T>(T value)
        {
            var having = new TSQLHaving();
            var parent = GetParentScope();

            if (parent.IsUpdate || parent.IsDelete)
                return having;

            parent.Children.Add(having);
            return having;
        }
    }

}

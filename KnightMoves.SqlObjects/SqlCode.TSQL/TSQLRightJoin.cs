using System;
using System.Text;

namespace KnightMoves.SqlObjects.SqlCode.TSQL
{
    /// <summary>
    /// This class implements <see cref="ISqlRightJoin"/> and builds a SQL RIGHT JOIN statement
    /// listing all <see cref="ISqlCondition"/>s that have been added to it
    /// </summary>
    public class TSQLRightJoin : TSQLStatement, ISqlRightJoin
    {
        /// <summary>
        /// The Schema of the Table
        /// </summary>
        public string Schema { get; set; }

        /// <summary>
        /// The Table name
        /// </summary>
        public string Table { get; set; }

        /// <summary>
        /// The table's Multipart Identifier alias value
        /// </summary>
        public string MultipartIdentifier { get; set; }

        public TSQLRightJoin() { }

        public TSQLRightJoin(string table) : this(string.Empty, table, string.Empty) { }

        public TSQLRightJoin(string schema, string table) : this(schema, table, string.Empty) { }

        public TSQLRightJoin(string schema, string table, string multipartIdentifier)
        {
            Schema = schema;
            Table = table;
            MultipartIdentifier = multipartIdentifier;
        }

        /// <summary>
        /// Builds a SQL RIGHT JOIN statement using in the format below where "t" is the <see cref="MultipartIdentifier"/>
        /// <code>
        /// RIGHT JOIN [Schema].[Table] t ON
        /// </code>
        /// <code>
        ///  -- Join conditions will be listed here
        /// </code>
        /// </summary>
        public override string SQL()
        {
            StringBuilder sql = new StringBuilder();

            sql.Append($"{IndentString}RIGHT JOIN ");

            if (!string.IsNullOrEmpty(Table))
            {
                if (!string.IsNullOrEmpty(Schema))
                    sql.Append($"[{Schema.SanitizeDelimitedValue()}].");

                sql.Append($"[{Table.SanitizeDelimitedValue()}]");
            }

            if (!string.IsNullOrEmpty(MultipartIdentifier))
            {
                sql.Append($" {MultipartIdentifier}");
            }

            sql.Append(" ON");

            foreach (SqlStatement sqlStmt in Children)
            {
                if (!sqlStmt.IsCondition)
                    continue;

                if (sqlStmt.IsConditionGroup)
                    sql.Append(Environment.NewLine);
                else
                    sql.Append(" ");

                sql.Append($"{sqlStmt}{Environment.NewLine}");
            }

            return sql.ToString();
        }
    }
}

﻿using System;
using System.Text;

namespace KnightMoves.SqlObjects.SqlCode.TSQL
{
    /// <summary>
    /// This class implements <see cref="ISqlInnerJoin"/> and builds a SQL INNER JOIN statement
    /// listing all <see cref="ISqlCondition"/>s that have been added to it
    /// </summary>
    public class TSQLInnerJoin : TSQLStatement, ISqlInnerJoin
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

        public TSQLInnerJoin() { }

        public TSQLInnerJoin(string table) : this(string.Empty, table, string.Empty) { }

        public TSQLInnerJoin(string schema, string table) : this(schema, table, string.Empty) { }

        public TSQLInnerJoin(string schema, string table, string multipartIdentifier)
        {
            Schema = schema;
            Table = table;
            MultipartIdentifier = multipartIdentifier;
        }

        /// <summary>
        /// Builds a SQL INNER JOIN statement using in the format below where "t" is the <see cref="MultipartIdentifier"/>
        /// <code>
        /// INNER JOIN [Schema].[Table] t ON
        /// </code>
        /// <code>
        ///  -- Join conditions will be listed here
        /// </code>
        /// </summary>
        public override string SQL()
        {
            var sql = new StringBuilder();

            sql.Append($"{IndentString}INNER JOIN ");

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

                if (sqlStmt is ISqlConditionGroup)
                    sql.Append(Environment.NewLine);
                else
                    sql.Append(" ");

                sql.Append($"{sqlStmt}{Environment.NewLine}");
            }

            return sql.ToString();
        }
    }
}

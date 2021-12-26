﻿using System;
using System.Data;

namespace KnightMoves.SqlObjects.SqlCode.TSQL
{
    public abstract partial class TSQLStatement : SqlStatement
    {
        /// <summary>
        /// Starts a SQL INSERT statement
        /// </summary>
        public override SqlStatement INSERT()
        {
            return new TSQLInsert();
        }

        /// <summary>
        /// Continues a SQL INSERT statement with INTO using <paramref name="table"/> to specify affected the table
        /// </summary>
        public override SqlStatement INTO(string table)
        {
            return INTO(string.Empty, table);
        }

        /// <summary>
        /// Continues a SQL INSERT statement with INTO using <paramref name="table"/> and <paramref name="table"/> to
        /// specify the affected table
        /// </summary>
        public override SqlStatement INTO(string schema, string table)
        {
            if (IsInsert == false)
                return this;

            var insert = this as ISqlInsert;

            insert.Schema = schema;
            insert.Table = table;

            return this;
        }

        /// <summary>
        /// Continues a SQL INSERT statement intended to be used after calling <see cref="INTO(string)"/> or <see cref="INTO(string, string)"/>
        /// </summary>
        public override SqlStatement VALUES()
        {
            return GetExpressionParent();
        }

        /// <summary>
        /// Adds <see cref="string"/> <paramref name="value"/> to the list of values in the VALUES clause of an INSERT statement
        /// </summary>
        public override SqlStatement VALUE(string value)
        {
            Children.Add(new TSQLLiteral
            {
                DataType = new TSQLDataType(SqlDbType.VarChar),
                Value = value
            });

            return this;
        }

        /// <summary>
        /// Adds <see cref="int"/> <paramref name="value"/> to the list of values in the VALUES clause of an INSERT statement
        /// </summary>
        public override SqlStatement VALUE(int value)
        {
            Children.Add(new TSQLLiteral
            {
                DataType = new TSQLDataType(SqlDbType.Int),
                Value = value.ToString()
            });

            return this;
        }

        /// <summary>
        /// Adds <see cref="DateTime"/> <paramref name="value"/> to the list of values in the VALUES clause of an INSERT statement
        /// </summary>
        public override SqlStatement VALUE(DateTime value)
        {
            Children.Add(new TSQLLiteral
            {
                DataType = new TSQLDataType(SqlDbType.DateTime),
                Value = value.ToString(SqlDataType.SQL_DATE_STRING_FORMAT)
            });

            return this;
        }

        /// <summary>
        /// Adds <see cref="bool"/> <paramref name="value"/> to the list of values in the VALUES clause of an INSERT statement
        /// </summary>
        public override SqlStatement VALUE(bool value)
        {
            Children.Add(new TSQLLiteral
            {
                DataType = new TSQLDataType(SqlDbType.Bit),
                Value = value ? "1" : "0"
            });

            return this;
        }

        /// <summary>
        /// Adds <see cref="long"/> <paramref name="value"/> to the list of values in the VALUES clause of an INSERT statement
        /// </summary>
        public override SqlStatement VALUE(long value)
        {
            Children.Add(new TSQLLiteral
            {
                DataType = new TSQLDataType(SqlDbType.BigInt),
                Value = value.ToString()
            });

            return this;
        }

        /// <summary>
        /// Adds <see cref="Guid"/> <paramref name="value"/> to the list of values in the VALUES clause of an INSERT statement
        /// </summary>
        public override SqlStatement VALUE(Guid value)
        {
            Children.Add(new TSQLLiteral
            {
                DataType = new TSQLDataType(SqlDbType.UniqueIdentifier),
                Value = value.ToString()
            });

            return this;
        }

        /// <summary>
        /// Adds <see cref="char"/> <paramref name="value"/> to the list of values in the VALUES clause of an INSERT statement
        /// </summary>
        public override SqlStatement VALUE(char value)
        {
            Children.Add(new TSQLLiteral
            {
                DataType = new TSQLDataType(SqlDbType.Char),
                Value = value.ToString()
            });

            return this;
        }

        /// <summary>
        /// Adds <see cref="decimal"/> <paramref name="value"/> to the list of values in the VALUES clause of an INSERT statement
        /// </summary>
        public override SqlStatement VALUE(decimal value)
        {
            Children.Add(new TSQLLiteral
            {
                DataType = new TSQLDataType(SqlDbType.Decimal),
                Value = value.ToString()
            });

            return this;
        }

    }
}

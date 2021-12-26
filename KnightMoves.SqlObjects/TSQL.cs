using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using KnightMoves.SqlObjects.SqlCode;
using KnightMoves.SqlObjects.SqlCode.TSQL;

namespace KnightMoves.SqlObjects
{
    public static class TSQL
    {
        public static TSQLStatement Script(string scriptCode)
        {
            var script = new TSQLScript();

            script.Children.Add(new TSQLLiteral
            {
                DataType = new TSQLDataType(SqlDbType.Variant),
                Value = scriptCode
            });

            return script;
        }

        public static SqlStatement Comment(string commentText, bool singleLineOnly = false)
        {
            return new TSQLScript().Comment(commentText, singleLineOnly);
        }

        public static SqlStatement Comment(IEnumerable<string> commentText, bool singleLineOnly = false)
        {
            return new TSQLScript().Comment(commentText, singleLineOnly);
        }

        public static TSQLStatement SELECT()
        {
            var sqlScript = new TSQLScript();

            var select = new TSQLSelect();

            sqlScript.Children.Add(select);

            return select;
        }

        public static TSQLInnerJoin INNERJOIN(this SqlStatement sqlStatement, string table)
        {
            return INNERJOIN(sqlStatement, string.Empty, table, string.Empty);
        }

        public static TSQLInnerJoin INNERJOIN(this SqlStatement sqlStatement, string table, string multipartIdentifier)
        {
            return INNERJOIN(sqlStatement, string.Empty, table, multipartIdentifier);
        }

        public static TSQLInnerJoin INNERJOIN(this SqlStatement sqlStatement, string schema, string table, string multipartIdentifier)
        {
            var innerJoin = new TSQLInnerJoin { Schema = schema, Table = table, MultipartIdentifier = multipartIdentifier };

            var selectStmt = GetSelectStatement(sqlStatement);

            if (selectStmt != null)
                selectStmt.Parent.Children.Add(innerJoin);

            return innerJoin;
        }

        public static TSQLLeftJoin LEFTJOIN(this SqlStatement sqlStatement, string table)
        {
            return LEFTJOIN(sqlStatement, string.Empty, table, string.Empty);
        }

        public static TSQLLeftJoin LEFTJOIN(this SqlStatement sqlStatement, string table, string multipartIdentifier)
        {
            return LEFTJOIN(sqlStatement, string.Empty, table, multipartIdentifier);
        }

        public static TSQLLeftJoin LEFTJOIN(this SqlStatement sqlStatement, string schema, string table, string multipartIdentifier)
        {
            var leftJoin = new TSQLLeftJoin { Schema = schema, Table = table, MultipartIdentifier = multipartIdentifier };

            var selectStmt = GetSelectStatement(sqlStatement);

            if (selectStmt != null)
                selectStmt.Parent.Children.Add(leftJoin);

            return leftJoin;
        }

        public static TSQLRightJoin RIGHTJOIN(this SqlStatement sqlStatement, string table)
        {
            return RIGHTJOIN(sqlStatement, string.Empty, table, string.Empty);
        }

        public static TSQLRightJoin RIGHTJOIN(this SqlStatement sqlStatement, string table, string multipartIdentifier)
        {
            return RIGHTJOIN(sqlStatement, string.Empty, table, multipartIdentifier);
        }

        public static TSQLRightJoin RIGHTJOIN(this SqlStatement sqlStatement, string schema, string table, string multipartIdentifier)
        {
            var rightJoin = new TSQLRightJoin { Schema = schema, Table = table, MultipartIdentifier = multipartIdentifier };

            var selectStmt = GetSelectStatement(sqlStatement);

            if (selectStmt != null)
                selectStmt.Parent.Children.Add(rightJoin);

            return rightJoin;
        }

        public static TSQLStatement CASE(this SqlStatement sqlStatement, SqlDbType returnType)
        {
            var caseBegin = new TSQLCase { DataType = new TSQLDataType(returnType) };

            var parent = sqlStatement.GetExpressionParent();

            var leftOperand = (sqlStatement as TSQLStatement).GetLeftOperand() as SqlStatement;

            // If leftOperand exists then this case statement should be on the right side of a condition
            // so we build the condition and then return the case statement. Otherwise proceed normally.
            if (leftOperand != null && leftOperand.ComparisonOperator.HasValue)
            {
                var basicCondition = new TSQLBasicCondition { Operator = leftOperand.ComparisonOperator.Value };

                parent.Children.Add(basicCondition);

                basicCondition.Children.Add(leftOperand);
                basicCondition.Children.Add(caseBegin);

                sqlStatement.ClearTempValues();

                return caseBegin;
            }

            parent.Children.Add(caseBegin);

            return caseBegin;
        }


        public static TSQLStatement WHEN(this SqlStatement sqlStatement, string operand)
        {
            var caseWhen = new TSQLCaseWhen 
            { 
                LeftOperand = new TSQLLiteral
                {
                    DataType = new TSQLDataType(SqlDbType.VarChar),
                    Value = operand
                }
            };

            var parent = sqlStatement.GetConditionParent();

            parent.Children.Add(caseWhen);

            return caseWhen;
        }

        public static TSQLStatement WHEN(this SqlStatement sqlStatement, string multipartIdentifier, string column)
        {
            var caseWhen = new TSQLCaseWhen 
            { 
                LeftOperand = new TSQLColumn
                {
                    MultiPartIdentifier = multipartIdentifier,
                    ColumnName = column
                }
            };

            var parent = sqlStatement.GetConditionParent();

            parent.Children.Add(caseWhen);

            return caseWhen;
        }


        public static TSQLStatement THEN(this SqlStatement sqlStatement, TSQLLiteral literal)
        {
            if (!(sqlStatement is TSQLStatement))
                throw new InvalidOperationException($"Cannot invoke {nameof(THEN)} from object of type {sqlStatement.GetType().Name}. It must be invoked form an object of type {nameof(TSQLStatement)} returned by methods such as {nameof(WHEN)}");

            var parent = sqlStatement.Parent as TSQLStatement;

            var caseExpression = sqlStatement as ITSQLCaseExpression;

            if (caseExpression != null && caseExpression.DataType == null)
                caseExpression.DataType = literal.DataType;

            sqlStatement.Children.Add(literal);

            return parent;
        }

        public static TSQLStatement THEN(this SqlStatement sqlStatement, string value)
        {
            if (!(sqlStatement is TSQLStatement))
                throw new InvalidOperationException($"Cannot invoke {nameof(THEN)} from object of type {sqlStatement.GetType().Name}. It must be invoked form an object of type {nameof(TSQLStatement)} returned by methods such as {nameof(WHEN)}");

            var returnType = new TSQLDataType(SqlDbType.VarChar);

            var parent = sqlStatement.Parent as TSQLStatement;

            var caseExpression = sqlStatement as ITSQLCaseExpression;

            if (caseExpression != null && caseExpression.DataType == null)
                caseExpression.DataType = returnType;

            sqlStatement.Children.Add(new TSQLLiteral
            {
                DataType = returnType,
                Value = value
            });

            return parent;
        }

        public static TSQLStatement THEN(this SqlStatement sqlStatement, string multipartIdentifier, string column)
        {
            if (!(sqlStatement is TSQLStatement))
                throw new InvalidOperationException($"Cannot invoke {nameof(THEN)} from object of type {sqlStatement.GetType().Name}. It must be invoked form an object of type {nameof(TSQLStatement)} returned by methods such as {nameof(WHEN)}");

            var returnType = new TSQLDataType(SqlDbType.VarChar);

            var parent = sqlStatement.Parent as TSQLStatement;

            var caseExpression = sqlStatement as ITSQLCaseExpression;

            if (caseExpression != null && caseExpression.DataType == null)
                caseExpression.DataType = returnType;

            sqlStatement.Children.Add(new TSQLColumn
            {
                DataType = returnType,
                MultiPartIdentifier = multipartIdentifier,
                ColumnName = column
            });

            return parent;
        }

        public static TSQLStatement THEN(this SqlStatement sqlStatement, int value)
        {
            if (!(sqlStatement is TSQLStatement))
                throw new InvalidOperationException($"Cannot invoke {nameof(THEN)} from object of type {sqlStatement.GetType().Name}. It must be invoked form an object of type {nameof(TSQLStatement)} returned by methods such as {nameof(WHEN)}");

            var returnType = new TSQLDataType(SqlDbType.Int);

            var parent = sqlStatement.Parent as TSQLStatement;

            var caseExpression = sqlStatement as ITSQLCaseExpression;

            if (caseExpression != null && caseExpression.DataType == null)
                caseExpression.DataType = returnType;

            sqlStatement.Children.Add(new TSQLLiteral
            {
                DataType = returnType,
                Value = value.ToString()
            });

            return parent;
        }

        public static TSQLStatement THEN(this SqlStatement sqlStatement, DateTime value)
        {
            if (!(sqlStatement is TSQLStatement))
                throw new InvalidOperationException($"Cannot invoke {nameof(THEN)} from object of type {sqlStatement.GetType().Name}. It must be invoked form an object of type {nameof(TSQLStatement)} returned by methods such as {nameof(WHEN)}");

            var returnType = new TSQLDataType(SqlDbType.DateTime);

            var parent = sqlStatement.Parent as TSQLStatement;

            var caseExpression = sqlStatement as ITSQLCaseExpression;

            if (caseExpression != null && caseExpression.DataType == null)
                caseExpression.DataType = returnType;

            sqlStatement.Children.Add(new TSQLLiteral
            {
                DataType = returnType,
                Value = value.ToString(SqlDataType.SQL_DATE_STRING_FORMAT)
            });

            return parent;
        }

        public static TSQLStatement THEN(this SqlStatement sqlStatement, bool value)
        {
            if (!(sqlStatement is TSQLStatement))
                throw new InvalidOperationException($"Cannot invoke {nameof(THEN)} from object of type {sqlStatement.GetType().Name}. It must be invoked form an object of type {nameof(TSQLStatement)} returned by methods such as {nameof(WHEN)}");

            var returnType = new TSQLDataType(SqlDbType.Bit);

            var parent = sqlStatement.Parent as TSQLStatement;

            var caseExpression = sqlStatement as ITSQLCaseExpression;

            if (caseExpression != null && caseExpression.DataType == null)
                caseExpression.DataType = returnType;

            sqlStatement.Children.Add(new TSQLLiteral
            {
                DataType = returnType,
                Value = value ? "1" : "0"
            });

            return parent;
        }

        public static TSQLStatement THEN(this SqlStatement sqlStatement, long value)
        {
            if (!(sqlStatement is TSQLStatement))
                throw new InvalidOperationException($"Cannot invoke {nameof(THEN)} from object of type {sqlStatement.GetType().Name}. It must be invoked form an object of type {nameof(TSQLStatement)} returned by methods such as {nameof(WHEN)}");

            var returnType = new TSQLDataType(SqlDbType.BigInt);

            var parent = sqlStatement.Parent as TSQLStatement;

            var caseExpression = sqlStatement as ITSQLCaseExpression;

            if (caseExpression != null && caseExpression.DataType == null)
                caseExpression.DataType = returnType;

            sqlStatement.Children.Add(new TSQLLiteral
            {
                DataType = returnType,
                Value = value.ToString()
            });

            return parent;
        }

        public static TSQLStatement THEN(this SqlStatement sqlStatement, Guid value)
        {
            if (!(sqlStatement is TSQLStatement))
                throw new InvalidOperationException($"Cannot invoke {nameof(THEN)} from object of type {sqlStatement.GetType().Name}. It must be invoked form an object of type {nameof(TSQLStatement)} returned by methods such as {nameof(WHEN)}");

            var returnType = new TSQLDataType(SqlDbType.UniqueIdentifier);

            var parent = sqlStatement.Parent as TSQLStatement;

            var caseExpression = sqlStatement as ITSQLCaseExpression;

            if (caseExpression != null && caseExpression.DataType == null)
                caseExpression.DataType = returnType;

            sqlStatement.Children.Add(new TSQLLiteral
            {
                DataType = returnType,
                Value = value.ToString()
            });

            return parent;
        }

        public static TSQLStatement THEN(this SqlStatement sqlStatement, char value)
        {
            if (!(sqlStatement is TSQLStatement))
                throw new InvalidOperationException($"Cannot invoke {nameof(THEN)} from object of type {sqlStatement.GetType().Name}. It must be invoked form an object of type {nameof(TSQLStatement)} returned by methods such as {nameof(WHEN)}");

            var returnType = new TSQLDataType(SqlDbType.Char);

            var parent = sqlStatement.Parent as TSQLStatement;

            var caseExpression = sqlStatement as ITSQLCaseExpression;

            if (caseExpression != null && caseExpression.DataType == null)
                caseExpression.DataType = returnType;

            sqlStatement.Children.Add(new TSQLLiteral
            {
                DataType = returnType,
                Value = value.ToString()
            });

            return parent;
        }

        public static TSQLStatement THEN(this SqlStatement sqlStatement, decimal value)
        {
            if (!(sqlStatement is TSQLStatement))
                throw new InvalidOperationException($"Cannot invoke {nameof(THEN)} from object of type {sqlStatement.GetType().Name}. It must be invoked form an object of type {nameof(TSQLStatement)} returned by methods such as {nameof(WHEN)}");

            var returnType = new TSQLDataType(SqlDbType.Decimal);

            var parent = sqlStatement.Parent as TSQLStatement;

            var caseExpression = sqlStatement as ITSQLCaseExpression;

            if (caseExpression != null && caseExpression.DataType == null)
                caseExpression.DataType = returnType;

            sqlStatement.Children.Add(new TSQLLiteral
            {
                DataType = returnType,
                Value = value.ToString()
            });

            return parent;
        }

        public static TSQLStatement ELSE(this SqlStatement sqlStatement, ISqlLiteral literal)
        {
            if (!(sqlStatement is TSQLStatement))
                throw new InvalidOperationException($"Cannot invoke {nameof(ELSE)} from object of type {sqlStatement.GetType().Name}. It must be invoked form an object of type {nameof(TSQLStatement)} returned by methods such as {nameof(THEN)}");

            if (literal.DataType == null)
                throw new ArgumentException($"The {nameof(literal.DataType)} property cannot be null for argument {nameof(literal)}");

            var returnType = new TSQLDataType(SqlDbType.VarChar);

            var caseElse = new TSQLCaseElse
            {
                DataType = returnType,
                Result = literal
            };

            sqlStatement.Children.Add(caseElse);

            return sqlStatement as TSQLStatement;
        }

        public static TSQLStatement ELSE(this SqlStatement sqlStatement, string value)
        {
            if (!(sqlStatement is TSQLStatement))
                throw new InvalidOperationException($"Cannot invoke {nameof(ELSE)} from object of type {sqlStatement.GetType().Name}. It must be invoked form an object of type {nameof(TSQLStatement)} returned by methods such as {nameof(THEN)}");

            var returnType = new TSQLDataType(SqlDbType.VarChar);

            var caseElse = new TSQLCaseElse
            {
                DataType = returnType,
                Result = new TSQLLiteral
                {
                    DataType = returnType,
                    Value = value
                }
            };

            sqlStatement.Children.Add(caseElse);

            return sqlStatement as TSQLStatement;
        }

        public static TSQLStatement ELSE(this SqlStatement sqlStatement, string multipartIdentifier, string column)
        {
            if (!(sqlStatement is TSQLStatement))
                throw new InvalidOperationException($"Cannot invoke {nameof(ELSE)} from object of type {sqlStatement.GetType().Name}. It must be invoked form an object of type {nameof(TSQLStatement)} returned by methods such as {nameof(THEN)}");

            var returnType = new TSQLDataType(SqlDbType.VarChar);

            var caseElse = new TSQLCaseElse
            {
                DataType = returnType,
                Result = new TSQLColumn
                {
                    DataType = returnType,
                    MultiPartIdentifier = multipartIdentifier,
                    ColumnName = column
                }
            };

            sqlStatement.Children.Add(caseElse);

            return sqlStatement as TSQLStatement;
        }

        public static TSQLStatement ELSE(this SqlStatement sqlStatement, int value)
        {
            if (!(sqlStatement is TSQLStatement))
                throw new InvalidOperationException($"Cannot invoke {nameof(ELSE)} from object of type {sqlStatement.GetType().Name}. It must be invoked form an object of type {nameof(TSQLStatement)} returned by methods such as {nameof(THEN)}");

            var returnType = new TSQLDataType(SqlDbType.Int);

            var caseElse = new TSQLCaseElse
            {
                DataType = returnType,
                Result = new TSQLLiteral
                {
                    DataType = returnType,
                    Value = value.ToString()
                }
            };

            sqlStatement.Children.Add(caseElse);

            return sqlStatement as TSQLStatement;
        }

        public static TSQLStatement ELSE(this SqlStatement sqlStatement, DateTime value)
        {
            if (!(sqlStatement is TSQLStatement))
                throw new InvalidOperationException($"Cannot invoke {nameof(ELSE)} from object of type {sqlStatement.GetType().Name}. It must be invoked form an object of type {nameof(TSQLStatement)} returned by methods such as {nameof(THEN)}");

            var returnType = new TSQLDataType(SqlDbType.DateTime);

            var caseElse = new TSQLCaseElse
            {
                DataType = returnType,
                Result = new TSQLLiteral
                {
                    DataType = returnType,
                    Value = value.ToString(SqlDataType.SQL_DATE_STRING_FORMAT)
                }
            };

            sqlStatement.Children.Add(caseElse);

            return sqlStatement as TSQLStatement;
        }

        public static TSQLStatement ELSE(this SqlStatement sqlStatement, bool value)
        {
            if (!(sqlStatement is TSQLStatement))
                throw new InvalidOperationException($"Cannot invoke {nameof(ELSE)} from object of type {sqlStatement.GetType().Name}. It must be invoked form an object of type {nameof(TSQLStatement)} returned by methods such as {nameof(THEN)}");

            var returnType = new TSQLDataType(SqlDbType.Bit);

            var caseElse = new TSQLCaseElse
            {
                DataType = returnType,
                Result = new TSQLLiteral
                {
                    DataType = returnType,
                    Value = value ? "1" : "0"
                }
            };

            sqlStatement.Children.Add(caseElse);

            return sqlStatement as TSQLStatement;
        }

        public static TSQLStatement ELSE(this SqlStatement sqlStatement, long value)
        {
            if (!(sqlStatement is TSQLStatement))
                throw new InvalidOperationException($"Cannot invoke {nameof(ELSE)} from object of type {sqlStatement.GetType().Name}. It must be invoked form an object of type {nameof(TSQLStatement)} returned by methods such as {nameof(THEN)}");

            var returnType = new TSQLDataType(SqlDbType.BigInt);

            var caseElse = new TSQLCaseElse
            {
                DataType = returnType,
                Result = new TSQLLiteral
                {
                    DataType = returnType,
                    Value = value.ToString()
                }
            };

            sqlStatement.Children.Add(caseElse);

            return sqlStatement as TSQLStatement;
        }

        public static TSQLStatement ELSE(this SqlStatement sqlStatement, Guid value)
        {
            if (!(sqlStatement is TSQLStatement))
                throw new InvalidOperationException($"Cannot invoke {nameof(ELSE)} from object of type {sqlStatement.GetType().Name}. It must be invoked form an object of type {nameof(TSQLStatement)} returned by methods such as {nameof(THEN)}");

            var returnType = new TSQLDataType(SqlDbType.UniqueIdentifier);

            var caseElse = new TSQLCaseElse
            {
                DataType = returnType,
                Result = new TSQLLiteral
                {
                    DataType = returnType,
                    Value = value.ToString()
                }
            };

            sqlStatement.Children.Add(caseElse);

            return sqlStatement as TSQLStatement;
        }

        public static TSQLStatement ELSE(this SqlStatement sqlStatement, char value)
        {
            if (!(sqlStatement is TSQLStatement))
                throw new InvalidOperationException($"Cannot invoke {nameof(ELSE)} from object of type {sqlStatement.GetType().Name}. It must be invoked form an object of type {nameof(TSQLStatement)} returned by methods such as {nameof(THEN)}");

            var returnType = new TSQLDataType(SqlDbType.Char);

            var caseElse = new TSQLCaseElse
            {
                DataType = returnType,
                Result = new TSQLLiteral
                {
                    DataType = returnType,
                    Value = value.ToString()
                }
            };

            sqlStatement.Children.Add(caseElse);

            return sqlStatement as TSQLStatement;
        }

        public static TSQLStatement ELSE(this SqlStatement sqlStatement, decimal value)
        {
            if (!(sqlStatement is TSQLStatement))
                throw new InvalidOperationException($"Cannot invoke {nameof(ELSE)} from object of type {sqlStatement.GetType().Name}. It must be invoked form an object of type {nameof(TSQLStatement)} returned by methods such as {nameof(THEN)}");

            var returnType = new TSQLDataType(SqlDbType.Decimal);

            var caseElse = new TSQLCaseElse
            {
                DataType = returnType,
                Result = new TSQLLiteral
                {
                    DataType = returnType,
                    Value = value.ToString()
                }
            };

            sqlStatement.Children.Add(caseElse);

            return sqlStatement as TSQLStatement;
        }


        public static SqlStatement END(this SqlStatement sqlStatement)
        {
            var parent = sqlStatement.GetExpressionParent();

            // If Left Operand values have been supplied then the CASE statement is meant to be on the right
            // side of the condition, so we build the condition and add the CASE statement on the right
            if ((parent?.LeftOperand != null || !string.IsNullOrEmpty(parent?.LeftOperandValue)) && parent.ComparisonOperator.HasValue)
            {
                ISqlQueryExpression leftOperand;

                if (parent.LeftOperand != null)
                {
                    leftOperand = parent.LeftOperand;
                }
                else if (!string.IsNullOrEmpty(parent.LeftMultiPartIdentifier))
                {
                    leftOperand = new TSQLColumn { MultiPartIdentifier = parent.LeftMultiPartIdentifier, ColumnName = parent.LeftOperandValue };
                }
                else
                {
                    leftOperand = new TSQLLiteral
                    {
                        DataType = new TSQLDataType(SqlDbType.VarChar),
                        Value = parent.LeftOperandValue
                    };
                }

                SqlStatement caseStmt = parent.Children.LastOrDefault(c => c is ITSQLCase) as SqlStatement;

                var basicCondition = new TSQLBasicCondition { Operator = parent.ComparisonOperator.Value };

                basicCondition.Children.Add(leftOperand as SqlStatement);
                basicCondition.Children.Add(caseStmt);

                caseStmt.Parent = basicCondition;

                // Re-initialize stuff
                parent.Children.Remove(caseStmt);
                parent.ClearTempValues();

                parent.Children.Add(basicCondition);

                return basicCondition;
            }

            return sqlStatement;
        }

        public static SqlStatement Literal(string literal)
        {
            var sqlScript = new TSQLScript();

            var literalObj = new TSQLLiteral
            {
                DataType = new TSQLDataType(SqlDbType.Variant),
                Value = literal
            };

            sqlScript.Children.Add(literalObj);

            return literalObj;
        }

        public static SqlStatement DATENAME(this SqlStatement sqlStatement, DateParts datePart, DateTime dateExpression)
        {
            var func = new TSQLFuncDateName();

            func.SetParameterValue("datePart", new TSQLLiteral
            {
                DataType = new TSQLDataType(SqlDbType.Variant),
                Value = datePart.ToString()
            });

            func.SetParameterValue("dateExpression", new TSQLLiteral
            {
                DataType = new TSQLDataType(SqlDbType.DateTime),
                Value = dateExpression.ToString(SqlDataType.SQL_DATE_STRING_FORMAT)
            });

            var parent = sqlStatement.GetExpressionParent();

            parent.Children.Add(func);

            return func;
        }

        public static SqlStatement DATENAME(this SqlStatement sqlStatement, DateParts datePart, string dateExpression)
        {
            var func = new TSQLFuncDateName();

            func.SetParameterValue("datePart", new TSQLLiteral
            {
                DataType = new TSQLDataType(SqlDbType.Variant),
                Value = datePart.ToString()
            });

            func.SetParameterValue("dateExpression", new TSQLLiteral
            {
                DataType = new TSQLDataType(SqlDbType.Variant),
                Value = dateExpression
            });

            var parent = sqlStatement.GetExpressionParent();

            parent.Children.Add(func);

            return func;
        }

        public static SqlStatement DATENAME(this SqlStatement sqlStatement, DateParts datePart, string multipartIdentifier, string column)
        {
            var func = new TSQLFuncDateName();

            func.SetParameterValue("datePart", new TSQLLiteral
            {
                DataType = new TSQLDataType(SqlDbType.Variant),
                Value = datePart.ToString()
            });

            func.SetParameterValue("dateExpression", new TSQLColumn
            {
                DataType = new TSQLDataType(SqlDbType.Variant),
                MultiPartIdentifier = multipartIdentifier,
                ColumnName = column
            });

            var parent = sqlStatement.GetExpressionParent();

            parent.Children.Add(func);

            return func;
        }




        public static SqlStatement DATEPART(this SqlStatement sqlStatement, DateParts datePart, DateTime dateExpression)
        {
            var func = new TSQLFuncDatePart();

            func.SetParameterValue("datePart", new TSQLLiteral
            {
                DataType = new TSQLDataType(SqlDbType.Variant),
                Value = datePart.ToString()
            });

            func.SetParameterValue("dateExpression", new TSQLLiteral
            {
                DataType = new TSQLDataType(SqlDbType.DateTime),
                Value = dateExpression.ToString(SqlDataType.SQL_DATE_STRING_FORMAT)
            });

            var parent = sqlStatement.GetExpressionParent();

            parent.Children.Add(func);

            return func;
        }

        public static SqlStatement DATEPART(this SqlStatement sqlStatement, DateParts datePart, string dateExpression)
        {
            var func = new TSQLFuncDatePart();

            func.SetParameterValue("datePart", new TSQLLiteral
            {
                DataType = new TSQLDataType(SqlDbType.Variant),
                Value = datePart.ToString()
            });

            func.SetParameterValue("dateExpression", new TSQLLiteral
            {
                DataType = new TSQLDataType(SqlDbType.Variant),
                Value = dateExpression
            });

            var parent = sqlStatement.GetExpressionParent();

            parent.Children.Add(func);

            return func;
        }

        public static SqlStatement DATEPART(this SqlStatement sqlStatement, DateParts datePart, string multipartIdentifier, string column)
        {
            var func = new TSQLFuncDatePart();

            func.SetParameterValue("datePart", new TSQLLiteral
            {
                DataType = new TSQLDataType(SqlDbType.Variant),
                Value = datePart.ToString()
            });

            func.SetParameterValue("dateExpression", new TSQLColumn
            {
                DataType = new TSQLDataType(SqlDbType.Variant),
                MultiPartIdentifier = multipartIdentifier,
                ColumnName = column
            });

            var parent = sqlStatement.GetExpressionParent();

            parent.Children.Add(func);

            return func;
        }

        public static SqlStatement GETDATE(this SqlStatement sqlStatement)
        {
            var func = new TSQLFuncGetDate();

            var parent = sqlStatement.GetExpressionParent();

            parent.Children.Add(func);

            return func;
        }

        public static SqlStatement INSERT()
        {
            var sqlScript = new TSQLScript();

            var insert = new TSQLInsert();

            sqlScript.Children.Add(insert);

            return insert;
        }

        public static SqlStatement UPDATE(string table)
        {
            return UPDATE(string.Empty, table);
        }

        public static SqlStatement UPDATE(string schema, string table)
        {
            var sqlScript = new TSQLScript();

            var update = new TSQLUpdate(schema, table);

            sqlScript.Children.Add(update);

            return update;
        }

        public static SqlStatement DELETE()
        {
            var sqlScript = new TSQLScript();

            var delete = new TSQLDelete();

            sqlScript.Children.Add(delete);

            return delete;
        }

        private static SqlStatement GetSelectStatement(SqlStatement sqlStatement)
        {
            SqlStatement selectStmt = null;

            // Check the current SqlStatement
            if (sqlStatement.IsSelect)
                selectStmt = sqlStatement;

            // Search the Siblings
            if (selectStmt == null)
                selectStmt = sqlStatement.Siblings.SingleOrDefault(s => s is ISqlSelect) as SqlStatement;

            // Search up the tree
            if (selectStmt == null)
            {
                sqlStatement.ProcessAncestors(
                    a =>
                    {
                        if (a is ISqlSelect)
                            selectStmt = a as SqlStatement;

                        return true;
                    },
                    sqlStatement,
                    a => selectStmt != null
                );
            }

            // Search from the Root down
            if (selectStmt == null)
            {
                sqlStatement.Root.ProcessChildren(a =>
                {
                    if (selectStmt == null && a is ISqlSelect)
                        selectStmt = a as SqlStatement;

                    return true;
                });
            }

            return selectStmt;
        }
    }
}

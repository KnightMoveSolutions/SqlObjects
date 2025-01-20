using System;
using System.Data;
using KnightMoves.SqlObjects.SqlCode;
using KnightMoves.SqlObjects.SqlCode.TSQL;
using Xunit;

namespace KnightMoves.SqlObjects.Tests.TSql;

public class TSQLOrderByExpression_Tests
{
    private readonly string NL = Environment.NewLine;

    [Fact]
    public void SQL_Returns_Aliased_Calculation()
    {
        // ARRANGE
        var expression = new TSQLOrderByExpression();

        expression.Children.Add(
            new TSQLCalculation
            {
                LeftOperand = new TSQLLiteral { DataType = new TSQLDataType(SqlDbType.Int), Value = "9999" },
                Operator = SqlArithmeticOperators.Plus,
                RightOperand = new TSQLLiteral { DataType = new TSQLDataType(SqlDbType.Int), Value = "1234" },
                Alias = "Result"
            }
        );

        // ACTION
        var sql = expression.SQL();

        var expected = $" [Result] ASC";

        // ASSERT
        Assert.Equal(expected, sql);
        TestHelper.Assert.SerializationWorks(expected, expression);
    }

    [Fact]
    public void SQL_Returns_Aliased_Column()
    {
        // ARRANGE
        var expression = new TSQLOrderByExpression();

        expression.Children.Add(new TSQLColumn { ColumnName = "ColumnA", Alias = "ColA" });

        // ACTION
        var sql = expression.SQL();

        var expected = $" [ColA] ASC";

        // ASSERT
        Assert.Equal(expected, sql);
        TestHelper.Assert.SerializationWorks(expected, expression);
    }

    [Fact]
    public void SQL_Returns_Aliased_Function()
    {
        // ARRANGE
        var expression = new TSQLOrderByExpression { Direction = SqlOrderByDirections.ASC };

        expression.Children.Add(new TSQLFuncGetDate { Alias = "CurDate" });

        // ACTION
        var sql = expression.SQL();

        var expected = $" [CurDate] ASC";

        // ASSERT
        Assert.Equal(expected, sql);
        TestHelper.Assert.SerializationWorks(expected, expression);
    }

    [Fact]
    public void SQL_Returns_Aliased_Literal()
    {
        // ARRANGE
        var expression = new TSQLOrderByExpression { Direction = SqlOrderByDirections.ASC };

        expression.Children.Add(new TSQLLiteral { Alias = "MyLit" });

        // ACTION
        var sql = expression.SQL();

        var expected = $" [MyLit] ASC";

        // ASSERT
        Assert.Equal(expected, sql);
        TestHelper.Assert.SerializationWorks(expected, expression);
    }

    [Fact]
    public void SQL_Returns_Aliased_SubQuery()
    {
        // ARRANGE
        var subQuery = new TSQLSubQuery { Alias = "Qry" };

        var select = new TSQLSelect();
        select.Children.Add(new TSQLColumn { ColumnName = "ColumnA" });
        
        subQuery.Children.Add(select);

        var from = new TSQLFrom { Schema = "dbo", Table = "MyTable" };
        
        subQuery.Children.Add(from);

        var expression = new TSQLOrderByExpression { Direction = SqlOrderByDirections.ASC };

        expression.Children.Add(subQuery);

        // ACTION
        var sql = expression.SQL();

        var expected = $"  [Qry] ASC";

        // ASSERT
        Assert.Equal(expected, sql);
        TestHelper.Assert.SerializationWorks(expected, expression);
    }

    [Fact]
    public void SQL_Returns_Calculation_ASC()
    {
        // ARRANGE
        var expression = new TSQLOrderByExpression { Direction = SqlOrderByDirections.ASC };

        expression.Children.Add(
            new TSQLCalculation
            {
                LeftOperand = new TSQLLiteral { DataType = new TSQLDataType(SqlDbType.Int), Value = "9999" },
                Operator = SqlArithmeticOperators.Plus,
                RightOperand = new TSQLLiteral { DataType = new TSQLDataType(SqlDbType.Int), Value = "1234" }
            }
        );

        // ACTION
        var sql = expression.SQL();

        var expected = $" (9999 + 1234) ASC";

        // ASSERT
        Assert.Equal(expected, sql);
        TestHelper.Assert.SerializationWorks(expected, expression);
    }

    [Fact]
    public void SQL_Returns_Column_ASC()
    {
        // ARRANGE
        var expression = new TSQLOrderByExpression { Direction = SqlOrderByDirections.ASC };

        expression.Children.Add(new TSQLColumn{ ColumnName = "ColumnA" });

        // ACTION
        var sql = expression.SQL();

        var expected = $" [ColumnA] ASC";

        // ASSERT
        Assert.Equal(expected, sql);
        TestHelper.Assert.SerializationWorks(expected, expression);
    }

    [Fact]
    public void SQL_Returns_Function_ASC()
    {
        // ARRANGE
        var expression = new TSQLOrderByExpression { Direction = SqlOrderByDirections.ASC };

        expression.Children.Add(new TSQLFuncGetDate());

        // ACTION
        var sql = expression.SQL();

        var expected = $" GETDATE() ASC";

        // ASSERT
        Assert.Equal(expected, sql);
        TestHelper.Assert.SerializationWorks(expected, expression);
    }

    [Fact]
    public void SQL_Returns_Literal_ASC()
    {
        // ARRANGE
        var expression = new TSQLOrderByExpression { Direction = SqlOrderByDirections.ASC };

        expression.Children.Add(
            new TSQLLiteral 
            { 
                DataType = new TSQLDataType(SqlDbType.VarChar), 
                Value = "SomeValue" 
            }
        );

        // ACTION
        var sql = expression.SQL();

        var expected = $" 'SomeValue' ASC";

        // ASSERT
        Assert.Equal(expected, sql);
        TestHelper.Assert.SerializationWorks(expected, expression);
    }

    [Fact]
    public void SQL_Returns_SubQuery_ASC()
    {
        // ARRANGE
        var subQuery = new TSQLSubQuery();

        var select = new TSQLSelect();
        select.Children.Add(new TSQLColumn { ColumnName = "ColumnA" });
        subQuery.Children.Add(select);

        var from = new TSQLFrom { Schema = "dbo", Table = "MyTable" };
        subQuery.Children.Add(from);

        var expression = new TSQLOrderByExpression { Direction = SqlOrderByDirections.ASC };

        expression.Children.Add(subQuery);

        // ACTION
        var sql = expression.SQL();

        var expected = $"  ({NL}" + 
                       $"   SELECT{NL}" +
                       $"    [ColumnA]{NL}" +
                       $"   FROM [dbo].[MyTable]{NL}" +
                       $"  ) ASC";

        // ASSERT
        Assert.Equal(expected, sql);
        TestHelper.Assert.SerializationWorks(expected, expression);
    }

    [Fact]
    public void SQL_Returns_Calculation_DESC()
    {
        // ARRANGE
        var expression = new TSQLOrderByExpression { Direction = SqlOrderByDirections.DESC };

        expression.Children.Add(
            new TSQLCalculation
            {
                LeftOperand = new TSQLLiteral { DataType = new TSQLDataType(SqlDbType.Int), Value = "9999" },
                Operator = SqlArithmeticOperators.Plus,
                RightOperand = new TSQLLiteral { DataType = new TSQLDataType(SqlDbType.Int), Value = "1234" }
            }
        );

        // ACTION
        var sql = expression.SQL();

        var expected = $" (9999 + 1234) DESC";

        // ASSERT
        Assert.Equal(expected, sql);
        TestHelper.Assert.SerializationWorks(expected, expression);
    }

    [Fact]
    public void SQL_Returns_Column_DESC()
    {
        // ARRANGE
        var expression = new TSQLOrderByExpression { Direction = SqlOrderByDirections.DESC };

        expression.Children.Add(new TSQLColumn { ColumnName = "ColumnA" });

        // ACTION
        var sql = expression.SQL();

        var expected = $" [ColumnA] DESC";

        // ASSERT
        Assert.Equal(expected, sql);
        TestHelper.Assert.SerializationWorks(expected, expression);
    }

    [Fact]
    public void SQL_Returns_Function_DESC()
    {
        // ARRANGE
        var expression = new TSQLOrderByExpression { Direction = SqlOrderByDirections.DESC };

        expression.Children.Add(new TSQLFuncGetDate());

        // ACTION
        var sql = expression.SQL();

        var expected = $" GETDATE() DESC";

        // ASSERT
        Assert.Equal(expected, sql);
        TestHelper.Assert.SerializationWorks(expected, expression);
    }

    [Fact]
    public void SQL_Returns_Literal_DESC()
    {
        // ARRANGE
        var expression = new TSQLOrderByExpression { Direction = SqlOrderByDirections.DESC };

        expression.Children.Add(
            new TSQLLiteral 
            { 
                DataType = new TSQLDataType(SqlDbType.VarChar), 
                Value = "SomeValue" 
            }
        );

        // ACTION
        var sql = expression.SQL();

        var expected = $" 'SomeValue' DESC";

        // ASSERT
        Assert.Equal(expected, sql);
        TestHelper.Assert.SerializationWorks(expected, expression);
    }

    [Fact]
    public void SQL_Returns_SubQuery_DESC()
    {
        // ARRANGE
        var subQuery = new TSQLSubQuery();

        var select = new TSQLSelect();
        select.Children.Add(new TSQLColumn { ColumnName = "ColumnA" });
        subQuery.Children.Add(select);

        var from = new TSQLFrom { Schema = "dbo", Table = "MyTable" };
        subQuery.Children.Add(from);

        var expression = new TSQLOrderByExpression { Direction = SqlOrderByDirections.DESC };

        expression.Children.Add(subQuery);

        // ACTION
        var sql = expression.SQL();

        var expected = $"  ({NL}" +
                       $"   SELECT{NL}" +
                       $"    [ColumnA]{NL}" +
                       $"   FROM [dbo].[MyTable]{NL}" +
                       $"  ) DESC";

        // ASSERT
        Assert.Equal(expected, sql);
        TestHelper.Assert.SerializationWorks(expected, expression);
    }
}

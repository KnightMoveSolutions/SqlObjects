using System;
using KnightMoves.SqlObjects.SqlCode;
using KnightMoves.SqlObjects.SqlCode.TSQL;
using Xunit;

namespace KnightMoves.SqlObjects.Tests.TSql;

public class TSQLOrderBy_Tests
{
    private readonly string NL = Environment.NewLine;

    [Fact]
    public void SQL_Returns_OrderBy_With_Expressions()
    {
        // ARRANGE
        var colAExpression = new TSQLOrderByExpression { Direction = SqlOrderByDirections.DESC };
        var colBExpression = new TSQLOrderByExpression { Direction = SqlOrderByDirections.ASC };
        var funcExpression = new TSQLOrderByExpression();

        colAExpression.Children.Add(new TSQLColumn { ColumnName = "ColumnA", Alias = "ColA" });
        colBExpression.Children.Add(new TSQLColumn { ColumnName = "ColumnB" });
        funcExpression.Children.Add(new TSQLFuncGetDate { Alias = "CurDate" });

        var orderBy = new TSQLOrderBy();

        orderBy.Children.Add(colAExpression);
        orderBy.Children.Add(colBExpression);
        orderBy.Children.Add(funcExpression);

        // ACTION 
        var sql = orderBy.SQL();

        var expected = $" ORDER BY{NL}" +
                       $"  [ColA] DESC,{NL}" +
                       $"  [ColumnB] ASC,{NL}" +
                       $"  [CurDate] ASC{NL}";

        // ASSERT
        Assert.Equal(expected, sql);
        TestHelper.Assert.SerializationWorks(expected, orderBy);
    }

    [Fact]
    public void SQL_Returns_No_Expressions_Found()
    {
        // ARRANGE
        var orderBy = new TSQLOrderBy();

        // ACTION 
        var sql = orderBy.SQL();

        var expected = $" -- No ORDER BY expressions found";

        // ASSERT
        Assert.Equal(expected, sql);
        TestHelper.Assert.SerializationWorks(expected, orderBy);
    }

    [Fact]
    public void SQL_Returns_OrderBy_With_Expressions_and_Comment()
    {
        // ARRANGE
        var colAExpression = new TSQLOrderByExpression { Direction = SqlOrderByDirections.DESC };
        var colBExpression = new TSQLOrderByExpression { Direction = SqlOrderByDirections.ASC };
        var funcExpression = new TSQLOrderByExpression();

        colAExpression.Children.Add(new TSQLColumn { ColumnName = "ColumnA", Alias = "ColA" });
        colBExpression.Children.Add(new TSQLColumn { ColumnName = "ColumnB" });
        funcExpression.Children.Add(new TSQLFuncGetDate { Alias = "CurDate" });

        var orderBy = new TSQLOrderBy();

        orderBy.Children.Add(colAExpression);
        orderBy.Children.Add(new TSQLComment { CommentText = "Comment about ColumnB below"});
        orderBy.Children.Add(colBExpression);
        orderBy.Children.Add(funcExpression);

        // ACTION 
        var sql = orderBy.SQL();

        var expected = $" ORDER BY{NL}" +
                       $"  [ColA] DESC,{NL}" +
                       $"  -- Comment about ColumnB below{NL}" +
                       $"  [ColumnB] ASC,{NL}" +
                       $"  [CurDate] ASC{NL}";

        // ASSERT
        Assert.Equal(expected, sql);
        TestHelper.Assert.SerializationWorks(expected, orderBy);
    }
}

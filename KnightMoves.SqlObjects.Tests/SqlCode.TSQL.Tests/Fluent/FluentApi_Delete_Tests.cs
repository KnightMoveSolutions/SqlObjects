using System;
using Xunit;

namespace KnightMoves.SqlObjects.Tests.TSql.Fluent;

public class FluentApi_Delete_Tests
{
    private readonly string NL = Environment.NewLine;

    [Fact]
    public void Delete_From_Table()
    {
        // ARRANGE
        var sqlObj = TSQL
            .DELETE().FROM("SomeTable")
            .WHERE("Id").IsEqualTo(99);

        // ACTION
        var sql = sqlObj.Build();

        var expected = $"  DELETE FROM [SomeTable]{NL}" +
                       $"  WHERE 1=1{NL}" +
                       $"   AND [Id] = 99{NL}";

        // ASSERT
        Assert.Equal(expected, sql);
        TestHelper.Assert.SerializationWorks(expected, sqlObj);
    }

    [Fact]
    public void Delete_From_Schema_Table()
    {
        // ARRANGE
        var sqlObj = TSQL
            .DELETE().FROM("dbo", "SomeTable")
            .WHERE("Id").IsEqualTo(99);

        // ACTION
        var sql = sqlObj.Build();

        var expected = $"  DELETE FROM [dbo].[SomeTable]{NL}" +
                       $"  WHERE 1=1{NL}" +
                       $"   AND [Id] = 99{NL}";

        // ASSERT
        Assert.Equal(expected, sql);
        TestHelper.Assert.SerializationWorks(expected, sqlObj);
    }
}

/*
    THE LICENSED WORK IS PROVIDED UNDER THE TERMS OF THE DEVELOPER TOOL LIMITED 
    LICENSE (“LICENSE”) AS FIRST COMPLETED BY THE ORIGINAL AUTHOR. ANY USE, PUBLIC 
    DISPLAY, PUBLIC PERFORMANCE, REPRODUCTION OR DISTRIBUTION OF, OR PREPARATION OF 
    DERIVATIVE WORKS BASED ON THE LICENSED WORK CONSTITUTES RECIPIENT’S ACCEPTANCE 
    OF THIS LICENSE AND ITS TERMS, WHETHER OR NOT SUCH RECIPIENT READS THE TERMS OF THE 
    LICENSE. “LICENSED WORK” AND “RECIPIENT” ARE DEFINED IN THE LICENSE. A COPY OF THE 
    LICENSE IS LOCATED IN THE TEXT FILE ENTITLED “LICENSE.TXT” ACCOMPANYING THE CONTENTS 
    OF THIS FILE. IF A COPY OF THE LICENSE DOES NOT ACCOMPANY THIS FILE, A COPY OF THE 
    LICENSE MAY ALSO BE OBTAINED AT THE FOLLOWING WEB SITE:  
 
    https://docs.knightmovesolutions.com/sql-objects/license.html
*/

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

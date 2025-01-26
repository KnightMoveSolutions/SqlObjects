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

using KnightMoves.SqlObjects.SqlCode;
using Xunit;

namespace KnightMoves.SqlObjects.Tests.TSql.Fluent;

public class FluentApi_With_Tests
{

    [Fact]
    public void Select_Column_WithId_From_Table()
    {
        // ARRANGE / ACTION 
        var sqlObj =
            TSQL
              .SELECT()
                .COLUMN("FirstColumn")
                .COLUMN("MyColumn").WithId("12345")
              .FROM("Customers");

        // ASSERT
        var lookup = sqlObj.Root.FindById("12345");

        Assert.NotNull(lookup);
        Assert.Equal("12345", lookup.Id);
    }

    [Fact]
    public void Select_Column_WithName_From_Table()
    {
        // ARRANGE / ACTION 
        var sqlObj =
            TSQL
              .SELECT()
                .COLUMN("FirstColumn")
                .COLUMN("MyColumn").WithName("ColSpec")
              .FROM("Customers");

        // ASSERT
        SqlStatement lookup = null;

        sqlObj.Root.ProcessTree(n =>
        {
            if ((n as SqlStatement).Name == "ColSpec")
                lookup = n as SqlStatement;
        });

        Assert.NotNull(lookup);
        Assert.Equal("ColSpec", lookup.Name);
    }
}

using KnightMoves.SqlObjects.SqlCode;

namespace KnightMoves.SqlObjects.Tests;

public class TestHelper
{
    public class Assert
    {
        public static void SerializationWorks(string expected, SqlStatement sqlObj)
        {
            var json = sqlObj.ToJson();
            var deserializedSqlObj = SqlStatement.FromJson(json);
            var sql = deserializedSqlObj.Build();
            Xunit.Assert.Equal(expected, sql);
        }
    }
}

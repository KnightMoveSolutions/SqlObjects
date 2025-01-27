# SqlObjects

A C# wrapper to the SQL language for creating SQL code in .NET projects for use with ADO, Dapper, or other ORMs

## Documentation

[https://docs.knightmovesolutions.com/sql-objects/index.html](https://docs.knightmovesolutions.com/sql-objects/index.html)

## Quick Example

Install the NuGet package

```csharp
PS c:\> Install-Package KnightMoves.SqlObjects
```

Import the namespace

```csharp
using KnightMoves.SqlObjects;
```

Use the Fluent API through the static `TSQL` class

```csharp
   var sql = TSQL

        .SELECT()
          .STAR()
        .FROM("Products")
        .Build()

   ;

   Console.WriteLine(sql);
```

Output:

```sql
     SELECT
      *
     FROM [Products]
```

## License

[LICENSE.txt](./LICENSE.txt)

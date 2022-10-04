.. include:: northwind-db-tip.rst

========
GROUP BY
========

``GROUP BY`` **Auto-Generated From** ``SELECT`` **List**

When calling the ``.GROUPBY()`` method alone, it will derive the items to group by from the 
``SELECT`` list by copying those items that are not aggregate query expressions.

.. code-block:: csharp 

    var sql = TSQL

        .SELECT()
          .COLUMN("c", "CategoryName")
          .COUNT("p", "ProductID").AS("TotalProducts")
          .AVG("p", "UnitPrice").AS("AvgPrice")
        .FROM("dbo", "Products", "p")
        .INNERJOIN("dbo", "Categories", "c").ON("c", "CategoryID").IsEqualTo("p", "CategoryID")
        .GROUPBY()
        .Build()

    ;

    Console.WriteLine(sql);

Output:

.. code-block:: sql

  SELECT
   [c].[CategoryName],
   COUNT([p].[ProductID]) AS [TotalProducts],
   AVG([p].[UnitPrice]) AS [AvgPrice]
  FROM [dbo].[Products] p
  INNER JOIN [dbo].[Categories] c ON [c].[CategoryID] = [p].[CategoryID]
  GROUP BY
   [c].[CategoryName]
   

``GROUP BY`` **Explicitly**

You can choose to specify the items to group by explicitly if you'd like, which produces the same output as above.

.. code-block:: csharp 

    var sql = TSQL

        .SELECT()
          .COLUMN("c", "CategoryName")
          .COUNT("p", "ProductID").AS("TotalProducts")
          .AVG("p", "UnitPrice").AS("AvgPrice")
        .FROM("dbo", "Products", "p")
        .INNERJOIN("dbo", "Categories", "c").ON("c", "CategoryID").IsEqualTo("p", "CategoryID")
        .GROUPBY()
          .COLUMN("c", "CategoryName")
        .Build()

    ;

    Console.WriteLine(sql);

Output:

.. code-block:: sql

  SELECT
   [c].[CategoryName],
   COUNT([p].[ProductID]) AS [TotalProducts],
   AVG([p].[UnitPrice]) AS [AvgPrice]
  FROM [dbo].[Products] p
  INNER JOIN [dbo].[Categories] c ON [c].[CategoryID] = [p].[CategoryID]
  GROUP BY
   [c].[CategoryName]

``GROUP BY`` **With Complex Query Expressions**

In the example below, even complex query expressions are automatically copied from the ``SELECT`` list

.. code-block:: csharp 

    var sql = TSQL

        .SELECT()
          .COLUMN("c", "CategoryName")
          .COUNT("p", "ProductID").AS("TotalProducts")
          .CASE(returnType: SqlDbType.VarChar, "p", "UnitsInStock")
            .WHEN(0).THEN("OutOfStock")
            .ELSE("InStock")
          .END().AS("Inventory")
          .AVG("p", "UnitPrice").AS("AvgPrice")
        .FROM("dbo", "Products", "p")
        .INNERJOIN("dbo", "Categories", "c").ON("c", "CategoryID").IsEqualTo("p", "CategoryID")
        .GROUPBY()
        .Build()

    ;

    Console.WriteLine(sql);

Output:

.. code-block:: sql

  SELECT
   [c].[CategoryName],
   COUNT([p].[ProductID]) AS [TotalProducts],
   CASE [p].[UnitsInStock]
    WHEN 0 THEN 'OutOfStock'
    ELSE 'InStock'
   END AS [Inventory],
   AVG([p].[UnitPrice]) AS [AvgPrice]
  FROM [dbo].[Products] p
  INNER JOIN [dbo].[Categories] c ON [c].[CategoryID] = [p].[CategoryID]
  GROUP BY
   [c].[CategoryName],
   CASE [p].[UnitsInStock]
    WHEN 0 THEN 'OutOfStock'
    ELSE 'InStock'
   END


.. include:: northwind-db-tip.rst

==========
Subqueries
==========

**Subquery In a SELECT List**

| ``.SubQueryStart()``
|   Subquery goes here
| ``.SubQueryEnd().AS("Name")``

.. code-block:: csharp 

    var sql = TSQL

        .SELECT()
          .COLUMN("c", "CategoryID")
          .COLUMN("c", "CategoryName")
          .SubQueryStart()
            .SELECT()
              .COUNT("*")
            .FROM("dbo", "Products", "p")
            .WHERE()
              .COLUMN("p", "CategoryID").IsEqualTo("c", "CategoryID").AND()
              .COLUMN("p", "Discontinued").IsEqualTo(false)
          .SubQueryEnd().AS("ActiveProducts")
        .FROM("dbo", "Categories", "c")
        .Build()

    ;

    Console.WriteLine(sql);

Output:

.. code-block:: sql

  SELECT
   [c].[CategoryID],
   [c].[CategoryName],
   (
    SELECT
     COUNT(*)
    FROM [dbo].[Products] p
    WHERE 1=1
     AND [p].[CategoryID] = [c].[CategoryID]
     AND [p].[Discontinued] = 0
   ) AS [ActiveProducts]
  FROM [dbo].[Categories] c
   
:doc:`where-1-equals-1`

**SELECT FROM a Subquery**

.. code-block:: csharp 

    var sql = TSQL

        .SELECT()
          .COLUMN("us", "EmployeeID")
          .COLUMN("us", "FullName")
          .COLUMN("us", "FullAddress")
        .FROM()
          .SubQueryStart()
            .SELECT()
              .Literal("[TitleOfCourtesy] + ' ' + [FirstName] + ' ' + [LastName] AS [FullName]")
              .Literal("[Address] + ' ' + [City] + ', ' + [Region] + ' ' + [PostalCode] AS [FullAddress]")
              .STAR()
            .FROM("Employees")
            .WHERE("Country").IsEqualTo("USA")
          .SubQueryEnd().AS("us")
        .Build()

    ;

    Console.WriteLine(sql);

Output:

.. code-block:: sql

  SELECT
   [us].[EmployeeID],
   [us].[FullName],
   [us].[FullAddress]
  FROM
  (
   SELECT
    [TitleOfCourtesy] + ' ' + [FirstName] + ' ' + [LastName] AS [FullName],
    [Address] + ' ' + [City] + ', ' + [Region] + ' ' + [PostalCode] AS [FullAddress],
    *
   FROM [Employees]
   WHERE 1=1
    AND [Country] = 'USA'
  ) AS [us]

:doc:`where-1-equals-1`

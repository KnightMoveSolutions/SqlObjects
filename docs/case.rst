.. include:: northwind-db-tip.rst

====
CASE
====

``CASE WHEN`` **Statement in a** ``SELECT`` **List**

.. code-block:: csharp

   using System.Data;
   using KnightMoves.SqlObjects;

.. code-block:: csharp 

    var sql = TSQL

        .SELECT()
          .COLUMN("p", "ProductID")
          .COLUMN("p", "ProductName")
          .COLUMN("p", "UnitPrice")
          .CASE()
            .WHEN("p", "UnitPrice")
              .IsLessThanOrEqualTo(10.00m)
            .THEN("Low")
            .WHEN("p", "UnitPrice")
              .IsGreaterThan(10.00m)
              .AND("p", "UnitPrice").IsLessThanOrEqualTo(50.00m)
            .THEN("Moderate")
            .WHEN("p", "UnitPrice")
              .IsGreaterThan(50.00m)
            .THEN("Expensive")
          .END().AS("CostLevel")
        .FROM("dbo", "Products", "p")
        .Build()

    ;

    Console.WriteLine(sql);

Output:

.. code-block:: sql

  SELECT
   [p].[ProductID],
   [p].[ProductName],
   CASE
    WHEN [p].[UnitPrice] <= 10.00 THEN 'Low'
    WHEN [p].[UnitPrice] > 10.00 AND [p].[UnitPrice] <= 50.00 THEN 'Moderate'
    WHEN [p].[UnitPrice] > 50.00 THEN 'Expensive'
   END AS [CostLevel]
  FROM [dbo].[Products] p
   

``CASE input_expression`` **Statement in a** ``SELECT`` **List**

.. code-block:: csharp

   using System.Data;
   using KnightMoves.SqlObjects;

.. code-block:: csharp 

    var sql = TSQL

        .SELECT()
          .COLUMN("c", "CustomerID")
          .COLUMN("c", "CompanyName").AS("Company")
          .COLUMN("c", "ContactName").AS("Contact")
          .COLUMN("c", "ContactTitle")
          .CASE("c", "ContactTitle")
            .WHEN("Owner").THEN("Owner")
            .WHEN("Accounting Manager").THEN("Management")
            .WHEN("Marketing Manager").THEN("Management")
            .WHEN("Sales Manager").THEN("Management")
            .ELSE("Employee")
          .END().AS("Level")
        .FROM("dbo", "Customers", "c")
        .Build()

    ;

    Console.WriteLine(sql);

Output:

.. code-block:: sql

  SELECT
   [c].[CustomerID],
   [c].[CompanyName] AS [Company],
   [c].[ContactName] AS [Contact],
   [c].[ContactTitle],
   CASE [c].[ContactTitle]
    WHEN 'Owner' THEN 'Owner'
    WHEN 'Accounting Manager' THEN 'Management'
    WHEN 'Marketing Manager' THEN 'Management'
    WHEN 'Sales Manager' THEN 'Management'
    ELSE 'Employee'
   END AS [Level]
  FROM [dbo].[Customers] c

``CASE WHEN`` **Statement in a** ``WHERE`` **Clause**

.. code-block:: csharp

   using System.Data;
   using KnightMoves.SqlObjects;

.. code-block:: csharp 

    var sql = TSQL

        .SELECT()
          .COLUMN("c", "CustomerID")
          .COLUMN("c", "CompanyName").AS("Company")
          .COLUMN("c", "ContactName").AS("Contact")
          .COLUMN("c", "ContactTitle")
        .FROM("dbo", "Customers", "c")
        .WHERE()
          .CASE("c", "ContactTitle")
            .WHEN("Owner").THEN("Owner")
            .WHEN("Accounting Manager").THEN("Management")
            .WHEN("Marketing Manager").THEN("Management")
            .WHEN("Sales Manager").THEN("Management")
            .ELSE("Employee")
          .END().IsEqualTo("Management")
        .Build()

    ;

    Console.WriteLine(sql);

Output:

.. code-block:: sql

  SELECT
   [c].[CustomerID],
   [c].[CompanyName] AS [Company],
   [c].[ContactName] AS [Contact],
   [c].[ContactTitle]
  FROM [dbo].[Customers] c
  WHERE 1=1
   AND CASE [c].[ContactTitle]
     WHEN 'Owner' THEN 'Owner'
     WHEN 'Accounting Manager' THEN 'Management'
     WHEN 'Marketing Manager' THEN 'Management'
     WHEN 'Sales Manager' THEN 'Management'
     ELSE 'Employee'
    END = 'Management'

   
:doc:`where-1-equals-1`

.. code-block:: csharp

   using System.Data;
   using KnightMoves.SqlObjects;

.. code-block:: csharp 

    var sql = TSQL

        .Script(@"
          DECLARE @orgLevel AS VARCHAR(50)
          SET @orgLevel = 'Management'
        ")
        .SELECT()
          .COLUMN("c", "CustomerID")
          .COLUMN("c", "CompanyName").AS("Company")
          .COLUMN("c", "ContactName").AS("Contact")
          .COLUMN("c", "ContactTitle")
        .FROM("dbo", "Customers", "c")
        .WHERE("@orgLevel").IsEqualTo()
          .CASE("c", "ContactTitle")
            .WHEN("Owner").THEN("Owner")
            .WHEN("Accounting Manager").THEN("Management")
            .WHEN("Marketing Manager").THEN("Management")
            .WHEN("Sales Manager").THEN("Management")
            .ELSE("Employee")
          .END()
        .Build()

    ;

    Console.WriteLine(sql);

Output: 

Contains an example of using ad hoc :doc:`SQL Scripts <scripts>` documented :doc:`here <scripts>`

.. code-block:: sql

  DECLARE @orgLevel AS VARCHAR(50)
  SET @orgLevel = 'Management'

  SELECT
   [c].[CustomerID],
   [c].[CompanyName] AS [Company],
   [c].[ContactName] AS [Contact],
   [c].[ContactTitle]
  FROM [dbo].[Customers] c
  WHERE 1=1
   AND @orgLevel = CASE [c].[ContactTitle]
     WHEN 'Owner' THEN 'Owner'
     WHEN 'Accounting Manager' THEN 'Management'
     WHEN 'Marketing Manager' THEN 'Management'
     WHEN 'Sales Manager' THEN 'Management'
     ELSE 'Employee'
    END

:doc:`where-1-equals-1`


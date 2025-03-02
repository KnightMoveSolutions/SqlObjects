`Documentation Home <https://docs.knightmovesolutions.com>`_

.. include:: northwind-db-tip.rst

=========
Functions
=========

.. _abs-function:

ABS
---

.. code-block:: csharp 

    var sql = TSQL

        .SELECT()
          .ABS("[UnitsInStock]")
        .FROM("Products")
        .Build()

    ;

    Console.WriteLine(sql);

Output:

.. code-block:: sql

  SELECT
   ABS([UnitsInStock])
  FROM [Products]

.. _avg-function:

AVG
---

.. code-block:: csharp 

    var sql = TSQL

        .SELECT()
          .AVG("[UnitPrice]")
        .FROM("Products")
        .Build()

    ;

    Console.WriteLine(sql);

Output:

.. code-block:: sql

  SELECT
   AVG([UnitPrice])
  FROM [Products]


.. _ceiling-function:

CEILING
-------

.. code-block:: csharp 

    var sql = TSQL

        .SELECT()
          .CEILING("[UnitPrice]")
        .FROM("Products")
        .Build()

    ;

    Console.WriteLine(sql);

Output:

.. code-block:: sql

  SELECT
   CEILING([UnitPrice])
  FROM [Products]

.. _count-function:

COUNT
-----

.. code-block:: csharp 

    var sql = TSQL

        .SELECT()
          .COUNT("*")
        .FROM("Products")
        .Build()

    ;

    Console.WriteLine(sql);

Output:

.. code-block:: sql

  SELECT
   COUNT(*)
  FROM [Products]

.. _dateadd-function:

DATEADD
-------

.. code-block:: csharp 

    var sql = TSQL

        .Script(@"
          DECLARE @dateVar AS DATETIME
          SET @dateVar = GETDATE()
        ")
        .SELECT()
          .DATEADD(DateParts.Day, 1, new DateTime(2021, 1, 1)).AS("Result1")
          .DATEADD(DateParts.Day, 1, "@dateVar").AS("Result2")
          .DATEADD(DateParts.Day, 1, "[HireDate]").AS("Result3")
        .FROM("Employees")
        .Build()

    ;

    Console.WriteLine(sql);

Output:

.. code-block:: sql

  DECLARE @dateVar AS DATETIME
  SET @dateVar = GETDATE()

  SELECT
   DATEADD(Day, 1, '2021-01-01 00:00:00') AS [Result1],
   DATEADD(Day, 1, @dateVar) AS [Result2],
   DATEADD(Day, 1, [HireDate]) AS [Result3]
  FROM [Employees]


.. _datediff-function:

DATEDIFF
--------

.. code-block:: csharp 

    var today = new DateTime(2021, 1, 1);
    var weekAgo = today.AddDays(-7);

    var sql = TSQL

        .Script(@"
          DECLARE @startDateVar DATETIME, @endDateVar DATETIME
          SET @startDateVar = '2020-01-15 00:00:00'
          SET @endDateVar = '2021-02-01 00:00:00'
        ")
        .SELECT()
          .DATEDIFF(DateParts.Day, weekAgo, today).AS("Result1")
          .DATEDIFF(DateParts.Day, "@startDateVar", today).AS("Result2")
          .DATEDIFF(DateParts.Day, weekAgo, "@endDateVar").AS("Result3")
          .DATEDIFF(DateParts.Day, "@startDateVar", "@endDateVar").AS("Result4")
          .DATEDIFF(DateParts.Day, "[HireDate]", today).AS("Result5")
          .DATEDIFF(DateParts.Day, "e", "HireDate", "@endDateVar").AS("Result6")
          .DATEDIFF(DateParts.Day, weekAgo, "e", "HireDate").AS("Result7")
          .DATEDIFF(DateParts.Day, "e", "BirthDate", "e", "HireDate").AS("Result8")
        .FROM("dbo", "Employees", "e")
        .Build()

    ;

    Console.WriteLine(sql);

Output:

.. code-block:: sql

  DECLARE @startDateVar DATETIME, @endDateVar DATETIME
  SET @startDateVar = '2020-01-15 00:00:00'
  SET @endDateVar = '2021-02-01 00:00:00'

  SELECT
   DATEDIFF(Day, '2020-12-25 00:00:00', '2021-01-01 00:00:00') AS [Result1],
   DATEDIFF(Day, @startDateVar, '2021-01-01 00:00:00') AS [Result2],
   DATEDIFF(Day, '2020-12-25 00:00:00', @endDateVar) AS [Result3],
   DATEDIFF(Day, @startDateVar, @endDateVar) AS [Result4],
   DATEDIFF(Day, [HireDate], '2021-01-01 00:00:00') AS [Result5],
   DATEDIFF(Day, [e].[HireDate], @endDateVar) AS [Result6],
   DATEDIFF(Day, '2020-12-25 00:00:00', [e].[HireDate]) AS [Result7],
   DATEDIFF(Day, [e].[BirthDate], [e].[HireDate]) AS [Result8]
  FROM [dbo].[Employees] e

   
.. _datename-function:

DATENAME
--------

.. code-block:: csharp 

    var today = new DateTime(2021, 1, 1);

    var sql = TSQL

        .Script(@"
          DECLARE @dateVar DATETIME
          SET @dateVar = GETDATE()
        ")
        .SELECT()
          .DATENAME(DateParts.Day, today).AS("Result1")
          .DATENAME(DateParts.Month, "@dateVar").AS("Result2")
          .DATENAME(DateParts.Month, "e", "HireDate").AS("Result3")
        .FROM("dbo", "Employees", "e")
        .Build()

    ;

    Console.WriteLine(sql);

Output:

.. code-block:: sql

  DECLARE @dateVar DATETIME
  SET @dateVar = GETDATE()

  SELECT
   DATENAME(Day, '2021-01-01 00:00:00') AS [Result1],
   DATENAME(Month, @dateVar) AS [Result2],
   DATENAME(Month, [e].[HireDate]) AS [Result3]
  FROM [dbo].[Employees] e

   
.. _datepart-function:

DATEPART
--------

.. code-block:: csharp 

    var today = new DateTime(2021, 1, 1);

    var sql = TSQL

        .Script(@"
          DECLARE @dateVar DATETIME
          SET @dateVar = GETDATE()
        ")
        .SELECT()
          .DATEPART(DateParts.Day, today).AS("Result1")
          .DATEPART(DateParts.Month, "@dateVar").AS("Result2")
          .DATEPART(DateParts.Month, "e", "HireDate").AS("Result3")
        .FROM("dbo", "Employees", "e")
        .Build()

    ;

    Console.WriteLine(sql);

Output:

.. code-block:: sql

  SELECT
   DATEPART(Day, '2021-01-01 00:00:00') AS [Result1],
   DATEPART(Month, @dateVar) AS [Result2],
   DATEPART(Month, [e].[HireDate]) AS [Result3]
  FROM [dbo].[Employees] e
   

.. _day-function:

DAY
---

.. code-block:: csharp 

    var today = new DateTime(2021, 1, 1);

    var sql = TSQL

        .Script(@"
          DECLARE @dateVar DATETIME
          SET @dateVar = GETDATE()
        ")
        .SELECT()
          .DAY(today).AS("Result1")
          .DAY("@dateVar").AS("Result2")
          .DAY("e", "HireDate").AS("Result3")
        .FROM("dbo", "Employees", "e")
        .Build()
    ;

    Console.WriteLine(sql);

Output:

.. code-block:: sql

  DECLARE @dateVar DATETIME
  SET @dateVar = GETDATE()

  SELECT
   DAY('2021-01-01 00:00:00') AS [Result1],
   DAY(@dateVar) AS [Result2],
   DAY([e].[HireDate]) AS [Result3]
  FROM [dbo].[Employees] e

.. _floor-function:

FLOOR
-----

.. code-block:: csharp 

    var sql = TSQL

        .Script(@"
          DECLARE @decVar DECIMAL(18,2)
          SET @decVar = 9.99
        ")
        .SELECT()
          .FLOOR(9.99m).AS("Result1")
          .FLOOR("@decVar").AS("Result2")
          .FLOOR("p", "UnitPrice").AS("Result3")
        .FROM("dbo", "Products", "p")
        .Build()

    ;

    Console.WriteLine(sql);

Output:

.. code-block:: sql

  DECLARE @decVar DECIMAL(18,2)
  SET @decVar = 9.99

  SELECT
   FLOOR(9.99) AS [Result1],
   FLOOR(@decVar) AS [Result2],
   FLOOR([p].[UnitPrice]) AS [Result3]
  FROM [dbo].[Products] p

   
.. _getdate-function:

GETDATE
-------

.. code-block:: csharp 

    var sql = TSQL

        .SELECT()
          .GETDATE().AS("CurrentDate")
          .COLUMN("HireDate")
        .FROM("Employees")
        .Build()

    ;

    Console.WriteLine(sql);

Output:

.. code-block:: sql

  SELECT
   GETDATE() AS [CurrentDate],
   [HireDate]
  FROM [Employees]

   
.. _isnull-function:

ISNULL
------

.. code-block:: csharp 

    var sql = TSQL

        .SELECT()
          .ISNULL("p", "UnitPrice", 0.00m)
          .ISNULL("UnitsOnOrder", 0)
          .ISNULL("@someVarThatMightBeNull", Guid.Empty)
        .FROM("dbo", "Products", "p")
        .Build()

    ;

    Console.WriteLine(sql);

Output:

.. code-block:: sql

  SELECT
   ISNULL([p].[UnitPrice], 0.00),
   ISNULL([UnitsOnOrder], 0),
   ISNULL(@someVarThatMightBeNull, '00000000-0000-0000-0000-000000000000')
  FROM [dbo].[Products] p

.. _max-function:

MAX
---

.. code-block:: csharp 

    var sql = TSQL

        .SELECT()
          .MAX("p", "UnitPrice")
          .MAX("UnitsOnOrder")
        .FROM("dbo", "Products", "p")
        .Build()

    ;

    Console.WriteLine(sql);

Output:

.. code-block:: sql

  SELECT
   MAX([p].[UnitPrice]),
   MAX(UnitsOnOrder)
  FROM [dbo].[Products] p

.. _min-function:

MIN
---

.. code-block:: csharp 

    var sql = TSQL

        .SELECT()
          .MIN("p", "UnitPrice")
          .MIN("UnitsOnOrder")
        .FROM("dbo", "Products", "p")
        .Build()

    ;

    Console.WriteLine(sql);

Output:

.. code-block:: sql

  SELECT
   MIN([p].[UnitPrice]),
   MIN(UnitsOnOrder)
  FROM [dbo].[Products] p

.. _month-function:

MONTH
-----

.. code-block:: csharp 

    var today = new DateTime(2021, 1, 1);

    var sql = TSQL

        .Script(@"
          DECLARE @dateVar DATETIME
          SET @dateVar = GETDATE()
        ")
        .SELECT()
          .MONTH(today).AS("Result1")
          .MONTH("@dateVar").AS("Result2")
          .MONTH("e", "HireDate").AS("Result3")
        .FROM("dbo", "Employees", "e")
        .Build()
    ;

    Console.WriteLine(sql);

Output:

.. code-block:: sql

  DECLARE @dateVar DATETIME
  SET @dateVar = GETDATE()

  SELECT
   MONTH('2021-01-01 00:00:00') AS [Result1],
   MONTH(@dateVar) AS [Result2],
   MONTH([e].[HireDate]) AS [Result3]
  FROM [dbo].[Employees] e

.. _sum-function:

SUM
---

.. code-block:: csharp 

    var sql = TSQL

        .SELECT()
          .SUM("p", "UnitsOnOrder")
        .FROM("dbo", "Products", "p")
        .Build()

    ;

    Console.WriteLine(sql);

Output:

.. code-block:: sql

  SELECT
   SUM([p].[UnitsOnOrder])
  FROM [dbo].[Products] p

.. _year-function:

YEAR
----

.. code-block:: csharp 

    var today = new DateTime(2021, 1, 1);

    var sql = TSQL

        .Script(@"
          DECLARE @dateVar DATETIME
          SET @dateVar = GETDATE()
        ")
        .SELECT()
          .YEAR(today).AS("Result1")
          .YEAR("@dateVar").AS("Result2")
          .YEAR("e", "HireDate").AS("Result3")
        .FROM("dbo", "Employees", "e")
        .Build()
    ;

    Console.WriteLine(sql);

Output:

.. code-block:: sql

  DECLARE @dateVar DATETIME
  SET @dateVar = GETDATE()

  SELECT
   YEAR('2021-01-01 00:00:00') AS [Result1],
   YEAR(@dateVar) AS [Result2],
   YEAR([e].[HireDate]) AS [Result3]
  FROM [dbo].[Employees] e

`Documentation Home <https://docs.knightmovesolutions.com>`_

.. include:: northwind-db-tip.rst

===========
SELECT FROM
===========

SELECT ALL
----------

**Using the default behavior**

.. code-block:: csharp 

   var sql = TSQL

      // * is the default
      .SELECT().FROM("Products").Build()

   ;

   Console.WriteLine(sql);

Output:

.. code-block:: sql

   SELECT
    *
   FROM [Products]

**Explicitly call the** ``.STAR()`` **method**

.. code-block:: csharp 

   var sql = TSQL

      .SELECT()
        .STAR()
      .FROM("Products")
      .Build()

   ;

   Console.WriteLine(sql);

Output:

.. code-block:: sql

   SELECT
    *
   FROM [Products]


COLUMN
------

**Specify a column by name**

.. code-block:: csharp 

   var sql = TSQL

      .SELECT()
        .COLUMN("ProductID")
        .COLUMN("ProductName")
      .FROM("Products")
      .Build()

   ;

   Console.WriteLine(sql);

Output:

.. code-block:: sql

   SELECT
    [ProductID],
    [ProductName]
   FROM [Products]

**Specify a column by name with a multipart identifier table alias**

.. code-block:: csharp 

   var sql = TSQL

      .SELECT()
        .COLUMN("p", "ProductID")
        .COLUMN("p", "ProductName")
      .FROM("dbo", "Products", "p")
      .Build()

   ;

   Console.WriteLine(sql);

Output:

.. code-block:: sql

   SELECT
    [p].[ProductID],
    [p].[ProductName]
   FROM [dbo].[Products] p 

.. note::

   When specifying a multipart identifier table alias, the schema must be included in order to 
   disambiguate the string arguments to the method call.

**Specify a column by name with a column alias using** ``.AS()``

.. code-block:: csharp 

   var sql = TSQL

      .SELECT()
        .COLUMN("ProductID").AS("Id")
        .COLUMN("ProductName")
      .FROM("Products")
      .Build()

   ;

   Console.WriteLine(sql);

Output:

.. code-block:: sql

   SELECT
    [ProductID] AS [Id],
    [ProductName]
   FROM [Products]

**Specify a column by name with a multipart identifier table alias and column alias
with a third argument.**

``.COLUMN(string multipartIdentifier, string columnName, string alias)``

.. code-block:: csharp 

   var sql = TSQL

      .SELECT()
        .COLUMN("p", "ProductID", "Id")
        .COLUMN("p", "ProductName")
      .FROM("dbo", "Products", "p")
      .Build()

   ;

   Console.WriteLine(sql);

Output:

.. code-block:: sql

   SELECT
    [p].[ProductID] AS [Id],
    [p].[ProductName]
   FROM [dbo].[Products] p


.. note::

   When specifying a column alias using the arguments, the multipart identifier table alias 
   must be included in order to disambiguate the string arguments to the method call.

COLUMNS
-------

**Provide a collection of column names as** ``IEnumerable<string>``

.. code-block:: csharp 

    using System;
    using System.Collections.Generic;
    using KnightMoves.SqlObjects;

.. code-block:: csharp 

    var columns = new List<string> { "ProductID", "ProductName" };

    var sql = TSQL

         .SELECT()
           .COLUMNS(columns)
         .FROM("dbo", "Products", "p")
         .Build()

    ;

    Console.WriteLine(sql);

Output:

.. code-block:: sql

  SELECT
   [ProductID],
   [ProductName]
  FROM [dbo].[Products] p

**Provide a collection of column names as** ``IEnumerable<string>`` **and a common multipart identifier**

.. code-block:: csharp 

    using System;
    using System.Collections.Generic;
    using KnightMoves.SqlObjects;

.. code-block:: csharp 

    var columns = new List<string> { "ProductID", "ProductName" };

    var sql = TSQL

         .SELECT()
           .COLUMNS("p", columns)
         .FROM("dbo", "Products", "p")
         .Build()

    ;

    Console.WriteLine(sql);

Output:

.. code-block:: sql

  SELECT
   [p].[ProductID],
   [p].[ProductName]
  FROM [dbo].[Products] p

**Provide a collection of** ``TSQLColumn`` **objects**

.. code-block:: csharp 

    using System;
    using System.Collections.Generic;
    using KnightMoves.SqlObjects;
    using KnightMoves.SqlObjects.SqlCode.TSQL;

.. code-block:: csharp 

    var columns = new List<TSQLColumn>
    {
        new TSQLColumn { MultiPartIdentifier = "p", ColumnName = "ProductID" },
        new TSQLColumn { MultiPartIdentifier = "p", ColumnName = "ProductName", Alias = "Name" }
    };

    var sql = TSQL

         .SELECT()
           .COLUMNS(columns)
         .FROM("dbo", "Products", "p")
         .Build()

    ;

    Console.WriteLine(sql);

Output:

.. code-block:: sql

  SELECT
   [p].[ProductID],
   [p].[ProductName] AS [Name]
  FROM [dbo].[Products] p


DISTINCT
--------

**Using** ``.DISTINCT()`` **with** ``.COLUMN()`` **Methods**

.. code-block:: csharp 

    var sql = TSQL

      .SELECT()
       .DISTINCT()
       .COLUMN("CategoryID")
       .COLUMN("Discontinued")
      .FROM("Products")
      .Build()

    ;

    Console.WriteLine(sql);

Output:

.. code-block:: sql

  SELECT
   DISTINCT
   [CategoryID],
   [Discontinued]
  FROM [Products]
   
**Using** ``.DISTINCT()`` **with an** ``IEnumerable<string>`` **Collection**

.. code-block:: csharp 

   var columns = new List<string> { "CategoryID", "Discontinued" };

   var sql = TSQL

      .SELECT()
        .DISTINCT()
        .COLUMNS(columns)
      .FROM("Products")
      .Build()

   ;

    Console.WriteLine(sql);

Output:

.. code-block:: sql

  SELECT
   DISTINCT
   [CategoryID],
   [Discontinued]
  FROM [Products]

**Using** ``.DISTINCT()`` **with a Collection of** ``TSQLColumn()`` **Objects**

.. code-block:: csharp 

   var columns = new List<TSQLColumn>
   {
         new TSQLColumn { ColumnName = "CategoryID" },
         new TSQLColumn { ColumnName = "Discontinued" }
   };

      var sql = TSQL

         .SELECT()
           .DISTINCT()
           .COLUMNS(columns)
         .FROM("Products")
         .Build()

   ;

    Console.WriteLine(sql);

Output:

.. code-block:: sql

  SELECT
   DISTINCT
   [CategoryID],
   [Discontinued]
  FROM [Products]

TOP
---

**Using** ``.TOP(int)`` **with** ``.STAR()``

.. code-block:: csharp 

    var sql = TSQL

      .SELECT()
       .TOP(10)
       .STAR()
      .FROM("Products")
      .Build()

    ;

    Console.WriteLine(sql);

Output:

.. code-block:: sql

  SELECT
   TOP 10
   *
  FROM [Products]

**Using** ``.TOP(int)`` **with Columns**

.. code-block:: csharp 

    var sql = TSQL

      .SELECT()
        .TOP(10)
        .COLUMN("ProductID")
        .COLUMN("ProductName")
      .FROM("Products")
      .Build()

    ;

    Console.WriteLine(sql);

Output:

.. code-block:: sql

  SELECT
   TOP 10
   [ProductID],
   [ProductName]
  FROM [Products]
   
**TOP n PERCENT**

.. code-block:: csharp 

   var sql = TSQL

      .SELECT()
        .TOP(10, isPercent: true)
        .COLUMN("ProductID")
        .COLUMN("ProductName")
      .FROM("Products")
      .Build()

   ;

    Console.WriteLine(sql);

Output:

.. code-block:: sql

  SELECT
   TOP 10 PERCENT
   [ProductID],
   [ProductName]
  FROM [Products]

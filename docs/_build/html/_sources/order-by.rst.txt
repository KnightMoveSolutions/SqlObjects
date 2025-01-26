`Documentation Home <https://docs.knightmovesolutions.com>`_

.. include:: northwind-db-tip.rst

========
ORDER BY
========

**ORDER BY a Single Column ASC**

.. code-block:: csharp 

    var sql = TSQL

        .SELECT()
          .COLUMN("EmployeeID")
          .COLUMN("FirstName")
          .COLUMN("LastName")
          .COLUMN("Title")
          .COLUMN("HireDate")
        .FROM("Employees")
        .ORDERBY("LastName").ASC()
        .Build()

    ;

    Console.WriteLine(sql);

Output:

.. code-block:: sql

  SELECT
   [EmployeeID],
   [FirstName],
   [LastName],
   [Title],
   [HireDate]
  FROM [Employees]
  ORDER BY
   [LastName] ASC
   
**ORDER BY a Single Column DESC**

.. code-block:: csharp 

    var sql = TSQL

        .SELECT()
          .COLUMN("EmployeeID")
          .COLUMN("FirstName")
          .COLUMN("LastName")
          .COLUMN("Title")
          .COLUMN("HireDate")
        .FROM("Employees")
        .ORDERBY("HireDate").DESC()
        .Build()

    ;

    Console.WriteLine(sql);

Output:

.. code-block:: sql

  SELECT
   [EmployeeID],
   [FirstName],
   [LastName],
   [Title],
   [HireDate]
  FROM [Employees]
  ORDER BY
   [HireDate] DESC

**ORDER BY a Single Column ASC Using the** ``.COLUMN()`` **Method**

.. code-block:: csharp 

    var sql = TSQL

        .SELECT()
          .COLUMN("EmployeeID")
          .COLUMN("FirstName")
          .COLUMN("LastName")
          .COLUMN("Title")
          .COLUMN("HireDate")
        .FROM("Employees")
        .ORDERBY()
          .COLUMN("LastName").ASC()
        .Build()

    ;

    Console.WriteLine(sql);

Output:

.. code-block:: sql

  SELECT
   [EmployeeID],
   [FirstName],
   [LastName],
   [Title],
   [HireDate]
  FROM [Employees]
  ORDER BY
   [LastName] ASC


**ORDER BY a Multiple Columns**

.. code-block:: csharp 

    var sql = TSQL

        .SELECT()
          .COLUMN("EmployeeID")
          .COLUMN("FirstName")
          .COLUMN("LastName")
          .COLUMN("Title")
          .COLUMN("HireDate")
        .FROM("Employees")
        .ORDERBY()
          .COLUMN("HireDate").DESC()
          .COLUMN("LastName").ASC()
        .Build()

    ;

    Console.WriteLine(sql);

Output:

.. code-block:: sql

  SELECT
   [EmployeeID],
   [FirstName],
   [LastName],
   [Title],
   [HireDate]
  FROM [Employees]
  ORDER BY
   [HireDate] DESC,
   [LastName] ASC

**ORDER BY an Object Collection**

You can pass the ``.ORDERBY()`` method a collection of ``ISqlOrderByExpression`` objects, which 
can be useful for automation purposes. This requires the following steps. 

1. Wrap an ``ISqlColumn`` object in an ``ISqlOrderByExpression`` object by adding the column object to the 
   order by expression object's ``Children`` collection.

2. Add the ``ISqlOrderExpression`` object to a collection

3. Pass the collection of ``ISqlOrderExpression`` objects to the ``.ORDERBY()`` method 

See the example below.

.. code-block:: csharp

    using System;
    using System.Collections.Generic;
    using System.Data;
    using KnightMoves.SqlObjects;
    using KnightMoves.SqlObjects.SqlCode;
    using KnightMoves.SqlObjects.SqlCode.TSQL;

.. code-block:: csharp 

    var orderByExpression1 = new TSQLOrderByExpression { Direction = SqlOrderByDirections.DESC };
    var orderByExpression2 = new TSQLOrderByExpression { Direction = SqlOrderByDirections.ASC };

    orderByExpression1.Children.Add(new TSQLColumn { ColumnName = "HireDate" });
    orderByExpression2.Children.Add(new TSQLColumn { ColumnName = "LastName" });

    var orderByExpressions = new List<ISqlOrderByExpression> 
    {
        orderByExpression1,
        orderByExpression2
    };

    var sql = TSQL

        .SELECT()
          .COLUMN("EmployeeID")
          .COLUMN("FirstName")
          .COLUMN("LastName")
          .COLUMN("Title")
          .COLUMN("HireDate")
        .FROM("Employees")
        .ORDERBY(orderByExpressions)
        .Build()

    ;

    Console.WriteLine(sql);

Output:

.. code-block:: sql

  SELECT
   [EmployeeID],
   [FirstName],
   [LastName],
   [Title],
   [HireDate]
  FROM [Employees]
  ORDER BY
   [HireDate] DESC,
   [LastName] ASC

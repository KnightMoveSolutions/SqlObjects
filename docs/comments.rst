`Documentation Home <https://docs.knightmovesolutions.com>`_

.. include:: northwind-db-tip.rst

========
Comments
========

You can pretty much put comments anywhere within your structure if you want. 
You might be wondering why you'd want to do that since you can easily just 
add C# comments to your source code and the SQL is typically sent to the database 
for execution anyway without you ever seeing it. 

Well, keep in mind that this library builds an object model that you can serialize, 
save, push over the network, cache, pass around in your application, or 
whatever. After all of that you could resurrect it by deserializing it and 
building the result. If you utilize any of those advanced features it may be 
useful to plant comments that go along with your SQL. 

Another use case is to plant the comments in with the SQL so that if you're ever 
debugging the rendered SQL, the comment is both in the C# code and in the rendered 
SQL by utilizing the ``.Comment()`` feature. A pleasant debugging experience is one
of the goals of this library. 

Simple Single Line Comments
---------------------------

.. code-block:: csharp 

    var sql = TSQL

        .Comment("Here's a simple query against the Employees table")
        .SELECT()
          .COLUMN("EmployeeID")
          .COLUMN("FirstName")
          .COLUMN("LastName")
          .Comment("We would like to output the hire date")
          .COLUMN("HireDate")
        .FROM("Employees")
        .Build()

    ;

    Console.WriteLine(sql);

Output:

.. code-block:: sql

  -- Here's a simple query against the Employees table
  SELECT
   [EmployeeID],
   [FirstName],
   [LastName],
   -- We would like to output the hire date
   [HireDate]
  FROM [Employees]

Multi-Line Comments
-------------------

.. code-block:: csharp 

    var sql = TSQL

        .Comment
        (
          @"Here's a multi-line comment that is more 
            verbose. The nice thing about this is
            that it will be formatted nicely with 
            slash-star multi-line comment syntax"
        )
        .SELECT()
          .COLUMN("e", "EmployeeID")
          .COLUMN("e", "FirstName")
          .COLUMN("e", "LastName")
          .COLUMN("e", "HireDate")
          .Literal("[m].[FirstName] + ' ' + [m].[LastName] AS [Manager]")
        .FROM("dbo", "Employees", "e")
        .Comment
        (
          @"Here we do a self-join against the Employees
            table to output each employee's supervising
            manager. This comment should also display nicely
            in the middle of the SQL structure."
        )
        .INNERJOIN("dbo", "Employees", "m").ON("m", "EmployeeID").IsEqualTo("e", "ReportsTo")
        .Build()

    ;

    Console.WriteLine(sql);

Output:

.. code-block:: sql

  /*
   * Here's a multi-line comment that is more
   * verbose. The nice thing about this is
   * that it will be formatted nicely with
   * slash-star multi-line comment syntax
  */
  SELECT
   [e].[EmployeeID],
   [e].[FirstName],
   [e].[LastName],
   [e].[HireDate],
   [m].[FirstName] + ' ' + [m].[LastName] AS [Manager]
  FROM [dbo].[Employees] e
  /*
   * Here we do a self-join against the Employees
   * table to output each employee's supervising
   * manager. This comment should also display nicely
   * in the middle of the SQL structure.
  */
  INNER JOIN [dbo].[Employees] m ON [m].[EmployeeID] = [e].[ReportsTo]

**Multiple Single-Line Comments**

If you don't like the ``/* ... */`` syntax you can pass ``true`` for the 
``singleLineOnly`` argument on the ``.Comment()`` method and it will 
render all lines using the ``-- comment`` syntax as shown below.

.. code-block:: csharp 

    var sql = TSQL

        .Comment
        (
          @"Here's a multi-line comment that is more 
            verbose. The nice thing about this is
            that it will be formatted nicely all 
            with single-line comment syntax",
            singleLineOnly: true
        )
        .SELECT()
          .COLUMN("e", "EmployeeID")
          .COLUMN("e", "FirstName")
          .COLUMN("e", "LastName")
          .COLUMN("e", "HireDate")
          .Literal("[m].[FirstName] + ' ' + [m].[LastName] AS [Manager]")
        .FROM("dbo", "Employees", "e")
        .Comment
        (
          @"Here we do a self-join against the Employees
            table to output each employee's supervising
            manager. This comment should also display nicely
            in the middle of the SQL structure.",
            singleLineOnly: true
        )
        .INNERJOIN("dbo", "Employees", "m").ON("m", "EmployeeID").IsEqualTo("e", "ReportsTo")
        .Build()

    ;

    Console.WriteLine(sql);

Output:

.. code-block:: sql

  -- Here's a multi-line comment that is more
  -- verbose. The nice thing about this is
  -- that it will be formatted nicely all
  -- with single-line comment syntax
  SELECT
   [e].[EmployeeID],
   [e].[FirstName],
   [e].[LastName],
   [e].[HireDate],
   [m].[FirstName] + ' ' + [m].[LastName] AS [Manager]
  FROM [dbo].[Employees] e
  -- Here we do a self-join against the Employees
  -- table to output each employee's supervising
  -- manager. This comment should also display nicely
  -- in the middle of the SQL structure.
  INNER JOIN [dbo].[Employees] m ON [m].[EmployeeID] = [e].[ReportsTo]


.. include:: northwind-db-tip.rst

=======
Scripts
=======

Overview
--------

There are two ways that you can provide ad hoc SQL scripts when necessary. You might want to 
involve some stored-procedure-like logic that needs to be executed in the database or you 
may need to compensate for something that isn't implemented in this library. A great deal of 
effort has been made to accommodate that majority of use cases for a .NET SQL builder in this
library but not all of the possible variations of SQL code are covered. The intention at this
time is to improve the library and add missing features over time but for now, whatever is 
missing can be accommodated with manually crafted SQL code using the two methods below. 

- The ``.Script()`` Method 
- The ``.Literal()`` Method

The ``.Script()`` and ``.Literal()`` methods essentially do the same thing, which is to output 
whatever you type into them exactly as you typed it. There is no effort to transform the code 
in any way (there is some whitespace formatting however - more on that below). 

Script() vs. Literal()
----------------------

The ``.Script()`` method will do two things for you.

First is that the code you enter into the ``.Script()`` method will be added as a child of the 
root SQL object in the underlying object model. Therefore, when it is rendered, it will not be 
indented if you reference it anywhere in the middle of the Fluent script. This is strictly 
eye-candy! It has no impact on the validity of the SQL you type into the script. The database 
engine doesn't care how ugly your SQL code is formatted. But if you want to ensure that your code 
looks optimally readable so you can have a pleasant debugging experience (which is one of the 
goals of this library), then you should only use the ``.Script()`` method at the top or the bottom 
of high-level SQL code structures. For example, use it before starting a ``.SELECT()`` query or 
after a ``.SELECT()`` query. If the SELECT is part of a UNION then only at the bottom, etc. In short, 
it is just a formatting issue. 

Keep in mind that the use of ``.Script()`` is garbage in garbage out. Your mileage will vary based on the quality of your 
manually-entered code. 

Secondly, the ``.Script()`` method will deal with multi-line code. You are free to indent your code 
so it looks pretty in the C# source and the object built by the ``.Script()`` method does the job of 
removing excessive indentation meant for pretty C# code and replaces it with whitespace suitable for 
rendering pretty SQL code. 

For example, if you do this in C#:

.. code-block:: csharp 
   
   ...

             // Your C# code is indented way over here because of your 
             // namespace, class, and method declaration so you do this:

             .Script(@"
                DECLARE @someVar INT
                SET @someVar = 99
             ")
             .SELECT()
               .STAR()
             .FROM("MyTable")
             .WHERE("Id").IsEqualTo("@someVar")

   ... 

| Without any intervention, the SQL code would be rendered like this:

.. code-block:: SQL

                   DECLARE @someVar INT
                   SET @someVar = 99
    
    SELECT
     *
    FROM [MyTable]
    WHERE 1=1
     AND [Id] = @someVar

  ...
  
| That looks weird and ugly so the use of ``.Script()`` doesn't do that. It cleans that up for you and gives you this:

.. code-block:: SQL

    DECLARE @someVar INT
    SET @someVar = 99
    
    SELECT
     *
    FROM [MyTable]  
    WHERE 1=1
     AND [Id] = @someVar

Now, the ``.Literal()`` method will also do two things for you. 

One is that it will respect the place that you put it. Wherever in the fluent code you use it, then 
it will be output indented at that level. It is basically a "child" script. Just keep in mind that 
whatever you code in that ``.Literal()`` will have to make sense and jive with the code above and 
below it. 

Just as with ``.Script()``, the ``.Literal()`` method takes manually-entered code and its use is garbage in 
garbage out so your mileage will also vary based on the quality of your code. 

The second thing that the ``.Literal()`` method does for you is that the underlying object that 
it produces implements the ``ISqlQueryExpression`` interface, which is an implementation
detail that you don't have to deal with but essentially means that it can be used anywhere that a
query expression is used and it will be treated as such. That means that it will automatically add 
your script as a child of one of the following parent SQL statements:

**SELECT**
  In the SELECT list

**JOIN**
  Can be used in JOIN conditions

**WHERE Clause**
  Can be used in conditions under the WHERE Clause

**Condition Group**
  Can be used within the scope delimted by parentheses ``( ... )``, typically necessary when scoping multiple
  conditions with ``OR``

**INSERT**
  Can be used under INSERT statements

**GROUP BY**
  Can be used to manually create a grouping expression

**ORDER BY**
  Can be used to manually create a sorting expression

**HAVING Clause**
  Similar to a WHERE clause it can be used in conditions under the HAVING clause 

One final thing to note about ``.Literal()`` is that it is meant for single lines. Unlike the ``.Script()`` 
method you will get no such multi-line clean-up. If you need multiple lines of SQL code in the middle of 
your structure then you should utilize multiple calls to the ``.Literal()`` method, one for each line.

All that said, please READ THE WARNING BELOW!

.. warning::

   When adding manually-crafted SQL code using ``.Script()`` or ``.Literal()`` you should know that it will be 
   vulnerable to SQL Injection attacks. Do not concatenate or string-interpolate variables containing values set 
   from an untrusted source like ... 
   
   <shivers> a user's data entry screen </shivers> 
   
   This, nor any other library, will protect you from a hack injected through that variable.

   Please read the :doc:`security` documentation to learn about the security features provided with this library 
   as well as its limitations.

   You have been warned!

Script() Example
----------------

.. code-block:: csharp 

    var sql = TSQL

        .Script(@"
          DECLARE @dateVar DATETIME, @now DATETIME
          SET @dateVar = DATEADD(Month, -3, GETDATE())
          SET @now = GETDATE()
        ")
        .SELECT()
          .COLUMN("EmployeeID")
          .COLUMN("FirstName")
          .COLUMN("LastName")
          .COLUMN("HireDate")
        .FROM("Employees")
        .WHERE("HireDate").IsGreaterThanOrEqualTo("@dateVar")
        .Script(@"
          EXEC [dbo].[Employee Sales by Country] @dateVar, @now
        ")
        .Build()

    ;

    Console.WriteLine(sql);

Output:

.. code-block:: sql

  DECLARE @dateVar DATETIME, @now DATETIME
  SET @dateVar = DATEADD(Month, -3, GETDATE())
  SET @now = GETDATE()

  SELECT
   [EmployeeID],
   [FirstName],
   [LastName],
   [HireDate]
  FROM [Employees]
  WHERE 1=1
   AND [HireDate] >= @dateVar

  EXEC [dbo].[Employee Sales by Country] @dateVar, @now

   
:doc:`where-1-equals-1`

Literal() Example
------------------

.. code-block:: csharp 

    var sql = TSQL

        .SELECT()
          .COLUMN("EmployeeID")
          .COLUMN("FirstName")
          .COLUMN("LastName")
          .COLUMN("HireDate")
          .Literal("[FirstName] + ' ' + [LastName] AS [FullName]")
        .FROM("Employees")
        .WHERE()
          .Literal("RIGHT(DATENAME(Month, [HireDate]), 3) = 'ber'")
        .Build()

    ;

    Console.WriteLine(sql);

Output:

.. code-block:: sql

  SELECT
   [EmployeeID],
   [FirstName],
   [LastName],
   [HireDate],
   [FirstName] + ' ' + [LastName] AS [FullName]
  FROM [Employees]
  WHERE 1=1
   AND RIGHT(DATENAME(Month, [HireDate]), 3) = 'ber'
   
:doc:`where-1-equals-1`

.. include:: northwind-db-tip.rst

=====================
Advanced Concepts
=====================

Overview
--------

If you've read through any significant portions of this documentation then you've probably noticed by now 
that this library is backed by a full object model. The Fluent API doesn't simply execute functions that 
return strings. Rather the functions create stateful objects each of which produces the SQL for their 
respective SQL fragments. Because there is a full object model underneath, it opens the door for all sorts
of possibilities. 

When crafting a SQL statement using the Fluent API, you must call the ``.Build()`` method to return the 
SQL code as a string. If you do not execute the ``.Build()`` method then the Fluent API methods return 
objects that implement the ``SqlStatement`` base class. 

The concrete type of the ``SqlStatement`` object 
returned by the Fluent API methods will differ depending on the method that was called but it will represent 
some SQL fragment. The SQL fragment represented by the ``SqlStatement`` object may be a fragment in the 
middle of the SQL statement being built or it may be the SQL object at the top. 

The SQL object model is in the form of a tree, where the ``SqLStatement`` base class is a self-referencing 
type containing a collection of child ``SqlStatement`` objects (children). Rendering the SQL for any fragment 
will automatically render the SQL for all children, children of those children, etc. recursively down the tree. 
The ``.Build()`` method essentially renders the SQL from the root object in order to produce the entire SQL
statement. 

To illustrate, the example below shows that ``sql1`` and ``sql2`` are equivalent.

.. code-block:: csharp 

    var sqlObj = TSQL 

        .SELECT()
          .COLUMN("ProductID")
          .COLUMN("ProductName")
        .FROM("Products")

    ;

    var sql1 = sqlObj.Build();
    var sql2 = sqlObj.Root.ToString();

    Console.WriteLine(sql1 == sql2);

Output:

.. code-block:: csharp

    True

Once you understand that the Fluent API is building a tree of ``SqlStatement`` objects, then you can take 
advantage of the model to do all sorts of cool things if you want to. The sections below demonstrate some 
of the things you can do with the SQL object model produced with this library. 

Sharing Columns
---------------

Consider as an example the ``Employees`` table in the Microsoft Northwind Database, which has been the 
subject database of all the examples in this documentation. This table has the following columns. 

- EmployeeID
- LastName
- FirstName
- Title 
- TitleOfCourtesy
- BirthDate
- HireDate
- Address
- City 
- Region 
- PostalCode 
- Country 
- HomePhone
- Extension 
- Photo 
- Notes 
- ReportsTo
- PhotoPath 

In addition to that, imagine that some day you may have to add a column to that table. This causes some 
maintenance overhead in the application code if you aren't using some SQL code builder to help you manage 
your queries. 

There are several ways you can make this easier in your application code but let's examine the most basic,
which is to share a collection of strings that your various SQL queries can utilize as demonstrated below.

.. code-block:: csharp 

   var table = "Employees";

   // Somewhere you can create your collection of column name strings.
   // You can put it in a base class, inject it with your IoC container ... whatever.
   var columns = new List<string>
   {
        "EmployeeID",
        "LastName",
        "FirstName",
        "Title ",
        "TitleOfCourtesy",
        "BirthDate",
        "HireDate",
        "Address",
        "City",
        "Region", 
        "PostalCode",
        "Country",
        "HomePhone",
        "Extension",
        "Photo",
        "Notes",
        "ReportsTo",
        "PhotoPath"
   };

   // Utilize it in your different SQL statements that require it
   var insert = TSQL

        .INSERT().INTO(table)
          .COLUMNS(columns)
        .VALUES(columns)

   ;

   var selectAll = TSQL 

        .SELECT()
          .COLUMNS(columns)
        .FROM(table)

   ;

   var selectById = TSQL

        .SELECT()
          .COLUMNS(columns)
        .FROM(table)
        .WHERE("EmployeeID").IsEqualTo("@EmployeeID")

   ;

   var update = TSQL

        .UPDATE(table).SET()
          .COLUMNS(columns)
        .WHERE("EmployeeID").IsEqualTo("@EmployeeID")

   ;

It is easy enough to utilize a collection of strings for ``SELECT`` statements in regular old  
concatenated or interpolated SQL strings but it isn't as easy to produce the parameterized 
queries that the ``.INSERT()`` and ``.UPDATE()`` suite of Fluent API methods gives you. 

Furthermore, the collection of columns can be ``TSQLColumn`` objects, which offer the ability 
to include data type information. 

Lastly, the ``.COLUMNS()`` method offers an overload to apply a multipart identifier 
to all the columns in the collection. That way, anywhere you need to JOIN to another table you 
have an easy way to disambiguate the column references. 

You can imagine ways to take this further by, for example, deriving the column names 
from an entity model class using reflection or by pulling the column names directly from the 
database using a schema metadata query, both of which can be executed on startup and placed in 
a singleton so the performance hit is taken only one time. 

In short, the ability to manage the columns in a table in one place is powerful making the 
application code far more maintainable while also promoting the DRY principle. 

Adding To & Merging SQL Objects
-------------------------------

Obtaining a SQL object model also provides the opportunity to add to an already-existing SQL object 
graph or combine SQL fragments that were independently built. You simply have to identify the fragment 
you want to attach another fragment to and thereby add to or combine multiple object graphs to produce 
a new single SQL statement. 

**Basic Add**

Below is an example where we use a base SQL select statement for pulling all records but then 
utilize it to produce a SQL statement for retrieving a single record by adding to the object 
graph that already exists. 

.. code-block:: csharp 

        var table = "Products";
        var columns = new List<string> { "ProductID", "ProductName" };

        var baseSelect = TSQL

            .SELECT()
              .COLUMNS(columns)
            .FROM(table)

        ;

        var selectAll = baseSelect
                            .Build();

        var selectOne = baseSelect
                            .WHERE("ProductID").IsEqualTo("@ProductID")
                            .Build();

        Console.WriteLine(selectAll);
        Console.WriteLine(selectOne);

Output:

.. code-block:: sql

  SELECT
   [ProductID],
   [ProductName]
  FROM [Products]

  SELECT
   [ProductID],
   [ProductName]
  FROM [Products]
  WHERE 1=1
   AND [ProductID] = @ProductID  

**Adding to Arbitrary Fragments**

The trouble you might run into is that you may not obtain a reference to the SQL fragment 
that you are trying to add to. When this happens you have a couple of options. 

1. Tag the target fragment with an ID using ``WithId()`` so that you can use the ``FindById()`` 
   method to obtain a reference to that fragment later. 

   .. code-block:: csharp 

      var productCols = new List<string> { "ProductID", "ProductName" };
      var categoryCols = new List<string> { "CategoryID", "CategoryName" };

      var sqlObj = TSQL

          // Tag the SELECT object with an ID to lookup later
          .SELECT().WithId("abc123") 
            .COLUMNS("p", productCols)
          .FROM("dbo", "Products", "p")

      ;

      // See the type of object returned at this point
      Console.WriteLine("sqlObj: " + sqlObj.GetType().Name);
      Console.WriteLine("");

      // Grab a reference to the SELECT object using the ID.
      // MUST find starting at a node above the target object.
      // Using Root to guarantee the lookup 
      var selectFragment = sqlObj.Root.FindById("abc123") as SqlStatement;

      // Confirm that we looked up the desired SELECT object
      Console.WriteLine("selectFragment: " + selectFragment.GetType().Name);
      Console.WriteLine("");

      // Now we can do extra stuff with the original SQL object graph
      selectFragment
        .COLUMNS("c", categoryCols)
        .INNERJOIN("dbo", "Categories", "c").ON("c", "CategoryID").IsEqualTo("p", "CategoryID");

      // Build and output the whole thing
      var sql = selectFragment.Build();

      Console.WriteLine(sql);

   Output:

   .. code-block::

      sqlObj: TSQLFrom

   .. code-block::
     
      selectFragment: TSQLSelect

   .. code-block:: sql
     
        SELECT
         [p].[ProductID],
         [p].[ProductName],
         [c].[CategoryID],
         [c].[CategoryName]
        FROM [dbo].[Products] p
        INNER JOIN [dbo].[Categories] c ON [c].[CategoryID] = [p].[CategoryID]

   Keep in mind that when looking up by ``Id`` you must start the lookup from a fragment that is above the fragment
   you're looking for. If you start the find from the ``Root`` object then you are guaranteed to get a successful 
   lookup assuming you provide an ``Id`` that exists in the object graph. This is because the ``.FindById()`` method 
   recursively walks the tree of SQL objects downward starting from the object you call the method upon BUT it 
   will not go UP the tree to check the ancestors.

2. You can manually walk the tree of ``SqlStatement`` objects checking each node until you find the one you're 
   looking for and obtain a reference to the desired SQL fragment that way.

   Below is the same example except that instead of using the ``.FindById()`` method, you can walk the tree manually 
   to accomplish the same thing. For example, you may need to do this if the fragment you're looking for is not 
   tagged with an ID.

   .. code-block:: csharp 

      var productCols = new List<string> { "ProductID", "ProductName" };
      var categoryCols = new List<string> { "CategoryID", "CategoryName" };

      var sqlObj = TSQL

          // This SELECT object is not tagged with an identifier
          .SELECT()
            .COLUMNS("p", productCols)
          .FROM("dbo", "Products", "p")

      ;

      // See the type of object returned at this point
      Console.WriteLine("sqlObj: " + sqlObj.GetType().Name);
      Console.WriteLine("");

      SqlStatement selectFragment = null;

      // ProcessTree recurses the tree for you. Each object s in the lambda 
      // is a SqlStatement node in the hierarchical object graph. You just 
      // check for what you're looking for, in this case a SELECT statement.
      sqlObj.Root.ProcessTree(s =>
      {
          var sqlFragment = s as SqlStatement;

          if (sqlFragment.IsSelect)
              selectFragment = sqlFragment;

          return true;
      });

      // Confirm that we looked up the desired SELECT object
      Console.WriteLine("selectFragment: " + selectFragment.GetType().Name);
      Console.WriteLine("");

      // Now we can do extra stuff with the original SQL object graph
      selectFragment
        .COLUMNS("c", categoryCols)
        .INNERJOIN("dbo", "Categories", "c").ON("c", "CategoryID").IsEqualTo("p", "CategoryID");

      // Build and output the whole thing
      var sql = selectFragment.Build();

      Console.WriteLine(sql);

   Output:

   .. code-block::

      sqlObj: TSQLFrom

   .. code-block::
     
      selectFragment: TSQLSelect

   .. code-block:: sql
     
        SELECT
         [p].[ProductID],
         [p].[ProductName],
         [c].[CategoryID],
         [c].[CategoryName]
        FROM [dbo].[Products] p
        INNER JOIN [dbo].[Categories] c ON [c].[CategoryID] = [p].[CategoryID]

**Merging SQL Object Graphs Together**

In some cases where you have two independently created statements you may need to combine them. 
To make this easier, you can call the ``Merge()`` method to combine two SQL object graphs together. 
This is not a simple concatenation of SQL strings. This method will do the job of adding the SQL 
object fragments from the second object graph under the SQL object fragments of the first object 
graph.

Consider the example below where two independently created SQL statements are combined to produce 
a multi-data set result. We can combine the two ro 

.. code-block:: csharp 

    var selectA = TSQL

        .SELECT()
          .STAR()
        .FROM("Products")
        .Terminate()
    ;

    var selectB = TSQL

        .SELECT()
          .STAR()
        .FROM("Categories")
    ;

    var sql = selectA
                .Merge(selectB)
                .Build();

    Console.WriteLine(sql);   

Output:

.. code-block:: sql

  SELECT
   *
  FROM [Products];
  SELECT
   *
  FROM [Categories]  

An important thing to consider in the example above is the use of the ``Terminate()`` 
method, which is explained in detail below. 

Terminate()
-----------

The ``Terminate()`` method simply ensures that the immediately preceding SQL fragment 
is output with a terminating semicolon ``;`` character. You may need to use this if 
you have a complex query or script that must or should be delineated by an explicitly 
terminated statement. 

The example below, similar to the example above where they are independently built and 
merged, shows two SELECT statements meant to return a multi-data-set result, which 
can be crafted in a single fluent statement.

.. code-block:: csharp 

    var sql = TSQL

        .SELECT()
          .STAR()
        .FROM("Products")
        .Terminate()
        .SELECT()
          .STAR()
        .FROM("Categories")
        .Build()
    ;

    Console.WriteLine(sql);  

Output:

.. code-block:: sql

  SELECT
   *
  FROM [Products];
  SELECT
   *
  FROM [Categories]

You can see that the first SELECT statement is terminated with the semicolon character ``;``, 
which was placed there using the ``Terminate()`` method.

.. _withid:

WithId() / FindById()
---------------------

Any SQL fragment object can be tagged with an identifier using the ``WithId()`` method if you 
plan to obtain a reference to that particular fragment later. It can then be used in 
combination with the ``FindById()`` method when the time comes to find it and use it. 

Under the hood, the ``WithId()`` method populates the ``Id`` property of the immediately preceding 
SQL fragment object with the value provided in the method call. 

.. code-block:: csharp 

    var productCols = new List<string> { "ProductID", "ProductName" };

    var sqlObj = TSQL

        .SELECT()
          .COLUMNS(productCols)
        .FROM("Products")
        .WHERE("CategoryID").WithId("wc") // Tag WHERE with Id = "wc"
            .IN(1, 2, 3);
    ;

    // Now grab a reference to the WHERE clause
    // It's good practice to start at the Root object 
    var whereClause = sqlObj.Root.FindById("wc") as SqlStatement;

    // Confirm we have the WHERE clause object
    Console.WriteLine(whereClause.GetType().Name);
    Console.WriteLine();

    var sql = whereClause
                .OR().COLUMN("ProductID").IsEqualTo(99)
                .Build();

    Console.WriteLine(sql);

Output:

.. code-block:: 

  TSQLWhereClause

.. code-block:: sql

  SELECT
   [ProductID],
   [ProductName]
  FROM [Products]
  WHERE 1=1
   AND 'CategoryID' IN (1,2,3)
   OR [ProductID] = 99

Keep in mind that when looking up by ``Id`` you must start the lookup from a fragment that is above the fragment
you're looking for. If you start the find from the ``Root`` object then you are guaranteed to get a successful 
lookup assuming you provide an ``Id`` that exists in the object graph. This is because the ``FindById()`` method 
recursively walks the tree of SQL objects downward starting from the object you call the method upon BUT it 
will not go UP the tree to check the ancestors.

WithName()
----------

The ``WithName()`` method functions exactly like the ``Withid()`` method except that it is 
simply for a different purpose. Tagging a fragment with an identifier is typically meant 
for numeric values or GUIDs whereas tagging a fragment with a name is for labeling the 
fragment with something human readable. 

However, the name is strictly metadata where the major difference is that there is NO corresponding 
``FindByName()`` method. You could use the ``Name`` property of the SQL object when :ref:`walking-the-tree` to 
print the underlying ``Name`` property or use it to find an object but typically if you want to find a SQL 
fragment it is easier to tag it with an ``Id`` and then use the ``FindById()`` method. 

You may find it most useful if you are building a UI that utilizes the underlying object model directly 
instead of the Fluent API and you need to label the objects for display in a form. Another use case is 
to plant names on the objects so that they make sense when looking at the serialized JSON of the SQL 
object graph. 

WithDoc()
---------

The ``WithDoc()`` is most like the ``WithName()`` method where the ``WithDoc()`` method populates the 
object's ``Description`` property with the value provided in the method call. 

The purpose of this is to add documentation about the SQL query in cases where the SQL object may 
need to be examined in JSON or conceivably, for example, in a UI that is designed to serve as a SQL
object explorer. You can imagine hovering the mouse over a SQL fragment that is wired up to 
display the ``Description`` text in a pop-over.

It's basically metadata that lives with the object graph but is completely ignored when producing the 
actual SQL code. 

You can see how it works by adding doc to a SQL fragment and then using :doc:`serialization` to render the 
object graph into JSON. An exercise I leave to the curious reader. 

.. _walking-the-tree:

Walking the Tree 
----------------

.. _KnightMoves.Hierarchical: https://www.nuget.org/packages/KnightMoves.Hierarchical/
.. _the documentation for the KnightMoves.Hierarchical library: https://github.com/KnightMoveSolutions/Hierarchical#knightmoveshierarchical
.. _the KnightMoves.Hierarchical documentation: https://github.com/KnightMoveSolutions/Hierarchical#knightmoveshierarchical

Though it may come as no surprise, the KnightMoves.SQLObjects library utilizes the `KnightMoves.Hierarchical`_ 
library at its core. Examining `the documentation for the KnightMoves.Hierarchical library`_ will give you 
insight into how this library functions. 

The SQL object graph that you build with the Fluent API basically creates a tree of objects that implement
the ``SqlStatement`` base class. Once you know that it is a tree built with the `KnightMoves.Hierarchical`_ 
library, you can see that the underlying SQL objects are tree nodes with all of the methods and properties 
that come with a tree node. As such, you can take advantage of the features offered by the `KnightMoves.Hierarchical`_
library. 

In particular, this object structure could be useful when you need to process the SQL objects recursively or 
in other words ... when you need to "walk the tree". The `KnightMoves.Hierarchical`_ library offers three methods 
for doing so, listed below. 

- ProcessTree()
- ProcessChildren()
- ProcessAncestors()

Each of these is described below. 

For each of the examples consider the following SQL object graph built with the Fluent API below. 

.. code-block:: csharp 

    var sqlObj = TSQL

        .SELECT()
          .COLUMN("c", "CategoryName")
          .COUNT("p", "ProductID").AS("TotalProducts").WithId("abc123")
          .AVG("p", "UnitPrice").AS("AvgPrice")
        .FROM("dbo", "Products", "p")
        .INNERJOIN("dbo", "Categories", "c").ON("c", "CategoryID").IsEqualTo("p", "CategoryID")
        .GROUPBY()
          .COLUMN("c", "CategoryName")

    ;

Notice that we tagged the ``COUNT()`` method with and identifier, which we will make use of later. 
So with that object graph above, you can walk the tree up or down using the relevant methods below. 

**ProcessTree()**

Use the ``ProcessTree()`` method to walk the tree of objects down from AND INCLUDING the object that 
the method is called from. 

.. code-block:: csharp

    sqlObj.Root.ProcessTree(node =>
    {
        Console.WriteLine(node.IndentString + node.GetType().Name);
        return true;
    });

In the code above, notice that we reference the ``Root`` object. The ``Root`` object is referenced by 
every node in the entire tree so you can get to the top of the tree from anywhere. 

The ``ProcessTree()`` method accepts a lambda where the parameter (here we called it ``node``) is the 
object in the tree passed to your lambda function as it is recursively walking down the tree. 

Also notice that we utilize the ``node.IndentString`` property to display the values with indentation 
so it is easier to see the structure of the tree. In fact, the ``IndentString`` property is used to format 
the SQL output when calling ``.Build()`` as you've seen throughout this documentation. 

Here's the output of the ``ProcessTree()`` logic above. 

.. code-block:: 

 TSQLScript
  TSQLSelect
   TSQLColumn
   TSQLFuncCount
   TSQLFuncAvg
  TSQLFrom
  TSQLInnerJoin
   TSQLBasicCondition
    TSQLColumn
    TSQLColumn
  TSQLGroupBy
   TSQLColumn  

This reveals the types of objects that are being created by the Fluent API as you call its methods to 
build your SQL object graph. If you cast the ``node`` object as ``SqlStatement`` or its concrete type 
then you can access their relevant properties or call the ``SQL()`` method to pull the SQL output from 
them individually. 

The most common use case for this is if you need to find an object so you can reference it for the purpose 
of building more SQL under it using the Fluent API, assuming you cannot use the ``FindById()`` method. 
Once you have a reference to it and cast it to ``SqlStatement``, then you can call the Fluent API methods 
upon it to continue to insert SQL statements at that point. The other objects below it will still remain.

.. warning::

   Manually obtaining references to SQL fragments is not the primary use case for this library so your 
   mileage may vary. What you do with it may or may not work as intended.

**ProcessChildren()**

The ``ProcessChildren()`` method functions just like the ``ProcessTree()`` method except that it will begin 
its processing starting with the child nodes. In the case of the ``Root`` object in this example, which is 
a ``TSQLScript`` object, then processing will not include itself (i.e. the ``TSQLScript`` object). 

.. code-block:: csharp

    sqlObj.Root.ProcessChildren(node =>
    {
        Console.WriteLine(node.IndentString + node.GetType().Name);
        return true;
    });

And here's the output:

.. code-block:: 

  TSQLSelect
   TSQLColumn
   TSQLFuncCount
   TSQLFuncAvg
  TSQLFrom
  TSQLInnerJoin
   TSQLBasicCondition
    TSQLColumn
    TSQLColumn
  TSQLGroupBy
   TSQLColumn

You can see that the ``TSQLScript`` object was not included. Use this if you do not want to include the 
current object in the recursion - you only want it to recurse the objects below the current object. 

**ProcessAncestors()**

The ``ProcessAncestors()`` is a bit more robust. Full documentation of this method is provided in `the 
KnightMoves.Hierarchical documentation`_. 

In the example below, we obtain a reference to the SQL COUNT() function that we tagged with an identifier
so that once we have a reference to a SQL fragment object in the middle of the tree somewhere, we can then
walk UP the tree using the ``ProcessAncestors()`` method. 

.. code-block:: csharp 

    var countFunction = sqlObj.Root.FindById("abc123");

    countFunction.ProcessAncestors(
        node =>
        {
            Console.WriteLine(node.IndentString + node.GetType().Name);
            return true;
        },
        countFunction
    );

Since we are going up the tree and printing the type names with indentation, it has the effect of flipping 
the tree upside down from the point we started processing. 

Here is the output:

.. code-block::

   TSQLFuncCount
  TSQLSelect
 TSQLScript

The first argument to the ``ProcessAncestors()`` method is a lambda that functions just like the ``ProcessTree()`` 
and ``ProcessChildren()`` methods except that instead of recursing down the tree it is recusing up the tree for you. 

The second argument to the ``ProcessAncestors()`` method is the object that you want to start from, in this case 
the reference to the SQL COUNT() function. The process is inclusive of the starting point as you can see in the 
output that it begins with printing the name of the ``TSQLFuncCount`` class. 

.. tip::

  Keep in mind that when recursing up the tree, it is going from parent to parent and siblings are ignored. This 
  means that the inverted tree output only goes so far. In the example above you see that the other SELECT list 
  items are not displayed because they are siblings of the COUNT() function.

Whether you need to go up or down the tree for whatever purpose you desire, you can see that using the methods 
above should provide you with the tools to do pretty much anything you want. 

Checking Fragment Types
-----------------------

If you take advantage of the underlying SQL object model in any way, you are likely find it useful to check what 
type of SQL fragment you are dealing with. Since each SQL fragment object implements the ``SqlStatement`` base 
class, they will all provide the following boolean properties that will tell you what sort of object you're 
dealing with, without having to know the name of the actual C# object type. 


+-----------------------+-------------------------------------------------------------------------------+
| Property              | Remarks                                                                       |
+=======================+===============================================================================+
| IsDelete              | ``DELETE`` statement                                                          |
+-----------------------+-------------------------------------------------------------------------------+
| IsFrom                | ``FROM`` clause                                                               |
+-----------------------+-------------------------------------------------------------------------------+
| IsGroupBy             | ``GROUP BY`` statement                                                        |
+-----------------------+-------------------------------------------------------------------------------+
| IsInsert              | ``INSERT`` statement                                                          |
+-----------------------+-------------------------------------------------------------------------------+
| IsJoin                | Any ``JOIN`` statement                                                        |
+-----------------------+-------------------------------------------------------------------------------+
| IsRightJoin           | ``RIGHT JOIN`` statement. If true then ``IsJoin`` is also true                |
+-----------------------+-------------------------------------------------------------------------------+
| IsLeftJoin            | ``LEFT JOIN`` statement. If true then ``IsJoin`` is also true                 |
+-----------------------+-------------------------------------------------------------------------------+
| IsInnerJoin           | ``INNER JOIN`` statement. If true then ``IsJoin`` is also true                |
+-----------------------+-------------------------------------------------------------------------------+
| IsScript              | True if the object is an ad hoc script or the root script                     |
+-----------------------+-------------------------------------------------------------------------------+
| IsSelect              | ``SELECT`` statement                                                          |
+-----------------------+-------------------------------------------------------------------------------+
| IsUnion               | ``UNION`` clause                                                              |
+-----------------------+-------------------------------------------------------------------------------+
| IsUpdate              | ``UPDATE`` statement                                                          |
+-----------------------+-------------------------------------------------------------------------------+
| IsWhereClause         | ``WHERE`` clause                                                              |
+-----------------------+-------------------------------------------------------------------------------+
| IsBasicCondition      | Condition as ``LeftOperand`` ``Operator`` ``RightOperand``                    |
+-----------------------+-------------------------------------------------------------------------------+
| IsBetween             | ``BETWEEN`` condition                                                         |
+-----------------------+-------------------------------------------------------------------------------+
| IsCalculation         | Calculation as ``LeftOperand`` ``ArithmeticOperator`` ``RightOperand``        |
+-----------------------+-------------------------------------------------------------------------------+
| IsColumn              | A column specification                                                        |
+-----------------------+-------------------------------------------------------------------------------+
| IsComment             | An object containing a SQL comment                                            |
+-----------------------+-------------------------------------------------------------------------------+
| IsCondition           | Any condition                                                                 |
+-----------------------+-------------------------------------------------------------------------------+
| IsConditionGroup      | A scope containing coditions output between ``(`` and ``)``                   |
+-----------------------+-------------------------------------------------------------------------------+
| IsDistinct            | ``DISTINCT`` keyword                                                          |
+-----------------------+-------------------------------------------------------------------------------+
| IsFunction            | Any type of function call                                                     |
+-----------------------+-------------------------------------------------------------------------------+
| IsFunctionAbs         | ``ABS()`` function call                                                       |
+-----------------------+-------------------------------------------------------------------------------+
| IsFunctionAvg         | ``AVG()`` function call                                                       |
+-----------------------+-------------------------------------------------------------------------------+
| IsFunctionCeiling     | ``CEILING()`` function call                                                   |
+-----------------------+-------------------------------------------------------------------------------+
| IsFunctionCount       | ``COUNT()`` function call                                                     |
+-----------------------+-------------------------------------------------------------------------------+
| IsFunctionDateAdd     | ``DATEADD()`` function call                                                   |
+-----------------------+-------------------------------------------------------------------------------+
| IsFunctionDateDiff    | ``DATEDIFF()`` function call                                                  |
+-----------------------+-------------------------------------------------------------------------------+
| IsFunctionDateName    | ``DATENAME()`` function call                                                  |
+-----------------------+-------------------------------------------------------------------------------+
| IsFunctionDatePart    | ``DATEPART()`` function call                                                  |
+-----------------------+-------------------------------------------------------------------------------+
| IsFunctionDay         | ``DAY()`` function call                                                       |
+-----------------------+-------------------------------------------------------------------------------+
| IsFunctionFloor       | ``FLOOR()`` function call                                                     |
+-----------------------+-------------------------------------------------------------------------------+
| IsFunctionGetDate     | ``GETDATE()`` function call                                                   |
+-----------------------+-------------------------------------------------------------------------------+
| IsFunctionMax         | ``MAX()`` function call                                                       |
+-----------------------+-------------------------------------------------------------------------------+
| IsFunctionMin         | ``MIN()`` function call                                                       |
+-----------------------+-------------------------------------------------------------------------------+
| IsFunctionMonth       | ``MONTH()`` function call                                                     |
+-----------------------+-------------------------------------------------------------------------------+
| IsFunctionParameter   | A function parameter object                                                   |
+-----------------------+-------------------------------------------------------------------------------+
| IsFunctionSum         | ``SUM()`` function call                                                       |
+-----------------------+-------------------------------------------------------------------------------+
| IsFunctionYear        | ``YEAR()`` function call                                                      |
+-----------------------+-------------------------------------------------------------------------------+
| IsHaving              | ``HAVING`` clause                                                             |
+-----------------------+-------------------------------------------------------------------------------+
| IsInList              | ``IN (...)`` condition                                                        |
+-----------------------+-------------------------------------------------------------------------------+
| IsLike                | ``LIKE ''`` condition                                                         |
+-----------------------+-------------------------------------------------------------------------------+
| IsLiteral             | An object containing an ad hoc literal                                        |
+-----------------------+-------------------------------------------------------------------------------+
| IsOrderBy             | ``ORDER BY`` clause                                                           |
+-----------------------+-------------------------------------------------------------------------------+
| IsOrderByExpression   | An expression under and ``ORDER BY`` clause making a sort specification       |
+-----------------------+-------------------------------------------------------------------------------+
| IsQueryExpression     | Any SQL fragment that constitutes a valid query expression                    |
+-----------------------+-------------------------------------------------------------------------------+
| IsSubQuery            | A subquery scope denoted by ``(`` and ``)``                                   |
+-----------------------+-------------------------------------------------------------------------------+
| IsTop                 | ``TOP`` filter against the rows of a ``SELECT`` result                        |
+-----------------------+-------------------------------------------------------------------------------+

These boolean properties can be utilized to identify the type of object you are seeking in your logic 
while processing the SQL fragments in the tree.

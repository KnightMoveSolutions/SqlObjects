`Documentation Home <https://docs.knightmovesolutions.com>`_

.. include:: northwind-db-tip.rst

=====
WHERE
=====

Basic Syntax
------------

.. code-block:: csharp

    var sql = TSQL

        .SELECT()
          .STAR()
        .FROM("dbo", "Categories")
        .WHERE("CategoryID").IsEqualTo(1)
        .Build()

    ;

    Console.WriteLine(sql);

Output:

.. code-block:: sql

  SELECT
   *
  FROM [dbo].[Categories]
  WHERE 1=1
   AND [CategoryID] = 1


:doc:`where-1-equals-1`

**WHERE Clause with Multipart Identifier Table Alias**

.. code-block:: csharp

    var sql = TSQL

        .SELECT()
          .STAR()
        .FROM("dbo", "Categories", "c")
        .WHERE("c", "CategoryID").IsEqualTo(1)
        .Build()

    ;

    Console.WriteLine(sql);

Output:

.. code-block:: sql

  SELECT
   *
  FROM [dbo].[Categories] c
  WHERE 1=1
   AND [c].[CategoryID] = 1


:doc:`where-1-equals-1`

.. _comparison-operators:

Comparison Operators
--------------------

WHERE clause conditions of the form 

   ``[LeftOperand] [ComparisonOperator] [RightOperand]`` 

can be written using the following list of Comparison Operators:

+------------------------------+---------+
| Operator                     | Output  |
+==============================+=========+
| ``IsEqualTo()``              |    =    |
+------------------------------+---------+
| ``IsGreatherThan()``         |    >    |
+------------------------------+---------+
| ``IsGreaterThanOrEqualTo()`` |    >=   |
+------------------------------+---------+
| ``IsLessThan()``             |    <    |
+------------------------------+---------+
| ``IsLessThanOrEqualTo()``    |    <=   |
+------------------------------+---------+
| ``IsNotEqualTo()``           |    <>   |
+------------------------------+---------+

.. _and-logical-operator:

AND
---

**WHERE Clause with Multiple Conditions Using** ``.AND()``

.. code-block:: csharp

    var sql = TSQL

        .SELECT()
          .STAR()
        .FROM("dbo", "Products")
        .WHERE("Discontinued").IsEqualTo(false).AND()
          .COLUMN("UnitPrice").IsGreaterThan(10.00m).AND()
          .COLUMN("UnitsInStock").IsGreaterThanOrEqualTo(1)
        .Build()

    ;

    Console.WriteLine(sql);

Output:

.. code-block:: sql

  SELECT
   *
  FROM [dbo].[Products]
  WHERE 1=1
   AND [Discontinued] = 0
   AND [UnitPrice] > 10.00
   AND [UnitsInStock] >= 1

This could also be written using the ``.AND(string operand)`` method as shown below and it will produce 
the same output as above.

.. code-block:: csharp 

    var sql = TSQL

        .SELECT()
          .STAR()
        .FROM("dbo", "Products")
        .WHERE("Discontinued").IsEqualTo(false)
          .AND("UnitPrice").IsGreaterThan(10.00m)
          .AND("UnitsInStock").IsGreaterThanOrEqualTo(1)
        .Build()

    ;

    Console.WriteLine(sql);

:doc:`where-1-equals-1`

.. note::

   For the most part, using ``.AND()`` with no arguments can be skipped. All conditions will be output 
   with the ``AND`` logical operator by default. It is included in all examples to mimic SQL as much as 
   possible and for explicit understanding. This is not the case with ``OR``. If you want to use the 
   ``OR`` logical operator with multiple conditions then you must use the ``.OR()`` method.

.. _or-logical-operator:

OR
--

**WHERE Clause with Multiple OR Conditions**

.. code-block:: csharp

    var sql = TSQL

        .SELECT()
          .STAR()
        .FROM("dbo", "Categories")
        .WHERE("CategoryName").IsEqualTo("Beverages").OR()
          .COLUMN("CategoryName").IsEqualTo("Produce").OR()
          .COLUMN("CategoryName").IsEqualTo("Seafood")
        .Build()

    ;

    Console.WriteLine(sql);

Output:

.. code-block:: sql

  SELECT
   *
  FROM [dbo].[Categories]
  WHERE 1=1
   AND [CategoryName] = 'Beverages'
   OR [CategoryName] = 'Produce'
   OR [CategoryName] = 'Seafood'

This could also be written using the ``.OR(string columnName)`` method as shown below and it will produce 
the same output as above.

.. code-block:: csharp 

    var sql = TSQL

        .SELECT()
          .STAR()
        .FROM("dbo", "Categories")
        .WHERE("CategoryName").IsEqualTo("Beverages")
          .OR("CategoryName").IsEqualTo("Produce")
          .OR("CategoryName").IsEqualTo("Seafood")
        .Build();

    ;

    Console.WriteLine(sql);

:doc:`where-1-equals-1`

**Using Condition Scopes for Mixing** ``.AND()`` **With** ``.OR()``

.. code-block:: csharp 

    var sql = TSQL

        .SELECT()
          .STAR()
        .FROM("dbo", "Products")
        .WHERE("Discontinued").IsEqualTo(false).AND()
          .COLUMN("UnitPrice").IsGreaterThan(10.00m).AND()
          .StartConditionScope()                              // Opens parentheses (
            .COLUMN("CategoryID").IsEqualTo(2).OR()
            .COLUMN("CategoryID").IsEqualTo(4)
          .EndConditionScope()                                // Closes parentheses )
        .Build()

    ;

    Console.WriteLine(sql);

Output:

.. code-block:: sql

  SELECT
   *
  FROM [dbo].[Products]
  WHERE 1=1
   AND [Discontinued] = 0
   AND [UnitPrice] > 10.00
   AND
   (
    [CategoryID] = 2 OR
    [CategoryID] = 4
   )


   
:doc:`where-1-equals-1`

.. _between-condition:

BETWEEN
-------

**WHERE Clause Using** ``BETWEEN [StartVal] AND [EndVal]``

.. code-block:: csharp 

    var sql = TSQL

        .SELECT()
          .STAR()
        .FROM("dbo", "Products")
        .WHERE("Discontinued").IsEqualTo(false).AND()
          .COLUMN("UnitPrice").BETWEEN(10.00m).AND(20.00m)
        .Build()

    ;

    Console.WriteLine(sql);

Output:

.. code-block:: sql

  SELECT
   *
  FROM [dbo].[Products]
  WHERE 1=1
   AND [Discontinued] = 0
   AND [UnitPrice] BETWEEN 10.00 AND 20.00
   
:doc:`where-1-equals-1`

.. _in-clause:

IN
--

**WHERE Clause Using** ``IN (...)``

.. code-block:: csharp 

    var sql = TSQL

        .SELECT()
          .STAR()
        .FROM("dbo", "Products")
        .WHERE("Discontinued").IsEqualTo(false).AND()
          .COLUMN("CategoryID").IN(1,2,3)
        .Build()

    ;

    Console.WriteLine(sql);

Output:

.. code-block:: sql

  SELECT
   *
  FROM [dbo].[Products]
  WHERE 1=1
   AND [Discontinued] = 0
   AND [CategoryID] IN (1,2,3)

   
:doc:`where-1-equals-1`

**WHERE Clause Using** ``IN (...)`` **with an Array**

.. code-block:: csharp 

    var intArray = new int[3] { 1, 2, 3 };

    var sql = TSQL

        .SELECT()
          .STAR()
        .FROM("dbo", "Products")
        .WHERE("Discontinued").IsEqualTo(false).AND()
          .COLUMN("CategoryID").IN(intArray)
        .Build()

    ;

    Console.WriteLine(sql);

Output:

.. code-block:: sql

  SELECT
   *
  FROM [dbo].[Products]
  WHERE 1=1
   AND [Discontinued] = 0
   AND [CategoryID] IN (1,2,3)

   
:doc:`where-1-equals-1`

**WHERE Clause Using** ``IN (...)`` **with a Collection**

.. code-block:: csharp 

    var intList = new List<int> { 1, 2, 3 };

    var sql = TSQL

        .SELECT()
          .STAR()
        .FROM("dbo", "Products")
        .WHERE("Discontinued").IsEqualTo(false).AND()
          .COLUMN("CategoryID").IN(intList.ToArray())
        .Build()

    ;

    Console.WriteLine(sql);

Output:

.. code-block:: sql

  SELECT
   *
  FROM [dbo].[Products]
  WHERE 1=1
   AND [Discontinued] = 0
   AND [CategoryID] IN (1,2,3)

   
:doc:`where-1-equals-1`

You can use the ``AND(string operand)`` method as shown below. However, if you want to specify a column name 
as the left operand then you must manually add the enclosing square brackets ``[]`` or it will interpret
the value as a string and enclose it in single quotes ``''``

.. code-block:: csharp 

    var sql = TSQL

        .SELECT()
          .STAR()
        .FROM("dbo", "Products")
        .WHERE("Discontinued").IsEqualTo(false)
          .AND("[CategoryID]").IN(1,2,3)
        .Build()

    ;

    Console.WriteLine(sql);
   
:doc:`where-1-equals-1`

.. _like-condition:

LIKE
----

**WHERE Clause Using a** ``LIKE`` **Condition**

.. code-block:: csharp 

    var sql = TSQL

        .SELECT()
          .STAR()
        .FROM("dbo", "Products")
        .WHERE("Discontinued").IsEqualTo(false).AND()
          .COLUMN("ProductName").LIKE("Ch%")
        .Build()

    ;

    Console.WriteLine(sql);

Output:

.. code-block:: sql

  SELECT
   *
  FROM [dbo].[Products]
  WHERE 1=1
   AND [Discontinued] = 0
   AND [ProductName] LIKE 'Ch%'

You can use the ``AND(string operand)`` method as shown below. However, if you want to specify a column name 
as the left operand then you must manually add the enclosing square brackets ``[]`` or it will interpret
the value as a string and enclose it in single quotes ``''``

.. code-block:: csharp 

    var sql = TSQL

        .SELECT()
          .STAR()
        .FROM("dbo", "Products")
        .WHERE("Discontinued").IsEqualTo(false)
          .AND("[ProductName]").LIKE("Ch%")
        .Build()

    ;

    Console.WriteLine(sql);
   
:doc:`where-1-equals-1`


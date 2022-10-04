.. include:: northwind-db-tip.rst

============
Calculations
============

Calculations are started with the ``.COLUMN()``, ``.Calculate()``, ``.CASE()``, or ``.WHEN()`` 
Fluent API methods and their various overloads. 

Each of the arithmetic operators (=, -, \*, /, %) 
are implemented with their respective Fluent API listed in the table below. 

Arithmetic Operators
--------------------

Calculations in the form of

   ``[LeftOperand] [ArithmeticOperator] [RightOperand]`` 

can be written using the ``.Calculate()`` method followed by one of the  Arithmetic Operator methods below:

+------------------------------+---------+
| Operator                     | Output  |
+==============================+=========+
| ``Plus()``                   |    +    |
+------------------------------+---------+
| ``Minus()``                  |    -    |
+------------------------------+---------+
| ``Times()``                  |    *    |
+------------------------------+---------+
| ``DividedBy()``              |    /    |
+------------------------------+---------+
| ``Modulo()``                 |    %    |
+------------------------------+---------+


Using .COLUMN()
---------------

.. code-block:: csharp 

    var sql = TSQL

        .SELECT()
          .COLUMN("UnitsInStock").Plus("UnitsOnOrder").AS("InventoryOnHand")
          .COLUMN("UnitsInStock").Minus("ReorderLevel").AS("EffectiveInventory")
          .COLUMN("UnitPrice").Times("ReorderLevel").AS("MinimumOrderTotal")
          .COLUMN("UnitsInStock").Modulo("ReorderLevel").AS("UnsellableUnits")
          .COLUMN("UnitsInStock").DividedBy(1).AS("DivideTest")
        .FROM("Products")
        .WHERE()
          .COLUMN("UnitPrice").IsGreaterThan(0).AND()
          .COLUMN("ReorderLevel").IsGreaterThan(0).AND()
          .COLUMN("UnitsInStock").IsGreaterThan("[ReorderLevel]")
        .Build()

    ;

    Console.WriteLine(sql);

Output:

.. code-block:: sql

  SELECT
   ([UnitsInStock] + [UnitsOnOrder]) AS [InventoryOnHand],
   ([UnitsInStock] - [ReorderLevel]) AS [EffectiveInventory],
   ([UnitPrice] * [ReorderLevel]) AS [MinimumOrderTotal],
   ([UnitsInStock] % [ReorderLevel]) AS [UnsellableUnits],
   ([UnitsInStock] / 1) AS [DivideTest]
  FROM [Products]
  WHERE 1=1
   AND [UnitPrice] > 0
   AND [ReorderLevel] > 0
   AND [UnitsInStock] > [ReorderLevel]


Using .Calculate()
------------------

.. code-block:: csharp 

    var sql = TSQL

        .SELECT()
          .Calculate("UnitsInStock").Plus("UnitsOnOrder").AS("InventoryOnHand")
          .Calculate("UnitsInStock").Minus("ReorderLevel").AS("EffectiveInventory")
          .Calculate("UnitPrice").Times("ReorderLevel").AS("MinimumOrderTotal")
          .Calculate("UnitsInStock").Modulo("ReorderLevel").AS("UnsellableUnits")
          .FLOOR("[UnitsInStock] / [ReorderLevel]").AS("FulfillableOrders")
          .Calculate(1).DividedBy(1).AS("DivideTest")
        .FROM("Products")
        .WHERE()
          .COLUMN("UnitPrice").IsGreaterThan(0).AND()
          .COLUMN("ReorderLevel").IsGreaterThan(0).AND()
          .COLUMN("UnitsInStock").IsGreaterThan("[ReorderLevel]")
        .Build()

    ;

    Console.WriteLine(sql);

Output:

.. code-block:: sql

  SELECT
   ([UnitsInStock] + [UnitsOnOrder]) AS [InventoryOnHand],
   ([UnitsInStock] - [ReorderLevel]) AS [EffectiveInventory],
   ([UnitPrice] * [ReorderLevel]) AS [MinimumOrderTotal],
   ([UnitsInStock] % [ReorderLevel]) AS [UnsellableUnits],
   FLOOR([UnitsInStock] / [ReorderLevel]) AS [FulfillableOrders],
   (1 / 1) AS [DivideTest]
  FROM [Products]
  WHERE 1=1
   AND [UnitPrice] > 0
   AND [ReorderLevel] > 0
   AND [UnitsInStock] > [ReorderLevel]

Things to note about the example above.

1. The ``Calculate()``, ``.{ArithmeticOperator}()``, and the ``.AS()`` methods all accept strings that 
   are assumed to be column names. Therefore, they are output in delimited format.

2. It includes a preview of the ``.FLOOR()`` function, which is :ref:`documented here<floor-function>`.

3. A calculation can be fed into a function, however, there is no provision for using Fluent API calls to 
   acheive this. You will need to code the raw SQL as an argument into the function to pass the result of 
   the calculation as the argument for the resulting SQL function. 

4. The ``.DividedBy()`` arithmetic operator method example uses a silly divide by one to show that the 
   ``.Calculate()`` method can be used to provide literal numeric values for the left operand.

.. note::
   If you need to provide a literal numeric value for the left operand use ``.Calculate()``. This cannot
   be done when using ``.COLUMN()`` for obvious reasons.

Using .CASE()
-------------

.. code-block:: csharp 

    var sql = TSQL

        .SELECT()
          .COLUMN("ProductID")
          .COLUMN("ProductName")
          .CASE(returnType: SqlDbType.VarChar, "[UnitsInStock]").Modulo("ReorderLevel")
            .WHEN(0).THEN("No")
            .ELSE("Yes")
          .END().AS("HasCloseOutUnits")
        .FROM("Products")
        .WHERE()
            .COLUMN("Discontinued").IsEqualTo(false).AND()
            .COLUMN("ReorderLevel").IsGreaterThan(0)
        .Build()

    ;

    Console.WriteLine(sql);

Output:

.. code-block:: sql

  SELECT
   [ProductID],
   [ProductName],
   CASE ([UnitsInStock] % [ReorderLevel])
    WHEN 0 THEN 'No'
    ELSE 'Yes'
   END AS [HasCloseOutUnits]
  FROM [Products]
  WHERE 1=1
   AND [Discontinued] = 0
   AND [ReorderLevel] > 0

:doc:`where-1-equals-1`

Using .WHEN()
-------------

.. code-block:: csharp 

    var sql = TSQL

        .SELECT()
          .COLUMN("ProductID")
          .COLUMN("ProductName")
          .CASE(returnType: SqlDbType.VarChar)
            .WHEN("[UnitsInStock]").Modulo("ReorderLevel").IsEqualTo(0).THEN("No")
            .ELSE("Yes")
          .END().AS("HasCloseOutUnits")
        .FROM("Products")
        .WHERE()
          .COLUMN("Discontinued").IsEqualTo(false).AND()
          .COLUMN("ReorderLevel").IsGreaterThan(0)
        .Build()

    ;

    Console.WriteLine(sql);

Output:

.. code-block:: sql

  SELECT
   [ProductID],
   [ProductName],
   CASE
    WHEN ([UnitsInStock] % [ReorderLevel]) = 0 THEN 'No'
    ELSE 'Yes'
   END AS [HasCloseOutUnits]
  FROM [Products]
  WHERE 1=1
   AND [Discontinued] = 0
   AND [ReorderLevel] > 0

   
:doc:`where-1-equals-1`

In the .WHERE() Clause
----------------------

Any of the methods above can be utilized in the WHERE clause. The example below demonstrates 
the use of a calculation initiated with the ``.COLUMN()`` method.

.. code-block:: csharp 

    var sql = TSQL

        .SELECT()
          .COLUMN("ProductID")
          .COLUMN("ProductName")
          .Literal("'Yes' AS [HasCloseOutUnits]")
        .FROM("Products")
        .WHERE()
          .COLUMN("Discontinued").IsEqualTo(false).AND()
          .COLUMN("ReorderLevel").IsGreaterThan(0).AND()
          .COLUMN("UnitsInStock").Modulo("ReorderLevel").IsGreaterThan(0)
        .Build()

    ;

    Console.WriteLine(sql);

Output:

.. code-block:: sql

  SELECT
   [ProductID],
   [ProductName],
   'Yes' AS [HasCloseOutUnits]
  FROM [Products]
  WHERE 1=1
   AND [Discontinued] = 0
   AND [ReorderLevel] > 0
   AND ([UnitsInStock] % [ReorderLevel]) > 0

:doc:`where-1-equals-1`


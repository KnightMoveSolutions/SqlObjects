`Documentation Home <https://docs.knightmovesolutions.com>`_

.. include:: northwind-db-tip.rst

======
UPDATE
======

**Basic Update**

.. code-block:: csharp

    var sql = TSQL

        .UPDATE("Categories").SET()
          .COLUMN("CategoryName").IsEqualTo("Vegan")
          .COLUMN("Description").IsEqualTo("Exclusively plant-based food")
        .WHERE("CategoryID").IsEqualTo(9)
        .Build()

    ;

    Console.WriteLine(sql);

Output:

.. code-block:: sql

  UPDATE [Categories] SET
   [CategoryName] = 'Vegan',
   [Description] = 'Exclusively plant-based food'
  WHERE 1=1
   AND [CategoryID] = 9
   
:doc:`where-1-equals-1`

.. note::

   Using any other comparison operator method beside ``IsEqualTo`` will cause the update for the column to be ignored

**Basic Update with Table Schema**

.. code-block:: csharp

    var sql = TSQL

        .UPDATE("dbo", "Categories").SET()
          .COLUMN("CategoryName").IsEqualTo("Vegan")
          .COLUMN("Description").IsEqualTo("Exclusively plant-based food")
        .WHERE("CategoryID").IsEqualTo(9)
        .Build()

    ;

    Console.WriteLine(sql);

Output:

.. code-block:: sql

  UPDATE [dbo].[Categories] SET
   [CategoryName] = 'Vegan',
   [Description] = 'Exclusively plant-based food'
  WHERE 1=1
   AND [CategoryID] = 9
   
:doc:`where-1-equals-1`

**Update Using a Collection of Strings**

Specifying an ``IEnumerable<string>`` collection for the UPDATE list will SET
the column values equal to @parameters using column names as a naming convention.

.. code-block:: csharp

    var columns = new List<string> { "CategoryName", "Description" };

    var sql = TSQL

        .UPDATE("dbo", "Categories").SET()
          .COLUMNS(columns)
        .WHERE("CategoryID").IsEqualTo(9)
        .Build()

    ;

    Console.WriteLine(sql);

Output:

.. code-block:: sql

  UPDATE [dbo].[Categories] SET
   [CategoryName] = @CategoryName,
   [Description] = @Description
  WHERE 1=1
   AND [CategoryID] = 9
   
:doc:`where-1-equals-1`

**Update Using a Collection of** ``TSQLColumn`` **Objects**

Specifying a collection of ``TSQLColumn`` objects for the UPDATE list will SET
the column values equal to @parameters using column names as a naming convention.

.. code-block:: csharp

    var columns = new List<TSQLColumn>
    { 
        new TSQLColumn { ColumnName = "CategoryName" },
        new TSQLColumn { ColumnName = "Description" }
    };

    var sql = TSQL

        .UPDATE("dbo", "Categories").SET()
          .COLUMNS(columns)
        .WHERE("CategoryID").IsEqualTo(9)
        .Build()

    ;

    Console.WriteLine(sql);

Output:

.. code-block:: sql

  UPDATE [dbo].[Categories] SET
   [CategoryName] = @CategoryName,
   [Description] = @Description
  WHERE 1=1
   AND [CategoryID] = 9
   
:doc:`where-1-equals-1`

**Using a Parameter in the** ``WHERE`` **Clause**

.. code-block:: csharp

    var sql = TSQL

        .UPDATE("Categories").SET()
          .COLUMN("CategoryName").IsEqualTo("Vegan")
          .COLUMN("Description").IsEqualTo("Exclusively plant-based food")
        .WHERE("CategoryID").IsEqualTo("@CategoryID")
        .Build()

    ;

    Console.WriteLine(sql);

Output:

.. code-block:: sql

  UPDATE [Categories] SET
   [CategoryName] = 'Vegan',
   [Description] = 'Exclusively plant-based food'
  WHERE 1=1
   AND [CategoryID] = @CategoryID
   
:doc:`where-1-equals-1`

.. tip::
   
   Obviously you can use a ``@ParameterName`` in the ``WHERE`` clause in conjunction with any of the 
   variations of ``UPDATE`` statements above, which is recommended.

`Documentation Home <https://docs.knightmovesolutions.com>`_

.. include:: northwind-db-tip.rst

======
INSERT
======

**Basic Insert**

.. code-block:: csharp

    var sql = TSQL

        .INSERT().INTO("Categories")
          .COLUMN("CategoryName")
          .COLUMN("Description")
        .VALUES()
          .VALUE("Vegan")
          .VALUE("Exclusively plant-based food")
        .Build()

    ;

    Console.WriteLine(sql);

Output:

.. code-block:: sql

  INSERT INTO [Categories]
  (
   [CategoryName],
   [Description]
  ) VALUES (
   'Vegan',
   'Exclusively plant-based food'
  )

**Basic Insert with Table Schema**

.. code-block:: csharp

    var sql = TSQL

        .INSERT().INTO("dbo", "Categories")
          .COLUMN("CategoryName")
          .COLUMN("Description")
        .VALUES()
          .VALUE("Vegan")
          .VALUE("Exclusively plant-based food")
        .Build()

    ;

    Console.WriteLine(sql);

Output:

.. code-block:: sql

  INSERT INTO [dbo].[Categories]
  (
   [CategoryName],
   [Description]
  ) VALUES (
   'Vegan',
   'Exclusively plant-based food'
  )

**Insert Using a Collection of Strings for Column Names**

.. code-block:: csharp

    var columns = new List<string> { "CategoryName", "Description" };

    var sql = TSQL

        .INSERT().INTO("dbo", "Categories")
          .COLUMNS(columns)
        .VALUES()
          .VALUE("Vegan")
          .VALUE("Exclusively plant-based food")
        .Build()

    ;

    Console.WriteLine(sql);

Output:

.. code-block:: sql

  INSERT INTO [dbo].[Categories]
  (
   [CategoryName],
   [Description]
  ) VALUES (
   'Vegan',
   'Exclusively plant-based food'
  )

**Insert Using a Collection of Strings for Columns and Values**

Specifying an ``IEnumerable<string>`` collection for the VALUES clause will produce parameters using the 
column names as a naming convention.

.. code-block:: csharp

    var columns = new List<string> { "CategoryName", "Description" };

    var sql = TSQL

        .INSERT().INTO("dbo", "Categories")
          .COLUMNS(columns)
        .VALUES(columns)
        .Build()

    ;

    Console.WriteLine(sql);

Output:

.. code-block:: sql

  INSERT INTO [dbo].[Categories]
  (
   [CategoryName],
   [Description]
  ) VALUES (
   @CategoryName,
   @Description
  )


**Insert Using a Collection of** ``TSQLColumn`` **Objects**

.. code-block:: csharp

    var columns = new List<TSQLColumn>
    { 
        new TSQLColumn { ColumnName = "CategoryName" },
        new TSQLColumn { ColumnName = "Description" }
    };

    var sql = TSQL

        .INSERT().INTO("dbo", "Categories")
          .COLUMNS(columns)
        .VALUES()
          .VALUE("Vegan")
          .VALUE("Exclusively plant-based food")
        .Build()

    ;

    Console.WriteLine(sql);

Output:

.. code-block:: sql

  INSERT INTO [dbo].[Categories]
  (
   [CategoryName],
   [Description]
  ) VALUES (
   'Vegan',
   'Exclusively plant-based food'
  )

**Insert Using a Collection of** ``TSQLColumn`` **Objects for Columns and Values**

Specifying a collection of ``TSQLColumn`` objects for the VALUES clause will produce parameters using the 
column names as a naming convention.

.. code-block:: csharp

    var columns = new List<TSQLColumn>
    { 
        new TSQLColumn { ColumnName = "CategoryName" },
        new TSQLColumn { ColumnName = "Description" }
    };

    var sql = TSQL

        .INSERT().INTO("dbo", "Categories")
          .COLUMNS(columns)
        .VALUES(columns)
        .Build()

    ;

    Console.WriteLine(sql);

Output:

.. code-block:: sql

  INSERT INTO [dbo].[Categories]
  (
   [CategoryName],
   [Description]
  ) VALUES (
   @CategoryName,
   @Description
  )

**Insert Using Explicit Parameter Names for the** ``VALUES`` **Clause**

Parameter names can be specified for the VALUES clause manually.

.. code-block:: csharp

    var sql = TSQL

        .INSERT().INTO("dbo", "Categories")
          .COLUMN("CategoryName")
          .COLUMN("Description")
        .VALUES()
          .VALUE("@CategoryName")
          .VALUE("@Description")
        .Build()

    ;

    Console.WriteLine(sql);

Output:

.. code-block:: sql

  INSERT INTO [dbo].[Categories]
  (
   [CategoryName],
   [Description]
  ) VALUES (
   @CategoryName,
   @Description
  )

**Insert Using a SELECT statement**

.. code-block:: csharp

    var sql = TSQL

        .INSERT().INTO("dbo", "Categories")
          .COLUMN("CategoryName")
          .COLUMN("Description")
        .SELECT()
          .VALUE("Vegan")
          .VALUE("Exclusively plant-based food")
        .Build()

    ;

    Console.WriteLine(sql);

Output:

.. code-block:: sql

  INSERT INTO [dbo].[Categories]
  (
   [CategoryName],
   [Description]
  ) 
  SELECT
   'Vegan',
   'Exclusively plant-based food'

**Insert Using a SELECT Statement with a Collection of Strings**

.. code-block:: csharp

    var columns = new List<string> { "CategoryName", "Description" };

    var sql = TSQL

        .INSERT().INTO("dbo", "Categories")
          .COLUMNS(columns)
        .SELECT()
          .VALUES(columns)
        .Build()

    ;

    Console.WriteLine(sql);

Output:

.. code-block:: sql

  INSERT INTO [dbo].[Categories]
  (
   [CategoryName],
   [Description]
  )
  SELECT
   @CategoryName,
   @Description


**Insert Using a SELECT Statement with a Collection of** ``TSQLColumn`` **Objects**

.. code-block:: csharp

    var columns = new List<TSQLColumn>
    {
        new TSQLColumn { ColumnName = "CategoryName" },
        new TSQLColumn { ColumnName = "Description" }
    };

    var sql = TSQL

        .INSERT().INTO("dbo", "Categories")
          .COLUMNS(columns)
        .SELECT()
          .VALUES(columns)
        .Build()

    ;

    Console.WriteLine(sql);

Output:

.. code-block:: sql

  INSERT INTO [dbo].[Categories]
  (
   [CategoryName],
   [Description]
  )
  SELECT
   @CategoryName,
   @Description

.. include:: northwind-db-tip.rst

====
JOIN
====

INNER JOIN
----------

.. code-block:: csharp 

   var sql = TSQL

        .SELECT()
          .COLUMN("p", "ProductID")
          .COLUMN("c", "CategoryName")
        .FROM("dbo", "Products", "p")
        .INNERJOIN("dbo", "Categories", "c").ON("c", "CategoryID").IsEqualTo("p", "CategoryID")
        .Build()

   ;

   Console.WriteLine(sql);

Output:

.. code-block:: sql

    SELECT
     [p].[ProductID],
     [c].[CategoryName]
    FROM [dbo].[Products] p
    INNER JOIN [dbo].[Categories] c ON [c].[CategoryID] = [p].[CategoryID]


**Multiple tables joined**

.. code-block:: csharp 

   var sql = TSQL

    .SELECT()
      .COLUMN("p", "ProductID")
      .COLUMN("p", "ProductName")
      .COLUMN("c", "CategoryName")
      .COLUMN("s", "CompanyName")
    .FROM("dbo", "Products", "p")
    .INNERJOIN("dbo", "Categories", "c").ON("c", "CategoryID").IsEqualTo("p", "CategoryID")
    .INNERJOIN("dbo", "Suppliers", "s").ON("s", "SupplierID").IsEqualTo("p", "SupplierID")
    .Build()

   ;

   Console.WriteLine(sql);

Output:

.. code-block:: sql

  SELECT
   [p].[ProductID],
   [p].[ProductName],
   [c].[CategoryName],
   [s].[CompanyName]
  FROM [dbo].[Products] p
  INNER JOIN [dbo].[Categories] c ON [c].[CategoryID] = [p].[CategoryID]
  INNER JOIN [dbo].[Suppliers] s ON [s].[SupplierID] = [p].[SupplierID]

**Join with multiple conditions using .AND(...)**

You can put the left column join condition in the ``.AND()`` method

.. code-block:: csharp 

   var sql = TSQL

    .SELECT()
      .COLUMN("p", "ProductID")
      .COLUMN("p", "ProductName")
      .COLUMN("s", "CompanyName")
    .FROM("dbo", "Products", "p")
    .INNERJOIN("dbo", "Suppliers", "s").ON("s", "SupplierID").IsEqualTo("p", "SupplierID")
      .AND("p", "Discontinued").IsEqualTo(false)
    .Build()

   ;

   Console.WriteLine(sql);

Output:

.. code-block:: sql

  SELECT
   [p].[ProductID],
   [p].[ProductName],
   [s].[CompanyName]
  FROM [dbo].[Products] p
  INNER JOIN [dbo].[Suppliers] s ON [s].[SupplierID] = [p].[SupplierID]
   AND [p].[Discontinued] = 0

**Join with multiple conditions using .COLUMN(...)**

You can also explicitly use the ``.COLUMN()`` method to specify the left column of the join condition 

.. code-block:: csharp 

   var sql = TSQL

    .SELECT()
      .COLUMN("p", "ProductID")
      .COLUMN("p", "ProductName")
      .COLUMN("s", "CompanyName")
    .FROM("dbo", "Products", "p")
    .INNERJOIN("dbo", "Suppliers", "s").ON("s", "SupplierID").IsEqualTo("p", "SupplierID")
      .AND().COLUMN("p", "Discontinued").IsEqualTo(false)
    .Build()

   ;

   Console.WriteLine(sql);

Output:

.. code-block:: sql

  SELECT
   [p].[ProductID],
   [p].[ProductName],
   [s].[CompanyName]
  FROM [dbo].[Products] p
  INNER JOIN [dbo].[Suppliers] s ON [s].[SupplierID] = [p].[SupplierID]
   AND [p].[Discontinued] = 0

LEFT JOIN
----------

.. code-block:: csharp 

   var sql = TSQL

        .SELECT()
          .COLUMN("p", "ProductID")
          .COLUMN("c", "CategoryName")
        .FROM("dbo", "Products", "p")
        .LEFTJOIN("dbo", "Categories", "c").ON("c", "CategoryID").IsEqualTo("p", "CategoryID")
        .Build()

   ;

   Console.WriteLine(sql);

Output:

.. code-block:: sql

    SELECT
     [p].[ProductID],
     [c].[CategoryName]
    FROM [dbo].[Products] p
    LEFT JOIN [dbo].[Categories] c ON [c].[CategoryID] = [p].[CategoryID]

**Multiple tables joined**

.. code-block:: csharp 

   var sql = TSQL

    .SELECT()
      .COLUMN("p", "ProductID")
      .COLUMN("p", "ProductName")
      .COLUMN("c", "CategoryName")
      .COLUMN("s", "CompanyName")
    .FROM("dbo", "Products", "p")
    .LEFTJOIN("dbo", "Categories", "c").ON("c", "CategoryID").IsEqualTo("p", "CategoryID")
    .LEFTJOIN("dbo", "Suppliers", "s").ON("s", "SupplierID").IsEqualTo("p", "SupplierID")
    .Build()

   ;

   Console.WriteLine(sql);

Output:

.. code-block:: sql

  SELECT
   [p].[ProductID],
   [p].[ProductName],
   [c].[CategoryName],
   [s].[CompanyName]
  FROM [dbo].[Products] p
  LEFT JOIN [dbo].[Categories] c ON [c].[CategoryID] = [p].[CategoryID]
  LEFT JOIN [dbo].[Suppliers] s ON [s].[SupplierID] = [p].[SupplierID]

**Join with multiple conditions using .AND(...)**

You can put the left column join condition in the ``.AND()`` method

.. code-block:: csharp 

   var sql = TSQL

    .SELECT()
      .COLUMN("p", "ProductID")
      .COLUMN("p", "ProductName")
      .COLUMN("s", "CompanyName")
    .FROM("dbo", "Products", "p")
    .LEFTJOIN("dbo", "Suppliers", "s").ON("s", "SupplierID").IsEqualTo("p", "SupplierID")
      .AND("p", "Discontinued").IsEqualTo(false)
    .Build()

   ;

   Console.WriteLine(sql);

Output:

.. code-block:: sql

  SELECT
   [p].[ProductID],
   [p].[ProductName],
   [s].[CompanyName]
  FROM [dbo].[Products] p
  LEFT JOIN [dbo].[Suppliers] s ON [s].[SupplierID] = [p].[SupplierID]
   AND [p].[Discontinued] = 0

**Join with multiple conditions using .COLUMN(...)**

You can also explicitly use the ``.COLUMN()`` method to specify the left column of the join condition 

.. code-block:: csharp 

   var sql = TSQL

    .SELECT()
      .COLUMN("p", "ProductID")
      .COLUMN("p", "ProductName")
      .COLUMN("s", "CompanyName")
    .FROM("dbo", "Products", "p")
    .LEFTJOIN("dbo", "Suppliers", "s").ON("s", "SupplierID").IsEqualTo("p", "SupplierID")
      .AND().COLUMN("p", "Discontinued").IsEqualTo(false)
    .Build()

   ;

   Console.WriteLine(sql);

Output:

.. code-block:: sql

  SELECT
   [p].[ProductID],
   [p].[ProductName],
   [s].[CompanyName]
  FROM [dbo].[Products] p
  LEFT JOIN [dbo].[Suppliers] s ON [s].[SupplierID] = [p].[SupplierID]
   AND [p].[Discontinued] = 0

RIGHT JOIN
----------

.. code-block:: csharp 

   var sql = TSQL

        .SELECT()
          .COLUMN("p", "ProductID")
          .COLUMN("c", "CategoryName")
        .FROM("dbo", "Products", "p")
        .RIGHTJOIN("dbo", "Categories", "c").ON("c", "CategoryID").IsEqualTo("p", "CategoryID")
        .Build()

   ;

   Console.WriteLine(sql);

Output:

.. code-block:: sql

    SELECT
     [p].[ProductID],
     [c].[CategoryName]
    FROM [dbo].[Products] p
    RIGHT JOIN [dbo].[Categories] c ON [c].[CategoryID] = [p].[CategoryID]

**Multiple tables joined**

.. code-block:: csharp 

   var sql = TSQL

    .SELECT()
      .COLUMN("p", "ProductID")
      .COLUMN("p", "ProductName")
      .COLUMN("c", "CategoryName")
      .COLUMN("s", "CompanyName")
    .FROM("dbo", "Products", "p")
    .RIGHTJOIN("dbo", "Categories", "c").ON("c", "CategoryID").IsEqualTo("p", "CategoryID")
    .RIGHTJOIN("dbo", "Suppliers", "s").ON("s", "SupplierID").IsEqualTo("p", "SupplierID")
    .Build()

   ;

   Console.WriteLine(sql);

Output:

.. code-block:: sql

  SELECT
   [p].[ProductID],
   [p].[ProductName],
   [c].[CategoryName],
   [s].[CompanyName]
  FROM [dbo].[Products] p
  RIGHT JOIN [dbo].[Categories] c ON [c].[CategoryID] = [p].[CategoryID]
  RIGHT JOIN [dbo].[Suppliers] s ON [s].[SupplierID] = [p].[SupplierID]

**Join with multiple conditions using .AND(...)**

You can put the left column join condition in the ``.AND()`` method

.. code-block:: csharp 

   var sql = TSQL

    .SELECT()
      .COLUMN("p", "ProductID")
      .COLUMN("p", "ProductName")
      .COLUMN("s", "CompanyName")
    .FROM("dbo", "Products", "p")
    .RIGHTJOIN("dbo", "Suppliers", "s").ON("s", "SupplierID").IsEqualTo("p", "SupplierID")
      .AND("p", "Discontinued").IsEqualTo(false)
    .Build()

   ;

   Console.WriteLine(sql);

Output:

.. code-block:: sql

  SELECT
   [p].[ProductID],
   [p].[ProductName],
   [s].[CompanyName]
  FROM [dbo].[Products] p
  RIGHT JOIN [dbo].[Suppliers] s ON [s].[SupplierID] = [p].[SupplierID]
   AND [p].[Discontinued] = 0

**Join with multiple conditions using .COLUMN(...)**

You can also explicitly use the ``.COLUMN()`` method to specify the left column of the join condition 

.. code-block:: csharp 

   var sql = TSQL

    .SELECT()
      .COLUMN("p", "ProductID")
      .COLUMN("p", "ProductName")
      .COLUMN("s", "CompanyName")
    .FROM("dbo", "Products", "p")
    .RIGHTJOIN("dbo", "Suppliers", "s").ON("s", "SupplierID").IsEqualTo("p", "SupplierID")
      .AND().COLUMN("p", "Discontinued").IsEqualTo(false)
    .Build()

   ;

   Console.WriteLine(sql);

Output:

.. code-block:: sql

  SELECT
   [p].[ProductID],
   [p].[ProductName],
   [s].[CompanyName]
  FROM [dbo].[Products] p
  RIGHT JOIN [dbo].[Suppliers] s ON [s].[SupplierID] = [p].[SupplierID]
   AND [p].[Discontinued] = 0

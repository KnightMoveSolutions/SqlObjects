.. include:: northwind-db-tip.rst

=====
UNION
=====

.. code-block:: csharp 

    var sql = TSQL

        .SELECT()
          .COLUMN("t", "TerritoryID")
        .FROM("dbo", "Territories", "t")
        .INNERJOIN("dbo", "Region", "r").ON("r", "RegionID").IsEqualTo("t", "RegionID")
        .WHERE("r", "RegionID").IsEqualTo(1)

        .UNION()

        .SELECT()
          .COLUMN("t", "TerritoryID")
        .FROM("dbo", "Territories", "t")
        .INNERJOIN("dbo", "Region", "r").ON("r", "RegionID").IsEqualTo("t", "RegionID")
        .WHERE("r", "RegionID").IsEqualTo(2)
        
        .Build()

    ;

    Console.WriteLine(sql);

Output:

.. code-block:: sql

  SELECT
   [t].[TerritoryID]
  FROM [dbo].[Territories] t
  INNER JOIN [dbo].[Region] r ON [r].[RegionID] = [t].[RegionID]
  WHERE 1=1
   AND [r].[RegionID] = 1

  UNION

  SELECT
   [t].[TerritoryID]
  FROM [dbo].[Territories] t
  INNER JOIN [dbo].[Region] r ON [r].[RegionID] = [t].[RegionID]
  WHERE 1=1
   AND [r].[RegionID] = 2
   
:doc:`where-1-equals-1`


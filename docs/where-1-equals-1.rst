:orphan:

`Documentation Home <https://docs.knightmovesolutions.com>`_

===========
WHERE 1=1 ?
===========

.. note::
   The information on this page applies to the ``HAVING`` clause as well where it outputs ``HAVING 1=1`` by default

If you've noticed in the examples or if you've run the code and noticed that the ``WHERE`` clause produces the 
default condition ``1=1`` and wondered, "What the heck is that?" or "Why?" then you've come to the right place
for an answer (below).

The ``WHERE`` clause will always produce ``1=1`` as the first condition for the following reasons.

1. **To guarantee valid SQL**\

   If for some reason a condition is not provided for the ``WHERE`` clause then the ``WHERE`` clause will
   produce valid SQL that essentially does nothing by default.


2. **For Easier Debugging (the main reason)**\
   
   Consider the SQL code below.

   .. code-block:: sql 

      SELECT
       [ColumnA],
       [ColumnB]
      FROM [MyTable]
      WHERE 
       [IsEnabled] = 1
       AND [Category] = 'Widgets'

   If this query did not produce the expected results and you wanted to debug by commenting out the 
   first condition, it would look like this:

   .. code-block:: sql

      SELECT
       [ColumnA],
       [ColumnB]
      FROM [MyTable]
      WHERE 
       --[IsEnabled] = 1
       AND [Category] = 'Widgets'

   However, now the SQL is broken because there is an ``AND`` logical operator that does not belong 
   because ``[Category] = 'Widgets'`` is now the first condition. So you have to do this:

   .. code-block:: sql

      SELECT
       [ColumnA],
       [ColumnB]
      FROM [MyTable]
      WHERE 
       --[IsEnabled] = 1
       --AND 
       [Category] = 'Widgets'

   You could write the code like so:

   .. code-block:: sql

      SELECT
       [ColumnA],
       [ColumnB]
      FROM [MyTable]
      WHERE 
       --[IsEnabled] = 1 AND 
       [Category] = 'Widgets'

   But if you have a third condition and you want to comment the last one then it is problematic.

   .. code-block:: sql

      SELECT
       [ColumnA],
       [ColumnB]
      FROM [MyTable]
      WHERE 
       [IsEnabled] = 1 AND 
       [Category] = 'Widgets' AND
       --[Price] > 10.00

   Again the SQL is broken and you have to do this:

   .. code-block:: sql

      SELECT
       [ColumnA],
       [ColumnB]
      FROM [MyTable]
      WHERE 
       [IsEnabled] = 1 AND 
       [Category] = 'Widgets' --AND
       --[Price] > 10.00

   Commenting/uncommenting conditions for debugging purposes is a bit of a pain when you have multiple conditions 
   in the ``WHERE`` clause. 

   Using the ``1=1`` trick solves this with virtually no performance hit.

   .. code-block:: sql

      SELECT
       [ColumnA],
       [ColumnB]
      FROM [MyTable]
      WHERE 1=1 
       AND [IsEnabled] = 1  
       AND [Category] = 'Widgets' 
       AND [Price] > 10.00

   Comment/uncomment any one of the conditions above (or comment combinations of them) at any time like so: 

   .. code-block:: sql

      SELECT
       [ColumnA],
       [ColumnB]
      FROM [MyTable]
      WHERE 1=1 
       --AND [IsEnabled] = 1  
       AND [Category] = 'Widgets' 
       AND [Price] > 10.00

       -- or 

      SELECT
       [ColumnA],
       [ColumnB]
      FROM [MyTable]
      WHERE 1=1 
       AND [IsEnabled] = 1  
       --AND [Category] = 'Widgets' 
       AND [Price] > 10.00

       -- or

      SELECT
       [ColumnA],
       [ColumnB]
      FROM [MyTable]
      WHERE 1=1 
       AND [IsEnabled] = 1  
       AND [Category] = 'Widgets' 
       --AND [Price] > 10.00

   All of the examples above result in valid SQL without the hassle of commenting/uncommenting the logical operator 
   that causes problems. 

   Lastly, you can disable the entire ``WHERE`` clause when debugging by flipping the ``1`` to a ``0`` in the condition.

   .. code-block:: sql

      SELECT
       [ColumnA],
       [ColumnB]
      FROM [MyTable]
      -- 1=0 disables the entire WHERE clause with a single character change
      WHERE 1=0  
       AND [IsEnabled] = 1  
       AND [Category] = 'Widgets' 
       AND [Price] > 10.00   

   All in all I have found the use of ``1=1`` to be very convenient when debugging so I built it into the library.

3. **Selfishly Easier Implementation**

   Aside from the above reasons, it also eliminated the need to detect multiple conditions added to the ``WHERE`` 
   clause and produce SQL that eliminated the extra hanging logical operator. When looping through conditions added 
   to the ``WHERE`` clause the implementation always outputs the specified logical operator.

If none of the reasons above make you comfortable about having that ``1=1`` condition in there then you should consider 
writing your own library.  (Just Sayin)  

=]


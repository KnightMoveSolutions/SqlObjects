`Documentation Home <https://docs.knightmovesolutions.com>`_

.. include:: northwind-db-tip.rst

=============
Serialization
=============

If you haven't caught on by now, this SQL builder creates a full object model that represents 
the SQL statements crafted via the Fluent API. As such, that object model can be serialized 
and deserialized for various purposes. 

ToJson()
--------

Serialization into a JSON string is provided by simply calling the ``.ToJson()`` method on the 
resulting object model before calling the ``.Build()`` method as shown below. 

.. code-block:: csharp

   // sqlObj contains the object model
   var sqlObj = TSQL   

      .SELECT()
        .COLUMN("ProductID")
        .COLUMN("ProductName")
      .FROM("Products")
      //.Build()  <-- Do not build or else it will return the string
   ;
   
   // Here you can do all sorts of stuff with sqlObj 

   // You can still get the SQL while retaining the object model
   var sql = sqlObj.Build();   

   // Serialize to JSON with .ToJson() (uses Newtonsoft.Json under the hood)
   var json = sqlObj.ToJson();


Once you have the ``json`` string you can:

- Persist it to a database or NoSQL data store
- Save it to a file
- Push it over the network perhaps as the result of an API call 
- Capture it in a log 
- etc.

.. warning::

   The JSON resulting from serialization of the SQL object model is **non-trivial**. 
   As such, even for the smallest SQL statement the JSON will emit a large payload. 
   Keep that in mind when deciding to use it in your application. The impact on 
   storage and network performance will need to be considered. 

FromJson()
----------

If you retrieve a JSON string representing a serialized KnightMoves.SqlObjects 
model, you can deserialize it easily by calling the static ``.FromJson()`` method
on the ``SqlStatement`` class.

.. code-block:: csharp

   using KnightMoves.SqlObjects.SqlCode;

.. code-block:: csharp

   var json = myJsonGetter(); // whatever that is 

   var sqlObj = SqlStatement.FromJson(json);

   // Now you can get whatever SQL was built into that model
   var sql = sqlObj.Build();

Custom Serialization
--------------------

If you need to customize your serialization with a JsonConverter or Formatter, you 
will have to take all of the steps below. 

.. code-block:: csharp

   using Newtonsoft.Json;

.. code-block:: csharp

   // Build your object model
   var sqlObj = TSQL   

      .SELECT()
        .COLUMN("ProductID")
        .COLUMN("ProductName")
      .FROM("Products")

   ;

   // You must work from the root 
   var script = sqlObj.Root ?? sqlObj;

   // After calling this it disables some stuff that messes up serialization
   // due to circular referencing issues. Therefore, you can't use the object 
   // model while it is marked for serialization.
   script.MarkAsSerializable();

   // Serialize the object model directly but here you have the ability to 
   // use converters, formatters, settings, etc.
   var json = JsonConvert.SerializeObject(script);

   // Restore the full functionality of the object model by unmarking it for serialization
   script.UnMarkAsSerializable();

If you don't have a need to do any sort of custom stuff then just use the ``.ToJson()`` 
method above so you don't have to deal with the hassle.


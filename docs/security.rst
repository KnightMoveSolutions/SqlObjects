.. include:: northwind-db-tip.rst

========
Security
========

Overview
--------

.. warning::

   **TL;DR;**

   Use :doc:`parameterized-queries` to be safe. This library has not been fully battle-tested against any attacks 
   including SQL Injection yet. A great deal of effort was made to build this library with security in mind. However, 
   modern ORMs have been in existence far longer and have been tested more thoroughly. Therefore, it is safest to 
   use this library to produce :doc:`parameterized-queries` that can be submitted to your chosen ORM along with 
   a value object and leave the security to the ORM. 

   The intention is to fully pen-test this library at some point to verify its mitigations. After that this 
   documentation will be updated. For now, it is highly recommended that you use :doc:`parameterized-queries`. 
   Use other features of this library at your own risk.

Security is of paramount importance especially when it comes to the data access layer. Attempting to exploit 
SQL injection vulnerabilities is one of the first vectors of attack utilized by adversaries. This library 
provides protection against SQL injection attacks in the following ways. 

 * Parameterized Queries
 * Strongly-Typed Values
 * Protecting Interpolated Strings
 * Value Sanitizing
 * Attack Detection

Each of these security features is described in their respective sections below. 

Parameterized Queries
---------------------

This library provides full support for Parameterized Queries. Anywhere that it makes sense to add an explicit 
value, you can instead provide parameters in the ``@paramName`` format. The SQL code will be rendered using 
parameter names without quotes so that it conforms to the expected parameterized SQL statement format. 

Examples of using ``@paramName`` are shown throughout this documentation. All examples of parameterized SQL 
statements assume that you will provide the parameter value object to the ``.Build()`` method (not recommended 
except for :doc:`debugging` purposes) 
or the ORM (recommended). 

Examples using value objects with parameterized SQL statements are provided on the following pages in this documentation:

  * :doc:`parameterized-queries`
  * :doc:`debugging`

You **SHOULD NOT** embed variables directly into hand-coded :doc:`scripts` using the ``.Script()`` method or 
the ``.Literal()`` method. Doing so circumvents protections provided by this library or your ORM. 

.. warning::

   NEVER do this 

   ``.Script($" ... {varFromInput} ...")``

   OR this

   ``.Literal($" ... {varFromInput} ...")``

   OR any string concatenation equivalent in a ``.Script()`` or ``.Literal()`` method call. This is a security hole waiting to 
   be exploited.

   INSTEAD USE: 
   
   * :doc:`parameterized-queries` (recommended)
   * Strongly-Typed Method Calls (recommended below) 
   * ``TSQLLiteral`` objects for interpolated strings (explained below but NOT recommended)

Strongly Typed Values
---------------------

The Fluent API provided in this library offers strongly-typed method overloads for the following data types, which will 
render in a compatible way to the expected SQL Server data types below:

* ``int``
* ``long``
* ``decimal``
* ``bool``
* ``char``
* ``DateTime``
* ``Guid``
* ``String``

For example, the ``.IsEqualTo()`` method offers the following overloads for the above data types:

.. code-block:: csharp

    IsEqualTo(int value);
    IsEqualTo(long value);
    IsEqualTo(decimal value);   
    IsEqualTo(bool value);
    IsEqualTo(char value);
    IsEqualTo(DateTime value);
    IsEqualTo(Guid value);
    IsEqualTo(string value);

You can provide hard-coded values or variables into these method overloads, or any of the many other methods with the same overloads, 
and not worry about it. 

You may be wondering 

    | What about the ``string`` overload? 
    | Surely we should not provide user input values into a string method

When using the ``string`` method overloads provided by the Fluent API it will sanitize the value for you.  

For example, if you use the ``.IsEqualTo(string value)`` overload with an input variable as shown below, you 
will see that it produces a sanitized value. 

.. code-block:: csharp 

    var inputVar = "'; sql attack here /*";

    var sql = TSQL

        .UPDATE("Categories").SET()
          .COLUMN("CategoryName").IsEqualTo("Vegan")
          .COLUMN("Description").IsEqualTo("Exclusively plant-based food")
        .WHERE("CategoryName").IsEqualTo(inputVar)
        .Build()

    ;

    Console.WriteLine(sql);

Output: 

.. code-block:: sql

  UPDATE [Categories] SET
   [CategoryName] = 'Vegan'
  WHERE 1=1
   AND [CategoryName] = '''; sql attack here /*'

You can see above that the attempted attack is not executable and will fail. 

If you're curious, it does this by wrapping the string value in a ``TSQLLiteral`` object as explained in the 
Interpolated Strings section below. However, the difference is that the ``string`` overloaded methods in the 
Fluent API do not cause the sanitization to fail by wrapping the value in quotes first as explained in the 
Interpolated Strings section below. The Fluent API methods prevent that error so you don't have to worry 
about it.

.. tip::

   Just use :doc:`parameterized-queries`. It's safer.

Interpolated Strings
--------------------

If you **really** insist on embedding a variable inside of an interpolated string or through string concatenation, 
which is strongly discouraged (you should be using :doc:`parameterized-queries`) then put the interpolated string 
inside of a manually created ``TSQLLiteral`` object as shown below. 

But first ...

**DO NOT DO THIS!** 

.. code-block:: csharp 
   :force:

   var inputVar = "'; sql attack here --";

    var sql = TSQL

        .UPDATE("Categories").SET()
          .COLUMN("CategoryName").IsEqualTo("Vegan")
          .Literal("[Description] = '" + inputVar + "'" + Environment.NewLine)
        .WHERE("CategoryID").IsEqualTo(9)
        .Build()

    ;

    Console.WriteLine(sql);

Output:

.. code-block:: sql

  UPDATE [Categories] SET
   [CategoryName] = 'Vegan',
   [Description] = ''; sql attack here /*'  WHERE 1=1
   AND [CategoryID] = 9

As you can see above **the attack is successfully executed!**

A better way ...

A safer (yet tedious) way to do it is to plant the ``inputVar`` in a ``TSQLLiteral`` object. The ``TSQLLiteral`` 
object takes care of sanitizing the string so it is safe to use in a SQL statement.

!! BUT YOU MUST **NOT** WRAP IT IN QUOTES !!

.. code-block:: csharp 
   :linenos:

    var inputVar = "'; sql attack here --";

    var literal = new TSQLLiteral
    {
        DataType = new TSQLDataType(SqlDbType.VarChar),
        Value = inputVar
    };

    var sql = TSQL

        .UPDATE("Categories").SET()
          .COLUMN("CategoryName").IsEqualTo("Vegan")
          .Literal("[Description] = " + literal + Environment.NewLine)
        .WHERE("CategoryID").IsEqualTo(9)
        .Build()

    ;

    Console.WriteLine(sql);

Output:

.. code-block:: sql

  UPDATE [Categories] SET
   [CategoryName] = 'Vegan',
   [Description] = '''; sql attack here /*'
  WHERE 1=1
   AND [CategoryID] = 9

Notice on ``line 13`` that the ``literal`` object concatenated into the string is not surrounded by 
single quotes. The ``TSQLLiteral`` object does the job of rendering the string with quotes for you. 
If you wrap it in quotes then you will undo the sanitization performed by the ``TSQLLiteral`` object 
and expose the SQL to injection.

.. warning::

   If you decide to use string interpolation or concatenation protected by a ``TSQLLiteral`` object then 
   you **MUST NOT** place the variable in single quotes. The ``TSQLLiteral`` object will render the 
   string with quotes for you. 

.. tip::

   Just don't use string interpolation or string concatenation. Use :doc:`parameterized-queries` instead. 
   It's safer. 

Sanitization
------------

You have the option to sanitize a value using this library before you involve it in any SQL code or Fluent 
API calls if you choose. If this interests you for any reason you can accomplish this with the security 
filter used by this library as explained below. 

 1. Create the ``SQLSecurityFilter`` object using the static factory method below.

    .. code-block:: csharp 
    
       var securityFilter = SQLSecurityFactory.Create();

 2. Next, you sanitize the input value 

    .. code-block:: csharp

       // result will contain the sanitized string 
       var result = securityFilter.SanitizeInput<string>(inputVar);

At this point you can do what you will with the sanitized string. 

If you provide any other primitive data type to the ``SanitizeInput<T>(string val)`` method, then it will return 
a default value suitable for data type ``T`` if the ``inputVar`` string is not a valid value for that data type. 

For example:

.. code-block:: csharp 

   var inputVar = "X";

   // result will be == 0 and be of data type int
   var result = securityFilter.SanitizeInput<int>(inputVar);

If the string value is suitable for data type ``T`` then it will return the value as data type ``T``. 

.. code-block:: csharp

   var inputVar = 99;

   // result will be == 99 and be of data type int 
   var result = securityFilter.SanitizeInput<int>(inputVar);


Attack Detection
----------------

The ``SQLSecurityFilter`` object also provides a ``CheckInput<T>(string val)`` method that returns a tuple of 
type ``(bool, IEnumerable<string>)``. Here is the signature.

.. code-block:: csharp 

   (bool, IEnumerable<string>) CheckInput<T>(string val);


The ``bool`` value will be ``true`` if the ``string val`` is valid for type ``T`` and ``false`` if not. 

The ``IEnumerable<string>`` collection will contain the SQL Injection signatures that were detected in ``string val`` 
if any were found. The ``Sanitize<T>(string val)`` method does not check for any attack signatures. It simply 
returns a value suitable for type ``T``, whereas the ``CheckInput<T>(string val)`` method runs the value through 
a number of SQL injection signature rules and returns ``false`` if the value is not good along with the messages 
showing which attack signatures it detected. 

.. code-block:: csharp 

   var securityFilter = SQLSecurityFactory.Create();

   var (okay, warnings) = securityFilter.CheckInput<string>(inputVar);

   if (!okay)
   {
      // Log warnings, send alert, publish event, or whatever ...
   }

This can be used for "trip wire" type intrusion detection functionality where you can check input strings and log 
the signatures triggered by the security filter's rules. Logs that include the IP address and other details can be sent 
to a monitoring system for alerts. 

If you use this to check values on a public-internet-facing system that anyone can access, then you are likely to 
trigger many logs of attacks. This may or may not be useful since it is practically a 100% certainty that all 
publicly facing systems will indeed be attacked. 

However, you may find it more useful to embed this type of intrusion detection within internal systems where an attack 
is more likely to be rare and therefore very alarming, making this feature incredibly useful. If you are concerned or 
even curious if there are roque employees, or that an adversary has quietly infiltrated your organization's systems 
then this type of intrusion detection could be a way to help solidify your security posture in the spirit of Defense 
in Depth principles. 

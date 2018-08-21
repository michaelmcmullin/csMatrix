![alt text](https://github.com/michaelmcmullin/csMatrix/blob/7c5510a06fadec5d3024b67048d9123edc7010fe/Assets/csMatrix_128x128.png "csMatrix Icon")

# csMatrix

A Matrix class library written in C#, targetting .NET Standard 2.0

## Installation

The easiest way to install this library is using NuGet. There are a few options depending on your
environment.

### Visual Studio 2017

With your solution open, go to the **Tools > NuGet Package Manager** menu, and select either the
**Package Manager Console** or **Manage NuGet Packages for Solution...**. The second option is a 
little more user-friendly: you are provided with a UI which you can use to search for **csMatrix**.
Once found, select it, select the project(s) you want to add it to, then press **Install**.

The **Package Manager Console** option opens a new command line in Visual Studio. Paste the
following code to install csMatrix:

**`Install-Package csMatrix -Version 1.0.0`**

### Command Line

Users working outside Visual Studio might prefer to use the .NET CLI to install. The command to
use is:

**`dotnet add package csMatrix --version 1.0.0`**

## Quick Start

csMatrix provides a number of constructors to create a new `Matrix`. A few simple examples are
shown below:

```C#
// Create a new Matrix with 10 rows and 20 columns
Matrix m1 = new Matrix(10, 20);

// Create a new square Matrix with 15 rows and 15 columns
Matrix m2 = new Matrix(15);

// Create a new Matrix with 5 rows and 10 columns, populating each element with 3.5
 Matrix m3 = new Matrix(5, 10, 3.5);
```

Changing individual element values can done using indices. Think of a `Matrix` as something
similar to a two-dimensional array of `double` values:

```C#
Matrix m = new Matrix(3);
m[0,0] = 2.5;
m[1,2] = 6.2;

Console.WriteLine(m);

// OUTPUT:
// 2.50 0.00 0.00
// 0.00 0.00 6.20
// 0.00 0.00 0.00
```

Arithmetic is fairly intuitive using standard operators. In general, you can operate on two `Matrix`
instances, or on a `Matrix` and a `double`. The first option requires each `Matrix` to have appropriate
dimensions to avoid throwing an `InvalidMatrixDimensionsException`. The second option carries out the
operation on each individual element within the `Matrix`. For example:

```C#
// Create a few test Matrix instances
Matrix m1 = new Matrix(10, 20);
Matrix m2 = new Matrix(10, 20);
Matrix m3 = new Matrix(20, 10);

// Populate with random numbers
m1.Rand();
m2.Rand();
m3.Rand();

// Matrix arithmetic
Matrix test1 = m1 + m2;
Matrix test2 = m1 * m3;
// Matrix error = m1 * m2; // InvalidMatrixDimensionsException

// Element-wise arithmetic
Matrix test3 = m1 * 2.0; // Multiply each element by 2.0
Matrix test4 = m2 + 1.5; // Add 1.5 to each element
```


## Current Status

Click on a button for more details.

| Travis | Appveyor | Coveralls |
|--------|----------|-----------|
| [![Build Status](https://travis-ci.org/michaelmcmullin/csMatrix.svg?branch=master)](https://travis-ci.org/michaelmcmullin/csMatrix) | [![Build status](https://ci.appveyor.com/api/projects/status/o5vna4byfl4047x2?svg=true)](https://ci.appveyor.com/project/michaelmcmullin/csmatrix) | [![Coverage Status](https://coveralls.io/repos/github/michaelmcmullin/csMatrix/badge.svg?branch=master)](https://coveralls.io/github/michaelmcmullin/csMatrix?branch=master) |

## License
[MIT License](https://github.com/michaelmcmullin/csMatrix/blob/48f2c07d97d079bbca2251453afd1e369857e099/LICENSE)

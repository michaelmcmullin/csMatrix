![alt text](https://github.com/michaelmcmullin/csMatrix/blob/7c5510a06fadec5d3024b67048d9123edc7010fe/Assets/csMatrix_128x128.png "csMatrix Icon")

# csMatrix

A Matrix class library written in C#, targetting .NET Standard 2.0. This means it should be compatible with the following platforms:

* .NET Core 2.0
* .NET Framework 4.6.1
* Mono 5.4
* Xamarin.iOS 10.14
* Xamarin.Mac 3.8
* Xamarin.Android 8.0
* Universal Windows Platform 10.0.16299

## Installation

The easiest way to install this library is using NuGet. There are a few options depending on your
environment.

### Visual Studio 2017

#### Windows
With your solution open, go to the **Tools > NuGet Package Manager** menu, and select either the
**Package Manager Console** or **Manage NuGet Packages for Solution...**. The second option is a 
little more user-friendly: you are provided with a UI which you can use to search for **csMatrix**.
Once found, select it, select the project(s) you want to add it to, then press **Install**.

The **Package Manager Console** option opens a new command line in Visual Studio. Paste the
following code to install csMatrix:

**`Install-Package csMatrix`**

#### Mac OSX
Right-click your project and choose **Add > Add NuGet Packages**. Search for **csMatrix**,
select it, then click the 'Add Package' button.

### Command Line

Users working outside Visual Studio might prefer to use the .NET CLI to install. The command to
use is:

**`dotnet add package csMatrix`**

## Quick Start

Before using csMatrix, you need to add the following line to the top of your code file:

```C#
using csMatrix;
```

csMatrix provides a number of constructors to create a new `Matrix`. A few simple examples are
shown below:

```C#
// Create a new Matrix with 10 rows and 20 columns
Matrix m1 = new Matrix(10, 20);

// Create a new square Matrix with 15 rows and 15 columns
Matrix m2 = new Matrix(15);

// Create a new Matrix with 5 rows and 10 columns, populating each element with 3.5
 Matrix m3 = new Matrix(5, 10, 3.5);

// Create a new Matrix and specify its contents
 Matrix m4 = new Matrix(new double[,] { { 1.0, 2.0, 3.0 }, { 4.0, 5.0, 6.0 } });
```

Changing individual element values can be done using indices. Think of a `Matrix` as something
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

A `Matrix` instance exposes a few useful properties. The most common ones are demonstrated below:

```C#
Matrix m = new Matrix(5, 10);
Console.WriteLine($"Matrix m has {m.Rows} rows and {m.Columns} columns.");
Console.WriteLine($"The total number of elements is {m.Size}.");

// OUTPUT:
// Matrix m has 5 rows and 10 columns.
// The total number of elements is 50.
```

Most `Matrix` methods have two forms: one that mutates the current instance, and another static
version that creates a new `Matrix` without affecting the original. Both forms return the resulting
`Matrix`, allowing a fluent programming approach. :

```C#
Matrix m1 = new Matrix(3, 3);
Matrix m2 = new Matrix(m1);

// Populate both source matrices, in this case making them both
// Magic Squares.
m1.Magic();
m2.Magic();

// Instance Methods
m1.Transpose(false).Inverse();
Console.WriteLine(m1);

// Static Methods (gives the same result as above, without affecting
// the original Matrix).
Matrix m3 = Matrix.Transpose(m2).Inverse();
Console.WriteLine(m3);
```

Hopefully that's enough to get you started. Detailed documentation will be added to the [wiki](https://github.com/michaelmcmullin/csMatrix/wiki) as it becomes available.

## Current Status

Click on a button for more details.

| Travis | Appveyor | Coveralls | NuGet |
|--------|----------|-----------|-------|
| [![Build Status](https://travis-ci.org/michaelmcmullin/csMatrix.svg?branch=master)](https://travis-ci.org/michaelmcmullin/csMatrix) | [![Build status](https://ci.appveyor.com/api/projects/status/o5vna4byfl4047x2?svg=true)](https://ci.appveyor.com/project/michaelmcmullin/csmatrix) | [![Coverage Status](https://coveralls.io/repos/github/michaelmcmullin/csMatrix/badge.svg?branch=master)](https://coveralls.io/github/michaelmcmullin/csMatrix?branch=master) | [![NuGet](https://img.shields.io/nuget/v/csMatrix.svg)](https://www.nuget.org/packages/csMatrix) |

## License
[MIT License](https://github.com/michaelmcmullin/csMatrix/blob/48f2c07d97d079bbca2251453afd1e369857e099/LICENSE)

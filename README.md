# Talysoft
 A utility library, created by Mitchell Talyat.
 
 Note: this library is constantly changing. Some methods may be deleted or rewritten as time progresses, until this project is at a stable state.

# Features

## A Better Console (Talysoft)

Talysoft provides a Console that has a multitude of helpful methods, in addition to all of the existing default C# Console class. Better Console contains methods for writing and reading, as well as cursor movement. In addition to that, it also contains methods for providing a nice user interface for entering other types of information, such as selecting an item within a list, selecting an option from multiple options, selecting a file path from the system, and entering any type of class that inherits IConvertible. In the terminal, all options can be selected using the arrow keys or tab, and enter to select an option.

If you would like to use this Console over System.Console, use the following code at the top of your document with the rest of the using statements:
```
using static Console = Talysoft.Console;
```

Here are a list of examples:

| Code | Console |
|---|---|
| `bool result = Console.EnterYesNo("Will you use this library?");` | ![Console.EnterYesNo() in the console](https://github.com/mtalyat/Talysoft/blob/main/Images/Q-YN.png) |
| `string result = Console.EnterChoiceInline(new string[] { "Maybe", "Yes", "No" }, "Now will you use this library?", 1);` | ![Console.EnterChoiceInline() in the console](https://github.com/mtalyat/Talysoft/blob/main/Images/Q-Inline.png) |
| `string result = Console.EnterChoice(new string[] { "Maybe", "Yes", "No" }, "Now will you use this library?", 1);` | ![Console.EnterChoice() in the console](https://github.com/mtalyat/Talysoft/blob/main/Images/Q-List.png) |
| `string result = Console.EnterPath("Enter a PNG file.", "C:\\", ".png");` | ![Console.EnterPath() in the console](https://github.com/mtalyat/Talysoft/blob/main/Images/Q-Path.png) |
| `double result = Console.EnterNumber(50, 1, 0, 100, "Enter a number between 0 and 100.");` | ![Console.EnterNumber() in the console](https://github.com/mtalyat/Talysoft/blob/main/Images/Q-Number.png) |
| `string[] result = Console.EnterMultipleChoice(new string[] { "Red", "Yellow", "Green", "Blue" }, "Select the colors you like.");` | ![Console.EnterMultipleChoice() in the console](https://github.com/mtalyat/Talysoft/blob/main/Images/Q-Multiple.png) |

After an option has been selected, the result will be printed to the terminal following the prompt. For instance, using the EnterYesNo() method example, the result after selecting an option would be:

![Console.EnterYesNo() result in the console](https://github.com/mtalyat/Talysoft/blob/main/Images/Q-YN-Result.png)

## Advanced Math Processing (Talysoft.Mathematics)

The Talysoft Math system allows for an easy way of manipulating math equations. By taking equations and splitting them up into tokens and expressions, any math equation can be represented by objects in the code, including custom functions.

Math can be broken up as follows:

| Object | Description | Example(s) |
| --- | --- | --- |
| Number | A constant number value. | 5 |
| Constant | A constant in math. | PI |
| Variable | A variable in math. | x |
| Operand | A value that can be operated on. | x, 5, PI, etc. |
| Operator | An operation that can be conducted on operands, represented by a symbol. | +, -, *, /, etc. |
| Term | A collection of operands that has a numerator and a denominator. All numerator and denominator values will be multiplied together. | 3x^2, 4x/3y, etc. |
| Expression | A collection of terms that are added together. | x^2 + x + 1 |
| Equation | A set of two expressions that are equal to one another. | 2x = 3y |
| Function | A set of operations that are more specific and not represented with a symbol. | cos(2x) |

Math can also be solved using the power of a Scope. A Scope is a collection of values that are associated with equations, such as knowing about a variable named 'x', which is equal to 5. Using this information, values can be simplified and solved.

In addition to that, Mathematics also provides a way to parse equations from strings, using the Parse class. A ParsingException will be thrown if there is an error in the parsing process, which will include error information in the exception. Alternatively, there are TryParse methods, but these will return true or false instead of throwing an exception.

Mathematics also provide some additional helper classes.

### BasicMath

The Math class provides additional methods to the System.Math class, such as:

* Clamp
* DegreesToRadians
* RadiansToDegrees

### Sort

The Sort class will provide useful different sorting methods that can be used given different circumstances. All sorting methods are generic and require the type to have the IComparable interface. This includes:

* Quicksort

### Statistics

Statistics includes useful methods for analyzing statistical values in data. This includes:

* Median
* Sum
* Mean
* Counts
* Max
* Min
* Mode

## Input Output (Talysoft.IO)

Talysoft also provides useful methods and classes for many things IO, such as:

* Compression/decompression
* Reading CSV files
* Writing/reading binary files

## Data Types (Talysoft.DataTypes)

### PackedInt

PackedInt is a helper struct that handles packing multiple values into an integer. It provides methods to directly manipulate bits and bytes within an integer, allowing an easy way to pack multiple data values into one integer.

## Debugging Tools (Talysoft.Debugging)

### Logger

Logger is a class that is meant to debug to a file, as opposed to the Console or other places. This can be used to debug things that get stuck in a softlock, such as an infinite loop, etc.

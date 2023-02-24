# Talysoft
 A utility library, created by Mitchell Talyat.
 
 Note: this library is constantly changing. Some methods may be deleted or rewritten as time progresses, until this project is at a stable state.

# Features

## A Better Console (Talysoft.BetterConsole)

Talysoft provides a Console that has a multitude of helpful methods, in addition to all of the existing default C# Console class. Better Console contains methods for writing and reading, as well as cursor movement. In addition to that, it also contains methods for providing a nice user interface for entering other types of information, such as selecting an item within a list, selecting an option from multiple options, selecting a file path from the system, and entering any type of class that inherits IConvertible. In the terminal, all options can be selected using the arrow keys or tab, and enter to select an option.

If you would like to use this Console over System.Console, use the following code at the top of your document with the rest of the using statements:
```
using static Console = Talysoft.BetterConsole.Console;
```

Here are a list of examples:

| Code | Console |
|---|---|
| `bool result = Console.EnterYesNo("Will you use this library?");` | ![Console.EnterYesNo() in the console](https://github.com/mtalyat/Talysoft/blob/main/Images/Q-YN.png) |
| `string result = Console.EnterChoiceInline(new string[] { "Maybe", "Yes", "No" }, "Now will you use this library?", 1);` | ![Console.EnterChoiceInline() in the console](https://github.com/mtalyat/Talysoft/blob/main/Images/Q-Inline.png) |
| `string result = Console.EnterChoice(new string[] { "Maybe", "Yes", "No" }, "Now will you use this library?", 1);` | ![Console.EnterChoice() in the console](https://github.com/mtalyat/Talysoft/blob/main/Images/Q-List.png) |
| `string result = Console.EnterPath("Enter a PNG file.", "C:\\", ".png");` | ![Console.EnterPath() in the console](https://github.com/mtalyat/Talysoft/blob/main/Images/Q-Path.png) |

After an option has been selected, the result will be printed to the terminal following the prompt. For instance, using the EnterYesNo() method example, the result after selecting an option would be:

![Console.EnterYesNo() result in the console](https://github.com/mtalyat/Talysoft/blob/main/Images/Q-YN-Result.png)

## Math (Talysoft.Mathematics)

There are many useful Math classes implemented into Talysoft. This includes, but is not limited to:

* int2
* float2
* decimal2
* Math
* Sort
* Statistics
* IMathematical

### Math

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

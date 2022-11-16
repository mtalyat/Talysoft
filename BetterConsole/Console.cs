using System;
using System.IO;
using System.Text;
using System.Linq;
using static Talysoft.Constants;
using System.Collections.Generic;
using Talysoft.Debugging;
using System.Windows.Forms;

namespace Talysoft.BetterConsole
{
    public static class Console
    {
        public static string Title
        {
            get => System.Console.Title;
            set => System.Console.Title = value;
        }

        public static int Indentation_Length { get; set; } = 2;

        public static ConsoleColor ForegroundColor
        {
            get => System.Console.ForegroundColor;
            set => System.Console.ForegroundColor = value;
        }

        public static ConsoleColor BackgroundColor
        {
            get => System.Console.BackgroundColor;
            set => System.Console.BackgroundColor = value;
        }

        #region Cursor

        /// <summary>
        /// Hides the cursor from the view.
        /// </summary>
        public static void HideCursor()
        {
            System.Console.CursorVisible = false;
        }

        /// <summary>
        /// Shows the cursor in the view.
        /// </summary>
        public static void ShowCursor()
        {
            System.Console.CursorVisible = true;
        }

        /// <summary>
        /// Sets the position of the cursor in the text buffer.
        /// </summary>
        /// <param name="left">The distance from the left side of the buffer.</param>
        /// <param name="top">The distance from the top of the buffer.</param>
        public static void SetCursorPosition(int left, int top)
        {
            System.Console.SetCursorPosition(left, top);
        }

        #endregion

        #region Output

        #region Printing

        private static string GetIndent(int indentation)
        {
            if (indentation <= 0)
                return string.Empty;
            else
                return new string(' ', indentation * Indentation_Length);
        }

        /// <summary>
        /// Prints an object as a string to the screen.
        /// </summary>
        /// <param name="o">The object to be printed.</param>
        /// <param name="indentation">The amount of indents to be used, printed before the object.</param>
        public static void Write(object o, int indentation = 0) => Write(o.ToString(), indentation);

        /// <summary>
        /// Prints a string to the screen.
        /// </summary>
        /// <param name="str">The string to be printed.</param>
        /// <param name="indentation">The amount of indents to be used, printed before the string.</param>
        public static void Write(string str, int indentation = 0)
        {
            System.Console.Write(GetIndent(indentation) + str);
        }

        /// <summary>
        /// Prints an array of strings to the screen.
        /// </summary>
        /// <param name="strs"></param>
        /// <param name="indentation"></param>
        public static void Write(string[] strs, int indentation = 0)
        {
            foreach(string str in strs)
            {
                Write(str, indentation);
            }
        }

        /// <summary>
        /// Prints an empty line to the screen.
        /// </summary>
        public static void WriteLine() => WriteLine("");

        /// <summary>
        /// Prints an object as a string, followed by a new line to the screen.
        /// </summary>
        /// <param name="o">The object to be printed.</param>
        public static void WriteLine(object o, int indentation = 0) => WriteLine(o.ToString(), indentation);

        /// <summary>
        /// Prints a string followed by a new line to the screen.
        /// </summary>
        /// <param name="str">The string to be printed.</param>
        public static void WriteLine(string str, int indentation = 0)
        {
            Write(str + '\n', indentation);
        }

        /// <summary>
        /// Prints an array of strings, each followed by a new line, to the screen.
        /// </summary>
        /// <param name="strs"></param>
        /// <param name="indentation"></param>
        public static void WriteLine(string[] strs, int indentation = 0)
        {
            foreach(string str in strs)
            {
                WriteLine(str, indentation);
            }
        }

        /// <summary>
        /// Prints an object as a string at the given position.
        /// </summary>
        /// <param name="o">The object to be printed.</param>
        /// <param name="left">The distance from the left side of the buffer.</param>
        /// <param name="top">The distance from the top of the buffer.</param>
        public static void WriteAt(object o, int left, int top) => WriteAt(o.ToString(), left, top);

        /// <summary>
        /// Prints a string at the given position.
        /// </summary>
        /// <param name="str">The string to be printed.</param>
        /// <param name="left">The distance from the left side of the buffer.</param>
        /// <param name="top">The distance from the top of the buffer.</param>
        public static void WriteAt(string str, int left, int top)
        {
            System.Console.SetCursorPosition(left, top);
            System.Console.Write(str);
        }

        /// <summary>
        /// Prints the given text into equal columns across the Console.
        /// </summary>
        /// <param name="text">The text to be printed.</param>
        /// <param name="columnCount">The amount of columns to be used.</param>
        public static void WriteColumns(string text, int columnCount)
        {
            WriteColumns(text, columnCount, System.Console.BufferWidth);
        }

        /// <summary>
        /// Prints the given text into equal columns across the given width. Intended for large amounts of text.
        /// </summary>
        /// <param name="text">The text to be printed.</param>
        /// <param name="columnCount">The amount of columns to be used.</param>
        /// <param name="width">The total width of all columns.</param>
        public static void WriteColumns(string text, int columnCount, int width)
        {
            int columnWidth = width / columnCount;

            string[] wrapped = text.HardTextWrap(columnWidth).Split('\n');

            //now find the total height for all of the text across the columns
            int height = (int)System.Math.Ceiling((double)wrapped.Length / columnCount);

            //now merge the lines accordingly, and print
            for (int i = 0; i < height; i++)
            {
                StringBuilder sb = new StringBuilder(columnWidth * columnCount + columnCount - 1);

                for (int j = 0; j < columnCount; j++)
                {
                    string strToAppend;

                    if (i + j * height < wrapped.Length)
                        strToAppend = wrapped[i + j * height];
                    else
                        strToAppend = "";

                    sb.Append(strToAppend);

                    if (strToAppend.Length < columnWidth)
                    {
                        sb.Append(new string(' ', columnWidth - strToAppend.Length));
                    }

                    //now add a space for gaps
                    sb.Append(' ');
                }

                WriteLine(sb.ToString());
            }
        }

        #endregion

        #region Clearing

        /// <summary>
        /// Clears the entire Console.
        /// </summary>
        public static void Clear()
        {
            System.Console.Clear();
        }

        /// <summary>
        /// Clears the line at the given top position.
        /// </summary>
        /// <param name="top"></param>
        public static void ClearLine(int top)
        {
            HideCursor();

            ClearLineNoCursor(top);

            ShowCursor();
        }

        /// <summary>
        /// Clears the line at the given top position, but does not change the visibility of the cursor.
        /// </summary>
        /// <param name="top"></param>
        private static void ClearLineNoCursor(int top)
        {
            WriteAt(new string(' ', System.Console.BufferWidth), 0, top);
            System.Console.SetCursorPosition(0, top);
        }

        /// <summary>
        /// Clears the line the cursor is currently on.
        /// </summary>
        public static void ClearCurrentLine()
        {
            ClearLine(System.Console.CursorTop);
        }

        /// <summary>
        /// Clears all of the lines from the start to the stop positions.
        /// </summary>
        /// <param name="topStart">The starting top position.</param>
        /// <param name="topStop">The ending top position.</param>
        public static void ClearLines(int topStart, int topStop)
        {
            HideCursor();

            ClearLinesNoCursor(topStart, topStop);

            ShowCursor();
        }

        /// <summary>
        /// Clears all of the lines from the start to the stop position, but does not change the visibility of the cursor.
        /// </summary>
        /// <param name="topStart"></param>
        /// <param name="topStop"></param>
        private static void ClearLinesNoCursor(int topStart, int topStop)
        {
            if (topStart > topStop)
            {
                int temp = topStart;
                topStart = topStop;
                topStop = temp;
            }

            string clearLine = new string(' ', System.Console.BufferWidth);

            //clear all lines
            for (int i = topStart; i <= topStop; i++)
            {
                WriteAt(clearLine, 0, i);
            }

            //go back to start
            System.Console.SetCursorPosition(0, topStart);
        }

        #endregion

        #region Coloring

        /// <summary>
        /// Sets the ForegroundColor and BackgroundColor back to their original colors.
        /// </summary>
        public static void ResetColors()
        {
            ForegroundColor = ConsoleColor.White;
            BackgroundColor = ConsoleColor.Black;
        }

        #endregion

        #endregion

        #region Input

        #region Reading

        /// <summary>
        /// Reads a string from the input stream.
        /// </summary>
        /// <returns></returns>
        public static string ReadString()
        {
            return System.Console.ReadLine();
        }

        /// <summary>
        /// Reads an int from the input stream.
        /// </summary>
        /// <returns></returns>
        public static int ReadInt()
        {
            return Read<int>();
        }

        /// <summary>
        /// Reads a float from the input stream.
        /// </summary>
        /// <returns></returns>
        public static float ReadFloat()
        {
            return Read<float>();
        }

        /// <summary>
        /// Reads a double from the input stream.
        /// </summary>
        /// <returns></returns>
        public static double ReadDouble()
        {
            return Read<double>();
        }

        /// <summary>
        /// Reads a ConsoleKey from the input stream.
        /// </summary>
        /// <returns></returns>
        public static ConsoleKey ReadKey()
        {
            return System.Console.ReadKey(true).Key;
        }

        /// <summary>
        /// Reads a ConsoleKeyInfo from the input stream.
        /// </summary>
        /// <returns></returns>
        public static ConsoleKeyInfo ReadInfo()
        {
            return System.Console.ReadKey(true);
        }

        /// <summary>
        /// Reads a bool from the input stream.
        /// </summary>
        /// <returns></returns>
        public static bool ReadBool()
        {
            return Read<bool>();
        }

        /// <summary>
        /// Reads a T from the input stream.
        /// </summary>
        /// <typeparam name="T">A type that inherits IConvertible.</typeparam>
        /// <returns></returns>
        public static T Read<T>() where T : IConvertible
        {
            int start = System.Console.CursorTop;
            int left = System.Console.CursorLeft;

            int attempts = 0;

            T t;

            while (true)
            {
                try
                {
                    t = (T)Convert.ChangeType(ReadString(), typeof(T));
                    break;
                }
                catch
                {
                    WriteLine($"Invalid input. Please enter a valid {typeof(T).Name}.");
                    attempts++;
                }
            }

            if (attempts > 0)
            {
                //if attempted more than once, clear all attempts
                WriteAt(new string(' ', System.Console.BufferWidth - start), left, start);
                ClearLines(start + 1, start + attempts * 2);

                //then print the result
                WriteAt(t, left, start);
                WriteLine();
            }

            return t;
        }

        /// <summary>
        /// Reads an array of the given type.
        /// </summary>
        /// <typeparam name="T">An IConvertible type. Only needed to convert from a string.</typeparam>
        /// <param name="length">The Length of the array to be entered.</param>
        /// <param name="prompt">The prompt to be given to the user.</param>
        /// <returns>The array entered by the user.</returns>
        public static T[] ReadArray<T>(int length, string prompt = "") where T : IConvertible
        {
            if (!string.IsNullOrWhiteSpace(prompt)) WriteLine(prompt);

            int offset = System.Console.CursorTop;

            T[] values = new T[length];

            //print the empty array at the top of the screen
            WriteLine(values.ArrayToString(0));

            int numberEntered = 0;

            do
            {
                T input = Read<T>();

                values[numberEntered++] = input;

                //rewrite array
                WriteAt(values.ArrayToString(numberEntered), 0, offset);

                //put cursor back after the visual of the array
                System.Console.SetCursorPosition(0, offset + 1);
            } while (numberEntered < length);

            return values;
        }

        /// <summary>
        /// Reads a string in the specified format. Use ? for one any character, and * for any length of any characters.
        /// </summary>
        /// <param name="format">The format to be used.</param>
        /// <returns>The string entered.</returns>
        public static string ReadString(string format, bool caseSensitive = true)
        {
            if (!caseSensitive)
            {
                format = format.ToLower();
            }

            int start = System.Console.CursorTop;

            int attempts = 0;

            string input;

            while (true)
            {
                input = ReadString();

                if (!caseSensitive) input = input.ToLower();

                if (MatchesFormat(input, format)) break;

                attempts++;

                WriteLine("Invalid input. Must match the format: " + format);
            }

            if (attempts > 0)
            {
                //if attempted more than once, clear all attempts
                ClearLines(start, start + attempts * 2);

                //then print the result
                WriteLine(input);
            }

            return input;
        }

        /// <summary>
        /// Checks if the string inputted matches a given format string.
        /// </summary>
        /// <param name="input">The string to be checked.</param>
        /// <param name="format">The format to be used.</param>
        /// <returns>True if input matches the format, false otherwise.</returns>
        private static bool MatchesFormat(string input, string format)
        {
            //go through the format and match what it should be on the string

            string[] formats = format.Split('*');

            int index = 0;

            //if it doesn't start with a *, make sure the first section is at the beginning
            if (!format.StartsWith("*"))
            {
                int i = FindNext(input, 0, formats[0]);

                if (i != formats[0].Length)
                    return false;
            }

            foreach (string f in formats)
            {
                if (string.IsNullOrEmpty(f)) continue;

                index = FindNext(input, index, f);

                if (index == -1)
                {
                    //not found, so match does not work
                    return false;
                }
            }

            //index must be at the end, or have an * at the end of the format
            if (index < input.Length && !format.EndsWith("*"))
            {
                return false;
            }

            //must be true here
            return true;
        }

        /// <summary>
        /// Finds the ending index of the next part of the input to match the format.
        /// </summary>
        /// <param name="input">The entire input string.</param>
        /// <param name="startIndex">The index to start finding from.</param>
        /// <param name="format">The format that is to be found in the input string.</param>
        /// <returns>The index following the found format, -1 if the format was not found in the rest of the input string.</returns>
        private static int FindNext(string input, int startIndex, string format)
        {
            //format index
            int index = 0;

            bool ignoreNext = false;

            for (int i = startIndex; i < input.Length; i++)
            {
                char c = input[i];

                char f = format[index];

                if (f == '\\' && !ignoreNext)
                {
                    ignoreNext = true;
                    index++;
                }
                else if ((!ignoreNext && f == '?') || f == c)
                {
                    index++;
                    ignoreNext = false;
                }
                else
                {
                    //if did not match, restart the checking process
                    index = 0;
                }

                //if the index is too big, it was found
                if (index >= format.Length)
                {
                    return i + 1;
                }
            }

            //not found
            return -1;
        }

        #endregion

        #region Entering

        /// <summary>
        /// Prompts the user to answer Yes or No.
        /// 
        /// If the user cancels the input, it will default to No.
        /// </summary>
        /// <param name="prompt">The prompt to be used.</param>
        /// <returns>True if Yes is selected, otherwise False.</returns>
        public static bool EnterYesNo(string prompt)
        {
            return EnterChoiceIndex(new string[] { "Yes", "No" }, prompt, 0) == 0;//if canceled or No, this will return false
        }

        /// <summary>
        /// Prompts the user to enter their choice between an array of options.
        /// 
        /// If the user cancels the input, it will return the default value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="options">The options the user can select from.</param>
        /// <param name="startIndex">The index at which the user's selection starts on.</param>
        /// <returns>The selected option.</returns>
        public static T EnterChoice<T>(IEnumerable<T> options, string prompt = "", int startIndex = 0, int displayCount = 10)
        {
            int index = EnterChoiceIndex(options, prompt, startIndex, displayCount);

            if (index >= 0)
            {
                return options.ElementAt(index);
            }
            else
            {
                return default;
            }
        }

        /// <summary>
        /// Prompts the user to enter their choice between an array of options.
        /// 
        /// If the user cancels the input, it will return -1.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="options">The options the user can select from.</param>
        /// <param name="startIndex">The index at which the user's selection starts on.</param>
        /// <returns>The index of the selected option.</returns>
        public static int EnterChoiceIndex<T>(IEnumerable<T> options, string prompt = "", int startIndex = 0, int displayCount = 10)
        {
            //cannot pick from an empty list
            if (!options.Any()) return -1;

            int selectedIndex = startIndex;
            int optionCount = options.Count();

            ConsoleKey key;

            HideCursor();

            int top = System.Console.CursorTop;
            int left = -1;

            if (!string.IsNullOrWhiteSpace(prompt))
            {
                WritePrompt(prompt);
                left = System.Console.CursorLeft;
                WriteLine();
                WriteLine();
            }

            int offset = System.Console.CursorTop;

            //first, print all of the options to the screen
            PrintOptions(options, startIndex, displayCount);

            WriteLine();
            WriteLine("Use the arrow keys to select an option.", 1);
            WriteLine("Press Enter when you are done, or Escape to cancel.", 1);

            //print the initial >
            WriteAt(IN_CHAR, 0, selectedIndex + offset);

            int scroll = Mathematics.Math.Clamp(selectedIndex - displayCount / 2, 0, optionCount - 1 - displayCount);

            do
            {
                key = ReadKey();

                //handle if up or down is pressed
                if (key == ConsoleKey.UpArrow || key == ConsoleKey.DownArrow ||
                    key == ConsoleKey.PageUp || key == ConsoleKey.PageDown)
                {
                    //clear current position
                    WriteAt(" ", 0, selectedIndex - scroll + offset);

                    // determine what to select
                    switch (key)
                    {
                        case ConsoleKey.UpArrow:
                            selectedIndex = (selectedIndex + optionCount - 1) % optionCount;
                            break;
                        case ConsoleKey.DownArrow:
                            selectedIndex = (selectedIndex + 1) % optionCount;
                            break;
                        case ConsoleKey.PageUp:
                            if (selectedIndex == 0)
                                selectedIndex = optionCount - 1;
                            else
                                selectedIndex = Math.Max(0, selectedIndex - 10);
                            break;
                        case ConsoleKey.PageDown:
                            if (selectedIndex == optionCount - 1)
                                selectedIndex = 0;
                            else
                                selectedIndex = Math.Min(optionCount - 1, selectedIndex + 10);
                            break;
                    }
                    
                    // clamp scroll
                    scroll = Mathematics.Math.Clamp(selectedIndex - displayCount / 2, 0, optionCount - 1 - (displayCount - 1));

                    // print new list based on scroll
                    SetCursorPosition(0, offset);
                    PrintOptions(options, scroll, displayCount);

                    //write new position, offset by the scroll
                    WriteAt(IN_CHAR, 0, selectedIndex - scroll + offset);
                }
                else if (key == ConsoleKey.Escape)
                {
                    ClearLinesNoCursor(top + 1, offset + displayCount + 3);
                    return -1;
                }
            } while (key != ConsoleKey.Enter);

            //clear the list, but not the whole screen
            if (left >= 0)
            {
                WriteAt(options.ElementAt(selectedIndex), left, top);
                ClearLinesNoCursor(top + 1, offset + displayCount + 3);
            }
            else
            {
                ClearLinesNoCursor(top, offset + displayCount + 3);
            }

            ShowCursor();

            return selectedIndex;
        }

        /// <summary>
        /// Prints the list of options to the screen, within the given range.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="options"></param>
        /// <param name="start"></param>
        /// <param name="count"></param>
        private static void PrintOptions<T>(IEnumerable<T> options, int start, int count)
        {
            int optionsCount = options.Count();

            for (int i = start; i < start + count && i < optionsCount; i++)
            {
                WriteLine("  " + options.ElementAt(i).ToString().PadRight(System.Console.BufferWidth - 2));
            }
        }

        /// <summary>
        /// Prompts the user using a number up-down to enter a number between the min and max value, starting at startValue.
        /// 
        /// If canceled, it will return startValue.
        /// </summary>
        /// <param name="startValue">The value that the up-down starts at.</param>
        /// <param name="increment">The value that the up-down will increment/decrement by when the arrow keys are pressed.</param>
        /// <param name="min">The minimum value the up-down can go to.</param>
        /// <param name="max">The maximum value the up-down can go to.</param>
        /// <param name="prompt">The prompt to be used.</param>
        /// <returns>The value the user selected.</returns>
        public static double EnterNumber(double startValue, double increment, double min, double max, string prompt = "")
        {
            double value = startValue;

            ConsoleKey key;

            HideCursor();

            int top = System.Console.CursorTop;
            int left = -1;

            if (!string.IsNullOrWhiteSpace(prompt))
            {
                WritePrompt(prompt);
                left = System.Console.CursorLeft;
                WriteLine();
            }

            WriteLine();

            int offset = System.Console.CursorTop;

            //first, print all of the options to the screen
            WriteLine($"/\\ [{value}] \\/");

            WriteLine();
            Write("Use the arrow keys to increment/decrement the value.\n", 1);
            Write("Press Enter when you are done, or Escape to cancel.", 1);

            do
            {
                key = ReadKey();

                //handle if up or down is pressed
                if (key == ConsoleKey.UpArrow || key == ConsoleKey.DownArrow ||
                    key == ConsoleKey.PageUp || key == ConsoleKey.PageDown)
                {
                    //clear the text
                    ClearLineNoCursor(offset);

                    //update the value
                    switch (key)
                    {
                        case ConsoleKey.UpArrow:
                            value = System.Math.Min(value + increment, max);
                            break;
                        case ConsoleKey.DownArrow:
                            value = System.Math.Max(value - increment, min);
                            break;
                        case ConsoleKey.PageUp:
                            value = System.Math.Min(value + increment * 10, max);
                            break;
                        case ConsoleKey.PageDown:
                            value = System.Math.Max(value - increment * 10, min);
                            break;
                    }

                    //rewrite the value
                    WriteLine($"/\\ [{value}] \\/");
                }
                else if (key == ConsoleKey.Escape)
                {
                    return startValue;
                }

            } while (key != ConsoleKey.Enter);

            //clear the list, but not the whole screen
            if(left >= 0)
            {
                WriteAt(value, left, top);
                ClearLinesNoCursor(top + 1, offset + 4);
            } else
            {
                ClearLinesNoCursor(top, offset + 4);
            }
            
            ShowCursor();

            return value;
        }

        /// <summary>
        /// Prompts the user using a number up-down to enter a number with no bounds, starting at startValue.
        /// 
        /// If canceled, it will return startValue.
        /// </summary>
        /// <param name="startValue">The value that the up-down starts at.</param>
        /// <param name="increment">The value that the up-down will increment/decrement by when the arrow keys are pressed.</param>
        /// <param name="prompt">The prompt to be used.</param>
        /// <returns>The value the user selected.</returns>
        public static double EnterNumber(double startValue, double increment, string prompt = "") => EnterNumber(startValue, increment, double.MinValue, double.MaxValue, prompt);

        /// <summary>
        /// Prompts the user using a number up-down to enter a number with no bounds, starting at 0.
        /// 
        /// If canceled, it will return 0.
        /// </summary>
        /// <param name="prompt">The prompt to be used.</param>
        /// <returns>The value the user selected.</returns>
        public static double EnterNumber(string prompt = "") => EnterNumber(0, 1, prompt);

        /// <summary>
        /// Prompts the user to enter a file path, using an in-console file viewer.
        /// </summary>
        /// <param name="prompt">The prompt to display to the user.</param>
        /// <param name="path">The directory that the user will select a file or directory from.</param>
        /// <param name="extension">The extension to filter the search by. Leave null to not filter at all.</param>
        /// <returns></returns>
        public static string EnterPath(string prompt = null, string path = @"C:\", string extension = null)
        {
            int top = System.Console.CursorTop;

            int left = -1;

            // print prompt
            if (!string.IsNullOrWhiteSpace(prompt))
            {
                // print prompt
                WritePrompt(prompt);
                Write(path);
                left = Math.Max(left, System.Console.CursorLeft);
                WriteLine();
            }

            // add '.' to extension if needed
            if (extension != null && !extension.StartsWith("."))
            {
                extension = $".{extension}";
            }

            // get all files and directories within the given directory

            string[] directories = Directory.GetDirectories(path);

            string[] files = Directory.GetFiles(path);

            List<string> options = new List<string>(1 + directories.Length + files.Length);
            List<string> optionPaths = new List<string>(options.Capacity);

            // display "back" to go to the previous directory
            options.Add("Back");

            // display all the directories to select from, preceded by a '/'
            foreach(string directory in directories)
            {
                options.Add($"{Path.DirectorySeparatorChar}{Path.GetFileName(directory)}");
                optionPaths.Add(directory);
            }

            // display all the files to select from that match the extension
            foreach(string file in files)
            {
                if (string.IsNullOrEmpty(extension) || (extension.Length > 0 && Path.HasExtension(extension)))
                {
                    options.Add(Path.GetFileName(file));
                    optionPaths.Add(file);
                }
            }

            // select from the files
            int selected = EnterChoiceIndex(options.ToArray());

            // clear top line which had the prompt/path/result
            ClearLine(top + 1);

            // set cursor back to top
            SetCursorPosition(0, top + 1);

            /*
             * Conditions:
             * 
             * -1                                           Cancel
             * 0                                            Go back a directory
             * 1 <= directories.Length                      Select a directory
             * directories.Length + 1 < files.Length        Select a file
             */

            if(selected <= -1)
            {
                if(left >= 0)
                {
                    // just show prompt with no output
                    ClearLine(top);
                    WritePrompt(prompt);
                    WriteLine();
                }
                else
                {
                    SetCursorPosition(0, top);
                }
                return string.Empty;
            } else if (selected == 0)
            {
                if (left >= 0)
                {
                    ClearLine(top);
                } else
                {
                    SetCursorPosition(0, top);
                }
                return EnterPath(prompt, Directory.GetParent(path.TrimEnd(Path.DirectorySeparatorChar)).FullName, extension);
            } else
            {
                // if option starts with a directory separator char, then load it
                // otherwise return the selected file
                if (options[selected].StartsWith(Path.DirectorySeparatorChar.ToString()))
                {
                    if (left >= 0)
                    {
                        ClearLine(top);
                    }
                    else
                    {
                        SetCursorPosition(0, top);
                    }
                    return EnterPath(prompt, optionPaths[selected - 1], extension);
                } else
                {
                    // normal file
                    string finalPath = optionPaths[selected - 1];

                    // print result if a prompt
                    if(left >= 0)
                    {
                        ClearLine(top);
                        WritePrompt(prompt);
                        Write(finalPath);
                        WriteLine();
                    }
                    else
                    {
                        SetCursorPosition(0, top);
                    }

                    return finalPath;
                }
            }
        }

        /// <summary>
        /// Prompts the user to enter a T on the same line.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="prompt">The prompt to give the user.</param>
        /// <returns>The value entered by the user.</returns>
        public static T Enter<T>(string prompt) where T : IConvertible
        {
            WritePrompt(prompt);

            return Read<T>();
        }

        private static void WritePrompt(string prompt)
        {
            //if no punctuation at the end, add some
            if (!string.IsNullOrEmpty(prompt) && !char.IsPunctuation(prompt[prompt.Length - 1]))
                prompt += ":";

            Write(prompt + " ");
        }

        #endregion

        /// <summary>
        /// Prevents the user from inputting anything until the Enter key has been pressed.
        /// </summary>
        public static void Wait()
        {
            HideCursor();

            int top = System.Console.CursorTop;

            Write("\nPress Enter to continue.");

            ConsoleKey key;

            do
            {
                key = ReadKey();
            } while (key != ConsoleKey.Enter);

            ClearLines(top, top + 1);
        }

        #endregion
    }
}

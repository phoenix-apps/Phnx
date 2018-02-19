using System;
using System.Diagnostics;

namespace MarkSFrancis.Console
{
    public class ConsoleIo : TextIo
    {
        public ConsoleIo() : base(System.Console.In, System.Console.Out)
        {
            
        }

        public void Clear() => System.Console.Clear();

        public override T Get<T>(Func<string, T> converter, string message = null)
        {
            if (converter == null)
            {
                throw new ArgumentNullException(nameof(converter));
            }

            message = FormatMessage(message);

            T returnValue = default(T);

            {
                bool conversionWorked;
                do
                {
                    if (message != null)
                    {
                        Clear();

                        Write(message);
                    }

                    var valueEntered = GetString();

                    try
                    {
                        returnValue = converter(valueEntered);

                        conversionWorked = true;
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine(ex);

                        conversionWorked = false;
                    }
                } while (!conversionWorked);
            }

            return returnValue;
        }

        public bool YesNo(string question)
        {
            ConsoleKeyInfo keyInfo;

            {
                do
                {
                    Write(question + " (y/n): ");

                    keyInfo = ReadKey();

                } while (keyInfo.Key != ConsoleKey.Y && keyInfo.Key != ConsoleKey.N);
            }

            WriteLine(keyInfo.KeyChar);

            return keyInfo.Key == ConsoleKey.Y;
        }

        public bool? YesNoCancel(string question)
        {
            ConsoleKeyInfo keyInfo;

            {
                do
                {
                    Write(question + " (y/n/escape): ");

                    keyInfo = ReadKey();

                } while (keyInfo.Key != ConsoleKey.Y && keyInfo.Key != ConsoleKey.N &&
                         keyInfo.Key != ConsoleKey.Escape);
            }

            if (keyInfo.Key == ConsoleKey.Escape)
            {
                WriteLine("Cancelled");
                return null;
            }

            WriteLine(keyInfo.KeyChar);

            return keyInfo.Key == ConsoleKey.Y;
        }

        /// <summary>
        /// Waits for a key press from the console
        /// </summary>
        public ConsoleKeyInfo ReadKey(bool intercept = true)
        {
            return System.Console.ReadKey(intercept);
        }
    }
}
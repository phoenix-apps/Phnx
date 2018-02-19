using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace MarkSFrancis.Console
{
    public class TextIo
    {
        public TextReader Input { get; }
        public TextWriter Output { get; }

        public TextIo(TextReader input, TextWriter output)
        {
            Input = input;
            Output = output;
        }

        public void WriteLine(string s)
        {
            Output.WriteLine(s);
        }

        public void WriteLine(object o)
        {
            Output.WriteLine(o);
        }

        public void Write(string s)
        {
            Output.Write(s);
        }

        public void Write(object o)
        {
            Output.Write(o);
        }

        public void WriteCollection(params string[] collection)
        {
            WriteCollection((IEnumerable<string>)collection);
        }

        public void WriteCollection<T>(params T[] collection)
        {
            WriteCollection((IEnumerable<T>)collection);
        }

        public void WriteCollection(IEnumerable<string> collection)
        {
            WriteCollection(collection, Environment.NewLine);
        }

        public void WriteCollection(IEnumerable<string> collection, string delimiter)
        {
            WriteLine(string.Join(delimiter, collection));
        }

        public void WriteCollection<T>(IEnumerable<T> collection)
        {
            WriteCollection(collection.Select(o => o.ToString()));
        }

        public void WriteCollection<T>(IEnumerable<T> collection, string delimiter)
        {
            WriteCollection(collection.Select(o => o.ToString()), delimiter);
        }

        public string GetString()
        {
            return Input.ReadLine();
        }

        public string GetString(string message)
        {
            message = FormatMessage(message);

            Write(message);
            return GetString();
        }

        public int GetInt(string message = null)
        {
            return Get(int.Parse, message);
        }

        public long GetLong(string message = null)
        {
            return Get(long.Parse, message);
        }

        public decimal GetDecimal(string message = null)
        {
            return Get(decimal.Parse, message);
        }

        public DateTime GetDateTime(string message = null)
        {
            return Get(DateTime.Parse, message);
        }

        public virtual T Get<T>(Func<string, T> converter, string message = null)
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

        protected string FormatMessage(string message)
        {
            if (!string.IsNullOrEmpty(message))
            {
                message = message.Trim();

                if (message.EndsWith(":"))
                {
                    message += " ";
                }
                else
                {
                    message += ": ";
                }
            }

            return message;
        }
    }
}
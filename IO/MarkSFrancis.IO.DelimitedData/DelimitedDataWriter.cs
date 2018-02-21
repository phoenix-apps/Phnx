using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MarkSFrancis.IO.DelimitedData
{
    public class DelimitedDataWriter
    {
        public string Splitter { get; }
        private StreamWriter MyStream { get; }

        public DelimitedDataWriter(Stream myStream, string splitter)
        {
            MyStream = new StreamWriter(myStream, EncodingHelper.DefaultEncoding) { AutoFlush = true };
            Splitter = splitter;
        }

        public static DelimitedDataWriter CsvWriter(Stream myStream)
        {
            return new DelimitedDataWriter(myStream, ",");
        }

        public static DelimitedDataWriter TsvWriter(Stream myStream)
        {
            return new DelimitedDataWriter(myStream, "\t");
        }

        public void WriteRow(IList<string> values)
        {
            StringBuilder textToWrite = new StringBuilder(values.Sum(x => x?.Length ?? 0) + values.Count);

            for (int valueIndex = 0; valueIndex < values.Count; valueIndex++)
            {
                string valueToPrint = values[valueIndex] ?? "";

                valueToPrint = valueToPrint.Replace("\"", "\"\"");
                valueToPrint = valueToPrint.Replace("'", "''");

                if (valueToPrint.Contains(Splitter) || valueToPrint.Contains(Environment.NewLine) || valueToPrint.Contains("\"") || valueToPrint.Contains("'"))
                {
                    textToWrite.Append("\"");
                    textToWrite.Append(valueToPrint);
                    textToWrite.Append("\"");
                }
                else
                {
                    textToWrite.Append(valueToPrint);
                }

                textToWrite.Append(Splitter);
            }

            if (textToWrite.Length > 0)
            {
                MyStream.Write(textToWrite.ToString(0, textToWrite.Length - Splitter.Length));
            }
            else
            {
                MyStream.Write("");
            }

            MyStream.Write(Environment.NewLine);
        }

        public void WriteRow(params string[] values)
        {
            WriteRow((IList<string>)values);
        }
    }
}
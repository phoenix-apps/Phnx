using System;

namespace MarkSFrancis.IO.DelimitedData.Extensions
{
    public static class ErrorFactoryExtensions
    {
        public static InvalidOperationException CantSetHeadersAsDataIsAlreadyWritten(this ErrorFactory factory)
        {
            return new InvalidOperationException("The column headings cannot be set for this stream as data has already been written to the output stream");
        }
    }
}

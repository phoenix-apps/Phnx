using System;

namespace MarkSFrancis
{
    /// <summary>
    /// An exception factory which contains a series of common errors and messages. To extend this object, use extension methods instead of inheriting
    /// </summary>
    /// <example>
    /// To use an error message from the <see cref="ErrorMessage"/>
    /// <code>
    /// void MustNotBeNegative(int argument)
    /// {
    ///     if (argument &lt; 0)
    ///     {
    ///         string errorMessage = ErrorMessages.Factory.ArgumentLessThanZero();
    ///         throw new ArgumentOutOfRangeException(nameof(argument), errorMessage);
    ///     }
    /// }
    /// </code>
    /// 
    /// To add more errors messages to the factory, and improve it within your own applications with additional standardised error messages, use extension methods for <see cref="ErrorMessage"/>.
    /// <code>
    /// public static class ErrorMessagesExtensions
    /// {
    ///     public static string MyCustomError(this ErrorMessages messages)
    ///     {
    ///         return "This is a custom error message";
    ///     }
    /// }
    /// </code>
    /// </example>
    public sealed class ErrorMessage
    {
        /// <summary>
        /// Create a new instance of <see cref="ErrorMessage"/>. An instance is also available through <see cref="Factory"/>
        /// </summary>
        public ErrorMessage()
        {
        }

        /// <summary>
        /// The default singleton instance of <see cref="ErrorMessage"/>
        /// </summary>
        public static ErrorMessage Factory { get; } = new ErrorMessage();

        /// <summary>
        /// Create an error message describing a failed cast
        /// </summary>
        /// <param name="paramName">The name of the parameter that could not be cast</param>
        /// <param name="castingFrom">The type being cast from</param>
        /// <param name="castingTo">The type being cast to</param>
        /// <returns></returns>
        public string InvalidCast(string paramName, Type castingFrom, Type castingTo)
        {
            return InvalidCast(paramName, castingFrom.FullName, castingTo.FullName);
        }

        /// <summary>
        /// Create an error message describing a failed cast
        /// </summary>
        /// <param name="paramName">The name of the parameter that could not be cast</param>
        /// <param name="castingFrom">The type being cast from's full name</param>
        /// <param name="castingTo">The type being cast to's full name</param>
        /// <returns></returns>
        public string InvalidCast(string paramName, string castingFrom, string castingTo)
        {
            return $"{paramName} cannot be cast from type {castingFrom} to type {castingTo}";
        }

        /// <summary>
        /// Create an error message for an indexer that is outside a collection of values
        /// </summary>
        /// <param name="indexerName">The name of the indexer that caused the exception</param>
        public string IndexOutOfRange(string indexerName)
        {
            return $"{indexerName} was outside the bounds of the array";
        }

        /// <summary>
        /// Create an error message for an indexer that is outside a collection of values
        /// </summary>
        /// <param name="indexerName">The name of the indexer that caused the exception</param>
        /// <param name="indexerValue">The value of the indexer</param>
        public string IndexOutOfRange(string indexerName, int indexerValue)
        {
            return $"{indexerName} at {indexerValue} was outside the bounds of the array";
        }

        /// <summary>
        /// Create an error message for an indexer that is outside a collection of values
        /// </summary>
        /// <param name="indexerName">The name of the indexer that caused the exception</param>
        /// <param name="indexerValue">The value of the indexer</param>
        /// <param name="collectionValuesCount">The number of values in the collection that was being accessed by the indexer</param>
        public string IndexOutOfRange(string indexerName, int indexerValue, int collectionValuesCount)
        {
            return $"{indexerName} at {indexerValue} was outside the bounds of the array, which had {collectionValuesCount} entries";
        }

        /// <summary>
        /// Create an error message for an indexer that is outside a collection of values
        /// </summary>
        /// <param name="indexerName">The name of the indexer that caused the exception</param>
        /// <param name="indexerValue">The value of the indexer</param>
        /// <param name="collectionName">The name of the collection of values that was being accessed by the indexer</param>
        public string IndexOutOfRange(string indexerName, int indexerValue, string collectionName)
        {
            return $"{indexerName} at {indexerValue} was outside the bounds of {collectionName}";
        }

        /// <summary>
        /// Create an error message for an indexer that is outside a collection of values
        /// </summary>
        /// <param name="indexerName">The name of the indexer that caused the exception</param>
        /// <param name="indexerValue">The value of the indexer</param>
        /// <param name="collectionName">The name of the collection of values that was being accessed by the indexer</param>
        /// <param name="collectionValuesCount">The number of values in the collection that was being accessed by the indexer</param>
        public string IndexOutOfRange(string indexerName, int indexerValue, string collectionName, int collectionValuesCount)
        {
            return $"{indexerName} at {indexerValue} was outside the bounds of {collectionName}, which had {collectionValuesCount} entries";
        }

        /// <summary>
        /// Create an error message for a section which is not completed yet, and needs to be done
        /// </summary>
        /// <param name="todoNote">The TODO note. The message is prepended with "TODO: " automatically</param>
        /// <returns></returns>
        public string NotImplemented(string todoNote)
        {
            return todoNote.ToUpperInvariant().StartsWith("TODO:") ? todoNote : "TODO: " + todoNote;
        }
    }
}

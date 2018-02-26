using System;

namespace MarkSFrancis
{
    /// <summary>
    /// An exception factory which contains a series of common errors and messages. To extend this object, use extension methods instead of inheriting
    /// </summary>
    public sealed class ErrorFactory
    {
        /// <summary>
        /// This <see cref="ErrorFactory"/> available as a static object for use across the rest of the application
        /// </summary>
        public static readonly ErrorFactory Default = new ErrorFactory();

        /// <summary>
        /// Create an <see cref="InvalidCastException"/> for a failed cast
        /// </summary>
        /// <param name="paramName">The name of the parameter that could not be cast</param>
        /// <param name="castingFrom">The type being cast from</param>
        /// <param name="castingTo">The type being cast to</param>
        /// <returns></returns>
        public InvalidCastException InvalidCast(string paramName, Type castingFrom, Type castingTo)
        {
            return InvalidCast(paramName, castingFrom.FullName, castingTo.FullName);
        }

        /// <summary>
        /// Create an <see cref="InvalidCastException"/> for a failed cast
        /// </summary>
        /// <param name="paramName">The name of the parameter that could not be cast</param>
        /// <param name="castingFrom">The type being cast from's full name</param>
        /// <param name="castingTo">The type being cast to's full name</param>
        /// <returns></returns>
        public InvalidCastException InvalidCast(string paramName, string castingFrom, string castingTo)
        {
            return new InvalidCastException(paramName + " cannot be cast from type " + castingFrom + " to type " + castingTo);
        }

        /// <summary>
        /// Create an <see cref="ArgumentNullException" /> with the name of the parameter that caused this exception
        /// </summary>
        /// <param name="paramName">The name of the parameter that caused the exception</param>
        public ArgumentNullException ArgumentNull(string paramName)
        {
            return new ArgumentNullException(paramName);
        }

        /// <summary>
        /// Create an <see cref="ArgumentNullException" /> with a specified error message and the name of the parameter that caused this exception
        /// </summary>
        /// <param name="paramName">The name of the parameter that caused the exception</param>
        /// <param name="message">A message that describes the error</param>
        public ArgumentNullException ArgumentNull(string paramName, string message)
        {
            return new ArgumentNullException(paramName, message);
        }

        /// <summary>
        /// Create an <see cref="ArgumentOutOfRangeException" /> with the name of the parameter that caused this exception
        /// </summary>
        /// <param name="paramName">The name of the parameter that caused the exception</param>
        public ArgumentOutOfRangeException ArgumentOutOfRange(string paramName)
        {
            return new ArgumentOutOfRangeException(paramName);
        }

        /// <summary>
        /// Create an <see cref="ArgumentOutOfRangeException" /> with a specified error message and the name of the parameter that caused this exception
        /// </summary>
        /// <param name="paramName">The name of the parameter that caused the exception</param>
        /// <param name="message">A message that describes the error</param>
        public ArgumentOutOfRangeException ArgumentOutOfRange(string paramName, string message)
        {
            return new ArgumentOutOfRangeException(paramName, message);
        }

        /// <summary>
        /// Create an <see cref="ArgumentOutOfRangeException" /> for an argument that cannot be less than zero, but was passed with a negative value
        /// </summary>
        /// <param name="paramName">The name of the parameter that caused the exception</param>
        public ArgumentOutOfRangeException ArgumentLessThanZero(string paramName)
        {
            return new ArgumentOutOfRangeException(paramName, paramName + " cannot be less than zero");
        }

        /// <summary>
        /// Create an <see cref="IndexOutOfRangeException" /> for an indexer that is outside a collection of values
        /// </summary>
        /// <param name="indexerName">The name of the indexer that caused the exception</param>
        public IndexOutOfRangeException IndexOutOfRange(string indexerName)
        {
            return new IndexOutOfRangeException(indexerName + " references an index outside the collection of values");
        }

        /// <summary>
        /// Create an <see cref="IndexOutOfRangeException" /> for an indexer that is outside a collection of values
        /// </summary>
        /// <param name="indexerName">The name of the indexer that caused the exception</param>
        /// <param name="indexerValue">The value of the indexer</param>
        public IndexOutOfRangeException IndexOutOfRange(string indexerName, int indexerValue)
        {
            return new IndexOutOfRangeException($"{indexerName} references an index ({indexerValue}) outside the collection of values");
        }

        /// <summary>
        /// Create an <see cref="IndexOutOfRangeException" /> for an indexer that is outside a collection of values
        /// </summary>
        /// <param name="indexerName">The name of the indexer that caused the exception</param>
        /// <param name="indexerValue">The value of the indexer</param>
        /// <param name="collectionValuesCount">The number of values in the collection that was being accessed by the indexer</param>
        public IndexOutOfRangeException IndexOutOfRange(string indexerName, int indexerValue, int collectionValuesCount)
        {
            return new IndexOutOfRangeException($"{indexerName} references an index ({indexerValue}) outside the collection of values (number of values in the collection: {collectionValuesCount})");
        }

        /// <summary>
        /// Create an <see cref="IndexOutOfRangeException" /> for an indexer that is outside a collection of values
        /// </summary>
        /// <param name="indexerName">The name of the indexer that caused the exception</param>
        /// <param name="indexerValue">The value of the indexer</param>
        /// <param name="collectionName">The name of the collection of values that was being accessed by the indexer</param>
        public IndexOutOfRangeException IndexOutOfRange(string indexerName, int indexerValue, string collectionName)
        {
            return new IndexOutOfRangeException($"{indexerName} references an index ({indexerValue}) outside the collection of values (collection name: {collectionName})");
        }

        /// <summary>
        /// Create an <see cref="IndexOutOfRangeException" /> for an indexer that is outside a collection of values
        /// </summary>
        /// <param name="indexerName">The name of the indexer that caused the exception</param>
        /// <param name="indexerValue">The value of the indexer</param>
        /// <param name="collectionName">The name of the collection of values that was being accessed by the indexer</param>
        /// <param name="collectionValuesCount">The number of values in the collection that was being accessed by the indexer</param>
        public IndexOutOfRangeException IndexOutOfRange(string indexerName, int indexerValue, string collectionName, int collectionValuesCount)
        {
            return new IndexOutOfRangeException($"{indexerName} references an index ({indexerValue}) outside the collection of values (collection name: {collectionName}, number of values in the collection: {collectionValuesCount})");
        }

        /// <summary>
        /// Create a <see cref="NotImplementedException"/> without a message
        /// </summary>
        /// <returns></returns>
        public NotImplementedException NotImplemented()
        {
            return new NotImplementedException();
        }

        /// <summary>
        /// Create a <see cref="NotImplementedException"/> with a message
        /// </summary>
        /// <param name="todoNote">The TODO note. The message is prepended with "TODO: " automatically</param>
        /// <returns></returns>
        public NotImplementedException NotImplemented(string todoNote)
        {
            string prependedTodoNote = todoNote.ToUpperInvariant().StartsWith("TODO:") ? todoNote : "TODO: " + todoNote;

            return new NotImplementedException(prependedTodoNote);
        }
    }
}

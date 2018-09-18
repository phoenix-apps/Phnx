using MarkSFrancis.ThrowHelpers;
using System;

namespace MarkSFrancis
{
    /// <summary>
    /// An exception factory which contains a series of common errors and messages. To extend this object, use extension methods instead of inheriting
    /// </summary>
    /// <example>
    /// To use an error from the <see cref="Errors"/>
    /// <code>
    /// // Throw the error automatically
    /// Errors.Factory.ArgumentNullException("index").Throw();
    /// 
    /// // Throw the error manually
    /// Exception error = Errors.Factory.ArgumentException("This is an example error").Create();
    /// throw error;
    /// </code>
    /// 
    /// To add more errors to the error factory, and improve it within your own applications with additional standardised error messages, use extension methods for <see cref="Errors"/>.
    /// It is recommended that you return an exception wrapped by <see cref="ThrowHelper"/>, but this is not required
    /// <code>
    /// public static class ErrorsExtensions
    /// {
    ///     public static ThrowHelper MyCustomError(this Errors errors)
    ///     {
    ///         return new ThrowHelper&lt;Exception&gt;("This is a custom error");
    ///     }
    /// }
    /// </code>
    /// </example>
    public sealed class Errors
    {
        /// <summary>
        /// Create a new instance of <see cref="Errors"/>. An instance is also available through <see cref="Factory"/>
        /// </summary>
        public Errors()
        {
        }

        /// <summary>
        /// The default singleton instance of <see cref="Errors"/>
        /// </summary>
        public static readonly Errors Factory = new Errors();

        /// <summary>
        /// Create an <see cref="InvalidCastException"/> for a failed cast
        /// </summary>
        /// <param name="paramName">The name of the parameter that could not be cast</param>
        /// <param name="castingFrom">The type being cast from</param>
        /// <param name="castingTo">The type being cast to</param>
        /// <returns></returns>
        public ThrowHelper InvalidCast(string paramName, Type castingFrom, Type castingTo)
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
        public ThrowHelper InvalidCast(string paramName, string castingFrom, string castingTo)
        {
            return new ThrowHelper<InvalidCastException>(paramName + " cannot be cast from type " + castingFrom + " to type " + castingTo);
        }

        /// <summary>
        /// Create an <see cref="ArgumentNullException" /> with the name of the parameter that caused this exception
        /// </summary>
        /// <param name="paramName">The name of the parameter that caused the exception</param>
        public ThrowHelper ArgumentNull(string paramName)
        {
            return new CustomThrowHelper<ArgumentNullException>(
                () => new ArgumentNullException(paramName),
                ex => new ArgumentNullException($"Value cannot be null.{Environment.NewLine}Parameter name: {paramName}", ex));
        }

        /// <summary>
        /// Create an <see cref="ArgumentNullException" /> with a specified error message and the name of the parameter that caused this exception
        /// </summary>
        /// <param name="paramName">The name of the parameter that caused the exception</param>
        /// <param name="message">A message that describes the error</param>
        public ThrowHelper ArgumentNull(string paramName, string message)
        {
            return new CustomThrowHelper<ArgumentNullException>(
                () => new ArgumentNullException(paramName, message),
                ex => new ArgumentNullException($"{message}{Environment.NewLine}Parameter name: {paramName}", ex));
        }

        /// <summary>
        /// Create an <see cref="ArgumentOutOfRangeException"/> with the name of the parameter that caused this exception
        /// </summary>
        /// <param name="paramName">The name of the parameter that caused the exception</param>
        public ThrowHelper ArgumentOutOfRange(string paramName)
        {
            return new CustomThrowHelper<ArgumentOutOfRangeException>(
                () => new ArgumentOutOfRangeException(paramName),
                ex => new ArgumentOutOfRangeException($"Specified argument was out of the range of valid values.{Environment.NewLine}Parameter name: {paramName}", ex));
        }

        /// <summary>
        /// Create an <see cref="ArgumentOutOfRangeException" /> with a specified error message and the name of the parameter that caused this exception
        /// </summary>
        /// <param name="paramName">The name of the parameter that caused the exception</param>
        /// <param name="message">The message that describes the error</param>
        public ThrowHelper ArgumentOutOfRange(string paramName, string message)
        {
            return new CustomThrowHelper<ArgumentOutOfRangeException>(
                () => new ArgumentOutOfRangeException(paramName, message),
                ex => new ArgumentOutOfRangeException($"{message}{Environment.NewLine}Parameter name: {paramName}", ex));
        }

        /// <summary>
        /// Create an <see cref="ArgumentOutOfRangeException" /> with a specified error message and the name of the parameter that caused this exception
        /// </summary>
        /// <param name="paramName">The name of the parameter that caused the exception</param>
        /// <param name="actualValue">The value of the argument that caused this exception</param>
        /// <param name="message">The message that describes the error</param>
        public ThrowHelper ArgumentOutOfRange(string paramName, object actualValue, string message)
        {
            return new CustomThrowHelper<ArgumentOutOfRangeException>(
                () => new ArgumentOutOfRangeException(paramName, actualValue, message),
                ex => new ArgumentOutOfRangeException($"{message}{Environment.NewLine}Parameter name: {paramName}{Environment.NewLine}Actual value was {actualValue}.")
            );
        }

        /// <summary>
        /// Create an <see cref="ArgumentOutOfRangeException" /> for an argument that cannot be less than zero, but was passed with a negative value
        /// </summary>
        /// <param name="paramName">The name of the parameter that caused the exception</param>
        public ThrowHelper ArgumentLessThanZero(string paramName)
        {
            return ArgumentOutOfRange(paramName, $"{paramName} cannot be less than zero");
        }

        /// <summary>
        /// Create an <see cref="ArgumentOutOfRangeException" /> for an argument that cannot be less than zero, but was passed with a negative value
        /// </summary>
        /// <param name="paramName">The name of the parameter that caused the exception</param>
        /// <param name="actualValue">The value of the argument that caused this exception</param>
        public ThrowHelper ArgumentLessThanZero(string paramName, object actualValue)
        {
            return ArgumentOutOfRange(paramName, actualValue, $"{paramName} cannot be less than zero");
        }

        /// <summary>
        /// Create an <see cref="ArgumentException(string)"/> with a specified error message
        /// </summary>
        /// <param name="message">A message that describes the error</param>
        public ThrowHelper ArgumentException(string message)
        {
            return new ThrowHelper<ArgumentException>(message);
        }

        /// <summary>
        /// Create an <see cref="ArgumentException(string)"/> with a specified error message and the name of the parameter that caused this exception
        /// </summary>
        /// <param name="message">A message that describes the error</param>
        /// <param name="paramName">The name of the parameter that caused this exception</param>
        public ThrowHelper ArgumentException(string message, string paramName)
        {
            return new CustomThrowHelper<ArgumentException>(
                () => new ArgumentException(message, paramName),
                ex => new ArgumentException($"{message}{Environment.NewLine}Parameter name: {paramName}", ex));
        }

        /// <summary>
        /// Create an <see cref="IndexOutOfRangeException" /> for an indexer that is outside a collection of values
        /// </summary>
        /// <param name="indexerName">The name of the indexer that caused the exception</param>
        public ThrowHelper IndexOutOfRange(string indexerName)
        {
            return new ThrowHelper<IndexOutOfRangeException>($"{indexerName} references an index outside the collection of values");
        }

        /// <summary>
        /// Create an <see cref="IndexOutOfRangeException" /> for an indexer that is outside a collection of values
        /// </summary>
        /// <param name="indexerName">The name of the indexer that caused the exception</param>
        /// <param name="indexerValue">The value of the indexer</param>
        public ThrowHelper IndexOutOfRange(string indexerName, int indexerValue)
        {
            return new ThrowHelper<IndexOutOfRangeException>($"{indexerName} references an index ({indexerValue}) outside the collection of values");
        }

        /// <summary>
        /// Create an <see cref="IndexOutOfRangeException" /> for an indexer that is outside a collection of values
        /// </summary>
        /// <param name="indexerName">The name of the indexer that caused the exception</param>
        /// <param name="indexerValue">The value of the indexer</param>
        /// <param name="collectionValuesCount">The number of values in the collection that was being accessed by the indexer</param>
        public ThrowHelper IndexOutOfRange(string indexerName, int indexerValue, int collectionValuesCount)
        {
            return new ThrowHelper<IndexOutOfRangeException>($"{indexerName} references an index ({indexerValue}) outside the collection of values (number of values in the collection: {collectionValuesCount})");
        }

        /// <summary>
        /// Create an <see cref="IndexOutOfRangeException" /> for an indexer that is outside a collection of values
        /// </summary>
        /// <param name="indexerName">The name of the indexer that caused the exception</param>
        /// <param name="indexerValue">The value of the indexer</param>
        /// <param name="collectionName">The name of the collection of values that was being accessed by the indexer</param>
        public ThrowHelper IndexOutOfRange(string indexerName, int indexerValue, string collectionName)
        {
            return new ThrowHelper<IndexOutOfRangeException>($"{indexerName} references an index ({indexerValue}) outside the collection of values (collection name: {collectionName})");
        }

        /// <summary>
        /// Create an <see cref="IndexOutOfRangeException" /> for an indexer that is outside a collection of values
        /// </summary>
        /// <param name="indexerName">The name of the indexer that caused the exception</param>
        /// <param name="indexerValue">The value of the indexer</param>
        /// <param name="collectionName">The name of the collection of values that was being accessed by the indexer</param>
        /// <param name="collectionValuesCount">The number of values in the collection that was being accessed by the indexer</param>
        public ThrowHelper IndexOutOfRange(string indexerName, int indexerValue, string collectionName, int collectionValuesCount)
        {
            return new ThrowHelper<IndexOutOfRangeException>($"{indexerName} references an index ({indexerValue}) outside the collection of values (collection name: {collectionName}, number of values in the collection: {collectionValuesCount})");
        }

        /// <summary>
        /// Create a <see cref="NotImplementedException"/> without a message
        /// </summary>
        /// <returns></returns>
        public ThrowHelper NotImplemented()
        {
            return new ThrowHelper<NotImplementedException>("The method or operation is not implemented.");
        }

        /// <summary>
        /// Create a <see cref="NotImplementedException"/> with a message
        /// </summary>
        /// <param name="todoNote">The TODO note. The message is prepended with "TODO: " automatically</param>
        /// <returns></returns>
        public ThrowHelper NotImplemented(string todoNote)
        {
            string prependedTodoNote = todoNote.ToUpperInvariant().StartsWith("TODO:") ? todoNote : "TODO: " + todoNote;

            return new ThrowHelper<NotImplementedException>(prependedTodoNote);
        }
    }
}

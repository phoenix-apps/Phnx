using System;

namespace Phnx
{
    /// <summary>
    /// Core extensions for <see cref="ErrorMessage"/>
    /// </summary>
    public static class ErrorMessageExtensions
    {
        /// <summary>
        /// Create an error message describing a failed cast
        /// </summary>
        /// <param name="factory">The <see cref="ErrorMessage"/> factory to extend</param>
        /// <param name="paramName">The name of the parameter that could not be cast</param>
        /// <param name="castingFrom">The type being cast from</param>
        /// <param name="castingTo">The type being cast to</param>
        /// <returns></returns>
        public static string InvalidCast(this ErrorMessage factory, string paramName, Type castingFrom, Type castingTo)
        {
            return InvalidCast(factory, paramName, castingFrom.FullName, castingTo.FullName);
        }

        /// <summary>
        /// Create an error message describing a failed cast
        /// </summary>
        /// <param name="factory">The <see cref="ErrorMessage"/> factory to extend</param>
        /// <param name="paramName">The name of the parameter that could not be cast</param>
        /// <param name="castingFrom">The type being cast from's full name</param>
        /// <param name="castingTo">The type being cast to's full name</param>
        /// <returns></returns>
        public static string InvalidCast(this ErrorMessage factory, string paramName, string castingFrom, string castingTo)
        {
            return $"{paramName} cannot be cast from type {castingFrom} to type {castingTo}";
        }

        /// <summary>
        /// Create an error message for an indexer that is outside a collection of values
        /// </summary>
        /// <param name="factory">The <see cref="ErrorMessage"/> factory to extend</param>
        /// <param name="indexerName">The name of the indexer that caused the exception</param>
        public static string IndexOutOfRange(this ErrorMessage factory, string indexerName)
        {
            return $"{indexerName} was outside the bounds of the array";
        }

        /// <summary>
        /// Create an error message for an indexer that is outside a collection of values
        /// </summary>
        /// <param name="factory">The <see cref="ErrorMessage"/> factory to extend</param>
        /// <param name="indexerName">The name of the indexer that caused the exception</param>
        /// <param name="indexerValue">The value of the indexer</param>
        public static string IndexOutOfRange(this ErrorMessage factory, string indexerName, int indexerValue)
        {
            return $"{indexerName} at {indexerValue} was outside the bounds of the array";
        }

        /// <summary>
        /// Create an error message for an indexer that is outside a collection of values
        /// </summary>
        /// <param name="factory">The <see cref="ErrorMessage"/> factory to extend</param>
        /// <param name="indexerName">The name of the indexer that caused the exception</param>
        /// <param name="indexerValue">The value of the indexer</param>
        /// <param name="collectionValuesCount">The number of values in the collection that was being accessed by the indexer</param>
        public static string IndexOutOfRange(this ErrorMessage factory, string indexerName, int indexerValue, int collectionValuesCount)
        {
            return $"{indexerName} at {indexerValue} was outside the bounds of the array, which had {collectionValuesCount} entries";
        }

        /// <summary>
        /// Create an error message for an indexer that is outside a collection of values
        /// </summary>
        /// <param name="factory">The <see cref="ErrorMessage"/> factory to extend</param>
        /// <param name="indexerName">The name of the indexer that caused the exception</param>
        /// <param name="indexerValue">The value of the indexer</param>
        /// <param name="collectionName">The name of the collection of values that was being accessed by the indexer</param>
        public static string IndexOutOfRange(this ErrorMessage factory, string indexerName, int indexerValue, string collectionName)
        {
            return $"{indexerName} at {indexerValue} was outside the bounds of {collectionName}";
        }

        /// <summary>
        /// Create an error message for an indexer that is outside a collection of values
        /// </summary>
        /// <param name="factory">The <see cref="ErrorMessage"/> factory to extend</param>
        /// <param name="indexerName">The name of the indexer that caused the exception</param>
        /// <param name="indexerValue">The value of the indexer</param>
        /// <param name="collectionName">The name of the collection of values that was being accessed by the indexer</param>
        /// <param name="collectionValuesCount">The number of values in the collection that was being accessed by the indexer</param>
        public static string IndexOutOfRange(this ErrorMessage factory, string indexerName, int indexerValue, string collectionName, int collectionValuesCount)
        {
            return $"{indexerName} at {indexerValue} was outside the bounds of {collectionName}, which had {collectionValuesCount} entries";
        }

        /// <summary>
        /// Create an error message for a section which is not completed yet, and needs to be done
        /// </summary>
        /// <param name="factory">The <see cref="ErrorMessage"/> factory to extend</param>
        /// <param name="todoNote">The TODO note. The message is prepended with "TODO: " automatically</param>
        /// <returns></returns>
        public static string NotImplemented(this ErrorMessage factory, string todoNote)
        {
            return todoNote.ToUpperInvariant().StartsWith("TODO:") ? todoNote : "TODO: " + todoNote;
        }
    }
}

using System;

namespace Stubsharp.Common.Infrastructure
{
    internal static class Guard
    {
        /// <summary>
        /// Check to ensure an object is not null
        /// </summary>
        /// <param name="value">The value to check.</param>
        /// <param name="argumentName">Name of the argument.</param>
        /// <exception cref="ArgumentNullException">If the object value is null</exception>
        internal static void IsNotNull(object value, string argumentName)
        {
            if ( value == null )
            {
                throw new ArgumentNullException(argumentName);
            }
        }

        /// <summary>
        /// Check to ensure the string value is not null or empty
        /// </summary>
        /// <param name="value">The string to check</param>
        /// <param name="argumentName">The name of the argument</param>
        /// <exception cref="ArgumentException">If the string is null or empty</exception>
        internal static void IsNotNullOrEmpty(string value, string argumentName)
        {
            IsNotNull(value, argumentName);

            if ( string.IsNullOrWhiteSpace(value) )
            {
                throw new ArgumentException("String cannot be null or an empty value", argumentName);
            }
        }
    }
}

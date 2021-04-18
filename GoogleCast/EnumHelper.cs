using System;

namespace GoogleCast
{
    /// <summary>
    /// Helper for <see cref="Enum"/>
    /// </summary>
    public static class EnumHelper
    {
        /// <summary>
        /// Converts the string representation of the name of an enumerated constant to an equivalent enumerated object
        /// </summary>
        /// <typeparam name="T">an enumeration type</typeparam>
        /// <param name="enumString">a string containing the name to convert</param>
        /// <returns>an object of type T whose value is represented by enumString</returns>
        public static T Parse<T>(string enumString) where T : struct, IConvertible
        {
            return (T)Enum.Parse(typeof(T), enumString.ToCamelCase(), false);
        }

        /// <summary>
        /// Converts the nullable string representation of the name of an enumerated constant to an equivalent enumerated object
        /// </summary>
        /// <typeparam name="T">an enumeration type</typeparam>
        /// <param name="enumString">a nullable string containing the name to convert</param>
        /// <returns>an object of type T whose value is represented by enumString</returns>
        public static T? ParseNullable<T>(string? enumString) where T : struct, IConvertible
        {
            return string.IsNullOrEmpty(enumString) ? null : Parse<T>(enumString!);
        }

        /// <summary>
        /// Retrieves the name of the constant in the specified enumeration that has the specified value
        /// </summary>
        /// <typeparam name="T">an enumeration type</typeparam>
        /// <param name="value">the value of a particular enumerated constant in terms of its underlying type</param>
        /// <returns>a string containing the name of the enumerated constant in enumType whose value is value; or null if no such constant is found</returns>
        public static string GetName<T>(this T value) where T : struct, IConvertible
        {
            return Enum.GetName(typeof(T), value).ToUnderscoreUpperInvariant();
        }

        /// <summary>
        /// Retrieves the name of the constant in the specified enumeration that has the specified value
        /// </summary>
        /// <typeparam name="T">an enumeration type</typeparam>
        /// <param name="value">the value of a particular enumerated constant in terms of its underlying type</param>
        /// <returns>a string containing the name of the enumerated constant in enumType whose value is value; or null if no such constant is found</returns>
        public static string? GetName<T>(this T? value) where T : struct, IConvertible
        {
            return value == null ? null : GetName((T)value);
        }
    }
}

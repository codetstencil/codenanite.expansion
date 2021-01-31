// ***********************************************************************
// Assembly         : ZeraSystems.CodeNanite.Expansion
// Author           : Ayodele-Desktop
// Created          : 12-07-2018
//
// Last Modified By : Ayodele-Desktop
// Last Modified On : 05-16-2020
// ***********************************************************************
// <copyright file="Extensions.cs" company="ZeraSystems Inc.">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************
using Pluralize.NET;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using ZeraSystems.CodeStencil.Contracts;

namespace ZeraSystems.CodeNanite.Expansion
{
    /// <summary>
    /// Class Extensions.
    /// </summary>
    public static partial class Extensions
    {
        /// <summary>
        /// Adds the after.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <param name="text">The text.</param>
        /// <param name="isTrue">if set to <c>true</c> [is true].</param>
        /// <returns>System.String.</returns>
        public static string AddAfter(this string str, string text, bool isTrue) => isTrue ? str + text : str + string.Empty;

        /// <summary>
        /// Adds character(s) after the string depending whether it is the last element output from a loop.
        /// For example when you want to add comma while listing items in a ForEach or For loop
        /// </summary>
        /// <param name="str">The string to be updated</param>
        /// <param name="text">The character of string to be added after the passed string</param>
        /// <param name="index">The index of the current element. Remember to ADD 1 for 0-based lists/elements</param>
        /// <param name="count">The total number of elements</param>
        /// <returns>System.String.</returns>
        public static string AddAfter(this string str, string text, int index, int count) => AddAfter(str, text, index != count);

        /// <summary>
        /// Adds the carriage.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <param name="addBefore">if set to <c>true</c> [add before].</param>
        /// <returns>System.String.</returns>
        public static string AddCarriage(this string str, bool addBefore = false)
        {
            if (!addBefore)
                return str + Environment.NewLine;
            else
                return Environment.NewLine + str;
        }

        /// <summary>
        /// Strips the carriage.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <returns>System.String.</returns>
        public static string StripCarriage(this string str) => str.Replace(Environment.NewLine, string.Empty);


        /// <summary>
        /// Adds the quotes.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <returns>System.String.</returns>
        public static string AddQuotes(this string str) => @"""" + str + @"""";

        /// <summary>
        /// Commas the specified string.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <returns>System.String.</returns>
        public static string Comma(this string str) => AddAfter(str, ", ", true);

        /// <summary>
        /// Commas the specified index.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <param name="index">The index.</param>
        /// <param name="count">The count.</param>
        /// <returns>System.String.</returns>
        public static string Comma(this string str, int index, int count) => AddAfter(str, ",", index != count);

        /// <summary>
        /// Determines whether the specified string is blank.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <returns><c>true</c> if the specified string is blank; otherwise, <c>false</c>.</returns>
        public static bool IsBlank(this string str) => string.IsNullOrEmpty(str) ? true : false;

        /// <summary>
        /// Pluralizes the specified string.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <returns>System.String.</returns>
        public static string Pluralize(this string str)
        {
            if (str.IsBlank()) return string.Empty;
            if (!str.IsBlank())
                return new Pluralizer().Pluralize(str);
            else
                return string.Empty;
        }

        /// <summary>
        /// Singularizes the specified string.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <returns>System.String.</returns>
        public static string Singularize(this string str)
        {
            if (str.IsBlank()) return string.Empty;
            return new Pluralizer().Singularize(str);
        }

        /// <summary>
        /// Deeps the clone.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj">The object.</param>
        /// <returns>T.</returns>
        public static T DeepClone<T>(this T obj)
        {
            if (!obj.GetType().IsSerializable) return default(T);

            T objResult;
            using (var ms = new MemoryStream())
            {
                var bf = new BinaryFormatter();
                bf.Serialize(ms, obj);
                ms.Position = 0;
                objResult = (T)bf.Deserialize(ms);
            }
            return objResult;
        }

        /// <summary>
        /// Selfs the reference column.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>System.String.</returns>
        public static string SelfReferenceColumn(this ISchemaItem item)
        {
            if (item.TableName != item.RelatedTable) return string.Empty;
            return item.ColumnName + "Navigation";
        }

        /// <summary>
        /// Selfs the reference nav property.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>System.String.</returns>
        public static string SelfRefNavProperty(this ISchemaItem item)
        {
            if (item.TableName != item.RelatedTable) return string.Empty;
            return "Inverse" + item.ColumnName + "Navigation";
        }
    }
}
// ***********************************************************************
// Assembly         : ZeraSystems.CodeNanite.Expansion
// Author           : Ayodele-Desktop
// Created          : 12-07-2018
//
// Last Modified By : Ayodele-Desktop
// Last Modified On : 01-03-2019
// ***********************************************************************
// <copyright file="Extensions.cs" company="ZeraSystems Inc.">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************
using Pluralize.NET;
using System;
using System.IO;
using System.Net.NetworkInformation;
using System.Runtime.Serialization.Formatters.Binary;

namespace ZeraSystems.CodeNanite.Expansion
{
    /// <summary>
    /// Class Extensions.
    /// </summary>
    public static partial class Extensions
    {

        #region Heading Tags
        private static string GetHTag(string text, int weight) => "<h" + weight + ">" + text + "</h" + weight + ">";
        #endregion
        public static string HTag(this string str, int weight) => GetHTag(str, weight);
        public static string Tag(this string str, string tag, string charBeforeCloser = "") => FormatTag(str, tag, charBeforeCloser);
        public static string TagEnd(this string str, string tag) => str + FormatTagClose(tag);
        private static string FormatTag(string text, string tag, string charBeforeCloser="") => "<" + tag + " "+ text + charBeforeCloser + ">";
        private static string FormatTagClose(string tag) => "</" + tag + ">";

        public static string AddTag(this string str, string tag, int indent=0)
        {
            return string.Empty.PadLeft(indent) + "<" + tag + ">" +
                   string.Empty.PadLeft(indent+4) + str +
                   string.Empty.PadLeft(indent) + "</" + tag + ">";
        }

        public static string AddTag(this string str, string tag, string extraText = "", int indent = 0)
        {
            var endTag = "";
            if (!extraText.IsBlank())
                endTag = string.Empty.PadLeft(indent + 4) + "".TagEnd("a");
            return string.Empty.PadLeft(indent) + "<" + tag + ">" + extraText +
                   string.Empty.PadLeft(indent) + str + endTag +
                   string.Empty.PadLeft(indent) + "</" + tag + ">";
        }

        public static string AddTagWithCr(this string str, string tag, string extraText="" ,int indent = 0)
        {
            var endTag = "";
            if (!extraText.IsBlank())
                endTag = string.Empty.PadLeft(indent + 4) + "".TagEnd("a").AddCarriage();
            return string.Empty.PadLeft(indent) + "<" + tag + ">".AddCarriage() + extraText +
                   string.Empty.PadLeft(indent + 4) + str.AddCarriage() + endTag +
                   string.Empty.PadLeft(indent) + "</" + tag + ">";
        }

    }
}
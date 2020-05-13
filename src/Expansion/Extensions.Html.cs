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

namespace ZeraSystems.CodeNanite.Expansion
{
    /// <summary>
    /// Class Extensions.
    /// </summary>
    public static partial class Extensions
    {
        #region Heading Tags

        private static string GetHTag(string text, int weight) => "<h" + weight + ">" + text + "</h" + weight + ">";

        #endregion Heading Tags

        public static string SetText(string property, string value = "true")
        {
            return " " + property.Trim() + "=" + value.AddQuotes();
        }

        public static string HTag(this string str, int weight) => GetHTag(str, weight);

        public static string Tag(this string str, string tag, string charBeforeCloser = "") => FormatTag(str, tag, charBeforeCloser);

        public static string TagEnd(this string str, string tag) => str + FormatTagClose(tag);

        private static string FormatTag(string text, string tag, string charBeforeCloser = "") => "<" + tag + " " + text + charBeforeCloser + ">";

        private static string FormatTagClose(string tag) => "</" + tag + ">";

        public static string AddTag(this string str, string tag, int indent = 0)
        {
            return string.Empty.PadLeft(indent) + "<" + tag + ">" +
                   string.Empty.PadLeft(indent + 4) + str +
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

        public static string AddTagWithCr(this string str, string tag, string extraText = "", int indent = 0)
        {
            var endTag = "";
            if (!extraText.IsBlank())
                endTag = string.Empty.PadLeft(indent + 4) + "".TagEnd("a").AddCarriage();
            return string.Empty.PadLeft(indent) + "<" + tag + ">".AddCarriage() + extraText +
                   string.Empty.PadLeft(indent + 4) + str.AddCarriage() + endTag +
                   string.Empty.PadLeft(indent) + "</" + tag + ">";
        }

        /// <summary>Creates an html tag using the passed tag.</summary>
        /// <param name="str">The string to be tagged.</param>
        /// <param name="tag">The passed tag.</param>
        /// <param name="indent">The indentation. Default is 0</param>
        /// <returns>  The tagged string.</returns>
        public static string HtmlTag(this string str, string tag, int indent = 0)
        {
            return "<" + tag + ">".PadLeft(indent).AddCarriage() +
                   str.PadLeft(indent).AddCarriage() +
                   "</" + tag + ">".PadLeft(indent);
        }

        /// <summary>Creates an html tag using the passed tag.</summary>
        /// <param name="str">The string to be tagged.</param>
        /// <param name="tag">The passed tag.</param>
        /// <param name="attribute">The attribute to added to the tag.</param>
        /// <param name="value">The value to assign to the attribute.</param>
        /// <param name="indent">The indentation.
        /// Default is 0.</param>
        /// <returns>Returned tagged string.</returns>
        public static string HtmlTag(this string str, string tag, string attribute, string value, int indent = 0)
        {
            return ("<" + tag + SetText(attribute, value) + ">").PadLeft(indent).AddCarriage() +
                   str.PadLeft(indent + 4).AddCarriage() +
                   ("</" + tag + ">").PadLeft(indent);
        }

        /// <summary>Create a list item(&lt;li&gt;) belonging to an unordered(&lt;ul&gt; or ordered list (&lt;ol&gt;)</summary>
        /// <param name="str">The string to apply this tag to</param>
        /// <param name="attribute">
        ///   <para>
        ///  The attribute of the list item. e.g. attribute can be "class"</para>
        /// </param>
        /// <param name="value">The value assigned to the attribute. e.g. value can be "dropdown dropdown-cols-2 nav-item"</param>
        /// <param name="indent">The indentation. Default is 0</param>
        /// <returns>System.String.</returns>
        public static string Li(this string str, string attribute, string value, int indent = 0) => HtmlTag(str, "li", attribute, value, indent);

        /// <summary>Creates an Unordered list tag</summary>
        /// <param name="str">The string to apply this tag to</param>
        /// <param name="indent">The number of characters to indent by.</param>
        /// <returns>  String tagged as an Unordered list</returns>
        public static string Ul(this string str, int indent = 0) => HtmlTag(str, "ul", indent);

        /// <summary>Indents the passed string</summary>
        /// <param name="str">The passed string.</param>
        /// <param name="indent">The indentation</param>
        /// <returns>System.String.</returns>
        public static string Indent(this string str, int indent) => str.PadLeft(indent);
    }
}
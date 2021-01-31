// ***********************************************************************
// Assembly         : ZeraSystems.CodeNanite.Expansion
// Author           : Ayodele-Desktop
// Created          : 12-07-2018
//
// Last Modified By : Ayodele-Desktop
// Last Modified On : 05-06-2020
// ***********************************************************************
// <copyright file="Extensions.Html.cs" company="ZeraSystems Inc.">
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

        /// <summary>
        /// Gets the h tag.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="weight">The weight.</param>
        /// <returns>System.String.</returns>
        private static string GetHTag(string text, int weight) => "<h" + weight + ">" + text + "</h" + weight + ">";

        #endregion Heading Tags

        /// <summary>
        /// Sets the text.
        /// </summary>
        /// <param name="property">The property.</param>
        /// <param name="value">The value.</param>
        /// <returns>System.String.</returns>
        public static string SetText(string property, string value = "true")
        {
            return " " + property.Trim() + "=" + value.AddQuotes();
        }

        /// <summary>
        /// hes the tag.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <param name="weight">The weight.</param>
        /// <returns>System.String.</returns>
        public static string HTag(this string str, int weight) => GetHTag(str, weight);

        /// <summary>
        /// Tags the specified tag.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <param name="tag">The tag.</param>
        /// <param name="charBeforeCloser">The character before closer.</param>
        /// <returns>System.String.</returns>
        public static string Tag(this string str, string tag, string charBeforeCloser = "") => FormatTag(str, tag, charBeforeCloser);

        /// <summary>
        /// Tags the end.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <param name="tag">The tag.</param>
        /// <returns>System.String.</returns>
        public static string TagEnd(this string str, string tag) => str + FormatTagClose(tag);

        /// <summary>
        /// Formats the tag.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="tag">The tag.</param>
        /// <param name="charBeforeCloser">The character before closer.</param>
        /// <returns>System.String.</returns>
        private static string FormatTag(string text, string tag, string charBeforeCloser = "") => "<" + tag + " " + text + charBeforeCloser + ">";

        /// <summary>
        /// Formats the tag close.
        /// </summary>
        /// <param name="tag">The tag.</param>
        /// <returns>System.String.</returns>
        private static string FormatTagClose(string tag) => "</" + tag + ">";

        /// <summary>
        /// Adds the tag.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <param name="tag">The tag.</param>
        /// <param name="indent">The indent.</param>
        /// <returns>System.String.</returns>
        public static string AddTag(this string str, string tag, int indent = 0)
        {
            return string.Empty.PadLeft(indent) + "<" + tag + ">" +
                   string.Empty.PadLeft(indent + 4) + str +
                   string.Empty.PadLeft(indent) + "</" + tag + ">";
        }

        /// <summary>
        /// Adds the tag.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <param name="tag">The tag.</param>
        /// <param name="extraText">The extra text.</param>
        /// <param name="indent">The indent.</param>
        /// <returns>System.String.</returns>
        public static string AddTag(this string str, string tag, string extraText = "", int indent = 0)
        {
            var endTag = "";
            if (!extraText.IsBlank())
                endTag = string.Empty.PadLeft(indent + 4) + "".TagEnd("a");
            return string.Empty.PadLeft(indent) + "<" + tag + ">" + extraText +
                   string.Empty.PadLeft(indent) + str + endTag +
                   string.Empty.PadLeft(indent) + "</" + tag + ">";
        }

        /// <summary>
        /// Adds the tag with cr.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <param name="tag">The tag.</param>
        /// <param name="extraText">The extra text.</param>
        /// <param name="indent">The indent.</param>
        /// <returns>System.String.</returns>
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
        /// <summary>
        /// HTMLs the tag.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <param name="tag">The tag.</param>
        /// <param name="indent">The indent.</param>
        /// <returns>System.String.</returns>
        public static string HtmlTag(this string str, string tag, int indent = 0)
        {
            return "<" + tag + ">".PadLeft(indent).AddCarriage() +
                   str.PadLeft(indent).AddCarriage() +
                   "</" + tag + ">".PadLeft(indent);
        }

        /// <summary>
        /// Creates an html tag using the passed tag.
        /// </summary>
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

        /// <summary>
        /// Create a list item(&lt;li&gt;) belonging to an unordered(&lt;ul&gt; or ordered list (&lt;ol&gt;)
        /// </summary>
        /// <param name="str">The string to apply this tag to</param>
        /// <param name="attribute">The attribute of the list item. e.g. attribute can be "class"</param>
        /// <param name="value">The value assigned to the attribute. e.g. value can be "dropdown dropdown-cols-2 nav-item"</param>
        /// <param name="indent">The indentation. Default is 0</param>
        /// <returns>System.String.</returns>
        public static string Li(this string str, string attribute, string value, int indent = 0) => HtmlTag(str, "li", attribute, value, indent);

        /// <summary>
        /// Creates an Unordered list tag
        /// </summary>
        /// <param name="str">The string to apply this tag to</param>
        /// <param name="indent">The number of characters to indent by.</param>
        /// <returns>String tagged as an Unordered list</returns>
        public static string Ul(this string str, int indent = 0) => HtmlTag(str, "ul", indent);

        /// <summary>
        /// Indents the passed string
        /// </summary>
        /// <param name="str">The passed string.</param>
        /// <param name="indent">The indentation</param>
        /// <returns>System.String.</returns>
        public static string Indent(this string str, int indent) => str.PadLeft(indent);
    }
}
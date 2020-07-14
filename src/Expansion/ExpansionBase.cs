// ***********************************************************************
// Assembly         : ZeraSystems.CodeNanite.Expansion
// Author           : Ayodele-Desktop
// Created          : 12-07-2018
//
// Last Modified By : Ayodele-Desktop
// Last Modified On : 01-03-2019
// ***********************************************************************
// <copyright file="ExpansionBase.cs" company="ZeraSystems Inc.">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using AngleSharp.Html;
using AngleSharp.Html.Parser;
using Pluralize.NET;
using ZeraSystems.CodeStencil.Contracts;

namespace ZeraSystems.CodeNanite.Expansion
{
    /// <summary>
    /// Class ExpansionBase.
    /// </summary>
    public abstract partial class ExpansionBase
    {
        /// <summary>
        /// The expander
        /// </summary>
        private List<IExpander> _expander;

        /// <summary>
        /// The schema item
        /// </summary>
        private List<ISchemaItem> _schemaItem;

        /// <summary>
        /// The expanded text
        /// </summary>
        public StringBuilder ExpandedText = new StringBuilder();

        public StringBuilder _snippet = new StringBuilder();

        #region AppendText

        /// <summary>
        /// Clear the current string - ExpandedText
        /// </summary>
        public virtual void AppendText() => ExpandedText.Clear();

        /// <summary>
        /// Add text to the ExpandText StringBuilder object.
        /// </summary>
        /// <param name="text">String to add</param>
        /// <param name="linefeed">Newline/carriage return</param>
        public virtual void AppendText(string text, string linefeed = Constants.StrLineFeed) => ExpandedText.Append(text + linefeed);

        /// <summary>
        /// Add text to the ExpandText StringBuilder object.
        /// This overload uses a passed list
        /// </summary>
        /// <param name="list">List containing attributes</param>
        /// <param name="indent">Spaces created by Indent(int) method</param>
        public virtual void AppendText(List<string> list, string indent)
        {
            AppendText(string.Empty);
            foreach (var item in list)
            {
                if (!item.IsBlank())
                    AppendText(indent + item);
            }
        }

        /// <summary>
        /// Add text to the ExpandText StringBuilder object.
        /// This overload will reformat the passed in text by:
        /// (a) Starting it on a new line;
        /// (b) Indenting every line with the passed indent
        /// </summary>
        /// <param name="text">String to add</param>
        /// <param name="indent"># of spaces to indent by method</param>
        public virtual void AppendText(string text, int indent) => AppendText(text, indent, Constants.StrLineFeed);


        /// <summary>
        /// Add text to the ExpandText StringBuilder object.
        /// This overload will reformat the passed in text by:
        /// (a) Starting it on a new line;
        /// (b) Indenting every line with the passed indent
        /// (c) Add a carriage return so that any subsequently added text will begin on a new line
        /// </summary>
        /// <param name="text">String to add</param>
        /// <param name="indent"># of spaces to indent by method</param>
        /// <param name="linefeed">Newline/carriage return</param>
        public virtual void AppendText(string text, int indent, string linefeed) => AppendText(text, Indent(indent), linefeed);

        /// <summary>
        /// Add text to the ExpandText StringBuilder object.
        /// This overload will reformat the passed in text by:
        /// (a) Starting it on a new line;
        /// (b) Indenting every line with the passed indent
        /// (c) Add a carriage return so that any subsequently added text will begin on a new line
        /// </summary>
        /// <param name="text">String to add</param>
        /// <param name="indent">Spaces created by Indent(int) method</param>
        /// <param name="linefeed">Newline/carriage return</param>
        public virtual void AppendText(string text, string indent, string linefeed)
        {
            AppendText(string.Empty);
            var rows = text.Split(new[] { linefeed }, StringSplitOptions.None);
            foreach (string row in rows)
                ExpandedText.Append(indent + row);

            if (!linefeed.IsBlank())
                AppendText(string.Empty);
        }

        #endregion AppendText

        /// <summary>
        /// Create an indent of x spaces
        /// </summary>
        /// <param name="indent">Number to indent by</param>
        /// <returns>Indented string</returns>
        public virtual string Indent(int indent) => string.Empty.PadLeft(indent);

        public string Indent(string text, int indent=4)
        {
            var lines = string.Empty;
            var numLines = text.Split('\n').Length;
            using (var reader = new StringReader(text))
            {
                var count = 0;
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    count++;
                    if (count < numLines)
                        lines += (Indent(indent)).AddCarriage();
                    else
                        lines += (Indent(indent));
                }
            }
            return lines;
        }


        /// <summary>
        /// Initializes SchemaItems, Expanders passed from CodeStencil to local fields.
        /// </summary>
        /// <param name="schemaItem">The SchemaItem.</param>
        /// <param name="expander">The Expanders</param>
        public void Initializer(List<ISchemaItem> schemaItem, List<IExpander> expander)
        {
            _schemaItem = schemaItem;
            _expander = expander;
        }

        /// <summary>
        /// Pluralizes the specified text.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="doNotPluralize">By default is false and means you do not want the text to be pluralized</param>
        /// <returns>Pluralized text.</returns>
        public string Pluralize(string text, bool doNotPluralize=false)
        {
            return doNotPluralize ? text : new Pluralizer().Pluralize(text);
        }

        /// <summary>
        /// Singularizes the specified text.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="doNotSingularize">By default is false and means you do not want the text to be singulalized</param>
        /// <returns>Singularized text</returns>
        public static string Singularize(string text, bool doNotSingularize=false)
        {
            return doNotSingularize ? text : new Pluralizer().Singularize(text);
        }

        /// <summary>
        /// Clones the passed SchemaTtem.
        /// </summary>
        /// <param name="schemaItems">The schema items.</param>
        /// <returns>Cloned object</returns>
        public List<ISchemaItem> CloneSchemaItems(IList<ISchemaItem> schemaItems) => schemaItems.ToList();

        #region Project Settings

        /// <summary>
        /// Gets the expansion string.
        /// </summary>
        /// <param name="label">The label.</param>
        /// <returns>Expansion string</returns>
        public string GetExpansionString(string label)
        {
            var text = _expander.Where(e => e.ExpansionLabel == label).Select(x => x.ExpansionString).FirstOrDefault();
            return text;
        }

        public IExpander GetExpanderObject(string label)
        {
            //if (ExpansionStringExists(label))
            return _expander.FirstOrDefault(e => e.ExpansionLabel == label);
            //return null;
        }

        /// <summary>Checks to see if an Expansion string exists.</summary>
        /// <param name="label">The expansion string we are checking</param>
        /// <returns>
        ///   <c>true</c> if the strin exists, <c>false</c> otherwise.</returns>
        public bool ExpansionStringExists(string label)
        {
            var expansionString = _expander.FirstOrDefault(e => e.ExpansionLabel == label);
            return expansionString != null;
        }

        /// <summary>
        /// Gets the default name space.
        /// </summary>
        /// <returns>Namespace</returns>
        public string GetDefaultNameSpace() => GetExpansionString("NAMESPACE");

        /// <summary>
        /// Gets the organization label.
        /// </summary>
        /// <returns>Organization Label</returns>
        public string GetOrganizationLabel() => GetExpansionString("ORGANIZATION_LABEL");

        /// <summary>
        /// Gets the name of the organization.
        /// </summary>
        /// <returns>Organization Name</returns>
        public string GetOrganizationName() => GetExpansionString("ORGANIZATION_NAME");

        /// <summary>
        /// Gets the name of the project.
        /// </summary>
        /// <returns>Project Name</returns>
        public string GetProjectName() => GetExpansionString("PROJECT_NAME");

        public string GetProjectNameLower() => GetExpansionString("PROJECT_NAME_LOWER");

        /// <summary>
        /// Gets the output folder.
        /// </summary>
        /// <returns>Output Folder</returns>
        public string GetOutputFolder() => GetExpansionString("OUTPUT_FOLDER");

        public string GetDbContext() => GetExpansionString("DB_CONTEXT");

        /// <summary>
        /// Gets the name of the company.
        /// </summary>
        /// <returns>Company Name</returns>
        public string GetCompanyName() => FileVersionInfo.GetVersionInfo(Assembly.GetEntryAssembly().Location).CompanyName;

        public bool PreserveTableName()
        {
            bool.TryParse(GetExpansionString("PRESERVE_TABLE_NAME"),out var result);
            return result;
        }


        #endregion Project Settings

        #region BuildSnippet

        /// <summary>Builds the snippet by adding passed text to the private StringBuilder object -  _snippet.</summary>
        /// <param name="text">The text.</param>
        /// <param name="indent">  Indentation to use</param>
        /// <param name="noCarriage">if set to <c>true</c> [no carriage].</param>
        public void BuildSnippet(string text, int indent = 8, bool noCarriage = false)
        {
            if (text == null)
                _snippet.Clear();
            else
                if (noCarriage)
                    _snippet.Append(Indent(indent) + text);
            else
                _snippet.Append(Indent(indent) + text.AddCarriage());
        }

        /// <summary>Builds the snippet by adding passed text to your passed StringBuilder object</summary>
        /// <param name="stringsBuilder">The strings builder.</param>
        /// <param name="text">The text.</param>
        /// <param name="indent">The indentation to use.</param>
        /// <param name="noCarriage">if set to <c>true</c> [no carriage].</param>
        public void BuildSnippet(StringBuilder stringsBuilder, string text, int indent = 8, bool noCarriage = false)
        {
            if (text == null)
                stringsBuilder.Clear();
            else
            if (noCarriage)
                stringsBuilder.Append(Indent(indent) + text);
            else
                stringsBuilder.Append(Indent(indent) + text.AddCarriage());
        }

        /// <summary>By Default, truncates all text currently saved in the StringBuilder object - _snippet.</summary>
        /// <param name="canClear">if set to <c>true</c> [default], truncates existing text, if false, will return currently saved text.</param>
        /// <returns>Null, if text is truncated, else returns saved text.</returns>
        public string BuildSnippet(bool canClear = true)
        {
            var result = _snippet.ToString();
            if (canClear)
                BuildSnippet(null);
            return result;
        }

        /// <summary>Builds the snippet with the items in the passed list..</summary>
        /// <param name="list">The list.</param>
        /// <param name="indent">The indent.</param>
        public void BuildSnippet(List<string> list, int indent)
        {
            foreach (var item in list)
                BuildSnippet(item, indent);
        }

        #endregion BuildSnippet

        /// <summary>Re aligns the passed text to use the passed indent.</summary>
        /// <param name="text">The text.</param>
        /// <param name="indent">The indent.</param>
        /// <returns>Realigned text</returns>
        public string AlignIndent(string text, int indent)
        {
            var alignedLines = string.Empty;
            var numLines = text.Split('\n').Length;
            using (var reader = new StringReader(text))
            {
                var count = 0;
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    count++;
                    if (count < numLines)
                        alignedLines += (Indent(indent) + line.Trim()).AddCarriage();
                    else
                        alignedLines += (Indent(indent) + line.Trim());
                }
            }
            return alignedLines;
        }

        /// <summary>Formats an string containing HTML.
        /// tags</summary>
        /// <param name="input">The string to format</param>
        /// <param name="indent">The indent.</param>
        /// <returns>  Formatted string</returns>
        public string FormatHtml(string input, int indent=4)
        {
            var parser = new HtmlParser();
            var document = parser.ParseDocument(input);
            using (var writer = new StringWriter())
            {
                document.ToHtml(writer, new PrettyMarkupFormatter
                {
                    Indentation = "\t",
                    NewLine = "\n"
                });
                var formattedHtml = writer.ToString();
                formattedHtml = RemoveOuterTags(formattedHtml);
                return Indent(indent) + formattedHtml.Replace("\n", "\n" + Indent(indent));
            }

            string RemoveOuterTags(string s)
            {
                var result = s;
                var tagsToRemove = new string[] { "<html>", "</html>", "<head></head>", "<body>", "</body>" };
                foreach (var tag in tagsToRemove) 
                    result = result.Replace(tag, string.Empty);

                result = Regex.Replace(result, @"^\s*$[\r\n]*", string.Empty, RegexOptions.Multiline);
                return result;
            }

        }

        public string GetNullSign(ISchemaItem item)
        {
            return
                item.AllowDbNull && (item.ColumnType == "DateTime" || item.ColumnType == "int") &&
                !item.IsPrimaryKey //disallow nullablity for primary keys
                    ? "?" : string.Empty;
        }

        public string StartBrace(int indent = 4) => Indent(indent) + "{";

        public string EndBrace(int indent = 4) => Indent(indent) + "}";

        public virtual string NavigationLabel() => "Navigation";
    }

    /// <exclude />
    public class SchemaItemComparer : IEqualityComparer<ISchemaItem>
    {
        /// <summary>
        /// Equals the specified p1.
        /// </summary>
        /// <param name="p1">The p1.</param>
        /// <param name="p2">The p2.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool Equals(ISchemaItem p1, ISchemaItem p2) => p1 == p2;

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <param name="obj">The <see cref="T:System.Object" /> for which a hash code is to be returned.</param>
        /// <returns>A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.</returns>
        public int GetHashCode(ISchemaItem obj) => obj.SchemaItemId;
    }
}
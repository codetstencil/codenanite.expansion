// ***********************************************************************
// Assembly         : ZeraSystems.CodeNanite.Expansion
// Author           : Ayodele-Desktop
// Created          : 12-07-2018
//
// Last Modified By : Ayodele-Desktop
// Last Modified On : 07-13-2020
// ***********************************************************************
// <copyright file="ExpansionBase.Html.cs" company="ZeraSystems Inc.">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using ZeraSystems.CodeStencil.Contracts;

namespace ZeraSystems.CodeNanite.Expansion
{
    /// <summary>
    /// Class ExpansionBase.
    /// </summary>
    public abstract partial class ExpansionBase
    {

        /// <summary>
        /// Sets the lookup.
        /// </summary>
        /// <param name="column">The column.</param>
        /// <param name="relatedTable">The related table.</param>
        /// <param name="indent">The indent.</param>
        void SetLookup(string column, string relatedTable, int indent)
        {
            AppendText(Indent(indent + 4) + "<select asp-for=" + AddQuotes(column) + " class=" + AddQuotes("form-control") + " asp-items=" + AddQuotes("ViewBag." + column) + ">");
            AppendText(Indent(indent + 8) + "<option value=" + AddQuotes(string.Empty) + ">-- Select " + relatedTable + " --</option>");
            AppendText(Indent(indent + 4) + "</select>");
        }

        /// <summary>
        /// Controls the label.
        /// </summary>
        /// <param name="column">The column.</param>
        /// <param name="indent">The indent.</param>
        internal void ControlLabel(string column, int indent)
        {
            AppendText(Indent(indent + 4) + "<label asp-for=" + AddQuotes(column) + " class=" + AddQuotes("control-label") + "></label>");
        }

        /// <summary>
        /// Adds the quotes.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns>System.String.</returns>
        public static string AddQuotes(string text) => @"""" + text + @"""";

        /// <summary>
        /// Returns part of the tag in the format - "label aso-for="Title"
        /// </summary>
        /// <param name="tag">Tag - e.g "label", "input"</param>
        /// <param name="column">Column to use</param>
        /// <returns>System.String.</returns>
        public string AspFor(string tag, string column) => (tag + " asp-for=" + AddQuotes(column));

        /// <summary>
        /// Displays for model.
        /// </summary>
        /// <param name="column">The column.</param>
        /// <param name="indent">The indent.</param>
        /// <param name="startTag">The start tag.</param>
        /// <param name="endTag">The end tag.</param>
        /// <returns>System.String.</returns>
        public string DisplayForModel(string column, int indent, string startTag = Constants.AspDivStartTag, string endTag = Constants.AspDivEndTag)
        {
            return (Indent(indent) + startTag + "@Html.DisplayFor(model => model." + column + ")" + endTag);
        }

        /// <summary>
        /// Displays for model item.
        /// </summary>
        /// <param name="column">The column.</param>
        /// <returns>System.String.</returns>
        public string DisplayForModelItem(ISchemaItem column)
        {
            var foreignKeyTable = column.RelatedTable;
            if (column.IsForeignKey)
            {
                var label = GetTableLabel(foreignKeyTable);
                if (string.IsNullOrEmpty(label))
                    label = column.ColumnName;

                if (column.RelatedTable == column.TableName) //A Table related to itself
                {
                    return DisplayForModelItem(GetSelfRelatedColumnName(column), 0, null, null);
                }
                else
                {
                    //return DisplayForModelItem(foreignKeyTable + "." + label, 0, null, null);
                    return DisplayForModelItem(GetLookupDisplayColumnWithPath(column.TableName, column.ColumnName), 0, null, null);
                }
            }
            else
                return DisplayForModelItem(column.ColumnName, 0, null, null);
            //
        }

        /// <summary>
        /// Gets the self related object.
        /// </summary>
        /// <param name="column">The column.</param>
        /// <returns>System.String.</returns>
        public string GetSelfRelatedObject(ISchemaItem column)
        {
            var result = column.RelatedTable;
            if (column.TableName == column.RelatedTable)
                result = column.ColumnName + NavigationLabel();
            return result;
        }

        /// <summary>
        /// Gets the name of the self related column.
        /// </summary>
        /// <param name="column">The column.</param>
        /// <returns>System.String.</returns>
        public string GetSelfRelatedColumnName(ISchemaItem column)
        {
            return GetSelfRelatedObject(column) + "." + column.ColumnName;
        }


        /// <summary>
        /// Displays for model item.
        /// </summary>
        /// <param name="column">The column.</param>
        /// <param name="indent">The indent.</param>
        /// <param name="startTag">The start tag.</param>
        /// <param name="endTag">The end tag.</param>
        /// <returns>System.String.</returns>
        public string DisplayForModelItem(string column, int indent, string startTag = Constants.AspDivStartTag, string endTag = Constants.AspDivEndTag)
        {
            return (Indent(indent) + startTag + "@Html.DisplayFor(modelItem => item." + column + ")" + endTag);
        }



        /// <summary>
        /// Displays the name for.
        /// </summary>
        /// <param name="column">The column.</param>
        /// <param name="indent">The indent.</param>
        /// <param name="startTag">The start tag.</param>
        /// <param name="endTag">The end tag.</param>
        /// <returns>System.String.</returns>
        public string DisplayNameFor(string column, int indent, string startTag = Constants.AspDivStartTag, string endTag = Constants.AspDivEndTag)
        {
            return (Indent(indent) + startTag + "@Html.DisplayNameFor(model => model." + column + ")" + endTag);
        }

        /// <summary>
        /// Displays the name for.
        /// </summary>
        /// <param name="table">The table.</param>
        /// <param name="column">The column.</param>
        /// <param name="indent">The indent.</param>
        /// <param name="startTag">The start tag.</param>
        /// <param name="endTag">The end tag.</param>
        /// <returns>System.String.</returns>
        public string DisplayNameFor(string table, string column, int indent, string startTag = Constants.AspDivStartTag, string endTag = Constants.AspDivEndTag)
        {
            //if (column.RelatedTable == column.TableName) //A Table related to itself
            //    label = GetRelatedObject(column);


            return (Indent(indent) + startTag + "@Html.DisplayNameFor(model => model."+table+"[0]." + column+ ")" + endTag);
        }

        /// <summary>
        /// Dls the tag details.
        /// </summary>
        /// <param name="table">The table.</param>
        /// <param name="schemaColumns">The schema columns.</param>
        public void DlTagDetails(string table, List<ISchemaItem> schemaColumns =null)
        {
            var columns = schemaColumns ?? GetColumns(table);
            AppendText(Indent(4) + "<dl class=" + AddQuotes("dl-horizontal") + ">");
            {
                foreach (var column in columns)
                    DlTagDetail(column);
            }
            AppendText(Indent(4) + "</dl>");
        }

        /// <summary>
        /// Dls the tag details.
        /// </summary>
        /// <param name="table">The table.</param>
        /// <param name="columns">The columns.</param>
        public void DlTagDetails(string table, List<string> columns)
        {
            //var columns = schemaColumns ?? GetColumns(table);
            AppendText(Indent(4) + "<dl class=" + AddQuotes("dl-horizontal") + ">");
            {
                foreach (var column in columns)
                {
                    var schemaItem = SchemaItem();
                    if (schemaItem != null)
                    {
                        DlTagDetail(schemaItem
                            .FirstOrDefault(x => (x.ColumnName == column.StripCarriage() && x.TableName == table)));
                    }
                }
            }
            AppendText(Indent(4) + "</dl>");
        }

        /// <summary>
        /// Dls the tag detail.
        /// </summary>
        /// <param name="column">The column.</param>
        private void DlTagDetail(ISchemaItem column)
        {
            if (column == null)
                return;
            var foreignKeyTable = column.RelatedTable;
            AppendText(Indent(8) + "<dt>");
            if (column.IsForeignKey)
                AppendText(Indent(12) + DisplayNameFor(foreignKeyTable, 0, null, null));
            else
                AppendText(Indent(12) + DisplayNameFor(column.ColumnName, 0, null, null));
            AppendText(Indent(8) + "</dt>");

            AppendText(Indent(8) + "<dd>");
            if (column.IsForeignKey)
            {
                var label = GetTableLabel(foreignKeyTable);
                if (string.IsNullOrEmpty(label))
                    label = column.ColumnName;
                AppendText(Indent(12) + DisplayForModel(foreignKeyTable + "." + label, 0, null, null));
            }
            else
                AppendText(Indent(12) + DisplayForModel(column.ColumnName, 0, null, null));
            AppendText(Indent(8) + "</dd>");
        }



        /// <summary>
        /// Forms the control.
        /// </summary>
        /// <param name="column">The column.</param>
        /// <param name="indent">The indent.</param>
        public void FormControl(string column, int indent)
        {
            AppendText(Indent(indent + 4) + "<input asp-for=" + AddQuotes(column) + " class=" + AddQuotes("form-control") + " />");
        }


        /// <summary>
        /// Forms the group.
        /// </summary>
        /// <param name="column">The column.</param>
        /// <param name="table">The table.</param>
        /// <param name="indent">The indent.</param>
        public void FormGroup(string column, string table, int indent = 12)
        {
            var item = SchemaItem()
                .FirstOrDefault(x => (x.ColumnName == column && x.TableName == table));
            if (item!= null)
                FormGroup(item, indent);
        }

        /// <summary>
        /// Forms the group.
        /// </summary>
        /// <param name="column">The column.</param>
        /// <param name="indent">The indent.</param>
        public void FormGroup(ISchemaItem column, int indent = 12)
        {
            AppendText(Indent(indent) + "<div class=" + AddQuotes("form-group") + ">");

            if (!column.IsForeignKey)
            {
                ControlLabel(column.ColumnName, indent);
                FormControl(column.ColumnName, indent);
                TextDanger(column.ColumnName, indent);
            }
            else
            {
                ControlLabel(column.RelatedTable, indent);
                SetLookup(column.ColumnName, column.RelatedTable, indent);
                TextDanger(column.ColumnName, indent, " />");
            }
            AppendText(Indent(indent) + Constants.AspDivEndTag);
        }


        //public void FormGroupDisplay(string column, string table, int indent = 12)
        //{
        //    var item = SchemaItem()
        //        .FirstOrDefault(x => (x.ColumnName == column && x.TableName == table));
        //    FormGroupDisplay(item, indent);
        //}


        /// <summary>
        /// Forms the group display.
        /// </summary>
        /// <param name="column">The column.</param>
        /// <param name="indent">The indent.</param>
        public void FormGroupDisplay(ISchemaItem column, int indent = 12)
        {
            AppendText(Indent(indent) + "<div class=" + AddQuotes("form-group") + ">");

            if (column.IsPrimaryKey)
            {
                ControlLabel(column.ColumnName, indent);
                //DisplayFor(column.ColumnName, indent);
                AppendText(Indent(4) + DisplayForModel(column.ColumnName, indent));
            }
            else if (!column.IsForeignKey)
            {
                ControlLabel(column.ColumnName, indent);
                FormControl(column.ColumnName, indent);
                TextDanger(column.ColumnName, indent);
            }
            else
            {
                ControlLabel(column.RelatedTable, indent);
                SetLookup(column.ColumnName, column.RelatedTable, indent);
                TextDanger(column.ColumnName, indent, " />");
            }
            AppendText(Indent(indent) + Constants.AspDivEndTag);
        }

        /// <summary>
        /// Inputs the type.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns>System.String.</returns>
        public string InputType(string text = "hidden") => ("input type=" + AddQuotes(text));
        /// <summary>
        /// Saves the specified indent.
        /// </summary>
        /// <param name="indent">The indent.</param>
        public void Save(int indent = 12)
        {
            AppendText(Indent(indent) + "<div class=" + AddQuotes("form-group") + ">");
            AppendText(Indent(indent + 4) + "<input type=" + AddQuotes("submit") + " value=" + AddQuotes("Save") +
                       " class=" + AddQuotes("btn btn-default") + " />");
            AppendText(Indent(indent) + Constants.AspDivEndTag);
        }

        /// <summary>
        /// Submits the specified indent.
        /// </summary>
        /// <param name="indent">The indent.</param>
        public void Submit(int indent = 12)
        {
            AppendText(Indent(indent) + "<div class=" + AddQuotes("form-group") + ">");
            AppendText(Indent(indent + 4) + "<input type=" + AddQuotes("submit") + " value=" + AddQuotes("Create") +
                       " class=" + AddQuotes("btn btn-default") + " />");
            AppendText(Indent(indent) + Constants.AspDivEndTag);
        }
        /// <summary>
        /// Texts the danger.
        /// </summary>
        /// <param name="column">The column.</param>
        /// <param name="indent">The indent.</param>
        /// <param name="span">The span.</param>
        public void TextDanger(string column, int indent, string span = "></span>")
        {
            AppendText(Indent(indent + 4) + "<span asp-validation-for=" + AddQuotes(column) + " class=" + AddQuotes("text-danger") + span);
        }

        /// <summary>
        /// Gets a CSHTML header.
        /// </summary>
        /// <param name="headerType">Type of the header.</param>
        /// <param name="table">The table.</param>
        /// <param name="nameSpace">The name space.</param>
        /// <returns>System.String.</returns>
        public string CshtmlHeader(string headerType, string table, string nameSpace)
        {
            BuildSnippet(null);
            BuildSnippet("@page",0);
            BuildSnippet("@model "+nameSpace+".Pages."+Pluralize(table,PreserveTableName())+"."+headerType+"Model",0);
            BuildSnippet("");
            BuildSnippet("@{",0);
            BuildSnippet("ViewData["+"Title".AddQuotes()+"] = "+ headerType.AddQuotes()+";",4);
            BuildSnippet("}",0);

            //BuildSnippet("<h1>"+headerType+"</h1>",0);
            BuildSnippet(headerType.HTag(1), 0);
            //BuildSnippet("");
            return BuildSnippet();
        }

        /// <summary>
        /// Gets the HTML string.
        /// </summary>
        /// <param name="leftString">The left string.</param>
        /// <param name="rightString">The right string.</param>
        /// <param name="tag">The tag.</param>
        /// <returns>System.String.</returns>
        public string GetHtmlString(string leftString, string rightString, string tag)
        {
            return (leftString + "=" + rightString.AddQuotes()).Tag(tag);
        }

        /// <summary>
        /// Gets the HTML string.
        /// </summary>
        /// <param name="leftString">The left string.</param>
        /// <param name="rightString">The right string.</param>
        /// <returns>System.String.</returns>
        public string GetHtmlString(string leftString, string rightString)
        {
            return (leftString + "=" + rightString.AddQuotes());
        }

        /// <summary>
        /// Gets the HTML string.
        /// </summary>
        /// <param name="leftString">The left string.</param>
        /// <param name="rightString">The right string.</param>
        /// <param name="leftString2">The left string2.</param>
        /// <param name="rightString2">The right string2.</param>
        /// <returns>System.String.</returns>
        public string GetHtmlString(string leftString, string rightString, string leftString2, string rightString2)
        {
            return ( GetHtmlString(leftString, rightString) + " " + GetHtmlString(leftString2, rightString2));
        }

        /// <summary>
        /// Gets the HTML string.
        /// </summary>
        /// <param name="leftString">The left string.</param>
        /// <param name="rightString">The right string.</param>
        /// <param name="leftString2">The left string2.</param>
        /// <param name="rightString2">The right string2.</param>
        /// <param name="leftString3">The left string3.</param>
        /// <param name="rightString3">The right string3.</param>
        /// <returns>System.String.</returns>
        public string GetHtmlString(string leftString, string rightString, string leftString2, string rightString2, string leftString3, string rightString3)
        {
            return (GetHtmlString(leftString, rightString,leftString2,rightString2) + " " + GetHtmlString(leftString3, rightString3));
        }

        /// <summary>
        /// Gets the HTML string.
        /// </summary>
        /// <param name="leftString">The left string.</param>
        /// <param name="rightString">The right string.</param>
        /// <param name="leftString2">The left string2.</param>
        /// <param name="rightString2">The right string2.</param>
        /// <param name="leftString3">The left string3.</param>
        /// <param name="rightString3">The right string3.</param>
        /// <param name="leftString4">The left string4.</param>
        /// <param name="rightString4">The right string4.</param>
        /// <returns>System.String.</returns>
        public string GetHtmlString(string leftString, string rightString, string leftString2, string rightString2, string leftString3, string rightString3, string leftString4, string rightString4)
        {
            return (GetHtmlString(leftString, rightString, leftString2, rightString2) + " " + GetHtmlString(leftString3, rightString3, leftString4, rightString4));
        }

        /// <summary>
        /// Gets the heading.
        /// </summary>
        /// <param name="column">The column.</param>
        /// <param name="isPlain">if set to <c>true</c> [is plain].</param>
        /// <returns>System.String.</returns>
        public string GetHeading(ISchemaItem column, bool isPlain=false)
        {
            if (!isPlain)
            {
                if ((!column.ColumnAttribute.IsBlank() && column.ColumnAttribute.Contains("[Display(Name=")) || column.ColumnLabel.IsBlank())
                    return "@Html.DisplayNameFor(model => model." + column.TableName + "[0]." + column.ColumnName + ")";
                else
                    return "@Html.DisplayName(" + column.ColumnLabel.AddQuotes() + ")";
            }
            else
            {
                return column.ColumnLabel;
            }

        }

        /// <summary>
        /// Returns a list of HTML columns.
        /// </summary>
        /// <param name="schemaItems">The schema items.</param>
        /// <param name="isPlain">if set to <c>true</c> [is plain].</param>
        /// <returns>List&lt;IHtmlColumns&gt;.</returns>
        public List<IHtmlColumns> GetHtmlColumns(IEnumerable<ISchemaItem> schemaItems, bool isPlain=false)
        {
            var columns = new List<IHtmlColumns>();
            foreach (var item in schemaItems)
            {
                var columnHeader = string.Empty;
                if (isPlain)
                    columnHeader = GetHeading(item, true);
                else
                    columnHeader = GetHeading(item);
                var columnString = DisplayForModelItem(item);
                columns.Add(new HtmlColumns(columnHeader, columnString, item.IsPrimaryKey, item.IsForeignKey, item.TableName,
                    item.ColumnName, item.RelatedTable, item.IsSortColumn, item.IsSearchColumn));
            }
            return columns;
        }

    }


}

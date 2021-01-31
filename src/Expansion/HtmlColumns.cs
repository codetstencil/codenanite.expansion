// ***********************************************************************
// Assembly         : ZeraSystems.CodeNanite.Expansion
// Author           : Ayo Dahunsi
// Created          : 05-15-2019
//
// Last Modified By : Ayo Dahunsi
// Last Modified On : 05-20-2019
// ***********************************************************************
// <copyright file="HtmlColumns.cs" company="ZeraSystems Inc.">
//     Copyright ©  2020 ZERA Systems Inc.
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace ZeraSystems.CodeNanite.Expansion
{
    /// <summary>
    /// Class HtmlColumns.
    /// Implements the <see cref="ZeraSystems.CodeNanite.Expansion.IHtmlColumns" />
    /// </summary>
    /// <seealso cref="ZeraSystems.CodeNanite.Expansion.IHtmlColumns" />
    public class HtmlColumns : IHtmlColumns
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HtmlColumns"/> class.
        /// </summary>
        /// <param name="heading">The heading.</param>
        /// <param name="fieldString">The field string.</param>
        /// <param name="isPrimary">if set to <c>true</c> [is primary].</param>
        /// <param name="isForeignKey">if set to <c>true</c> [is foreign key].</param>
        /// <param name="tableName">Name of the table.</param>
        /// <param name="columnName">Name of the column.</param>
        /// <param name="relatedTable">The related table.</param>
        /// <param name="allowSort">if set to <c>true</c> [allow sort].</param>
        /// <param name="allowSearch">if set to <c>true</c> [allow search].</param>
        public HtmlColumns(string heading, string fieldString, bool isPrimary, bool isForeignKey,
            string tableName,
            string columnName,
            string relatedTable,
            bool allowSort = false, bool allowSearch = false)
        {
            Heading = heading;
            FieldString = fieldString;
            IsPrimaryKey = isPrimary;
            IsForeignKey = isForeignKey;
            TableName = tableName;
            ColumnName = columnName;
            RelatedTable = relatedTable;
            AllowSearch = allowSearch;
            AllowSort = allowSort;
        }
        /// <summary>
        /// Gets or sets the heading.
        /// </summary>
        /// <value>The heading.</value>
        public string Heading { get; set; }
        /// <summary>
        /// Gets or sets the field string.
        /// </summary>
        /// <value>The field string.</value>
        public string FieldString { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this instance is primary key.
        /// </summary>
        /// <value><c>true</c> if this instance is primary key; otherwise, <c>false</c>.</value>
        public bool IsPrimaryKey { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this instance is foreign key.
        /// </summary>
        /// <value><c>true</c> if this instance is foreign key; otherwise, <c>false</c>.</value>
        public bool IsForeignKey { get; set; }
        /// <summary>
        /// Gets or sets the name of the table.
        /// </summary>
        /// <value>The name of the table.</value>
        public string TableName { get; set; }
        /// <summary>
        /// Gets or sets the name of the column.
        /// </summary>
        /// <value>The name of the column.</value>
        public string ColumnName { get; set; }
        /// <summary>
        /// Gets or sets the related table.
        /// </summary>
        /// <value>The related table.</value>
        public string RelatedTable { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether [allow sort].
        /// </summary>
        /// <value><c>true</c> if [allow sort]; otherwise, <c>false</c>.</value>
        public bool AllowSort { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether [allow search].
        /// </summary>
        /// <value><c>true</c> if [allow search]; otherwise, <c>false</c>.</value>
        public bool AllowSearch { get; set; }
    }
}
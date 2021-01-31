// ***********************************************************************
// Assembly         : ZeraSystems.CodeNanite.Expansion
// Author           : Ayo Dahunsi
// Created          : 05-04-2019
//
// Last Modified By : Ayo Dahunsi
// Last Modified On : 05-15-2019
// ***********************************************************************
// <copyright file="IHtmlColumns.cs" company="ZeraSystems Inc.">
//     Copyright ©  2020 ZERA Systems Inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeraSystems.CodeNanite.Expansion
{
    /// <summary>
    /// Interface IHtmlColumns
    /// </summary>
    public interface IHtmlColumns
    {
        /// <summary>
        /// Gets or sets the heading.
        /// </summary>
        /// <value>The heading.</value>
        string Heading { get; set; }
        /// <summary>
        /// Gets or sets the field string.
        /// </summary>
        /// <value>The field string.</value>
        string FieldString { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this instance is primary key.
        /// </summary>
        /// <value><c>true</c> if this instance is primary key; otherwise, <c>false</c>.</value>
        bool IsPrimaryKey { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this instance is foreign key.
        /// </summary>
        /// <value><c>true</c> if this instance is foreign key; otherwise, <c>false</c>.</value>
        bool IsForeignKey { get; set; }
        /// <summary>
        /// Gets or sets the name of the table.
        /// </summary>
        /// <value>The name of the table.</value>
        string TableName { get; set; }
        /// <summary>
        /// Gets or sets the name of the column.
        /// </summary>
        /// <value>The name of the column.</value>
        string ColumnName { get; set; }
        /// <summary>
        /// Gets or sets the related table.
        /// </summary>
        /// <value>The related table.</value>
        string RelatedTable { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [allow sort].
        /// </summary>
        /// <value><c>true</c> if [allow sort]; otherwise, <c>false</c>.</value>
        bool AllowSort { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether [allow search].
        /// </summary>
        /// <value><c>true</c> if [allow search]; otherwise, <c>false</c>.</value>
        bool AllowSearch { get; set; }

    }
}

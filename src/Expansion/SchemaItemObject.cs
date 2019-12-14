// ***********************************************************************
// Assembly         : ZeraSystems.CodeNanite.Expansion
// Author           : Ayodele-Desktop
// Created          : 12-07-2018
//
// Last Modified By : Ayodele-Desktop
// Last Modified On : 12-07-2018
// ***********************************************************************
// <copyright file="SchemaItemObject.cs" company="ZeraSystems Inc.">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************
using ZeraSystems.CodeStencil.Contracts;

namespace ZeraSystems.CodeNanite.Expansion
{
    /// <summary>
    /// Class SchemaItemObject.
    /// Implements the <see cref="ZeraSystems.CodeStencil.Contracts.ISchemaItem" />
    /// </summary>
    /// <seealso cref="ZeraSystems.CodeStencil.Contracts.ISchemaItem" />
    public class SchemaItemObject : ISchemaItem
    {
        /// <summary>
        /// Gets or sets a value indicating whether [allow database null].
        /// </summary>
        /// <value><c>true</c> if [allow database null]; otherwise, <c>false</c>.</value>
        public bool AllowDbNull { get; set; }
        /// <summary>
        /// Gets or sets the name of the alternate key.
        /// </summary>
        /// <value>The name of the alternate key.</value>
        public string AlternateKeyName { get; set; }
        /// <summary>
        /// Gets or sets the calculated column.
        /// </summary>
        /// <value>The calculated column.</value>
        public string CalculatedColumn { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this instance can delete.
        /// </summary>
        /// <value><c>true</c> if this instance can delete; otherwise, <c>false</c>.</value>
        public bool CanDelete { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this instance can get.
        /// </summary>
        /// <value><c>true</c> if this instance can get; otherwise, <c>false</c>.</value>
        public bool CanGet { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this instance can post.
        /// </summary>
        /// <value><c>true</c> if this instance can post; otherwise, <c>false</c>.</value>
        public bool CanPost { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this instance can put.
        /// </summary>
        /// <value><c>true</c> if this instance can put; otherwise, <c>false</c>.</value>
        public bool CanPut { get; set; }
        /// <summary>
        /// Gets or sets the column attribute.
        /// </summary>
        /// <value>The column attribute.</value>
        public string ColumnAttribute { get; set; }
        /// <summary>
        /// Gets or sets the column label.
        /// </summary>
        /// <value>The column label.</value>
        public string ColumnLabel { get; set; }
        /// <summary>
        /// Gets or sets the name of the column.
        /// </summary>
        /// <value>The name of the column.</value>
        public string ColumnName { get; set; }
        /// <summary>
        /// Gets or sets the column sequence.
        /// </summary>
        /// <value>The column sequence.</value>
        public int ColumnSequence { get; set; }
        /// <summary>
        /// Gets or sets the size of the column.
        /// </summary>
        /// <value>The size of the column.</value>
        public int ColumnSize { get; set; }
        /// <summary>
        /// Gets or sets the type of the column.
        /// </summary>
        /// <value>The type of the column.</value>
        public string ColumnType { get; set; }
        /// <summary>
        /// Gets or sets the computed column SQL.
        /// </summary>
        /// <value>The computed column SQL.</value>
        public string ComputedColumnSql { get; set; }
        /// <summary>
        /// Gets or sets the name of the constraint.
        /// </summary>
        /// <value>The name of the constraint.</value>
        public string ConstraintName { get; set; }
        /// <summary>
        /// Gets or sets the default value.
        /// </summary>
        /// <value>The default value.</value>
        public string DefaultValue { get; set; }
        /// <summary>
        /// Gets or sets the default value SQL.
        /// </summary>
        /// <value>The default value SQL.</value>
        public string DefaultValueSql { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this instance has sequence.
        /// </summary>
        /// <value><c>true</c> if this instance has sequence; otherwise, <c>false</c>.</value>
        public bool HasSequence { get; set; }
        /// <summary>
        /// Gets or sets the name of the index.
        /// </summary>
        /// <value>The name of the index.</value>
        public string IndexName { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this instance is automatic increment.
        /// </summary>
        /// <value><c>true</c> if this instance is automatic increment; otherwise, <c>false</c>.</value>
        public bool IsAutoIncrement { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this instance is calculated column.
        /// </summary>
        /// <value><c>true</c> if this instance is calculated column; otherwise, <c>false</c>.</value>
        public bool IsCalculatedColumn { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this instance is checked.
        /// </summary>
        /// <value><c>true</c> if this instance is checked; otherwise, <c>false</c>.</value>
        public bool IsChecked { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this instance is foreign key.
        /// </summary>
        /// <value><c>true</c> if this instance is foreign key; otherwise, <c>false</c>.</value>
        public bool IsForeignKey { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this instance is primary key.
        /// </summary>
        /// <value><c>true</c> if this instance is primary key; otherwise, <c>false</c>.</value>
        public bool IsPrimaryKey { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this instance is required.
        /// </summary>
        /// <value><c>true</c> if this instance is required; otherwise, <c>false</c>.</value>
        public bool IsRequired { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this instance is table label.
        /// </summary>
        /// <value><c>true</c> if this instance is table label; otherwise, <c>false</c>.</value>
        public bool IsTableLabel { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this instance is unique.
        /// </summary>
        /// <value><c>true</c> if this instance is unique; otherwise, <c>false</c>.</value>
        public bool IsUnique { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this instance is updated by nanite.
        /// </summary>
        /// <value><c>true</c> if this instance is updated by nanite; otherwise, <c>false</c>.</value>
        public bool IsUpdatedByNanite { get; set; }
        /// <summary>
        /// Gets or sets the linked row.
        /// </summary>
        /// <value>The linked row.</value>
        public int LinkedRow { get; set; }
        /// <summary>
        /// Gets or sets the lookup column.
        /// </summary>
        /// <value>The lookup column.</value>
        public string LookupColumn { get; set; }
        /// <summary>
        /// Gets or sets the lookup display column.
        /// </summary>
        /// <value>The lookup display column.</value>
        public string LookupDisplayColumn { get; set; }
        /// <summary>
        /// Gets or sets the name of the mapped column.
        /// </summary>
        /// <value>The name of the mapped column.</value>
        public string MappedColumnName { get; set; }
        /// <summary>
        /// Gets or sets the maximum length.
        /// </summary>
        /// <value>The maximum length.</value>
        public int MaxLength { get; set; }
        /// <summary>
        /// Gets or sets the numeric precision.
        /// </summary>
        /// <value>The numeric precision.</value>
        public int NumericPrecision { get; set; }
        /// <summary>
        /// Gets or sets the name of the original.
        /// </summary>
        /// <value>The name of the original.</value>
        public string OriginalName { get; set; }
        /// <summary>
        /// Gets or sets the parent identifier.
        /// </summary>
        /// <value>The parent identifier.</value>
        public int ParentId { get; set; }
        /// <summary>
        /// Gets or sets the name of the primary key.
        /// </summary>
        /// <value>The name of the primary key.</value>
        public string PrimaryKeyName { get; set; }
        /// <summary>
        /// Gets or sets the related table.
        /// </summary>
        /// <value>The related table.</value>
        public string RelatedTable { get; set; }
        /// <summary>
        /// Gets or sets the relationship.
        /// </summary>
        /// <value>The relationship.</value>
        public string Relationship { get; set; }
        /// <summary>
        /// Gets or sets the schema.
        /// </summary>
        /// <value>The schema.</value>
        public string Schema { get; set; }
        /// <summary>
        /// Gets or sets the schema item identifier.
        /// </summary>
        /// <value>The schema item identifier.</value>
        public int SchemaItemId { get; set; }
        /// <summary>
        /// Gets or sets the name of the table.
        /// </summary>
        /// <value>The name of the table.</value>
        public string TableName { get; set; }
        public bool IsSortColumn { get; set; }
        public bool IsSearchColumn { get; set; }


    }
}
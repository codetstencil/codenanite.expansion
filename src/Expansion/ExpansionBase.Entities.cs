// ***********************************************************************
// Assembly         : ZeraSystems.CodeNanite.Expansion
// Author           : Ayo Dahunsi
// Created          : 12-07-2018
//
// Last Modified By : Ayodele-Desktop
// Last Modified On : 07-13-2020
// ***********************************************************************
// <copyright file="ExpansionBase.Entities.cs" company="ZeraSystems Inc.">
//     Copyright © 2020
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text.RegularExpressions;
using ZeraSystems.CodeStencil.Contracts;

namespace ZeraSystems.CodeNanite.Expansion
{
    /// <summary>
    /// Class ExpansionBase.
    /// </summary>
    public abstract partial class ExpansionBase
    {
        #region Primary Key

        /// <summary>
        /// Get the primary key of a table
        /// </summary>
        /// <param name="table">Table we need primary key for</param>
        /// <returns>Primary Key</returns>
        public string GetPrimaryKey(string table, bool setTableName = true)
        {
            string name;
            name = GetTheTableName(table);
            if (name.IsBlank()  && setTableName)
            {
                table = Singularize(table);
                table = char.ToUpper(table[0]) + table.Substring(1);
                name = GetTheTableName(table);
            }
            return name;

            string GetTheTableName(string s)
            {
                if (PreserveTableName())
                {
                    name = SchemaItem()
                        .Where(e => e.TableName == s && e.IsPrimaryKey)
                        .Select(e => e.ColumnName)
                        .FirstOrDefault();
                }
                else
                {
                    name = SchemaItem()
                        .Where(e => e.TableName.Singularize() == s.Singularize() && e.IsPrimaryKey)
                        .Select(e => e.ColumnName)
                        .FirstOrDefault();
                }

                return name;
            }
        }

        /// <summary>
        /// Gets the primary key row.
        /// </summary>
        /// <param name="table">The table.</param>
        /// <returns>ISchemaItem.</returns>
        public ISchemaItem GetPrimaryKeyRow(string table)
        {
            ISchemaItem row;
            if (PreserveTableName())
            {
                row = SchemaItem()
                    .FirstOrDefault(e => e.TableName == table && e.IsPrimaryKey);
            }
            else
            {
                row = SchemaItem()
                    .FirstOrDefault(e => e.TableName.Singularize() == table.Singularize() && e.IsPrimaryKey);
            }
            return row;
        }

        /// <summary>
        /// Gets the type of the primary key.
        /// </summary>
        /// <param name="table">The table.</param>
        /// <returns>System.String.</returns>
        public string GetPrimaryKeyType(string table) => GetPrimaryKeyRow(table).ColumnType;

        /// <summary>
        /// Gets the table and primary key.
        /// (table.primaryKey)
        /// </summary>
        /// <param name="table">The table.</param>
        /// <returns>System.String.</returns>
        public string GetTableAndPrimaryKey(string table)
        {
            var primaryKey = GetPrimaryKey(table);
            return !primaryKey.IsBlank() ? table + "." + primaryKey : string.Empty;
        }

        #endregion Primary Key

        /// <summary>
        /// Struct containing actions flags
        /// </summary>
        public struct Actions
        {
            /// <summary>
            /// The delete
            /// </summary>
            public bool Delete;

            /// <summary>
            /// The get
            /// </summary>
            public bool Get;

            /// <summary>
            /// The post
            /// </summary>
            public bool Post;

            /// <summary>
            /// The put
            /// </summary>
            public bool Put;
        }

        /// <summary>
        /// <para>
        /// Creates the name of the related table property.
        /// </para>
        /// <para>When the table row object cannot be passed, passing the table and column name enables CreateTablePropertyName() to be called.</para>
        /// </summary>
        /// <param name="column">The column.</param>
        /// <param name="table">The table.</param>
        /// <returns>System.String.</returns>
        public string CreateRelatedTablePropertyName(string column, string table)
        {
            var schemaItem = SchemaItem().FirstOrDefault(e => e.TableName == table && e.ColumnName == column);
            return schemaItem != null ? CreateTablePropertyName(schemaItem) : column;
        }

        /// <summary>
        /// Creates the name of the table property.
        /// </summary>
        /// <param name="item">The row/record that represents the Table object</param>
        /// <returns>If "id" is part of the name it is removed. e.g. "SupportStaffId" becomes "SupportStaff", otherwise returns the Related Table name.</returns>
        public string CreateTablePropertyName(ISchemaItem item)
        {
            if (item.ColumnName == item.LookupColumn)
            {
                return item.RelatedTable;
            }

            if (item.ColumnName.ToLower().Contains("id") &&
                item.ColumnName.ToLower().Substring(item.ColumnName.Length - 2) == "id")
            {
                return Regex.Replace(item.ColumnName, "id", string.Empty, RegexOptions.IgnoreCase);
            }

            return item.RelatedTable;
        }

        /// <summary>
        /// Gets a Struct containing settings for actions.
        /// </summary>
        /// <param name="theTable">Passed table.</param>
        /// <returns>Actions.</returns>
        public Actions GetActions(string theTable)
        {
            var action = new Actions();
            var table = GetTableObject(theTable);
            if (table != null)
            {
                action.Get = table.CanGet;
                action.Post = table.CanPost;
                action.Put = table.CanPut;
                action.Delete = table.CanDelete;
            }
            return action;
        }

        /// <summary>
        /// Gets the column attribute (Data Annotation) of passed column.
        /// </summary>
        /// <param name="column">The column.</param>
        /// <param name="table">The table.</param>
        /// <returns>System.String.</returns>
        public string GetColumnAttribute(string column, string table)
        {
            var name = _schemaItem
                .Where(e => e.TableName == table && e.ColumnName == column)
                .Select(e => e.ColumnAttribute)
                .FirstOrDefault();
            return name.IsBlank() ? string.Empty : name;
        }

        /// <summary>
        /// Gets a List of columns for passed table.
        /// </summary>
        /// <param name="schemaItem">The schema item.</param>
        /// <param name="table">The table.</param>
        /// <param name="onlyIsChecked">if set to <c>true</c> [only is checked].</param>
        /// <param name="excludeColumn">The column to exclude</param>
        /// <param name="noCalculated">Exclude Calculated Columns</param>
        /// <returns>List&lt;ISchemaItem&gt;.</returns>
        public List<ISchemaItem> GetColumns(List<ISchemaItem> schemaItem, string table, bool onlyIsChecked = true, string excludeColumn = null, bool noCalculated = false)
        {
            if (onlyIsChecked)
            {
                return schemaItem
                    .Where(e => !string.IsNullOrEmpty(e.ColumnType))
                    .Where(e => (e.TableName == table && e.IsChecked == onlyIsChecked))
                    .Where(e => e.ColumnName != excludeColumn)
                    .OrderBy(e => e.ColumnSequence)
                    .ToList();
            }
            else if (noCalculated)
            {
                return schemaItem
                    .Where(e => !string.IsNullOrEmpty(e.ColumnType))
                    .Where(e => (e.TableName == table))
                    .Where(e => e.ColumnName != excludeColumn)
                    .Where(e => !e.IsCalculatedColumn)
                    .OrderBy(e => e.ColumnSequence)
                    .ToList();
            }
            else
            {
                return schemaItem
                    .Where(e => !string.IsNullOrEmpty(e.ColumnType))
                    .Where(e => (e.TableName == table))
                    .Where(e => e.ColumnName != excludeColumn)
                    .OrderBy(e => e.ColumnSequence)
                    .ToList();
            }
        }

        /// <summary>
        /// Returns columns in the passed table as a List Collection
        /// </summary>
        /// <param name="table">Table we need columns for</param>
        /// <param name="onlyIsChecked">If TRUE is passed (which is default), indicates that only columns checked on the Global Schema grid are returned</param>
        /// <param name="excludeColumn">If we want to exclude a specific column</param>
        /// <returns>Returned List of Columns</returns>
        public List<ISchemaItem> GetColumns(string table, bool onlyIsChecked = true, string excludeColumn = null)
        {
            return GetColumns(_schemaItem, table, onlyIsChecked, excludeColumn);
        }

        /// <summary>
        /// Returns columns (including navigation columns) in the passed table as a List Collection
        /// </summary>
        /// <param name="table">Table we need columns for</param>
        /// <param name="onlyIscChecked">if set to <c>true</c> [only isc checked].</param>
        /// <returns>List&lt;ISchemaItem&gt;.</returns>
        public List<ISchemaItem> GetColumnsAndNavigation(string table, bool onlyIscChecked = true)
        {
            var columns = GetColumns(table, false);

            // Get primary key
            var primaryKey = _schemaItem
                .FirstOrDefault(e => (e.TableName == table && e.IsPrimaryKey));

            var list = new List<ISchemaItem>();
            if (primaryKey != null)
            {
                list = _schemaItem
                    .Where(e => !string.IsNullOrEmpty(e.ColumnType))
                    .Where(e => (e.IsForeignKey && e.RelatedTable == table))
                    //.Where(e => (e.ColumnName == primaryKey.ColumnName && e.RelatedTable == table))
                    .ToList();
            }

            if (list.Count > 0)
            {
                //return columns.Concat(list).ToList();

                var hs = new HashSet<ISchemaItem>(columns, new SchemaItemComparer());
                hs.UnionWith(list);
                return hs.ToList();
            }
            return columns;
        }

        /// <summary>
        /// Return a list of columns as dictionary.
        /// </summary>
        /// <param name="table">The passed table</param>
        /// <returns>Dictionary&lt;System.String, System.String&gt;.</returns>
        public Dictionary<string, string> GetColumnsAsDictionary(string table)
        {
            var cols = SchemaItem()
                .Where(e => e.TableName == table && e.ParentId != 0)
                .Select(e => new KeyValuePair<string, string>(e.ColumnName, string.Empty))
                .ToDictionary(e => e.Key, e => e.Value);

            return cols;
        }

        /// <summary>
        /// Gets the columns excluding calculated columns.
        /// </summary>
        /// <param name="table">The table.</param>
        /// <returns>List&lt;ISchemaItem&gt;.</returns>
        public List<ISchemaItem> GetColumnsExCalculated(string table)
        {
            //return GetColumns(_schemaItem, table, onlyIsChecked, excludeColumn);
            return GetColumns(_schemaItem, table, false, null, true);
        }

        /// <summary>
        /// Gets the display expression column.
        /// </summary>
        /// <param name="table">This is the FK table of the main table</param>
        /// <returns>System.String.</returns>
        public string GetDisplayExpressionColumn(string table)
        {
            return SchemaItem()
                .Where(e => e.TableName == table && e.IsTableLabel)
                .Select(e => e.ColumnName)
                .FirstOrDefault();

            //return SchemaItem()
            //    .Where(e => !string.IsNullOrEmpty(e.ColumnType))
            //    .Where(e => (e.RelatedTable == table))
            //    .ToList();
        }

        /// <summary>
        /// Gets the foreign key column.
        /// </summary>
        /// <param name="table">The table.</param>
        /// <param name="related">The related table</param>
        /// <returns>System.String.</returns>
        public string GetForeignKeyColumn(string table, string related)
        {
            if (related.IsBlank())
                return null;

            var column = SchemaItem()
                .Where(e => (e.TableName == table && e.RelatedTable == related))
                .Select(e => e.ColumnName).FirstOrDefault();
            return column;
        }

        /// <summary>
        /// Gets the foreign keys in one to one relationship.
        /// </summary>
        /// <param name="schemaItem">The schema item.</param>
        /// <param name="table">The table.</param>
        /// <param name="allowPrimaryKeys">if set to <c>true</c> [allow primary keys].</param>
        /// <returns>List&lt;ISchemaItem&gt;.</returns>
        public List<ISchemaItem> GetForeignKeysInOneToOne(List<ISchemaItem> schemaItem, string table, bool allowPrimaryKeys = false)
        {
            // By default, we will exclude intermediate files with rows have both primary and foreign keys.
            // However, you may have files with a one-to-one relationship, in thus case we need to add the row
            // to the result so that a navigation property can be created to complement the FluentApi configuration
            // that will map a "HasOne", "WithOne" configuration
            return schemaItem
                .Where(e => !string.IsNullOrEmpty(e.ColumnType))
                .Where(e => (e.IsForeignKey && e.IsPrimaryKey == allowPrimaryKeys))
                .Where(e => e.RelatedTable == table)
                .ToList();
        }

        /// <summary>
        /// Return the foreign keys in table as a List. You can pass your own specific list of SChema Items
        /// </summary>
        /// <param name="schemaItem">The schema item.</param>
        /// <param name="table">The table.</param>
        /// <param name="allowPrimaryKeys">if set to <c>true</c> [allow primary keys].</param>
        /// <returns>List&lt;ISchemaItem&gt;.</returns>
        public List<ISchemaItem> GetForeignKeysInTable(List<ISchemaItem> schemaItem, string table, bool allowPrimaryKeys = false)
        {
            return schemaItem
                .Where(e => !string.IsNullOrEmpty(e.ColumnType))
                .Where(e => (e.IsForeignKey) || (e.IsForeignKey && e.IsPrimaryKey == allowPrimaryKeys))
                //.Where(e => (e.IsPrimaryKey == allowPrimaryKeys))      //We need to exclude intermediate files with rows have both primary and foreign keys
                .Where(e => e.TableName == table)
                .ToList();
        }

        /// <summary>
        /// Returns the foreign keys in table as a List.
        /// </summary>
        /// <param name="table">The table.</param>
        /// <param name="allowPrimaryKeys">if set to <c>true</c> [allow primary keys].</param>
        /// <returns>List&lt;ISchemaItem&gt;.</returns>
        public List<ISchemaItem> GetForeignKeysInTable(string table, bool allowPrimaryKeys = false)
        {
            var result = GetForeignKeysInTable(_schemaItem, table, allowPrimaryKeys);
            if (allowPrimaryKeys)
            {
                var result2 = GetForeignKeysInOneToOne(_schemaItem, table, true);
                result.AddRange(result2);
            }
            return result;
        }

        /// <summary>
        /// Gets the lookup column.
        /// </summary>
        /// <param name="table">The table.</param>
        /// <param name="related">The related.</param>
        /// <returns>System.String.</returns>
        public string GetLookupColumn(string table, string related)
        {
            if (related.IsBlank())
                return null;

            var column = SchemaItem()
                .Where(e => (e.TableName == table && e.RelatedTable == related))
                .Select(e => e.LookupColumn).FirstOrDefault();
            return column;
        }

        /// <summary>
        /// Gets the lookup display column.
        /// </summary>
        /// <param name="table">The table.</param>
        /// <param name="foreignKey">The foreign key.</param>
        /// <returns>The column used for displaying the lookup</returns>
        public string GetLookupDisplayColumn(string table, string foreignKey)
        {
            var lookupTable = GetLookupTable(table, foreignKey);
            var row = SchemaItem()
                        .FirstOrDefault(e => e.TableName == table && e.ColumnName == foreignKey && e.IsForeignKey);

            var displayColumn = string.Empty;
            if (row != null && !row.IsCalculatedColumn) 
                displayColumn = row.LookupDisplayColumn;

            if (displayColumn.IsBlank())
                displayColumn = GetLookupTableLabel(table, foreignKey); //Use Table Label Column

            if (displayColumn.IsBlank())
                displayColumn = GetStringColumn(lookupTable); //Use the first string column of lookup table

            if (displayColumn.IsBlank()) 
                displayColumn = GetPrimaryKey(lookupTable); //Use Primary Key of lookup table

            return displayColumn;
        }

        /// <summary>
        /// Gets the lookup display column with table name, e.g. myTable.myLookupColumn
        /// </summary>
        /// <param name="table">The table.</param>
        /// <param name="foreignKey">The foreign key.</param>
        /// <returns>String representing table.lookup column</returns>
        public string GetLookupDisplayColumnWithPath(string table, string foreignKey)
        {
            var lookupTable = GetLookupTable(table, foreignKey);
            var result = CreateRelatedTablePropertyName(foreignKey, table) + "." + GetLookupDisplayColumn(table, foreignKey);
            //var result = lookupTable + "." + GetLookupDisplayColumn(table, foreignKey);
            return result;
        }

        /// <summary>
        /// Gets the lookup table.
        /// </summary>
        /// <param name="table">The table.</param>
        /// <param name="foreignKey">The foreign key.</param>
        /// <returns>System.String.</returns>
        public string GetLookupTable(string table, string foreignKey)
        {
            string result;
            if (PreserveTableName())
            {
                result = SchemaItem()
                    .Where(e => e.TableName == table && e.ColumnName == foreignKey && e.IsForeignKey)
                    .Select(e => e.RelatedTable).FirstOrDefault();
            }
            else
            {
                result = SchemaItem()
                    .Where(e => e.TableName.Singularize() == table.Singularize() && e.ColumnName == foreignKey && e.IsForeignKey)
                    .Select(e => e.RelatedTable).FirstOrDefault();
            }
            return result;
        }

        /// <summary>
        /// Gets the lookup table label.
        /// </summary>
        /// <param name="table">The table.</param>
        /// <param name="foreignKey">The foreign key.</param>
        /// <returns>String representing the label of the lookup</returns>
        public string GetLookupTableLabel(string table, string foreignKey)
        {
            var lookupTable = GetLookupTable(table, foreignKey);
            if (!lookupTable.IsBlank())
            {
                return SchemaItem()
                    .Where(e => e.TableName == lookupTable && e.IsTableLabel)
                    .Select(e => e.ColumnName)
                    .FirstOrDefault();
            }
            else
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Gets the lookup table label with path.
        /// </summary>
        /// <param name="table">The table.</param>
        /// <param name="foreignKey">The foreign key.</param>
        /// <returns>System.String.</returns>
        public string GetLookupTableLabelWithPath(string table, string foreignKey)
        {
            //var lookupTable = GetLookupTable(table, foreignKey);
            //if (lookupTable == table)
            //    return table + "." + GetLookupTableLabel(table, foreignKey);
            //else
            return table + "." + GetLookupTableLabel(table, foreignKey);
        }

        /// <summary>
        /// Gets navigation properties.
        /// </summary>
        /// <param name="schemaItem">The schema item.</param>
        /// <param name="table">The table.</param>
        /// <returns>List&lt;ISchemaItem&gt;.</returns>
        public List<ISchemaItem> GetNavProperties(List<ISchemaItem> schemaItem, string table)
        {
            var result = schemaItem
                .Where(e => ((e.RelatedTable == table && e.ColumnType != null) ||
                             (e.TableName == table && e.RelatedTable != null)))
                .Where(e => e.OriginalName == e.LookupColumn)
                .ToList();

            if (!result.Any())
            {
                result = schemaItem
                    .Where(e => ((e.RelatedTable == table && e.ColumnType != null) ||
                                 (e.TableName == table && e.RelatedTable != null)))
                    .ToList();
            }

            return result;
        }

        /// <summary>
        /// Gets navigation properties.
        /// </summary>
        /// <param name="table">The table.</param>
        /// <returns>List&lt;ISchemaItem&gt;.</returns>
        public List<ISchemaItem> GetNavProperties(string table) => GetNavProperties(SchemaItem().ToList(), table);

        /// <summary>
        /// Gets the 12M table.
        /// </summary>
        /// <param name="table">The table.</param>
        /// <returns>IEnumerable&lt;ISchemaItem&gt;.</returns>
        public IEnumerable<ISchemaItem> GetOne2ManyTable(string table)
        {
            return SchemaItem()
                .Where(e => e.RelatedTable == table);
        }

        /// <summary>
        /// Gets the original name of the table .
        /// </summary>
        /// <param name="table">The table.</param>
        /// <param name="allowNullReturn">if set to <c>true</c> [allow null return].</param>
        /// <returns>System.String.</returns>
        public string GetOriginalTableName(string table, bool allowNullReturn = false)
        {
            var name = _schemaItem
                .Where(e => e.TableName == table && string.IsNullOrEmpty(e.ColumnType))
                .Select(e => e.OriginalName)
                .FirstOrDefault();

            if (name.IsBlank() && allowNullReturn)
                return name;

            return name.IsBlank() ? GetTable(table) : name;
        }

        /// <summary>
        /// Gets the name of the related column.
        /// </summary>
        /// <param name="column">The column.</param>
        /// <returns>String representing the table.column</returns>
        public string GetRelatedColumnName(IHtmlColumns column)
        {
            //var columnName = CreateRelatedTablePropertyName(column.ColumnName, column.TableName);
            var columnName = column.ColumnName;
            if (column.IsForeignKey && (column.RelatedTable == column.TableName))
            {
                return column.TableName + "." + columnName + NavigationLabel();
            }
            else if (column.IsForeignKey)
            {
                return column.TableName + "." + GetLookupDisplayColumnWithPath(column.TableName, columnName);
            }
            else
            {
                return column.TableName + "." + columnName;
            }
        }

        /// <summary>
        /// Gets the related tables to the passed table as a List.
        /// </summary>
        /// <param name="schemaItem">The schema item.</param>
        /// <param name="table">The table.</param>
        /// <returns>List&lt;ISchemaItem&gt;.</returns>
        public List<ISchemaItem> GetRelatedTables(List<ISchemaItem> schemaItem, string table)
        {
            return schemaItem
                .Where(e => !string.IsNullOrEmpty(e.ColumnType))
                .Where(e => (e.RelatedTable == table))
                //.Where(e=> (IsTableEnabled(e.RelatedTable))
                .ToList();
        }

        /// <summary>
        /// Gets the related tables.
        /// </summary>
        /// <param name="table">The table.</param>
        /// <returns>List&lt;ISchemaItem&gt;.</returns>
        public List<ISchemaItem> GetRelatedTables(string table) => GetRelatedTables(SchemaItem().ToList(), table);

        /// <summary>
        /// Gets the corresponding SchemaItem column based on the passed column name.
        /// </summary>
        /// <param name="column">The name of the column</param>
        /// <param name="table">The table column belongs to</param>
        /// <returns>SchemaItem column row</returns>
        public ISchemaItem GetSchemaItemColumn(string column, string table)
        {
            var schemaItemColumn = SchemaItem()
                .FirstOrDefault(x => (x.ColumnName == column.StripCarriage() && x.TableName == table.StripCarriage()));
            return schemaItemColumn;
        }

        /// <summary>
        /// Returns a list of the search columns.
        /// </summary>
        /// <param name="table">The table.</param>
        /// <returns>List&lt;ISchemaItem&gt;.</returns>
        public List<ISchemaItem> GetSearchColumns(string table)
        {
            if (PreserveTableName())
            {
                return SchemaItem()
                    .Where(e => e.TableName == table && e.IsSearchColumn).ToList();
            }
            else
            {
                return SchemaItem()
                    .Where(e => e.TableName.Singularize() == table.Singularize() && e.IsSearchColumn).ToList();
            }
        }

        /// <summary>
        /// Gets the self join columns.
        /// </summary>
        /// <param name="schemaItem">The schema item.</param>
        /// <param name="table">The table.</param>
        /// <returns>List&lt;ISchemaItem&gt;.</returns>
        public List<ISchemaItem> GetSelfJoinColumns(List<ISchemaItem> schemaItem, string table)
        {
            return schemaItem
                .Where(e => (e.TableName == table && e.RelatedTable == table))
                .ToList();
        }

        /// <summary>
        /// Gets the self join columns .
        /// </summary>
        /// <param name="table">The table.</param>
        /// <returns>List&lt;ISchemaItem&gt;.</returns>
        public List<ISchemaItem> GetSelfJoinColumns(string table) => GetSelfJoinColumns(SchemaItem().ToList(), table);

        /// <summary>
        /// Returns a list of the sort columns.
        /// </summary>
        /// <param name="table">The table.</param>
        /// <returns>List&lt;ISchemaItem&gt;.</returns>
        public List<ISchemaItem> GetSortColumns(string table)
        {
            if (PreserveTableName())
            {
                return SchemaItem()
                    .Where(e => e.TableName == table && e.IsSortColumn).ToList();
            }
            else
            {
                return SchemaItem()
                    .Where(e => e.TableName.Singularize() == table.Singularize() && e.IsSortColumn).ToList();
            }
        }

        /// <summary>
        /// Gets the first string column in a lookup table.
        /// </summary>
        /// <param name="table">The table.</param>
        /// <returns>Label of the string column</returns>
        public string GetStringColumn(string table)
        {
            return SchemaItem()
                .Where(e => (e.TableName == table && e.ColumnType == "string"))
                .Select(e => e.ColumnName)
                .FirstOrDefault();
        }

        /// <summary>
        /// Returns the label of a table
        /// </summary>
        /// <param name="table">The table.</param>
        /// <param name="enabledOnly">Indicated if list should contain only enabled tables</param>
        /// <param name="setTableName">This flag forces the method to covert a passed table name into an actual table.
        /// For example, "categories" may be passed due to an expansion from another Code Nanite, so this will be converted
        /// To: "Category". This will only work for Singularized table names imported into the Global Schema.</param>
        /// <returns>System.String.</returns>
        public string GetTable(string table, bool enabledOnly = true, bool setTableName = true)
        {
            
            string name;
            name = GetTheTableName(table, enabledOnly);
            if (name.IsBlank() && setTableName)
            {
                table = Singularize(table);
                if (!table.IsBlank())
                {
                    table = char.ToUpper(table[0]) + table.Substring(1);
                    name = GetTheTableName(table, enabledOnly);
                }
            }
            return name;

            string GetTheTableName(string tableName, bool b)
            {
                if (PreserveTableName())
                {
                    name = GetTables(b)
                        .Where(e => e.ColumnName == tableName)
                        .Select(e => e.ColumnName)
                        .SingleOrDefault() ?? tableName;
                }
                else
                {
                    name = GetTables(b)
                        .Where(e => e.ColumnName.Singularize() == tableName.Singularize())
                        .Select(e => e.ColumnName)
                        .SingleOrDefault() ?? tableName;
                }

                return name;
            }

        }

        /// <summary>
        /// Gets the table label.
        /// </summary>
        /// <param name="table">The table.</param>
        /// <param name="allowNullReturn">If TRUE is passed, it allows a null/empty string to be returned
        /// even if the Table Name has not yet been defined.</param>
        /// <returns>The Table Name specified for a Table. By Default, the primary key is returned if no
        /// Table Name has been defined</returns>
        public string GetTableLabel(string table, bool allowNullReturn = false)
        {
            var name = _schemaItem
                .Where(e => e.TableName == table && e.ColumnType != null && e.IsTableLabel == true)
                .Select(e => e.ColumnName)
                .FirstOrDefault();

            if (name.IsBlank() && allowNullReturn)
                return name;

            return name.IsBlank() ? GetPrimaryKey(table) : name;
        }

        /// <summary>
        /// Returns a table object ( row from SchemaItems) to give access to the columns in that table header
        /// </summary>
        /// <param name="table">Table to use</param>
        /// <param name="enabledOnly">If FALSE is passed, then all tables are returned. Default is TRUE</param>
        /// <returns>ISchemaItem.</returns>
        public ISchemaItem GetTableObject(string table, bool enabledOnly = true)
        {
            var name = GetTables(enabledOnly)
                           .FirstOrDefault(e => (e.TableName == table && e.ParentId == 0));
            return name;
        }

        /// <summary>
        /// Return the tables in database as a List Collection
        /// </summary>
        /// <param name="enabledOnly">If FALSE is passed, then all tables are returned. Default is TRUE</param>
        /// <returns>Returned List of Table</returns>
        public List<ISchemaItem> GetTables(bool enabledOnly = true)
        {
            return GetTables(_schemaItem, enabledOnly);
            //return _schemaItem
            //    .Where(e => string.IsNullOrEmpty(e.ColumnType))
            //    .ToList();
        }

        /// <summary>
        /// Gets a list of tables.
        /// </summary>
        /// <param name="schemaItem">The schema item.</param>
        /// <param name="enabledOnly">Indicates that only enabled tables should be listed</param>
        /// <returns>List&lt;ISchemaItem&gt;.</returns>
        public List<ISchemaItem> GetTables(List<ISchemaItem> schemaItem, bool enabledOnly = true)
        {
            if (enabledOnly)
            {
                return schemaItem
                    .Where(e => e.ParentId == 0 && (e.TableName == e.ColumnName) && e.IsChecked == enabledOnly)
                    .ToList();
            }
            else
            {
                return schemaItem
                    .Where(e => e.ParentId == 0 && (e.TableName == e.ColumnName) )
                    .ToList();
            }
        }

        /// <summary>
        /// Determines whether a table has calculated column(s)
        /// </summary>
        /// <param name="table">The table.</param>
        /// <returns><c>true</c> if [has calculated column] [the specified table]; otherwise, <c>false</c>.</returns>
        public bool HasCalculatedColumn(string table)
        {
            var name = _schemaItem
                .FirstOrDefault(e => e.TableName == table && e.IsCalculatedColumn);
            return name != null;
        }

        /// <summary>
        /// Determines whether the passed table is in the list of tables
        /// </summary>
        /// <param name="table">The table.</param>
        /// <param name="enabledOnly">if set to <c>true</c> [enabled only].</param>
        /// <returns><c>true</c> if [is in list of tables] [the specified table]; otherwise, <c>false</c>.</returns>
        public bool IsInListOfTables(string table, bool enabledOnly = true)
        {
            var foundTable = GetTables(enabledOnly).FirstOrDefault(t => t.TableName == table && t.ParentId == 0);
            return foundTable != null;
        }

        /// <summary>
        /// Determines whether primary key is in related tables
        /// </summary>
        /// <param name="table">The table.</param>
        /// <param name="targetTable">The target table.</param>
        /// <returns><c>true</c> if [is primary in related] [the specified table]; otherwise, <c>false</c>.</returns>
        public bool IsPrimaryInRelated(string table, string targetTable)
        {
            var result =
                SchemaItem()
                .Where(e => e.RelatedTable == table && e.TableName == targetTable && !string.IsNullOrEmpty(e.ColumnType))
                .FirstOrDefault(x => x.IsPrimaryKey && x.IsForeignKey);
            return result != null;
        }

        /// <summary>
        /// Determines whether a table is enabled
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns><c>true</c> if table enabled otherwise, <c>false</c>.</returns>
        public bool IsTableEnabled(string name)
        {
            var result = false;
            var table = GetTableObject(name);
            if (table != null)
            {
                result = true;
            }

            return result;
        }

        /// <summary>
        /// Confirm the passed table is in database
        /// </summary>
        /// <param name="name">The name.</param>
        public void IsTableInDatabase(string name)
        {
            var isTable = _schemaItem.Where(s => s.ColumnName == name)
                .Where(s => string.IsNullOrEmpty(s.ColumnType));
        }

        /// <summary>
        /// Schema Items.
        /// </summary>
        /// <returns>IEnumerable&lt;ISchemaItem&gt;.</returns>
        private IEnumerable<ISchemaItem> SchemaItem()
        {
            Debug.Assert(_schemaItem != null);
            return _schemaItem;
        }
    }
}

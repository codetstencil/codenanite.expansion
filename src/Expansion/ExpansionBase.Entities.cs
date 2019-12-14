// ***********************************************************************
// Assembly         : ZeraSystems.CodeNanite.Expansion
// Author           : Ayodele-Desktop
// Created          : 12-07-2018
//
// Last Modified By : Ayodele-Desktop
// Last Modified On : 01-03-2019
// ***********************************************************************
// <copyright file="ExpansionBase.Entities.cs" company="ZeraSystems Inc.">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using ZeraSystems.CodeStencil.Contracts;

namespace ZeraSystems.CodeNanite.Expansion
{
    /// <summary>
    /// Class ExpansionBase.
    /// </summary>
    public abstract partial class ExpansionBase
    {
        /// <summary>
        /// Schema Items.
        /// </summary>
        /// <returns>IEnumerable&lt;ISchemaItem&gt;.</returns>
        private IEnumerable<ISchemaItem> SchemaItem()
        {
            Debug.Assert(_schemaItem != null);
            return _schemaItem;
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

        public List<ISchemaItem> GetColumnsExCalculated(string table)
        {
            //return GetColumns(_schemaItem, table, onlyIsChecked, excludeColumn);
            return GetColumns(_schemaItem, table, false, null, true);
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

        //public List<ISchemaItem> GetColumns(string table, bool onlyIsChecked = true, bool noComputed = false)
        //{
        //    return GetColumns(_schemaItem, table, onlyIsChecked, null, noComputed);
        //}

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
            {
                return null;
            }

            var column = SchemaItem()
                .Where(e => (e.TableName == table && e.RelatedTable == related))
                .Select(e => e.ColumnName).FirstOrDefault();
            return column;
        }

        public string GetLookupColumn(string table, string related)
        {
            if (related.IsBlank())
            {
                return null;
            }

            var column = SchemaItem()
                .Where(e => (e.TableName == table && e.RelatedTable == related))
                .Select(e => e.LookupColumn).FirstOrDefault();
            return column;
        }

        /// <summary>
        /// Gets the lookup table.
        /// </summary>
        /// <param name="table">The table.</param>
        /// <param name="foreignKey">The foreign key.</param>
        /// <returns>System.String.</returns>
        public string GetLookupTable(string table, string foreignKey)
        {
            return SchemaItem()
                .Where(e => e.TableName.Singularize() == table.Singularize() && e.ColumnName == foreignKey && e.IsForeignKey)
                .Select(e => e.RelatedTable)
                .FirstOrDefault();
        }

        public string GetLookupTableLabel(string table, string foreignKey)
        {
            //var lookupColumn = GetLookupColumn(table, foreignKey);
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

        public string GetLookupTableLabelWithPath(string table, string foreignKey)
        {
            //var lookupTable = GetLookupTable(table, foreignKey);
            //if (lookupTable == table)
            //    return table + "." + GetLookupTableLabel(table, foreignKey);
            //else
            return table + "." + GetLookupTableLabel(table, foreignKey);
        }

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
                displayColumn = GetStringColumn(lookupTable); //Use the first strin column of lookup table

            if (displayColumn.IsBlank()) displayColumn = GetPrimaryKey(lookupTable); //Use Primary Key of lookup table

            return displayColumn;
        }

        public string GetStringColumn(string table)
        {
            return SchemaItem()
                .Where(e => (e.TableName == table && e.ColumnType == "string"))
                .Select(e => e.ColumnName)
                .FirstOrDefault();
        }

        public string GetLookupDisplayColumnWithPath(string table, string foreignKey)
        {
            var lookupTable = GetLookupTable(table, foreignKey);
            return lookupTable + "." + GetLookupDisplayColumn(table, foreignKey);

            //var lookupTable = GetLookupTable(table, foreignKey);
            //var displayColumn = //GetLookupDisplayColumn(table, foreignKey);  //Use LookupDislayColumn
            //    SchemaItem()
            //        .Where(e => e.TableName == table && e.ColumnName == foreignKey && e.IsForeignKey)
            //        .Select(e => e.LookupDisplayColumn)
            //        .FirstOrDefault();

            //if (displayColumn.IsBlank())
            //    displayColumn = GetLookupTableLabel(table, foreignKey);     //Use Table Label Column
            //if (displayColumn.IsBlank())
            //    displayColumn = GetPrimaryKey(lookupTable);                 //Use Primary Key of lookup table

            //if (!lookupTable.IsBlank())
            //    return lookupTable + "." + displayColumn;
            //else
            //    return string.Empty;
        }

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

        #region Primary Key
        public string GetTableAndPrimaryKey(string table)
        {
            var primaryKey = GetPrimaryKey(table);
            return !primaryKey.IsBlank() ? table + "." + primaryKey : string.Empty;
        }

        /// <summary>
        /// Get the primary key of a table
        /// </summary>
        /// <param name="table">Table we need primary key for</param>
        /// <returns>Primary Key</returns>
        public string GetPrimaryKey(string table)
        {
            var name = SchemaItem()
                .Where(e => e.TableName.Singularize() == table.Singularize() && e.IsPrimaryKey)
                .Select(e => e.ColumnName)
                .FirstOrDefault(); //?? string.Empty;
            return name;
        }
        public string GetPrimaryKeyType(string table) => GetPrimaryKeyRow(table).ColumnType;

        public ISchemaItem GetPrimaryKeyRow(string table)
        {
            var row = SchemaItem()
                .FirstOrDefault(e => e.TableName.Singularize() == table.Singularize() && e.IsPrimaryKey);
            return row;
        }
        #endregion


        public List<ISchemaItem> GetSortColumns(string table)
        {
            return SchemaItem()
                .Where(e => e.TableName.Singularize() == table.Singularize() && e.IsSortColumn).ToList();
        }

        public List<ISchemaItem> GetSearchColumns(string table)
        {
            return SchemaItem()
                .Where(e => e.TableName.Singularize() == table.Singularize() && e.IsSearchColumn).ToList();
        }

        /// <summary>
        /// Gets the related tables.
        /// </summary>
        /// <param name="table">The table.</param>
        /// <returns>List&lt;ISchemaItem&gt;.</returns>
        public List<ISchemaItem> GetRelatedTables(string table) => GetRelatedTables(SchemaItem().ToList(), table);

        public List<ISchemaItem> GetRelatedTables(List<ISchemaItem> schemaItem, string table)
        {
            return schemaItem
                .Where(e => !string.IsNullOrEmpty(e.ColumnType))
                .Where(e => (e.RelatedTable == table ))
                //.Where(e=> (IsTableEnabled(e.RelatedTable)) 
                .ToList();
        }

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

        public List<ISchemaItem> GetForeignKeysInTable(List<ISchemaItem> schemaItem, string table, bool allowPrimaryKeys = false)
        {
            return schemaItem
                .Where(e => !string.IsNullOrEmpty(e.ColumnType))
                .Where(e => (e.IsForeignKey) || (e.IsForeignKey && e.IsPrimaryKey == allowPrimaryKeys))
                //.Where(e => (e.IsPrimaryKey == allowPrimaryKeys))      //We need to exclude intermediate files with rows have both primary and foreign keys
                .Where(e => e.TableName == table)
                .ToList();
        }

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
        /// Returns the label of a table
        /// </summary>
        /// <param name="table">The table.</param>
        /// <returns>System.String.</returns>
        public string GetTable(string table)
        {
            var name = GetTables()
                           .Where(e => e.ColumnName.Singularize() == table.Singularize() && string.IsNullOrEmpty(e.ColumnType))
                           .Select(e => e.ColumnName)
                           .SingleOrDefault() ?? string.Empty;
            return name;
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
        /// <returns>ISchemaItem.</returns>
        public ISchemaItem GetTableObject(string table)
        {
            var name = GetTables()
                           .FirstOrDefault(e => (e.TableName == table && e.ParentId == 0));
            return name;
        }

        /// <summary>
        /// Return the tables in database as a List Collection
        /// </summary>
        /// <returns>Returned List of Table</returns>
        public List<ISchemaItem> GetTables()
        {
            return GetTables(_schemaItem);
            //return _schemaItem
            //    .Where(e => string.IsNullOrEmpty(e.ColumnType))
            //    .ToList();
        }

        /// <summary>
        /// Gets a list of tables.
        /// </summary>
        /// <param name="schemaItem">The schema item.</param>
        /// <returns>List&lt;ISchemaItem&gt;.</returns>
        public List<ISchemaItem> GetTables(List<ISchemaItem> schemaItem)
        {
            return schemaItem
                .Where(e => string.IsNullOrEmpty(e.ColumnType))
                .Where(e => e.IsChecked)
                .ToList();
        }

        public string GetRelatedColumnName(IHtmlColumns column)
        {
            if (column.IsForeignKey && (column.RelatedTable == column.TableName))
            {
                return column.TableName + "." + column.ColumnName + NavigationLabel();
            }
            else if (column.IsForeignKey)
            {
                return column.TableName + "." + GetLookupDisplayColumnWithPath(column.TableName, column.ColumnName);
            }
            else
            {
                return column.TableName + "." + column.ColumnName;
            }
        }

        public bool IsPrimaryInRelated(string table, string targetTable)
        {
            var result =
                SchemaItem()
                .Where(e => e.RelatedTable == table && e.TableName == targetTable && !string.IsNullOrEmpty(e.ColumnType))
                .FirstOrDefault(x => x.IsPrimaryKey && x.IsForeignKey);
            return result != null;
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
        /// <returns><c>true</c> if [is in list of tables] [the specified table]; otherwise, <c>false</c>.</returns>
        public bool IsInListOfTables(string table)
        {
            var foundTable = GetTables().FirstOrDefault(t => t.TableName == table && t.ParentId == 0);
            return foundTable != null;
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

        public bool IsTableEnabled(string name)
        {
            var result = false;
            var table = GetTableObject(name);
            if (table != null)
                result = true;
            return result;
        }

        /// <summary>
        /// Struct containing actions flags
        /// </summary>
        public struct Actions
        {
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

            /// <summary>
            /// The delete
            /// </summary>
            public bool Delete;
        }
    }
}
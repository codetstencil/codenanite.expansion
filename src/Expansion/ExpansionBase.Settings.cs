// ***********************************************************************
// Assembly         : ZeraSystems.CodeNanite.Expansion
// Author           : Ayodele-Desktop
// Created          : 01-01-2019
//
// Last Modified By : Ayodele-Desktop
// Last Modified On : 03-31-2020
// ***********************************************************************
// <copyright file="ExpansionBase.Settings.cs" company="ZeraSystems Inc.">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
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
        /// Retrieves the saved settings.
        /// </summary>
        /// <param name="identifier">The identifier.</param>
        /// <returns>System.String[].</returns>
        /// <summary>
        /// Retrieves the saved settings.
        /// </summary>
        /// <param name="identifier">The identifier.</param>
        /// <returns>System.String[].</returns>
        public string[] RetrieveSavedSettings(string identifier)
        {
            var settings = GetExpansionString(identifier);
            return settings?.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
        }

        /// <summary>
        /// Return a string containing the value for a setting
        /// </summary>
        /// <param name="setting">Setting we want to retrieve from</param>
        /// <param name="configLabel">Configuration label</param>
        /// <returns>System.String.</returns>
        /// <summary>
        /// Gets the settings value.
        /// </summary>
        /// <param name="setting">The setting.</param>
        /// <param name="configLabel">The configuration label.</param>
        /// <returns>System.String.</returns>
        public string GetSettingsValue(string setting, string configLabel)
        {
            var arraySettings = RetrieveSavedSettings(configLabel);
            return GetSettingsValue(setting, arraySettings);
        }

        /// <summary>
        /// Return a string containing the value for a setting
        /// </summary>
        /// <param name="setting">Setting we want to retrieve from</param>
        /// <param name="theArray">Passed array</param>
        /// <returns>System.String.</returns>
        /// <summary>
        /// Gets the settings value.
        /// </summary>
        /// <param name="setting">The setting.</param>
        /// <param name="theArray">The array.</param>
        /// <returns>System.String.</returns>
        public string GetSettingsValue(string setting, string[] theArray)
        {
            var element = Array.Find(theArray, e => e.StartsWith(setting, StringComparison.Ordinal));
            if (!string.IsNullOrEmpty(element))
            {
                var i = element.IndexOf('=') + 1;
                return element.Substring(i);
            }
            return null;
        }

        /// <summary>
        /// Return a string containing the value for a setting
        /// </summary>
        /// <param name="setting">Setting we want to retrieve from</param>
        /// <param name="configLabel">Configuration label</param>
        /// <param name="list">The list.</param>
        /// <summary>
        /// Gets the settings value.
        /// </summary>
        /// <param name="setting">The setting.</param>
        /// <param name="configLabel">The configuration label.</param>
        /// <param name="list">The list.</param>
        public void GetSettingsValue(string setting, string configLabel, ref List<string> list)
        {
            var settings = GetSettingsValue(setting, RetrieveSavedSettings(configLabel));
            if (!settings.IsBlank())
                list = settings.Split(',').ToList();
        }

        /// <summary>
        /// Gets table and columns string.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="startPos">The start position.</param>
        /// <returns>System.String.</returns>
        /// <summary>
        /// Gets the table and columns string.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="startPos">The start position.</param>
        /// <returns>System.String.</returns>
        public string GetTableAndColumnsString(string text, int startPos)
        {
            if (text.IsBlank() || startPos > text.Length) return string.Empty;
            var endPos = text.IndexOf("[", startPos + 1, StringComparison.Ordinal);
            if (endPos == -1)
                endPos = text.Length;
            return text.Substring(startPos, endPos - startPos);
        }

        /// <summary>
        /// Gets tables and columns from settings.
        /// </summary>
        /// <param name="text">The text.</param>
        public void GetTablesAndColumnsFromSettings(string text)
        {
            if (text.IsBlank()) return;
            var i = 0;
            while ((i = text.IndexOf('[', i)) != -1)
            {
                var str = GetTableAndColumnsString(text, i);
                i++;
            }
        }

        /// <summary>
        /// Gets a list of columns from settings.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="table">The table.</param>
        /// <returns>List&lt;System.String&gt;.</returns>
        public List<string> GetColumnsFromSettings(string text, string table)
        {
            if (text.IsBlank() || table.IsBlank()) return null;
            table = "[" + table + "]";
            if (!text.Contains(table)) return null;

            var pos = text.IndexOf(table, StringComparison.Ordinal);
            if (pos == -1) return null;

            var colStartPos = pos + table.Length;
            var end = text.IndexOf(Environment.NewLine, colStartPos + 1, StringComparison.Ordinal);
            var columns = text.Substring(colStartPos, end - colStartPos).StripCarriage().Split(',').ToList();
            return columns;
        }

        /// <summary>
        /// Gets the columns from schema items.
        /// </summary>
        /// <param name="columns">The columns.</param>
        /// <param name="table">The table.</param>
        /// <returns>IEnumerable&lt;ISchemaItem&gt;.</returns>
        public IEnumerable<ISchemaItem> GetColumnsFromSchemaItems(List<string> columns, string table)
        {
            var result = SchemaItem()
                .Where(a => columns.Any(x => x.Equals(a.ColumnName) && a.TableName == table));

            //var result = from firstItem in columns
            //    join seconditem in SchemaItem()
            //        on firstItem.Equals(seconditem..)

            //        .Where(p => columns.Any(p2 => p2.Equals(p.ColumnName) ));
            return result;
        }
    }
}
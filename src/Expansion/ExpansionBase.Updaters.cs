// ***********************************************************************
// Assembly         : ZeraSystems.CodeNanite.Expansion
// Author           : Ayodele-Desktop
// Created          : 12-07-2018
//
// Last Modified By : Ayodele-Desktop
// Last Modified On : 01-02-2019
// ***********************************************************************
// <copyright file="ExpansionBase.Updaters.cs" company="ZeraSystems Inc.">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Linq;
using ZeraSystems.CodeNanite.DevExtreme;
using ZeraSystems.CodeStencil.Contracts;

namespace ZeraSystems.CodeNanite.Expansion
{
    /// <summary>
    /// Class ExpansionBase.
    /// </summary>
    public abstract partial class ExpansionBase
    {
        #region Expander

        /// <summary>
        /// Updates the expander.
        /// </summary>
        /// <param name="expander">The expander.</param>
        public void UpdateExpander(IExpander expander)
        {
            var row = _expander
                .Where((x => x.ExpansionLabel == expander.ExpansionLabel))
                .FirstOrDefault();

            if (row != null && row.UpdatedByNanite) //We already have a matching item
                _expander.Remove(row);

            expander.UpdatedByNanite = true;
            _expander.Add(expander);
        }

        /// <summary>
        /// Expanders'  updater.
        /// </summary>
        /// <param name="expansionString">The expansion string.</param>
        /// <param name="expansionLabel">The expansion label.</param>
        public void ExpanderUpdater(string expansionString, string expansionLabel) => ExpanderUpdater(expansionString, expansionLabel, 0, false, string.Empty);

        /// <summary>
        /// Expanders' updater.
        /// </summary>
        /// <param name="expansionString">if set to <c>true</c> [expansion string].</param>
        /// <param name="expansionLabel">The expansion label.</param>
        public void ExpanderUpdater(bool expansionString, string expansionLabel)
        {
            ExpanderUpdater(Convert.ToInt32(expansionString).ToString(), expansionLabel, 0, false, string.Empty);
        }

        /// <summary>
        /// Expanders' updater.
        /// </summary>
        /// <param name="expansionString">The expansion string.</param>
        /// <param name="expansionLabel">The expansion label.</param>
        /// <param name="expansionValue">The expansion value.</param>
        /// <param name="isMultiple">if set to <c>true</c> [is multiple].</param>
        /// <param name="delimiter">The delimiter.</param>
        public void ExpanderUpdater(string expansionString, string expansionLabel, int expansionValue, bool isMultiple = false, string delimiter = "")
        {
            var expander = new ExpanderObject
            {
                ExpansionLabel = expansionLabel,
                ExpansionString = expansionString,
                ExpansionValue = expansionValue,
                IsMultiple = isMultiple,
                Delimiter = delimiter
            };
            UpdateExpander(expander);
        }

        #endregion Expander

        #region SchemaItem

        /// <summary>
        /// SchemaItem updater.
        /// </summary>
        /// <param name="schemaItem">The schema item.</param>
        /// <returns>ISchemaItem.</returns>
        public ISchemaItem SchemaItemUpdater(ISchemaItem schemaItem) => UpdateSchemaItem(schemaItem);

        /// <summary>
        /// Updates the schema item.
        /// </summary>
        /// <param name="schemaItem">The schema item.</param>
        /// <returns>ISchemaItem.</returns>
        public ISchemaItem UpdateSchemaItem(ISchemaItem schemaItem)
        {
            try
            {
                var row = _schemaItem
                    .FirstOrDefault(x => x.TableName == schemaItem.TableName && x.ColumnName == schemaItem.ColumnName);

                //if (row != null && row.UpdatedByNanite)    //We already have a matching item
                if (row != null)                //We already have a matching item
                    _schemaItem.Remove(row);
                schemaItem.IsUpdatedByNanite = true;
                schemaItem.MaxLength = schemaItem.SchemaItemId;    //testing

                _schemaItem.Add(schemaItem);
                return row;
            }
            catch (Exception)
            {
            }
            return null;
        }

        #endregion SchemaItem
    }
}
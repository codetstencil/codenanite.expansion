// ***********************************************************************
// Assembly         : ZeraSystems.CodeNanite.Expansion
// Author           : Ayodele-Desktop
// Created          : 12-29-2018
//
// Last Modified By : Ayodele-Desktop
// Last Modified On : 07-06-2020
// ***********************************************************************
// <copyright file="General.cs" company="ZeraSystems Inc.">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Collections.Generic;
using System.Windows.Forms;
using ZeraSystems.CodeStencil.Contracts;

namespace ZeraSystems.CodeNanite.Expansion
{
    /// <summary>
    /// Class General.
    /// </summary>
    public static class General
    {
        /// <summary>
        /// Loads a form that allows you to select Tables and Columns that can be used in code generation.
        /// The result of the selection is in the property - SchemaString
        /// </summary>
        /// <param name="schemaItem">This is the schema passed along from CodeStencil (See Global Schema).</param>
        /// <param name="expander">This list contains the Expander passed from CodeStencil.</param>
        /// <param name="entitiesString">String containing Tables/Columns</param>
        /// <param name="text">This is text that can be displayed as the title of the form</param>
        /// <param name="comments">Comments or Notes</param>
        /// <param name="url">Passed URL/Hyperlink.</param>
        /// <returns>Selected tables and columns in the format: [table1] column1, column2</returns>
        public static string SelectEntitiesForm(List<ISchemaItem> schemaItem, List<IExpander> expander, string entitiesString, string text, string comments = null, string url=null)
        {
            var frm = new FrmSelectEntities(schemaItem, expander, entitiesString);
            frm.Text = text;
            frm.Url = url;
            if (frm.ShowDialog() == DialogResult.OK)
                return frm.SchemaString;
            else
                return null;
        }

    }
}
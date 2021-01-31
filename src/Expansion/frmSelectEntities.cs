// ***********************************************************************
// Assembly         : ZeraSystems.CodeNanite.Expansion
// Author           : Ayodele-Desktop
// Created          : 12-09-2018
//
// Last Modified By : Ayodele-Desktop
// Last Modified On : 11-15-2019
// ***********************************************************************
// <copyright file="frmSelectEntities.cs" company="ZeraSystems Inc.">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using ZeraSystems.CodeStencil.Contracts;

namespace ZeraSystems.CodeNanite.Expansion
{
    /// <summary>
    /// Class frmSelectEntities.
    /// Implements the <see cref="System.Windows.Forms.Form" />
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Form" />
    public partial class FrmSelectEntities : Form
    {
        #region Fields

        /// <summary>
        /// The utility
        /// </summary>
        private readonly BaseHelper _util = new BaseHelper();

        /// <summary>
        /// The selected object string
        /// </summary>
        private readonly StringBuilder _selectedObjectString = new StringBuilder();

        /// <summary>
        /// The tables
        /// </summary>
        private List<ISchemaItem> _tables;

        /// <summary>
        /// The columns
        /// </summary>
        private List<ISchemaItem> _columns;

        /// <summary>
        /// The schema item
        /// </summary>
        private readonly List<ISchemaItem> _schemaItem;

        /// <summary>
        /// The schema item copy
        /// </summary>
        private readonly List<ISchemaItem> _schemaItemCopy;

        /// <summary>
        /// The expander
        /// </summary>
        private List<IExpander> _expander;
        /// <summary>
        /// The entities string
        /// </summary>
        private string _entitiesString;

        /// <summary>
        /// Contains the result of the selected tables and columns in the form:
        /// [Table1]
        /// column1, column2, ccolumn3
        /// </summary>
        /// <value>The schema string.</value>
        public string SchemaString { get; private set; }

        #endregion Fields

        /// <summary>
        /// Gets or sets the URL.
        /// </summary>
        /// <value>The URL.</value>
        public string Url { get; set; }
        /// <summary>
        /// Gets or sets the comments.
        /// </summary>
        /// <value>The comments.</value>
        public string Comments { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="FrmSelectEntities" /> class.
        /// </summary>
        public FrmSelectEntities() => InitializeComponent();

        /// <summary>
        /// Initializes a new instance of the <see cref="FrmSelectEntities" /> class.
        /// </summary>
        /// <param name="schemaItem">The schema item.</param>
        /// <param name="expander">The expander.</param>
        /// <param name="entitiesString">String containing the Tables/Columns</param>
        public FrmSelectEntities(List<ISchemaItem> schemaItem, List<IExpander> expander, string entitiesString) : this()
        {
            _schemaItem = schemaItem;
            _schemaItemCopy = schemaItem.DeepClone();
            _expander = expander;
            _entitiesString = entitiesString;

            _util.Initializer(_schemaItemCopy, expander);
            FillTables(true);
            linkLabel.Text = Url;
            richTextBox.Text = Comments;
        }

        /// <summary>
        /// Handles the Load event of the FrmSelectEntities control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void FrmSelectEntities_Load(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the checkedListBoxTables control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
        private void CheckedListBoxTables_SelectedIndexChanged(object sender, System.EventArgs e) => SelectTableRow();

        /// <summary>
        /// Handles the ItemCheck event of the checkedListBoxColumns control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ItemCheckEventArgs" /> instance containing the event data.</param>
        private void CheckedListBoxColumns_ItemCheck(object sender, ItemCheckEventArgs e) => CheckColumn(sender, e);

        /// <summary>
        /// Handles the ItemCheck event of the checkedListBoxTables control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ItemCheckEventArgs" /> instance containing the event data.</param>
        private void CheckedListBoxTables_ItemCheck(object sender, ItemCheckEventArgs e) => CheckTable(e);

        /// <summary>
        /// Handles the CheckedChanged event of the chkTables control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void ChkTables_CheckedChanged(object sender, EventArgs e) => CheckUncheckAll(chkTables.Checked);

        /// <summary>
        /// Handles the CheckedChanged event of the chkColumns control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void ChkColumns_CheckedChanged(object sender, EventArgs e) => CheckUncheckAll(chkColumns.Checked, _selectedTable);

        /// <summary>
        /// Handles the Click event of the BtnOk control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void BtnOk_Click(object sender, EventArgs e) => SaveEntities();
    }

    /// <summary>
    /// Class BaseHelper.
    /// Implements the <see cref="ZeraSystems.CodeNanite.Expansion.ExpansionBase" />
    /// </summary>
    /// <seealso cref="ZeraSystems.CodeNanite.Expansion.ExpansionBase" />
    public class BaseHelper : ExpansionBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExpansionBase" /> class.
        /// </summary>
        public BaseHelper()
        {
            //Initializer(schemaItem, Expander);
        }
    }
}
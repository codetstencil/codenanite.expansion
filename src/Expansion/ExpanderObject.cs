// ***********************************************************************
// Assembly         : ZeraSystems.CodeNanite.Expansion
// Author           : Ayodele-Desktop
// Created          : 12-07-2018
//
// Last Modified By : Ayodele-Desktop
// Last Modified On : 05-21-2020
// ***********************************************************************
// <copyright file="ExpanderObject.cs" company="ZeraSystems Inc.">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using ZeraSystems.CodeStencil.Contracts;

namespace ZeraSystems.CodeNanite.Expansion
{
    /// <summary>
    /// Class ExpanderObject.
    /// Implements the <see cref="ZeraSystems.CodeStencil.Contracts.IExpander" />
    /// </summary>
    /// <seealso cref="ZeraSystems.CodeStencil.Contracts.IExpander" />
    /// <summary>
    /// Class ExpanderObject.
    /// Implements the <see cref="ZeraSystems.CodeStencil.Contracts.IExpander" />
    /// </summary>
    /// <seealso cref="ZeraSystems.CodeStencil.Contracts.IExpander" />
    internal class ExpanderObject : IExpander
    {
        /// <summary>
        /// Gets or sets the code nanite description.
        /// </summary>
        /// <value>The code nanite description.</value>
        /// <summary>
        /// Gets or sets the code nanite description.
        /// </summary>
        /// <value>The code nanite description.</value>
        public string CodeNaniteDescription { get; set; }
        /// <summary>
        /// Gets or sets the code nanite label.
        /// </summary>
        /// <value>The code nanite label.</value>
        /// <summary>
        /// Gets or sets the code nanite label.
        /// </summary>
        /// <value>The code nanite label.</value>
        public string CodeNaniteLabel { get; set; }
        /// <summary>
        /// Gets or sets the code nanite namespace.
        /// </summary>
        /// <value>The code nanite namespace.</value>
        /// <summary>
        /// Gets or sets the code nanite namespace.
        /// </summary>
        /// <value>The code nanite namespace.</value>
        public string CodeNaniteNamespace { get; set; }
        /// <summary>
        /// Gets or sets the code nanite original.
        /// </summary>
        /// <value>The code nanite original.</value>
        /// <summary>
        /// Gets or sets the code nanite original.
        /// </summary>
        /// <value>The code nanite original.</value>
        public string CodeNaniteOriginal { get; set; }
        /// <summary>
        /// Gets or sets the delimiter.
        /// </summary>
        /// <value>The delimiter.</value>
        /// <summary>
        /// Gets or sets the delimiter.
        /// </summary>
        /// <value>The delimiter.</value>
        public string Delimiter { get; set; }
        /// <summary>
        /// Gets or sets the expander identifier.
        /// </summary>
        /// <value>The expander identifier.</value>
        /// <summary>
        /// Gets or sets the expander identifier.
        /// </summary>
        /// <value>The expander identifier.</value>
        public int ExpanderID { get; set; }
        /// <summary>
        /// Gets or sets the expansion label.
        /// </summary>
        /// <value>The expansion label.</value>
        /// <summary>
        /// Gets or sets the expansion label.
        /// </summary>
        /// <value>The expansion label.</value>
        public string ExpansionLabel { get; set; }
        /// <summary>
        /// Gets or sets the expansion string.
        /// </summary>
        /// <value>The expansion string.</value>
        /// <summary>
        /// Gets or sets the expansion string.
        /// </summary>
        /// <value>The expansion string.</value>
        public string ExpansionString { get; set; }
        /// <summary>
        /// Gets or sets the expansion value.
        /// </summary>
        /// <value>The expansion value.</value>
        /// <summary>
        /// Gets or sets the expansion value.
        /// </summary>
        /// <value>The expansion value.</value>
        public int ExpansionValue { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this instance is disabled.
        /// </summary>
        /// <value><c>true</c> if this instance is disabled; otherwise, <c>false</c>.</value>
        public bool IsDisabled { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this instance is multiple.
        /// </summary>
        /// <value><c>true</c> if this instance is multiple; otherwise, <c>false</c>.</value>
        public bool IsMultiple { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this instance is schema.
        /// </summary>
        /// <value><c>true</c> if this instance is schema; otherwise, <c>false</c>.</value>
        public bool IsSchema { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this instance is system label.
        /// </summary>
        /// <value><c>true</c> if this instance is system label; otherwise, <c>false</c>.</value>
        public bool IsSystemLabel { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether [updated by nanite].
        /// </summary>
        /// <value><c>true</c> if [updated by nanite]; otherwise, <c>false</c>.</value>
        public bool UpdatedByNanite { get; set; }

        /// <summary>
        /// Gets or sets the help URL.
        /// </summary>
        /// <value>The help URL.</value>
        public string HelpUrl { get; set; }
    }
}

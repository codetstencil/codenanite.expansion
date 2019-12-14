namespace ZeraSystems.CodeStencil.Contracts
{
    public interface IExpander
    {
        int ExpanderID { get; set; }
        /// <summary>
        /// Label indicating the string to be expanded, e.g. HORIZ_NAME. This is 
        /// the label inserted in the code in the tree node
        /// </summary>
        string ExpansionLabel { get; set; }
        string ExpansionString { get; set; }
        /// <summary>
        /// Value to expand. Can be incremented
        /// </summary>
        int ExpansionValue { get; set; }
        bool IsMultiple { get; set; }
        /// <summary>
        /// Indicates a specific Delimiter within the expansion string. The default is ","
        /// </summary>
        string Delimiter { get; set; }
        bool IsSystemLabel { get;  set; }
        /// <summary>
        /// Indicates that this nanite performs a schema-related function, hence SchemaColumns
        /// table should not be empty.
        /// </summary>
        bool IsSchema { get;  set; }
        bool IsDisabled { get; set; }
        /// <summary>
        /// This is the name of the method that is called in the external Code Nanite
        /// </summary>
        string CodeNaniteLabel { get; set; }
        /// <summary>
        /// The namespace the nanite belongs to
        /// </summary>
        string CodeNaniteNamespace { get; set; }
        /// <summary>
        /// Original method name including the namespace, 
        /// e.g. "ZeraSystems.CodeStencil.CodeNanites.CurrentTable"
        /// </summary>
        string CodeNaniteOriginal { get; set; }
        string CodeNaniteDescription { get; set; }
        /// <summary>
        /// Indicates That a Code Nanite updated this
        /// </summary>
        bool UpdatedByNanite { get; set; }
    }
}


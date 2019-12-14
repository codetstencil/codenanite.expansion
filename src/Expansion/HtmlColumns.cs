
namespace ZeraSystems.CodeNanite.Expansion
{
    public class HtmlColumns : IHtmlColumns
    {
        public HtmlColumns(string heading, string fieldString, bool isPrimary, bool isForeignKey,
            string tableName,
            string columnName,
            string relatedTable,
            bool allowSort = false, bool allowSearch = false)
        {
            Heading = heading;
            FieldString = fieldString;
            IsPrimaryKey = isPrimary;
            IsForeignKey = isForeignKey;
            TableName = tableName;
            ColumnName = columnName;
            RelatedTable = relatedTable;
            AllowSearch = allowSearch;
            AllowSort = allowSort;
        }
        public string Heading { get; set; }
        public string FieldString { get; set; }
        public bool IsPrimaryKey { get; set; }
        public bool IsForeignKey { get; set; }
        public string TableName { get; set; }
        public string ColumnName { get; set; }
        public string RelatedTable { get; set; }
        public bool AllowSort { get; set; }
        public bool AllowSearch { get; set; }
    }
}
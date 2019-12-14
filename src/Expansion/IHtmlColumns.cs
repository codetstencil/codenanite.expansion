using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeraSystems.CodeNanite.Expansion
{
    public interface IHtmlColumns
    {
        string Heading { get; set; }
        string FieldString { get; set; }
        bool IsPrimaryKey { get; set; }
        bool IsForeignKey { get; set; }
        string TableName { get; set; }
        string ColumnName { get; set; }
        string RelatedTable { get; set; }
        
        bool AllowSort { get; set; }
        bool AllowSearch { get; set; }

    }
}

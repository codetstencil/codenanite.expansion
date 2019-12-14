using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ZeraSystems.CodeStencil.Contracts
{
    public abstract class ExpansionBase
    {
        public StringBuilder ExpandedText = new StringBuilder();

        private List<ISchemaItem> _schemaItem;
        private List<IExpander> _expander;
        public const string StrLineFeed = "\n";
        public const string StrCarriageReturn = "\r";

        //public string Name { get { return name; } }

        //protected ExpansionBase(List<ISchemaItem> schemaItem, List<IExpander> expander)
        //{
        //    this._schemaItem = schemaItem;
        //    this._expander = expander;
        //}

        //protected ExpansionBase()
        //{
        //    //throw new NotImplementedException();
        //    //Initializer();
        //}

        public void Initializer(List<ISchemaItem> schemaItem, List<IExpander> expander)
        {
            _schemaItem = schemaItem;
            _expander = expander;
        }

        public string GetTable(string table)
        {

            var name = GetTables()
                           .Where(e => e.ColumnName == table && string.IsNullOrEmpty(e.ColumnType))
                           .Select(e => e.ColumnName)
                           .SingleOrDefault() ?? string.Empty;

            //var name = GetTables().FirstOrDefault(e => e.ColumnName == table && string.IsNullOrEmpty(e.ColumnType))
            //    .Select(e => e.ColumnName).ToString();
            return name;
        }
        public List<ISchemaItem> GetTables()
        {
            return _schemaItem
                .Where(e => string.IsNullOrEmpty(e.ColumnType))
                .ToList();
        }

        public List<ISchemaItem> GetColumns(string table)
        {
            return _schemaItem
                .Where(e => !string.IsNullOrEmpty(e.ColumnType))
                .Where(e=>e.TableName == table)
                .ToList();
        }

        public virtual void AppendText()
        {
            ExpandedText.Clear();
        }
        public virtual void AppendText(string text, string linefeed = StrLineFeed)
        {
            ExpandedText.Append(text + linefeed);
        }

        public void IsTableInDatabase(string name)
        {
            var isTable = _schemaItem.Where(s => s.ColumnName == name)
                .Where(s => string.IsNullOrEmpty(s.ColumnType));
        }


        #region Project Settings
        public string GetExpansionString(string label)
        {
            var text = _expander.Where(e => e.ExpansionLabel == label).Select(x => x.ExpansionString);
            return text.FirstOrDefault();
        }
        public string GetDefaultNameSpace() { return GetExpansionString("NAMESPACE"); }
        public string GetOrganizationLabel() { return GetExpansionString("ORGANIZATION_LABEL"); }
        public string GetOrganizationName() { return GetExpansionString("ORGANIZATION_NAME"); }
        public string GetProjectName() { return GetExpansionString("PROJECT_NAME"); }
        public string GetOutputFolder() { return GetExpansionString("OUTPUT_FOLDER"); }
        #endregion





    }
}

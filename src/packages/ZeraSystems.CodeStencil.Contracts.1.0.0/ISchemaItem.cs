using System;

namespace ZeraSystems.CodeStencil.Contracts
{
    public interface ISchemaItem
    {
        int SchemaItemId { get; set; }
        int ParentId { get; set; }
        string TableName { get; set; }
        string ColumnName { get; set; }
        string ColumnType { get; set; }
        int ColumnSize { get; set; }
        string ColumnAttribute { get; set; }
        int NumericPrecision { get; set; }
        bool AllowDbNull { get; set; }
        bool IsUnique { get; set; }
        bool IsPrimaryKey { get; set; }
        bool IsAutoIncrement { get; set; }
        bool IsChecked { get; set; }
        bool IsUpdatedByNanite { get; set; }


        bool IsRequired { get; set; }
         bool IsForeignKey { get; set; }
         string Schema { get; set; }
         int MaxLength { get; set; }
         string MappedColumnName { get; set; }
         string ComputedColumnSql { get; set; }  //SQL fragment 
         string PrimaryKeyName { get; set; }
         bool HasSequence { get; set; }
         string DefaultValue { get; set; }  //will have to cast to the type
         string DefaultValueSql { get; set; }  //SQL fragment that is used to calculate the default value.
         string IndexName { get; set; }
         string ConstraintName { get; set; }
         string AlternateKeyName { get; set; }
         string RelatedTable { get; set; }
         string Relationship { get; set; }
        string ColumnLabel { get; set; }
        bool IsTableLabel { get; set; }
        bool IsCalculatedColumn { get; set; }
        string CalculatedColumn { get; set; }
        string OriginalName { get; set; }
        int ColumnSequence { get; set; }
        string LookupColumn { get; set; }
        string LookupDisplayColumn { get; set; }
        bool CanGet { get; set; }
        bool CanPost { get; set; }
        bool CanPut { get; set; }
        bool CanDelete { get; set; }
        int LinkedRow { get; set; }
        bool IsSortColumn { get; set; }
        bool IsSearchColumn { get; set; }

    }

}

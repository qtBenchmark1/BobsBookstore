using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Linq;

namespace Bookstore.Data
{
    public static class SchemaMapper
    {   
        private class DatabaseMapping
        {   
            public string Source_Database { get; set; }
            public string Target_Database { get; set; }
            public string Source_Schema { get; set; }
            public string Target_Schema { get; set; }
        }

        private class ColumnMapping
        {   
            public string Source_Column { get; set; }
            public string Target_Column { get; set; }
        }

        private class TableMapping
        {   
            public string Source_Table { get; set; }
            public string Target_Table { get; set; }
            public List<ColumnMapping> Column_Mappings { get; set; }
        }

        private class SchemaMappingFile
        {   
            public DatabaseMapping Database_Mapping { get; set; }
            public List<TableMapping> Table_Mappings { get; set; }
        }

        private static readonly string _targetSchema = "public";
        private static readonly Dictionary<string, string> _tableNameMappings = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            { "Address", "address" },
            { "Book", "book" },
            { "Customer", "customer" },
            { "Order", "order" },
            { "ShoppingCart", "shopping_cart" },
            { "ShoppingCartItem", "shopping_cart_item" },
            { "OrderItem", "order_item" },
            { "Offer", "offer" },
            { "ReferenceData", "reference_data" }
        };

        public static string GetTargetTableName(string sourceTableName)
        {   
            if (_tableNameMappings.TryGetValue(sourceTableName, out string targetTable))
            {
                return targetTable;
            }
            
            return sourceTableName.ToLower();
        }
        
        public static string GetTargetSchemaName()
        {   
            return _targetSchema;
        }
    }
}

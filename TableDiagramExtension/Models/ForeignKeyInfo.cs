using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TableDiagramExtension.Models
{
    public class ForeignKeyInfo
    {
        public string ForeignKeyName { get; set; }
        public string ParentTableName { get; set; }
        public string ParentColumnName { get; set; }
        public string ReferencedTableName { get; set; }
        public string ReferencedColumnName { get; set; }
    }

}

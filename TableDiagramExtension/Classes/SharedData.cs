using Microsoft.SqlServer.Management.Common;
//using Microsoft.SqlServer.Management.UI.VSIntegration.ObjectExplorer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TableDiagramExtension.Classes
{
    public class SharedData
    {
        public string SelectedServerName { get; set; }
        public INodeInformation SelectedNode { get; set; }
        public bool IsEnabled { get; set; } = false;
        public bool IsTable { get; internal set; }
        public string TableName { get; internal set; }
        //public SqlOlapConnectionInfoBase SqlOlapConnectionInfoBase { get; internal set; }
    }
}

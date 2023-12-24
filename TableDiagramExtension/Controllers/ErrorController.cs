using Microsoft.Extensions.Logging;
using Syncfusion.Windows.Forms.Diagram;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TableDiagramExtension.Interfaces;
using TableDiagramExtension.Resources;

namespace TableDiagramExtension.Controllers
{
    internal class ErrorController : IErrorController
    {
        public ErrorController()
        {
        }

        public void DisplayErrorMessage(string message)
        {
            MessageBox.Show(String.Format(TextStrings.StandardErrorMessage, message), TextStrings.StandardErrorMessageTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            Console.WriteLine(message);
        }

    }
}

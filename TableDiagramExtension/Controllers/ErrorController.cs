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
        ILogger<ErrorController> _logger;

        public ErrorController()
        {
        }

        public void DisplayErrorMessage(string message)
        {
            MessageBox.Show(String.Format(TextStrings.StandardErrorMessage, message), TextStrings.StandardErrorMessageTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            //_logger.LogError(message);
            Console.WriteLine(message);
        }

    }
}

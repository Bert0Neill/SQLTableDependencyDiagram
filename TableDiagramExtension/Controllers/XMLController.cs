using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using TableDiagramExtension.Interfaces;

namespace TableDiagramExtension.Controllers
{
    internal class XMLController : IXMLController
    {
        public XMLController()
        {

        }

        

        private string GenerateXmlFilter(List<string> acceptableTables)
        {
            //const string BEGIN_FILTER = "//Table[not(@TableName='Customer' or @TableName='Address')]";
            const string END_FILTER = "or @TableName='{0}'";
            const string BEGIN_FILTER = "//Table[not(@TableName='{0} {1}')]";

            string buildFilter = string.Empty;
            string extraFilter = string.Empty;
            string initialTable = string.Empty;

            for (int loop = 0; loop < acceptableTables.Count(); loop++)
            {
                if (loop == 0) initialTable = acceptableTables[loop]; //String.Format(BEGIN_FILTER, acceptableTables[loop]);
                else
                {
                    extraFilter += String.Format(END_FILTER, acceptableTables[loop]);
                }
            }

            buildFilter += String.Format(BEGIN_FILTER, initialTable, extraFilter);

            return buildFilter;
        }

        public string RenameNodes(string xmlString)
        {
            try
            {
                // Load the XML document
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(@"<?xml version=""1.0"" encoding=""utf-8"" ?>
                            <Tables>
                                <Table Schema=""dbo"" TableName=""ErrorLog"" ColumnID=""245575913"">
                                    <Column Name=""ErrorLogID"" ColumnId=""1"" IsPrimaryKey=""1"" />
                                    <Column Name=""ErrorTime"" ColumnId=""2"" IsPrimaryKey=""0"" />
                                    <Column Name=""UserName"" ColumnId=""3"" IsPrimaryKey=""0"" />
                                    <Column Name=""ErrorNumber"" ColumnId=""4"" IsPrimaryKey=""0"" />
                                    <Column Name=""ErrorSeverity"" ColumnId=""5"" IsPrimaryKey=""0"" />
                                    <Column Name=""ErrorState"" ColumnId=""6"" IsPrimaryKey=""0"" />
                                    <Column Name=""ErrorProcedure"" ColumnId=""7"" IsPrimaryKey=""0"" />
                                    <Column Name=""ErrorLine"" ColumnId=""8"" IsPrimaryKey=""0"" />
                                    <Column Name=""ErrorMessage"" ColumnId=""9"" IsPrimaryKey=""0"" />
                                </Table>
                                <Table Schema=""dbo"" TableName=""BuildVersion"" ColumnID=""325576198"">
                                    <Column Name=""SystemInformationID"" ColumnId=""1"" IsPrimaryKey=""0"" />
                                    <Column Name=""Database Version"" ColumnId=""2"" IsPrimaryKey=""0"" />
                                    <Column Name=""VersionDate"" ColumnId=""3"" IsPrimaryKey=""0"" />
                                    <Column Name=""ModifiedDate"" ColumnId=""4"" IsPrimaryKey=""0"" />
                                </Table>
                            </Tables>");

                // Select all Table nodes
                XmlNodeList tableNodes = xmlDoc.SelectNodes("//Table");

                // Rename the ColumnID attribute to TableID for each Table node
                foreach (XmlNode tableNode in tableNodes)
                {
                    XmlAttribute columnIdAttribute = tableNode.Attributes["ColumnID"];

                    if (columnIdAttribute != null)
                    {
                        // Create a new attribute with the same value as the old attribute
                        XmlAttribute tableIdAttribute = xmlDoc.CreateAttribute("TableID");
                        tableIdAttribute.Value = columnIdAttribute.Value;

                        // Add the new attribute to the Table node
                        tableNode.Attributes.Append(tableIdAttribute);

                        // Remove the old attribute
                        tableNode.Attributes.RemoveNamedItem("ColumnID");
                    }
                }

                /*
                 * <?xml version="1.0" encoding="UTF-8"?>
<root oldAttribute="oldValue">
  <element1>value1</element1>
  <element2>value2</element2>
</root>

// Load the XML document
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load("path/to/your/xml/example.xml");

            // Specify the old and new attribute names
            string oldAttributeName = "oldAttribute";
            string newAttributeName = "newAttribute";

            // Get the root node
            XmlNode rootNode = xmlDoc.DocumentElement;

            // Check if the old attribute exists
            if (rootNode.Attributes[oldAttributeName] != null)
            {
                // Create a new attribute with the same value as the old attribute
                XmlAttribute newAttribute = xmlDoc.CreateAttribute(newAttributeName);
                newAttribute.Value = rootNode.Attributes[oldAttributeName].Value;

                // Add the new attribute to the root node
                rootNode.Attributes.Append(newAttribute);

                // Remove the old attribute
                rootNode.Attributes.RemoveNamedItem(oldAttributeName);            
            }
                 */

                return string.Empty;

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return string.Empty;
            }
        }

        public XmlDocument LoadXMLString(string xmlString)
        {
            // Create a new XmlDocument
            XmlDocument xmlDoc = new XmlDocument();

            try
            {
                // Load the XML string into the XmlDocument
                xmlDoc.LoadXml(xmlString);
                return xmlDoc;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return xmlDoc;
            }
        }

        public string GenerateXmlDocFromDBData(string xmlString)
        {
            // Create an XmlDocument
            XmlDocument xmlDoc = new XmlDocument();

            // Create the root element <dataroot>
            XmlElement datarootElement = xmlDoc.CreateElement("dataroot");
            datarootElement.SetAttribute("xmlns:od", "urn:schemas-microsoft-com:officedata");
            datarootElement.SetAttribute("xmlns:xsi", "http://www.w3.org/2001/XMLSchema-instance");
            //datarootElement.SetAttribute("xsi:noNamespaceSchemaLocation", "Employees.xsd");
            datarootElement.SetAttribute("generated", "2005-01-18T15:03:23");

            // Create the child element <Employee>
            XmlElement employeeElement = xmlDoc.CreateElement("Employee");

            // Array of column names
            string[] columnNames = { "emp_id", "FName", "minit", "lname", "job_id", "job_lvl", "pub_id", "hire_date" };

            // Add individual elements for each column
            foreach (var columnName in columnNames)
            {
                XmlElement columnElement = xmlDoc.CreateElement(columnName);
                columnElement.InnerText = columnName;
                employeeElement.AppendChild(columnElement);
            }

            // loop collection again and create element for PK's
            // Add <PrimaryKey> element
            XmlElement primaryKeyElement = xmlDoc.CreateElement("PrimaryKey");
            primaryKeyElement.InnerText = "emp_id";
            employeeElement.AppendChild(primaryKeyElement);

            // Add <ForeignKey> element (empty in this example, customize as needed)
            XmlElement foreignKeyElement = xmlDoc.CreateElement("ForeignKey");
            employeeElement.AppendChild(foreignKeyElement);

            // Add <TableID> element
            XmlElement tableIdElement = xmlDoc.CreateElement("TableID");
            tableIdElement.InnerText = "123"; // Replace with your actual TableID
            employeeElement.AppendChild(tableIdElement);

            // Append the <Employee> element to the <dataroot> element
            datarootElement.AppendChild(employeeElement);

            // Append the root element to the XmlDocument
            xmlDoc.AppendChild(employeeElement);

            return string.Empty;
        }
    }
}

using Microsoft.SqlServer.Management.Smo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
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
            try
            {
                // Create an XmlDocument
                XmlDocument xmlDocFormatted = new XmlDocument();

                // Create the root element <dataroot>
                XmlElement datarootElement = xmlDocFormatted.CreateElement("dataroot");
                //datarootElement.SetAttribute("xmlns:od", "urn:schemas-microsoft-com:officedata");
                //datarootElement.SetAttribute("xmlns:xsi", "http://www.w3.org/2001/XMLSchema-instance");                
                //datarootElement.SetAttribute("generated", "2005-01-18T15:03:23");
                
                xmlDocFormatted.AppendChild(datarootElement);

                // craete xmlDoc for parsing current format - to retrieve data
                XDocument xmlDoc = XDocument.Parse(xmlString);

                // Accessing Table elements and attributes
                var tableElements = xmlDoc.Descendants("Table");
                foreach (var tableElement in tableElements)
                {
                    //string schema = tableElement.Attribute("Schema").Value;
                    string tableName = tableElement.Attribute("Name").Value;
                    string tableId = tableElement.Attribute("TableId").Value;

                    // add <Address>
                    XmlElement xmlFormattedTableRoot = xmlDocFormatted.CreateElement(tableName);

                    // add to xmlDocFormatted node <TableID>123</TableID> to <Address> node
                    XmlElement tableElementID = xmlDocFormatted.CreateElement("TableId");
                    tableElementID.InnerText = tableId;
                    xmlFormattedTableRoot.AppendChild(tableElementID);

                    // Accessing Column elements and attributes within each Table
                    var columnElements = tableElement.Elements("Column");

                    foreach (var columnElement in columnElements)
                    {
                        string columnName = columnElement.Attribute("Name").Value;
                        string isPrimaryKey = columnElement.Attribute("IsPrimaryKey").Value;
                        string foreignKey = columnElement.Attribute("ForeignKey")?.Value ?? "N/A";

                        // add column node <EmpID>EmpID</EmpID>
                        XmlElement columnElementName = xmlDocFormatted.CreateElement(columnName);
                        columnElementName.InnerText = columnName;
                        xmlFormattedTableRoot.AppendChild(columnElementName);

                        if (foreignKey != "N/A")
                        {
                            // add <ForeignKey> 123 </ForeignKey>
                            XmlElement foreignKeyElementName = xmlDocFormatted.CreateElement("ForeignKey");
                            foreignKeyElementName.InnerText = foreignKey;
                            xmlFormattedTableRoot.AppendChild(foreignKeyElementName);
                        }

                        if (isPrimaryKey == "1")
                        {
                            // add <PrimaryKey> empID </PrimaryKey>
                            XmlElement primaryKeyElementName = xmlDocFormatted.CreateElement("PrimaryKey");
                            primaryKeyElementName.InnerText = columnName;
                            xmlFormattedTableRoot.AppendChild(primaryKeyElementName);
                        }

                    }

                    // finally add to <dataroot>
                    datarootElement.AppendChild(xmlFormattedTableRoot);
                }

                return xmlDocFormatted.OuterXml;


                /*
                 // Load the XML
        XmlDocument originalXml = new XmlDocument();
        originalXml.LoadXml(@"<Tables>
                                <Table Schema=""SalesLT"" Name=""Address"" TableId=""69575286"">
                                    <Column Name=""AddressID"" IsPrimaryKey=""1""/>
                                    <!-- Add more columns as needed -->
                                </Table>
                                <!-- Add more tables as needed -->
                            </Tables>");

        // Create a new XmlDocument for the result
        XmlDocument newXmlDocument = new XmlDocument();

        // Create the root element
        XmlElement tablesElement = newXmlDocument.CreateElement("Tables");
        newXmlDocument.AppendChild(tablesElement);

        // Loop through tables in the original XML
        foreach (XmlNode tableNode in originalXml.SelectNodes("//Table"))
        {
            XmlElement tableElement = newXmlDocument.CreateElement("Table");

            // Copy attributes from the original table to the new table
            foreach (XmlAttribute attribute in tableNode.Attributes)
            {
                tableElement.SetAttribute(attribute.Name, attribute.Value);
            }

            // Loop through columns in the original table
            foreach (XmlNode columnNode in tableNode.SelectNodes("Column"))
            {
                XmlElement columnElement = newXmlDocument.CreateElement("Column");

                // Copy attributes from the original column to the new column
                foreach (XmlAttribute attribute in columnNode.Attributes)
                {
                    columnElement.SetAttribute(attribute.Name, attribute.Value);
                }

                // Add the new column to the new table
                tableElement.AppendChild(columnElement);
            }

            // Add the new table to the root element
            tablesElement.AppendChild(tableElement);
        }
                  */
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return string.Empty;
            }
        }

    }
}

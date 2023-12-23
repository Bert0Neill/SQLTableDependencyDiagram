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
                datarootElement.SetAttribute("xmlns:od", "urn:schemas-microsoft-com:officedata");
                datarootElement.SetAttribute("xmlns:xsi", "http://www.w3.org/2001/XMLSchema-instance");                
                datarootElement.SetAttribute("generated", "2005-01-18T15:03:23");
                
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
                        string isForeignKey = columnElement.Attribute("ForeignKey")?.Value ?? "N/A";

                        // add column node <EmpID>EmpID</EmpID>
                        XmlElement columnElementName = xmlDocFormatted.CreateElement(columnName);
                        columnElementName.InnerText = columnName;
                        xmlFormattedTableRoot.AppendChild(columnElementName);

                        if (isForeignKey != "N/A")
                        {
                            // add <ForeignKey> 123 </ForeignKey>
                            XmlElement foreignKeyElementName = xmlDocFormatted.CreateElement("ForeignKey");
                            //foreignKeyElementName.InnerText = foreignKey;
                            foreignKeyElementName.InnerText = columnName;
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
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return string.Empty;
            }
        }

    }
}

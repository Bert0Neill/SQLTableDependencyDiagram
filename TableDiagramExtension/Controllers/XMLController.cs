using System;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using TableDiagramExtension.Interfaces;

namespace TableDiagramExtension.Controllers
{
    internal class XMLController : IXMLController
    {
        public XMLController() { }

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
                        string columnDataType = columnElement.Attribute("ColumnDataType")?.Value ?? "N/A"; ;
                        string isForeignKey = columnElement.Attribute("ForeignKey")?.Value ?? "N/A";

                        // add column node <EmpID>EmpID</EmpID>
                        XmlElement columnElementName = xmlDocFormatted.CreateElement(columnName);
                        columnElementName.InnerText = columnName;
                        xmlFormattedTableRoot.AppendChild(columnElementName);

                        if (columnDataType != "N/A")
                        {                            
                            // add attribute, that will later be added to concrete class collection of columns
                            columnElementName.SetAttribute("ColumnDataType", columnDataType);
                        }

                        if (isForeignKey != "N/A")
                        {
                            // add <ForeignKey> 123 </ForeignKey>
                            XmlElement foreignKeyElementName = xmlDocFormatted.CreateElement("ForeignKey");
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
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return string.Empty;
            }
        }

    }
}

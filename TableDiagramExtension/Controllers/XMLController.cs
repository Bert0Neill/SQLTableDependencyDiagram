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

        public void DeleteNodes()
        {
            try
            {
                // Load the XML document
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(@"<?xml version=""1.0"" encoding=""utf-8"" ?>
                            <Tables>
                                <!-- ... (your XML content) ... -->
                            </Tables>");

                // Select nodes with TableName not equal to "Customer" or "Address"
                XmlNodeList nodesToDelete = xmlDoc.SelectNodes("//Table[not(@TableName='Customer' or @TableName='Address')]");

                // Delete the selected nodes
                foreach (XmlNode node in nodesToDelete)
                {
                    node.ParentNode.RemoveChild(node);
                }

                // Save the modified XML document
                xmlDoc.Save("path/to/your/xml/modified_example.xml");

                //********************************

                //                <? xml version = "1.0" encoding = "UTF-8" ?>
                //< root >
                //  < person id = "1" name = "John" age = "25" />
                //  < person id = "2" name = "Alice" age = "30" />
                //  < person id = "3" name = "Bob" age = "22" />
                //  < person id = "4" name = "Eve" age = "35" />
                //</ root >


                //            // Specify the attribute name and values for filtering
                //            string attributeName = "age";
                //                string[] filterValues = { "30", "35" };

                //                // Select nodes with the specified attribute not equal to the filter values
                //                XmlNodeList nodesToDelete = xmlDoc.SelectNodes($"//person[not(@{attributeName}='{string.Join("' or not(@", filterValues)}')]");

                //                // Delete the selected nodes
                //                foreach (XmlNode node in nodesToDelete)
                //                {
                //                    node.ParentNode.RemoveChild(node);
                //                }



            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        public void RenameNodes()
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

                // Save the modified XML document
                xmlDoc.Save("path/to/your/xml/modified_example.xml");

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

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        public void LoadXMLString()
        {

            string xmlString = "<root><element1>value1</element1><element2>value2</element2></root>";
            // Create a new XmlDocument
            XmlDocument xmlDoc = new XmlDocument();
            // Load the XML string into the XmlDocument
            xmlDoc.LoadXml(xmlString);
        }

    }
}

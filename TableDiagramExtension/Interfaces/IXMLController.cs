using System.Collections.Generic;
using System.Xml;

namespace TableDiagramExtension.Interfaces
{
    internal interface IXMLController
    {        
        XmlDocument LoadXMLString(string xmlString);
        string RenameNodes(string xmlString);
        string GenerateXmlDocFromDBData(string xmlString);
    }
}
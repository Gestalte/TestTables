using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace TestTables
{
    public class ParseXml
    {
        public string LoadXmlFile(string path)
        {
            string xmlContent = null;

            using (XmlReader reader = XmlReader.Create(path))
            {

                while (reader.Read())
                {
                    xmlContent = reader.ReadOuterXml();
                }
            }

            return xmlContent;
        }

        public XElement LoadXElement(string xml)
        {
            XElement xElem = XElement.Parse(xml);

            return xElem;
        }
    }
}

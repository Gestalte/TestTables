using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Linq;

namespace TestTables
{
    public class ParseNunitTestResultsXml
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

        public List<XElement> IsolateResults(string xml)
        {
            var xElementResults = LoadXElement(xml)
                .Elements()
                .Where(w => w.Name.LocalName == "Results")
                .FirstOrDefault()
                .Elements()
                .ToList();

            return xElementResults;
        }

        public List<XElement> IsolateResultsSummaryCounters(string xml)
        {
            var xElementResults = LoadXElement(xml)
                 .Elements()
                 .Where(w => w.Name.LocalName == "ResultSummary")
                 .FirstOrDefault()
                 .Elements()
                 .Where(w => w.Name.LocalName == "Counters")
                 .ToList();

            return xElementResults;
        }

        public List<Result> ConvertToResult(List<XElement> xmlResults)
        {
            var results = xmlResults.Select(s => new Result
                 (s.Attribute("testName").Value
                 , s.Attribute("duration").Value
                 , s.Attribute("outcome").Value
                 ,s.Value
                 ))
                .ToList();

            return results;
        }

        public List<SummaryCounter> ConvertToSummaryCounters(List<XElement> xmlSummaryCounters)
        {
            var results = xmlSummaryCounters.Select(s => new SummaryCounter
                 (s.Attribute("total").Value
                 , s.Attribute("executed").Value
                 , s.Attribute("passed").Value
                 , s.Attribute("failed").Value
                 , s.Attribute("error").Value
                 , s.Attribute("timeout").Value
                 , s.Attribute("aborted").Value
                 , s.Attribute("inconclusive").Value
                 , s.Attribute("passedButRunAborted").Value
                 , s.Attribute("notRunnable").Value
                 , s.Attribute("notExecuted").Value
                 , s.Attribute("disconnected").Value
                 , s.Attribute("warning").Value
                 , s.Attribute("completed").Value
                 , s.Attribute("inProgress").Value
                 , s.Attribute("pending").Value
                 ))
                .ToList();

            return results;
        }
    }
}

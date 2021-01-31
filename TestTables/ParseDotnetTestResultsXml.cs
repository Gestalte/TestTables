using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace TestTables
{
    public class ParseDotnetTestResultsXml
    {

        private readonly ParseXml parseXml;

        public ParseDotnetTestResultsXml(ParseXml parseXml)
        {
            this.parseXml = parseXml;
        }

        public List<XElement> IsolateResults(string xml)
        {
            var xElementResults = this.parseXml.LoadXElement(xml)
                .Elements()
                .Where(w => w.Name.LocalName == "Results")
                .FirstOrDefault()
                .Elements()
                .ToList();

            return xElementResults;
        }

        public List<XElement> IsolateResultsSummaryCounters(string xml)
        {
            var xElementResults = this.parseXml.LoadXElement(xml)
                 .Elements()
                 .Where(w => w.Name.LocalName == "ResultSummary")
                 .FirstOrDefault()
                 .Elements()
                 .Where(w => w.Name.LocalName == "Counters")
                 .ToList();

            return xElementResults;
        }

        private ErrorInfo ConvertToError(XElement x)
        {
            if (x.Value != "")
            {
                var errorInfo = x.Elements().FirstOrDefault().Elements().Where(w => w.Name.LocalName == "ErrorInfo");

                var message = errorInfo.Elements().Where(w => w.Name.LocalName == "Message").FirstOrDefault()?.Value;
                var stackTrace = errorInfo.Elements().Where(w => w.Name.LocalName == "StackTrace").FirstOrDefault()?.Value;

                return new ErrorInfo(message, stackTrace);
            }

            return null;
        }

        public List<Result> ConvertToResult(List<XElement> xmlResults)
        {
            var results = xmlResults.Select(s => new Result
                 (s.Attribute("testName").Value
                 , s.Attribute("duration").Value
                 , s.Attribute("outcome").Value
                 , ConvertToError(s)
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

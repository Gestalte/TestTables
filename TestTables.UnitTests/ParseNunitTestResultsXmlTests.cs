using NUnit.Framework;
using System.Collections.Generic;
using FluentAssertions;
using System.Linq;

namespace TestTables.UnitTests
{
    [TestFixture]
    public class ParseNunitTestResultsXmlTests
    {
        public const string TestPath = @"C:\Users\Desktop\Documents\Github\TestTables\TestTables.UnitTests\TestResults\TestResults.trx";

        [Test]
        public void LoadXmlFile()
        {
            ParseNunitTestResultsXml sut = new ParseNunitTestResultsXml();

            string xmlContents = sut.LoadXmlFile(TestPath);

            Assert.NotNull(xmlContents);
        }

        [Test]
        public void IsolateResults()
        {
            ParseNunitTestResultsXml sut = new ParseNunitTestResultsXml();
            string xml = sut.LoadXmlFile(TestPath);

            var xElements = sut.IsolateResults(xml);

            xElements.Should().NotBeNull();
            xElements.Count().Should().BeGreaterThan(0);
        }

        [Test]
        public void IsolateResultsSummaryCounters()
        {
            ParseNunitTestResultsXml sut = new ParseNunitTestResultsXml();
            string xml = sut.LoadXmlFile(TestPath);

            var xElements = sut.IsolateResultsSummaryCounters(xml);

            xElements.Should().NotBeNull();
            xElements.Count().Should().BeGreaterThan(0);
        }

        [Test]
        public void ExtractResults()
        {
            ParseNunitTestResultsXml sut = new ParseNunitTestResultsXml();
            string xml = sut.LoadXmlFile(TestPath);

            var xElements = sut.IsolateResults(xml);

            List<Result> results = sut.ConvertToResult(xElements);

            results.Count().Should().BeGreaterThan(0);
        }

        [Test]
        public void ExtractSummaryCounters()
        {
            ParseNunitTestResultsXml sut = new ParseNunitTestResultsXml();
            string xml = sut.LoadXmlFile(TestPath);

            var xElements = sut.IsolateResultsSummaryCounters(xml);

            List<SummaryCounter> results = sut.ConvertToSummaryCounters(xElements);

            results.Count().Should().BeGreaterThan(0);
        }

        [Test]
        public void fails()
        {
            true.Should().BeFalse();
        }

        [Test]
        public void fails2()
        {
            Assert.IsTrue(false);
        }

        [Test]
        [Ignore("Gets skipped")]
        public void Skip()
        {
            true.Should().BeFalse();
        }
    }
}

using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;


namespace TestTables.UnitTests
{
    [TestFixture]
    public class ParseDotnetTestResultsXmlTests
    {
        private readonly string TestPath = ConfigurationHelper.RetrieveTestResultsPath();

        public ParseXml ParseXml { get; set; } = new ParseXml();

        [Test]
        public void IsolateResults_GoodXml_NotNull()
        {
            ParseDotnetTestResultsXml sut = new ParseDotnetTestResultsXml(ParseXml);
            string xml = ParseXml.LoadXmlFile(TestPath);

            var xElements = sut.IsolateResults(xml);

            xElements.Should().NotBeNull();
        }

        [Test]
        public void IsolateResults_GoodXml_MoreThan0Results()
        {
            ParseDotnetTestResultsXml sut = new ParseDotnetTestResultsXml(ParseXml);
            string xml = ParseXml.LoadXmlFile(TestPath);

            var xElements = sut.IsolateResults(xml);

            xElements.Count().Should().BeGreaterThan(0);
        }

        [Test]
        public void IsolateResultsSummaryCounters_GoodXml_NotNull()
        {
            ParseDotnetTestResultsXml sut = new ParseDotnetTestResultsXml(ParseXml);
            string xml = ParseXml.LoadXmlFile(TestPath);

            var xElements = sut.IsolateResultsSummaryCounters(xml);

            xElements.Should().NotBeNull();
        }

        [Test]
        public void IsolateResultsSummaryCounters_GoodXml_MoreThan0Results()
        {
            ParseDotnetTestResultsXml sut = new ParseDotnetTestResultsXml(ParseXml);
            string xml = ParseXml.LoadXmlFile(TestPath);

            var xElements = sut.IsolateResultsSummaryCounters(xml);

            xElements.Count().Should().BeGreaterThan(0);
        }

        [Test]
        public void ConvertToResult_GoodXElements_ListOfMoreThan0Results()
        {
            ParseDotnetTestResultsXml sut = new ParseDotnetTestResultsXml(ParseXml);
            string xml = ParseXml.LoadXmlFile(TestPath);
            var xElements = sut.IsolateResults(xml);

            List<Result> results = sut.ConvertToResult(xElements);

            results.Count().Should().BeGreaterThan(0);
        }

        [Test]
        public void ConvertToSummaryCounters_GoodXElements_ListOMoreThan0SummaryCounters()
        {
            ParseDotnetTestResultsXml sut = new ParseDotnetTestResultsXml(ParseXml);
            string xml = ParseXml.LoadXmlFile(TestPath);
            var xElements = sut.IsolateResultsSummaryCounters(xml);

            List<SummaryCounter> results = sut.ConvertToSummaryCounters(xElements);

            results.Count().Should().BeGreaterThan(0);
        }
    }
}

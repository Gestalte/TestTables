using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestTables.UnitTests
{
    [TestFixture]
    public class ParseXmlTests
    {
        public const string TestPath = @"C:\Users\Desktop\Documents\Github\TestTables\TestTables.UnitTests\TestResults\TestResults.trx";

        [Test]
        public void LoadXmlFile_GoodXml_NotNull()
        {
            ParseXml sut = new ParseXml();

            string xmlContents = sut.LoadXmlFile(TestPath);

            Assert.NotNull(xmlContents);
        }

        [Test]
        public void LoadXElement_GoodXml_NotNull()
        {
            ParseXml sut = new ParseXml();

            string xmlContents = sut.LoadXmlFile(TestPath);

            var result = sut.LoadXElement(xmlContents);

            result.Should().NotBeNull();
        }
    }
}

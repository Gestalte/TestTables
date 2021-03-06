﻿using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Linq;

namespace TestTables
{
    class Program
    {
        // Generate test results xml with: dotnet test --logger:trx;LogFileName=TestResults.trx

        static void Main(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            string TestPath = configuration["TestResultsPath"];

            ParseXml parseXml = new ParseXml();
            ParseDotnetTestResultsXml parser = new ParseDotnetTestResultsXml(parseXml);

            var xml = parseXml.LoadXmlFile(TestPath);

            var xmlResults = parser.IsolateResults(xml);
            var xmlSummaryCounters = parser.IsolateResultsSummaryCounters(xml);

            var results = parser.ConvertToResult(xmlResults);
            var summaryCounters = parser.ConvertToSummaryCounters(xmlSummaryCounters);

            var testNamePadding = results.Select(s => s.TestName.Length).OrderBy(l => l).Last();
            var outcomePadding = results.Select(s => s.Outcome.Length).OrderBy(l => l).Last();

            var divider = string.Empty.PadRight(testNamePadding + 2 + outcomePadding + 2 + "Duration".Length + 2, '-');

            // Heading
            Console.WriteLine($"{"Test Name".PadRight(testNamePadding + 1, ' ')} {"Outcome".PadRight(outcomePadding + 1, ' ')} {"Duration"}");
            Console.WriteLine(divider);

            // Results
            results.ForEach(f =>
            {
                // Write TestName
                Console.Write(f.TestName.PadRight(testNamePadding + 2, ' '));

                // Write Outcome
                Console.ForegroundColor = f.Outcome switch
                {
                    "Passed" => ConsoleColor.Green,
                    "Failed" => ConsoleColor.Red,
                    "NotExecuted" => ConsoleColor.DarkYellow,
                    _ => ConsoleColor.DarkYellow
                };
                Console.Write(f.Outcome.PadRight(outcomePadding + 2, ' '));
                Console.ResetColor();

                // Write Time
                Console.Write($"{TimeSpan.Parse(f.Duration).TotalMilliseconds} ms  ");

                // Write Error
                if (f.Error != null)
                {
                    var err = $"{f.Error.Message.Replace("\n", "")}".Trim();

                    err = err.Split(' ').ToList().Aggregate("", (a, b) => a.Trim() + " " + b.Trim());

                    Console.Write(err);
                }

                Console.WriteLine();
            });

            var counter = summaryCounters.FirstOrDefault();
            Console.WriteLine();

            // Summary
            int total = int.Parse(counter.Total);
            int passed = int.Parse(counter.Passed);
            int failed = int.Parse(counter.Failed);
            int notExecuted = total - passed - failed;

            Action<string, string, ConsoleColor> writeSummary = (label, count, color) =>
            {
                Console.Write(label);
                if ("0" != count)
                {
                    Console.ForegroundColor = color;
                }
                Console.Write($"{count} ");
                Console.ResetColor();
            };

            Console.WriteLine(divider);

            writeSummary("Total: ", counter.Total, ConsoleColor.White);
            writeSummary("Passed: ", counter.Passed, ConsoleColor.Green);
            writeSummary("Failed: ", counter.Failed, ConsoleColor.Red);
            writeSummary("Not Executed: ", notExecuted.ToString(), ConsoleColor.DarkYellow);

            Console.WriteLine();

            Console.WriteLine(divider);

            Console.ReadKey();
        }
    }
}

using System;
using System.Linq;

namespace TestTables
{
    class Program
    {
        private const string TestPath = @"";

        static void Main(string[] args)
        {
            ParseNunitTestResultsXml parser = new ParseNunitTestResultsXml();

            var xml = parser.LoadXmlFile(TestPath);

            var xmlResults = parser.IsolateResults(xml);
            var xmlSummaryCounters = parser.IsolateResultsSummaryCounters(xml);

            var results = parser.ConvertToResult(xmlResults);
            var summaryCounters = parser.ConvertToSummaryCounters(xmlSummaryCounters);

            var testNamePadding = results.Select(s => s.TestName.Length).OrderBy(l => l).Last();
            var outcomePadding = results.Select(s => s.Outcome.Length).OrderBy(l => l).Last();

            Action<string> setColour = outcome =>
            {
                Console.ForegroundColor = outcome switch
                {
                    "Passed" => ConsoleColor.Green,
                    "Failed" => ConsoleColor.Red,
                    "NotExecuted" => ConsoleColor.DarkYellow,
                    _ => ConsoleColor.DarkYellow
                };
            };

            // Heading
            Console.WriteLine($"{"Test Name".PadRight(testNamePadding + 1, ' ')} {"Outcome".PadRight(outcomePadding + 1, ' ')} {"Duration"}");

            Console.WriteLine("");

            // Results
            results.ForEach(f =>
            {
                Console.Write(f.TestName.PadRight(testNamePadding + 2, ' '));
                setColour(f.Outcome);
                Console.Write(f.Outcome.PadRight(outcomePadding + 2, ' '));
                Console.ResetColor();

                var time = TimeSpan.Parse(f.Duration);
                Console.Write($"{time.TotalMilliseconds} ms");

                Console.WriteLine();
            });

            var counter = summaryCounters.ToList().FirstOrDefault();
            Console.WriteLine();

            // Summary
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

            Func<string, int> convertStringToInt = a => int.Parse(a);

            int total = convertStringToInt(counter.Total);
            int passed = convertStringToInt(counter.Passed);
            int failed = convertStringToInt(counter.Failed);
            int notExecuted = total - passed - failed;

            writeSummary("Total: ", counter.Total, ConsoleColor.White);
            writeSummary("Passed: ", counter.Passed, ConsoleColor.Green);
            writeSummary("Failed: ", counter.Failed, ConsoleColor.Red);
            writeSummary("Not Executed: ", notExecuted.ToString(), ConsoleColor.DarkYellow);

            Console.WriteLine();

            Console.ReadKey();
        }
    }
}

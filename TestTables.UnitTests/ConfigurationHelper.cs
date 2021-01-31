using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;

namespace TestTables.UnitTests
{
    public static class ConfigurationHelper
    {
        static Func<string, string> GetSolutionDirectory = (currentPath)
               =>
        {
            if (currentPath.Split('\\').Last() != "TestTables")
            {
                currentPath = GetSolutionDirectory(Directory.GetParent(currentPath).FullName);
            }

            return currentPath;
        };

        public static string RetrieveAppsetting(string entry)
        {
            var solutionDirectory = GetSolutionDirectory(Directory.GetCurrentDirectory());

            string applicationDirectory = Path.Combine(solutionDirectory, "TestTables");

            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(applicationDirectory)
                .AddJsonFile("appsettings.json")
                .Build();

            return configuration[entry];
        }

        public static string RetrieveTestResultsPath()
            => RetrieveAppsetting("TestResultsPath");
    }
}

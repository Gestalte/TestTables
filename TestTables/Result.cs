using System;
using System.Collections.Generic;
using System.Text;

namespace TestTables
{
    public class Result
    {
        public Result(string testName, string duration, string outcome, string error)
        {
            TestName = testName;
            Duration = duration;
            Outcome = outcome;
            Error = error;
        }

        public string TestName { get; set; }
        public string Duration { get; set; }
        public string Outcome { get; set; }
        public string Error { get; set; }
    }
}

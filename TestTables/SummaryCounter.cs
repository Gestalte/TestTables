using System;
using System.Collections.Generic;
using System.Text;

namespace TestTables
{
    public class SummaryCounter
    {
        public SummaryCounter
            (string total
            , string executed
            , string passed
            , string failed
            , string error
            , string timeout
            , string aborted
            , string inconclusive
            , string passedButRunAborted
            , string notRunnable
            , string notExecuted
            , string disconnected
            , string warning
            , string completed
            , string inProgress
            , string pending
            )
        {
            Total = total;
            Executed = executed;
            Passed = passed;
            Failed = failed;
            Error = error;
            Timeout = timeout;
            Aborted = aborted;
            Inconclusive = inconclusive;
            PassedButRunAborted = passedButRunAborted;
            NotRunnable = notRunnable;
            NotExecuted = notExecuted;
            Disconnected = disconnected;
            Warning = warning;
            Completed = completed;
            InProgress = inProgress;
            Pending = pending;
        }
        public string Total { get; set; }
        public string Executed { get; set; }
        public string Passed { get; set; }
        public string Failed { get; set; }
        public string Error { get; set; }
        public string Timeout { get; set; }
        public string Aborted { get; set; }
        public string Inconclusive { get; set; }
        public string PassedButRunAborted { get; set; }
        public string NotRunnable { get; set; }
        public string NotExecuted { get; set; }
        public string Disconnected { get; set; }
        public string Warning { get; set; }
        public string Completed { get; set; }
        public string InProgress { get; set; }
        public string Pending { get; set; }
    }
}

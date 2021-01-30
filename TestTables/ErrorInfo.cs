namespace TestTables
{
    public class ErrorInfo
    {
        public ErrorInfo(string message,string stackTrace)
        {
            Message = message;
            StackTrace = stackTrace;
        }

        public string Message { get; set; }
        public string StackTrace { get; set; }
    }
}

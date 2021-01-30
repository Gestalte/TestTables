using System;

namespace TestTables
{
    public class OutputWriter : IOutputWriter
    {
        public void Write(string output)
        {
            Console.WriteLine(output);
        }
    }
}

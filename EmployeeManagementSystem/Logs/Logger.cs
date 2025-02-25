using System;

namespace EmployeeManagementSystem.Logs
{
    public class Logger
    {
        public static void WriteLog(string message)
        {
            Console.WriteLine($"[LOG] {message}");
        }
    }
}

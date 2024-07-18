using System;
using System.IO;

public static class Logger
{
    public static void LogMessage(string fileName, string message, string level)
    {
        string directory = Path.GetDirectoryName(fileName);
        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }

        string logEntry = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] [{level}] {message}";
        using (StreamWriter writer = new StreamWriter(fileName, true))
        {
            writer.WriteLine(logEntry);
        }
    }
}

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Threading;

namespace LoggerExample.Tests
{
    [TestClass]
    public class LoggerTests
    {
        private string GetTestFilePath()
        {
            string projectDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string testFileName = Path.Combine(projectDirectory, "test.log");
            return testFileName;
        }

        [TestInitialize]
        public void Setup()
        {
            string testFileName = GetTestFilePath();
            if (File.Exists(testFileName))
            {
                File.Delete(testFileName);
            }
        }

        [TestMethod]
        public void TestLogMessage_Info()
        {
            string testFileName = GetTestFilePath();
            Logger.LogMessage(testFileName, "User logged in", "INFO");

            WaitForFile(testFileName);
            string[] logContents = File.ReadAllLines(testFileName);
            Assert.AreEqual(1, logContents.Length);
            StringAssert.Contains(logContents[0], "[INFO] User logged in");
        }

        [TestMethod]
        public void TestLogMessage_Warning()
        {
            string testFileName = GetTestFilePath();
            Logger.LogMessage(testFileName, "Failed login attempt", "WARNING");

            WaitForFile(testFileName);
            string[] logContents = File.ReadAllLines(testFileName);
            Assert.AreEqual(1, logContents.Length);
            StringAssert.Contains(logContents[0], "[WARNING] Failed login attempt");
        }

        [TestMethod]
        public void TestLogMessage_Deleted()
        {
            string testFileName = GetTestFilePath();
            Logger.LogMessage(testFileName, "The account was deleted", "DELETED");

            WaitForFile(testFileName);
            string[] logContents = File.ReadAllLines(testFileName);
            Assert.AreEqual(1, logContents.Length);
            StringAssert.Contains(logContents[0], "[DELETED] The account was deleted");
        }

        private void WaitForFile(string filePath)
        {
            int maxRetries = 5;
            int delay = 200;

            for (int i = 0; i < maxRetries; i++)
            {
                try
                {
                    if (File.Exists(filePath))
                    {
                        using (FileStream stream = File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                        {
                            stream.Close();
                        }
                        return;
                    }
                }
                catch (IOException)
                {
                    Thread.Sleep(delay);
                }
            }

            throw new FileNotFoundException($"Could not find or access the file '{filePath}' after {maxRetries} retries.");
        }
    }
}

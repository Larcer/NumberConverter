using System;
using System.IO;
using System.Text;

namespace Nameless.NumberConverter.Tools
{
    // Handles log actions
    internal static class Logger
    {
        // Logs specified message to a file
        internal static void Log(string message)
        {
            lock (FileFolderHelper.LogFilePath)
            {
                StreamWriter writer = null;
                FileStream file = null;
                try
                {
                    FileFolderHelper.CheckAndCreateFile(FileFolderHelper.LogFilePath);

                    file = new FileStream(FileFolderHelper.LogFilePath, FileMode.Append);
                    writer = new StreamWriter(file);

                    writer.WriteLine(DateTime.Now.ToString("HH:mm:ss.ms") + " " + message);
                }
                finally
                {
                    writer?.Close();
                    file?.Close();

                    writer = null;
                    file = null;
                }
            }
        }

        // Logs specified message and exception to a file
        internal static void Log(string message, Exception e)
        {
            var stringBuilder = new StringBuilder();

            if (message != string.Empty)
                stringBuilder.AppendLine(message);

            while (e != null)
            {
                stringBuilder.AppendLine(e.Message);
                stringBuilder.AppendLine(e.StackTrace);

                e = e.InnerException;
            }

            Log(stringBuilder.ToString());
        }

        // Logs specified exception to a file
        internal static void Log(Exception e)
        {
            Log("", e);
        }
    }
}

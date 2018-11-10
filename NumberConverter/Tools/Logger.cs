using System;
using System.IO;
using System.Text;

namespace Nameless.NumberConverter.Tools
{
    internal static class Logger
    {
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

        internal static void Log(string message, Exception ex)
        {
            StringBuilder stringBuilder = new StringBuilder();

            if (message != string.Empty)
                stringBuilder.AppendLine(message);

            while (ex != null)
            {
                stringBuilder.AppendLine(ex.Message);
                stringBuilder.AppendLine(ex.StackTrace);

                ex = ex.InnerException;
            }

            Log(stringBuilder.ToString());
        }

        internal static void Log(Exception ex)
        {
            Log("", ex);
        }
    }
}

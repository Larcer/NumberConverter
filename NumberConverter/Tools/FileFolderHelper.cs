using System;
using System.IO;
using Nameless.NumberConverter.Managers;

namespace Nameless.NumberConverter.Tools
{
    internal static class FileFolderHelper
    {
        private static readonly string AppDataPath =
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

        internal static readonly string ClientFolderPath =
            Path.Combine(AppDataPath, "NumberConverter");

        internal static readonly string LogFolderPath =
            Path.Combine(ClientFolderPath, "Log");

        internal static readonly string LogFilePath = Path.Combine(
            LogFolderPath, "App_" + DateTime.Now.ToString("YYYY_MM_DD") + ".txt");

        internal static readonly string StorageFilePath =
            Path.Combine(ClientFolderPath, "Storage.numconv");

        internal static readonly string LastUserFilePath =
            Path.Combine(ClientFolderPath, "LastUser.numconv");

        internal static void CheckAndCreateFile(string filePath)
        {
            FileInfo file = new FileInfo(filePath);

            if (!file.Directory.Exists)
            {
                file.Directory.Create();
            }

            if (!file.Exists)
            {
                file.Create().Close();
            }
        }

        internal static void ClearFile(string filePath)
        {
            FileInfo file = new FileInfo(filePath);

            if (file.Directory.Exists && file.Exists)
            {
                using (FileStream stream = new FileStream(filePath, FileMode.Truncate))
                {

                }
            }
        }

    }
}

using System;
using System.IO;

namespace Nameless.NumberConverter.Tools
{
    // Handles actions with filesystem
    internal static class FileFolderHelper
    {
        // Path to the applications data folder
        private static readonly string AppDataPath =
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

        // Path to the current application data folder
        internal static readonly string ClientFolderPath =
            Path.Combine(AppDataPath, "NumberConverter");

        // Path to the log folder
        internal static readonly string LogFolderPath =
            Path.Combine(ClientFolderPath, "Log");

        // Path to the log file
        internal static readonly string LogFilePath = Path.Combine(
            LogFolderPath, "App_" + DateTime.Now.ToString("YYYY_MM_DD") + ".txt");

        // Path to the database folder
        internal static readonly string StorageFilePath =
            Path.Combine(ClientFolderPath, "Storage.numconv");

        // Path to the file where last user is serialized to
        internal static readonly string LastUserFilePath =
            Path.Combine(ClientFolderPath, "LastUser.numconv");

        // Creates specified file if it does not exists.
        // If some directories from the path do not exist, they also will be created
        internal static void CheckAndCreateFile(string filePath)
        {
            var file = new FileInfo(filePath);

            if (!file.Directory.Exists)
                file.Directory.Create();

            if (!file.Exists)
                file.Create().Close();
        }

        // Clears all data from specified file
        internal static void ClearFile(string filePath)
        {
            var file = new FileInfo(filePath);
            if (file.Exists)
            {
                using (var stream = new FileStream(filePath, FileMode.Truncate))
                {
                    // Should be empty because it only clears file with truncate mode
                }
            }
        }

    }
}

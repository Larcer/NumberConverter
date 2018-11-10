using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

using Nameless.NumberConverter.Tools;

namespace Nameless.NumberConverter.Managers
{
    internal static class SerializationManager
    {
        internal static void Serialize<TObject>(TObject obj, string filePath)
        {
            try
            {
                FileFolderHelper.CheckAndCreateFile(filePath);

                BinaryFormatter formatter = new BinaryFormatter();
                using (FileStream stream = new FileStream(filePath, FileMode.Create))
                {
                    formatter.Serialize(stream, obj);
                }
            }
            catch (Exception e)
            {
                MessageManager.Log($"Failed to serialize data to file {filePath}", e);
                throw;
            }
        }

        internal static TObject Deserialize<TObject>(string filePath) where TObject : class
        {
            try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                using (FileStream stream = new FileStream(filePath, FileMode.Open))
                {
                    if (stream.Length == 0)
                        return null;

                    return (TObject) formatter.Deserialize(stream);
                }
            }
            catch (Exception e)
            {
                MessageManager.Log($"Failed to deserialize data from file: {filePath}", e);
                return null;
            }
        }

    }
}

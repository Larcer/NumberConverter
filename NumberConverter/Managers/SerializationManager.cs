using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Nameless.NumberConverter.Tools;

namespace Nameless.NumberConverter.Managers
{
    // Handles object serialization and deserialization
    internal static class SerializationManager
    {
        // Serializes specified object to the file with specified path
        internal static void Serialize<TObject>(TObject obj, string filePath)
        {
            try
            {
                FileFolderHelper.CheckAndCreateFile(filePath);

                var formatter = new BinaryFormatter();
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    formatter.Serialize(stream, obj);
                }
            }
            catch (Exception e)
            {
                MessageManager.Log($"Failed to serialize data to the file: {filePath}", e);
                throw;
            }
        }

        // Deserializes object from the file with specified path
        internal static TObject Deserialize<TObject>(string filePath) where TObject : class
        {
            try
            {
                var formatter = new BinaryFormatter();
                using (var stream = new FileStream(filePath, FileMode.Open))
                {
                    if (stream.Length == 0)
                        return null;

                    return (TObject) formatter.Deserialize(stream);
                }
            }
            catch (Exception e)
            {
                MessageManager.Log($"Failed to deserialize data from the file: {filePath}", e);
                return null;
            }
        }

    }
}

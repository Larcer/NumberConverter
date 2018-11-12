using System;
using Nameless.NumberConverter.Data;
using Nameless.NumberConverter.Models;
using Nameless.NumberConverter.Tools;

namespace Nameless.NumberConverter.Managers
{
    // Manages user sessions
    internal class SessionManager : SingletonBase<SessionManager>
    {
        // User that is currently logged in
        internal User CurrentUser { get; private set; }

        private SessionManager()
        {
            DeserializeUser();
        }
        
        // Starts new user session.
        // Serializes the user to a file
        internal void StartSession(User user)
        {
            CurrentUser = user;
            SerializeUser();
        }
        
        // Ends user session.
        // Clears serialized user file
        internal void EndSession()
        {
            CurrentUser = null;
            DestroySerializedUser();
        }

        // Serializes user to a file
        private void SerializeUser()
        {
            try
            {
                SerializationManager.Serialize(CurrentUser, FileFolderHelper.LastUserFilePath);
            }
            catch (Exception e)
            {
                MessageManager.Log("Failed to serialize user", e);
            }
        }

        // Deserializes user from a file
        private void DeserializeUser()
        {
            User user;
            try
            {
                user = SerializationManager.Deserialize<User>(FileFolderHelper.LastUserFilePath);
            }
            catch (Exception e)
            {
                user = null;
                MessageManager.Log("Failed to deserialize last user", e);
            }

            if (user == null)
            {
                MessageManager.Log("User was not deserialized");
                return;
            }

            user = DBManager.Instance.CheckCachedUser(user);
            if (user == null)
                MessageManager.Log("Failed to relogin last user");
            else
            {
                CurrentUser = user;
                CurrentUser.LastLoginDateTime = DateTime.Now;
                DBManager.Instance.UpdateUser(CurrentUser);
            }
        }

        // Clears serialized file
        private void DestroySerializedUser()
        {
            try
            {
                FileFolderHelper.ClearFile(FileFolderHelper.LastUserFilePath);
            }
            catch (Exception e)
            {
                MessageManager.Log("Failed to clear last user in the file", e);
            }
        }
    }
}

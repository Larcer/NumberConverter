using System;

using Nameless.NumberConverter.Data;
using Nameless.NumberConverter.Models;
using Nameless.NumberConverter.Tools;

namespace Nameless.NumberConverter.Managers
{
    // Manages user sessions
    public class SessionManager : SingletonBase<SessionManager>
    {
        public User CurrentUser { get; private set; }

        private SessionManager()
        {
            DeserializeUser();
        }
        
        public void StartSession(User user)
        {
            CurrentUser = user;
            SerializeUser();
        }
        
        public void EndSession()
        {
            CurrentUser = null;
            DestroySerializedUser();
        }

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

using System;
using System.Collections.Generic;
using System.Linq;
using Nameless.NumberConverter.Managers;
using Nameless.NumberConverter.Models;
using Nameless.NumberConverter.Tools;

namespace Nameless.NumberConverter.Data
{
    // Represents in memory database
    public class DBManager : SingletonBase<DBManager>
    {
        private readonly IDictionary<string, User> _users = new Dictionary<string, User>();

        private DBManager()
        {
            _users = DeserializeDB();
        }

        // Checks if user with the specified login exists
        public bool UserExists(string login)
        {
            return _users.ContainsKey(login);
        }

        // Checks if the specified email exists
        public bool EmailExists(string email)
        {
            return _users.FirstOrDefault(lu => lu.Value.Email.Equals(email)).Key != null;
        }

        // Returns user with the specified login or null if one does not exist
        public User GetUserByLogin(string login)
        {
            return _users.ContainsKey(login) ? _users[login] : null;
        }

        // Adds new user into the database
        public void AddUser(User user)
        {
            _users.Add(user.Login, user);
            SaveChanges();
        }

        // Updates user in the database
        public void UpdateUser(User user)
        {
            SaveChanges();
        }

        // Checks if deserialized user exists in the database
        internal User CheckCachedUser(User user)
        {
            if (user == null || user.Login == null || !_users.ContainsKey(user.Login))
                return null;

            User userInStorage = _users[user.Login];
            if (userInStorage != null && userInStorage.Password != null && userInStorage.Password.Equals(user.Password))
                return userInStorage;

            return null;
        }

        private void SaveChanges()
        {
            SerializationManager.Serialize(_users, FileFolderHelper.StorageFilePath);
        }

        private IDictionary<string, User> DeserializeDB()
        {
            try
            {
                return SerializationManager.Deserialize<Dictionary<string, User>>(
                    FileFolderHelper.StorageFilePath) ?? new Dictionary<string, User>();
            }
            catch (Exception e)
            {
                MessageManager.Log("Failed to deserialize database", e);
            }

            return new Dictionary<string, User>();
        }
    }
}

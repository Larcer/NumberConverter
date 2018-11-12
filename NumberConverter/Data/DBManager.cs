using System;
using System.Collections.Generic;
using System.Linq;
using Nameless.NumberConverter.Managers;
using Nameless.NumberConverter.Models;
using Nameless.NumberConverter.Tools;

namespace Nameless.NumberConverter.Data
{
    // Represents in memory database
    internal class DBManager : SingletonBase<DBManager>
    {
        // Map of users' logins and users
        private readonly IDictionary<string, User> _users;

        private DBManager()
        {
            _users = DeserializeDB();
        }

        // Returns a user with specified login or null if one does not exist
        internal User GetUserByLogin(string login)
        {
            return UserExists(login) ? _users[login] : null;
        }

        // Checks if a user with specified login exists
        internal bool UserExists(string login)
        {
            return _users.ContainsKey(login);
        }

        // Checks if specified email exists
        internal bool EmailExists(string email)
        {
            return _users.FirstOrDefault(userEntry => userEntry.Value.Email == email).Key != null;
        }

        // Adds new user into the database
        internal void AddUser(User user)
        {
            _users.Add(user.Login, user);
            SaveChanges();
        }

        // Updates specified user in the database
        internal void UpdateUser(User user)
        {
            SaveChanges();
        }

        // Checks if deserialized user exists in the database
        internal User CheckCachedUser(User user)
        {
            if (user?.Login == null || !_users.ContainsKey(user.Login))
                return null;

            var userInStorage = _users[user.Login];
            if (userInStorage?.Password != null && userInStorage.Password == user.Password)
                return userInStorage;

            return null;
        }

        // Serializes the database to a file
        private void SaveChanges()
        {
            SerializationManager.Serialize(_users, FileFolderHelper.StorageFilePath);
        }

        // Deserializes the database from a file
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

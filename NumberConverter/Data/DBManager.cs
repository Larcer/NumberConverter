using System.Collections.Generic;
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
            
        }

        // Checks if user with the specified login exists
        public bool UserExists(string login)
        {
            return _users.ContainsKey(login);
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
        }
    }
}

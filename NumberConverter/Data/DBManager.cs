using System.Collections.Generic;
using Nameless.NumberConverter.Models;
using Nameless.NumberConverter.Tools;

namespace Nameless.NumberConverter.Data
{
    public class DBManager : SingletonBase<DBManager>
    {
        private readonly IDictionary<string, User> _users = new Dictionary<string, User>();

        private DBManager()
        {

        }

        public bool UserExists(string login)
        {
            return _users.ContainsKey(login);
        }

        public User GetUserByLogin(string login)
        {
            return _users.ContainsKey(login) ? _users[login] : null;
        }

        public void AddUser(User user)
        {
            _users.Add(user.Login, user);
        }
    }
}

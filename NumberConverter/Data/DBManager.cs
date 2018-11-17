using Nameless.NumberConverter.Models;

namespace Nameless.NumberConverter.Data
{
    internal static class DBManager
    {
        // Returns a user with specified login or null if one does not exist
        internal static User GetUserByLogin(string login)
        {
            return EntityWrapper.GetUserByLogin(login);
        }

        // Checks if a user with specified login exists
        internal static bool UserExists(string login)
        {
            return EntityWrapper.UserExists(login);
        }

        // Checks if specified email exists
        internal static bool EmailExists(string email)
        {
            return EntityWrapper.EmailExists(email);
        }

        // Adds new user into the database
        internal static void AddUser(User user)
        {
            EntityWrapper.AddUser(user);
        }

        // Updates specified user in the database
        internal static void UpdateUser(User user)
        {
            EntityWrapper.SaveUser(user);
        }

        // Checks if deserialized user exists in the database
        internal static User CheckCachedUser(User user)
        {
            if (user?.Login == null || !EntityWrapper.UserExists(user.Login))
                return null;

            var userInStorage = EntityWrapper.GetUserByLogin(user.Login);
            if (userInStorage?.Password != null && userInStorage.Password == user.Password)
                return userInStorage;

            return null;
        }

        // Adds specified request
        internal static void AddRequest(Request request)
        {
            EntityWrapper.AddRequest(request);
        }
    }
}

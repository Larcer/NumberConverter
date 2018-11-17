using System.Data.Entity;
using System.Linq;
using Nameless.NumberConverter.Models;

namespace Nameless.NumberConverter.Data
{
    internal static class EntityWrapper
    {
        // Returns a user with specified login or null if one does not exist
        internal static User GetUserByLogin(string login)
        {
            using (var context = new NumberConverterDBContext())
            {
                return context.Users.Include(u => u.Requests).FirstOrDefault(u => u.Login == login);
            }
        }

        // Checks if a user with specified login exists
        internal static bool UserExists(string login)
        {
            using (var context = new NumberConverterDBContext())
            {
                return context.Users.Any(u => u.Login == login);
            }
        }

        // Checks if specified email exists
        internal static bool EmailExists(string email)
        {
            using (var context = new NumberConverterDBContext())
            {
                return context.Users.Any(u => u.Email == email);
            }
        }

        // Adds new user into the database
        internal static void AddUser(User user)
        {
            using (var context = new NumberConverterDBContext())
            {
                context.Users.Add(user);
                context.SaveChanges();
            }
        }

        // Updates specified user in the database
        internal static void SaveUser(User user)
        {
            using (var context = new NumberConverterDBContext())
            {
                context.Entry(user).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        // Adds specified request
        internal static void AddRequest(Request request)
        {
            using (var context = new NumberConverterDBContext())
            {
                context.Requests.Add(request);
                context.SaveChanges();
            }
        }
    }
}

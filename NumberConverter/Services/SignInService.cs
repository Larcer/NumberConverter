using System;
using System.Security.Cryptography;
using Nameless.NumberConverter.Data;
using Nameless.NumberConverter.Models;
using Nameless.NumberConverter.Managers;
using Nameless.NumberConverter.Properties;

namespace Nameless.NumberConverter.Services
{
    // Handles sign in actions
    public class SignInService
    {
        // Authenticates user in the application
        public bool LoginUser(string login, string password)
        {
            var currentUser = DBManager.Instance.GetUserByLogin(login);
            if (currentUser == null)
            {
                MessageManager.UserMessage(string.Format(
                    Resources.SignIn_UserDoesNotExist, login));
                MessageManager.Log($"The user has tried to sign in with non existing login: {login}");

                return false;
            }

            try
            {
                if (!currentUser.PasswordMatches(password))
                {
                    MessageManager.UserMessage(Resources.SignIn_PasswordIsWrong);
                    MessageManager.Log($"The user \"{currentUser.Login}\" tried to sign in with not matching password");

                    return false;
                }

                /* Everything is ok, login user */
                SessionManager.Instance.StartSession(currentUser);
                SaveLastLoginDate(currentUser);

                MessageManager.UserMessage(Resources.SignIn_LoginSuccess);
                MessageManager.Log($"The user \"{currentUser.Login}\" has successfully logged in");
            }
            catch (CryptographicException ce)
            {
                MessageManager.UserMessage(string.Format(Resources.SignIn_FailedToDecryptPassword,
                    Environment.NewLine, ce.Message));
                MessageManager.Log($"Failed to decrypt password while checking matching password. User login: {currentUser.Login}", ce);
                
                return false;
            }

            return true;
        }

        // Updates last login user date
        private void SaveLastLoginDate(User user)
        {
            user.LastLoginDateTime = DateTime.Now;
            DBManager.Instance.UpdateUser(user);
        }
    }
}

using System;
using System.Diagnostics;
using System.Security.Cryptography;
using Nameless.NumberConverter.Data;
using Nameless.NumberConverter.Models;
using NumberConverter.Managers;
using NumberConverter.Properties;

namespace Nameless.NumberConverter.Services
{
    // Handles sign in actions
    public class SignInService
    {
        // Authenticates user in the application
        public bool LoginUser(string login, string password)
        {
            /* Check user existence first */
            Debug.Assert(login != null);
            User currentUser = DBManager.Instance.GetUserByLogin(login); // Throws an exception only when login == null
            if (currentUser == null)
            {
                MessageManager.UserMessage(string.Format(
                    Resources.SignIn_UserDoesNotExist, login));
                return false;
            }

            /* Then check password */
            Debug.Assert(password != null);
            try
            {
                if (!currentUser.PasswordMatches(password))
                {
                    MessageManager.UserMessage(Resources.SignIn_PasswordIsWrong);
                    return false;
                }

                /* Everything is ok, login user */
                SessionManager.Instance.StartSession(currentUser);
                SaveLastLoginDate(currentUser);
                MessageManager.UserMessage(Resources.SignIn_LoginSuccess);
            }
            catch (CryptographicException ce)
            {
                MessageManager.UserMessage(string.Format(Resources.SignIn_FailedToDecryptPassword,
                    Environment.NewLine, ce.Message));
                MessageManager.UserMessage(ce.StackTrace);

                // Log the message into some error.log file

                return false;
            }

            return true;
        }

        private void SaveLastLoginDate(User user)
        {
            user.LastLoginDateTime = DateTime.Now;
        }
    }
}

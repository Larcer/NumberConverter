using System.ComponentModel.DataAnnotations;

using Nameless.NumberConverter.Data;
using Nameless.NumberConverter.Models;
using Nameless.NumberConverter.Managers;
using Nameless.NumberConverter.Properties;

namespace Nameless.NumberConverter.Services
{
    // Handles sign up actions
    public class SignUpService
    {
        // Signs up a user
        public bool SignUp(User user)
        {
            if (!EmailIsValid(user.Email))
            {
                MessageManager.UserMessage(Resources.SignUp_EmailIsNotValid);
                MessageManager.Log($"The user has failed to sign up because of not valid email: {user.Email}");

                return false;
            }

            /* Check email existence */
            if (DBManager.Instance.EmailExists(user.Email))
            {
                MessageManager.UserMessage(string.Format(
                    Resources.SignUp_EmailAlreadyExists, user.Email));
                MessageManager.Log($"The user has failed to sign up because of already existing email: {user.Email}");

                return false;
            }


            /* Check login existence */
            if (DBManager.Instance.UserExists(user.Login))
            {
                MessageManager.UserMessage(string.Format(
                    Resources.SignUp_UserAlreadyExists, user.Login));
                MessageManager.Log($"The user has failed to sign up because of already existing login: {user.Login}");

                return false;
            }

            DBManager.Instance.AddUser(user);
            MessageManager.Log($"New user \"{user.Login}\" was signed up");

            return true;
        }

        // Returns true if the specified string is a valid email
        private bool EmailIsValid(string email)
        {
            return new EmailAddressAttribute().IsValid(email);
        }
    }
}

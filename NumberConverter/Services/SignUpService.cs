using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

using Nameless.NumberConverter.Data;
using Nameless.NumberConverter.Models;
using NumberConverter.Managers;
using NumberConverter.Properties;

namespace NumberConverter.Services
{
    // Handles sign up actions
    public class SignUpService
    {
        // Signs up a user
        public bool SignUp(User user)
        {
            CheckNotNullFields(user);

            if (!EmailIsValid(user.Email))
            {
                MessageManager.UserMessage(Resources.SignUp_EmailIsNotValid);
                return false;
            }

            /* Check login existence first */
            if (DBManager.Instance.UserExists(user.Login))
            {
                MessageManager.UserMessage(string.Format(
                    Resources.SignUp_UserAlreadyExists, user.Login));

                return false;
            }

            DBManager.Instance.AddUser(user);

            return true;
        }

        private void CheckNotNullFields(User user)
        {
            Debug.Assert(!IsNull(user));
            Debug.Assert(!IsNull(user.FirstName));
            Debug.Assert(!IsNull(user.LastName));
            Debug.Assert(!IsNull(user.Login));
            Debug.Assert(!IsNull(user.Email));
            Debug.Assert(!IsNull(user.Password));
        }

        private bool EmailIsValid(string email)
        {
            return new EmailAddressAttribute().IsValid(email);
        }

        private bool IsNull(object o)
        {
            return o == null;
        }
    }
}

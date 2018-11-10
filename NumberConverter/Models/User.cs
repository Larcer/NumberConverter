using System;
using System.Collections.Generic;

using Nameless.NumberConverter.Tools;

namespace Nameless.NumberConverter.Models
{
    [Serializable]
    public class User
    {
        public Guid Guid { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public string Login { get; }
        public string Email { get; }
        public string Password { get; }
        public DateTime LastLoginDateTime { get; set; }
        public IList<Request> Requests { get; }

        public User(string firstName, string lastName,
            string login, string email, string password)
        {
            Guid = Guid.NewGuid();
            FirstName = firstName;
            LastName = lastName;
            Login = login;
            Email = email;
            Password = EncryptPassword(password);
            Requests = new List<Request>();
        }

        // Checks if original password matches passed password
        public bool PasswordMatches(string password)
        {
            string encryptedPassword = EncryptPassword(password);
            return Password.Equals(encryptedPassword);
        }

        // Returns encrypted password
        private string EncryptPassword(string password)
        {
            return SimpleEncryptor.EncryptText(password);
        }

        public override string ToString()
        {
            return $"{LastName} {FirstName}";
        }
    }
}

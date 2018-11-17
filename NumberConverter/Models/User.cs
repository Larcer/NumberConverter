using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using Nameless.NumberConverter.Tools;

namespace Nameless.NumberConverter.Models
{
    [Serializable]
    public class User
    {
        public Guid Guid { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime LastLoginDateTime { get; set; }
        public IList<Request> Requests { get; set; }

        public User()
        {

        }

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

        // Checks if original password matches passed password.
        // Passed password should not be encoded
        internal bool PasswordMatches(string password)
        {
            var encryptedPassword = EncryptPassword(password);
            return Password == encryptedPassword;
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

        public class UserEntityConfiguration : EntityTypeConfiguration<User>
        {
            public UserEntityConfiguration()
            {
                ToTable("Users");
                HasKey(u => u.Guid);

                Property(u => u.Guid)
                    .HasColumnName("Guid")
                    .IsRequired();
                Property(u => u.FirstName)
                    .HasColumnName("FirstName")
                    .IsRequired();
                Property(u => u.LastName)
                    .HasColumnName("LastName")
                    .IsRequired();
                Property(u => u.Login)
                    .HasColumnName("Login")
                    .IsRequired();
                Property(u => u.Email)
                    .HasColumnName("Email")
                    .IsRequired();
                Property(u => u.Password)
                    .HasColumnName("Password")
                    .IsRequired();
                Property(u => u.LastLoginDateTime)
                    .HasColumnName("LastLoginDateTime")
                    .IsRequired()
                    .HasColumnType("datetime2");
            }
        }
    }
}

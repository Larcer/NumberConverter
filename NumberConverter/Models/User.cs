using System;
using System.Collections.Generic;

using Nameless.NumberConverter.Tools;

namespace Nameless.NumberConverter.Models
{

    public class User
    {
        // Private key to decrypt password
        private const string PrivateKey = "<RSAKeyValue><Modulus>sr4l8EwMgPqPhRTK+dPTwGs9uYhDeUohSRL48ZDf85/5/Lo469WltKFaQvA2Msy92xq14YPPN6p6mQD7stVbinRd+hihnYoqfclYRMe+FG6jqu/QACl0N6JgwM7iKGiyzBjL1vkvcSoaqwbxD8QPHgrdNlkyP0z6Vz7j79PFEos=</Modulus><Exponent>AQAB</Exponent><P>6OkjEhjlvbDCuOl8e0Ep2zACTxkfSta8WFBmdvrinhQSowkT5xDXL0EFa/Z03XNUmjJ0xGe1aNCgG+6dDpTnSw==</P><Q>xHZTH4hXAv7uJsb/VHrcYOM5l4AyC+OxP7bhmAoGJGf4TpPxh+B0RhMxssrkc1d/72TIfRpuPbSLEqkqCSk5wQ==</Q><DP>SKFzK1CSTB4UCv/crr76Y3zMK4hlBryCDXQ9D7ta8frGeQr6puLMh9LZ8vnvJaOybUdwvFKu8pmkZDF7zrFGkw==</DP><DQ>J3ZNBAxyzds/IvLd3q4/DgcWTmQlqVW3CMFHVy7MRQvNSJtW7KAdOuYoGW2/rZtpy0BHNTnV4vcc6EaqduSdAQ==</DQ><InverseQ>4/jjapjJHdDqr5FG5a29ISgO6mRnjty6nrOisPNDi4336JdEKfAdtZvDUQoBAwKsV0oMvJ9RtPB2tS0hf5i8pA==</InverseQ><D>qcnyY/b5kbNxjasYvIQ5i3jTY2BLJ/YA9FcvXtiNw/DdGPMUiwGhrJnxEdD4yvyuBGm1CAmbV3d7icfjUBdYIe9VaZqPQ2FgYzI5DbB401+4z6Di7uKBVajLIOawlnufW4+K68T0EAFO2l9eo1RcU66W921G/pz6hObeUXt65QE=</D></RSAKeyValue>";
        // Public key to encrypt password
        private const string PublicKey = "<RSAKeyValue><Modulus>sr4l8EwMgPqPhRTK+dPTwGs9uYhDeUohSRL48ZDf85/5/Lo469WltKFaQvA2Msy92xq14YPPN6p6mQD7stVbinRd+hihnYoqfclYRMe+FG6jqu/QACl0N6JgwM7iKGiyzBjL1vkvcSoaqwbxD8QPHgrdNlkyP0z6Vz7j79PFEos=</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>";

        public Guid Guid { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Login { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }
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
            LastLoginDateTime = DateTime.Now;
            Requests = new List<Request>();
        }

        // Checks if original password matches passed password
        public bool PasswordMatches(string password)
        {
            try
            {
                string actualPasswordMd5 = Encrypting.DecryptString(Password, PrivateKey);
                string passedPasswordMd5 = Encrypting.GetMd5HashForString(password);

                return actualPasswordMd5.Equals(passedPasswordMd5);
            }
            catch (Exception)
            {
                return false;
            }
        }

        // Returns password encrypted with md5 and the public key
        private string EncryptPassword(string password)
        {
            return Encrypting.EncryptText(password, PublicKey);
        }

        public override string ToString()
        {
            return $"{LastName} {FirstName}";
        }
    }
}

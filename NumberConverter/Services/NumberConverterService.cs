using System;
using System.Collections.Generic;
using System.Text;
using Nameless.NumberConverter.Data;
using Nameless.NumberConverter.Models;
using Nameless.NumberConverter.Managers;
using Nameless.NumberConverter.Properties;

namespace Nameless.NumberConverter.Services
{
    // Handles numbers converting actions
    public class NumberConverterService
    {
        private readonly IList<(string, int)> _romanNumbers;

        public NumberConverterService()
        {
            _romanNumbers = new List<(string, int)>()
            {
                ( "M", 1000 ), ( "CM", 900 ), ( "D", 500 ),
                ( "CD", 400 ), ( "C", 100 ), ( "XC", 90 ),
                ( "L", 50 ), ( "XL", 40 ), ( "X", 10 ),
                ( "IX", 9 ), ( "V", 5 ), ( "IV", 4 ),
                ( "I", 1 )
            };
        }

        // Tries to convert string representation of a number to its unsigned integer representation.
        // Returns true if the conversion was successful or false otherwise.
        // Writes result into variable 'result'. If conversion was not successful, 0 will be written
        public bool TryConvertToUintNumber(string number, out int result)
        {
            result = 0;
            try
            {
                result = Convert.ToInt32(number);
                if (result < 0)
                    throw new FormatException();
            
                return true;
            }
            catch (OverflowException)
            {
                MessageManager.UserMessage(string.Format(Resources.NumberConverter_NumberOutOfRange, uint.MinValue, uint.MaxValue));
                MessageManager.Log($"The user \"{SessionManager.Instance.CurrentUser.Login}\" has typed a number that is out of range: {number}");
            }
            catch (FormatException)
            {
                MessageManager.UserMessage(Resources.NumberConverter_PositiveIntegerNumber);
                MessageManager.Log($"The user \"{SessionManager.Instance.CurrentUser.Login}\" has typed not a number: {number}");
            }

            return false;
        }

        // Converts specified arabic number to roman number
        public string ExecuteConversion(int number, out Request request)
        {
            string romanRepresentation = ToRoman(number);
            User user = SessionManager.Instance.CurrentUser;

            request = new Request(number, romanRepresentation);
            request.UserGuid = user.Guid;
            DBManager.AddRequest(request);
            user.Requests.Add(request);

            MessageManager.Log($"The user \"{SessionManager.Instance.CurrentUser.Login}\" has executed number conversion: {request}");

            return romanRepresentation;
        }

        // Converts arabic number to roman number of type string
        private string ToRoman(int arabicNumber)
        {
            var romanNumberBuilder = new StringBuilder();
            while (arabicNumber != 0)
            {
                foreach (var (representation, number) in _romanNumbers)
                {
                    if (arabicNumber >= number)
                    {
                        romanNumberBuilder.Append(representation);
                        arabicNumber -= number;

                        break;
                    }
                }
            }

            return romanNumberBuilder.ToString();
        }

        // Returns list of user requests
        public IList<Request> GetCurrentUserRequests()
        {
            var user = SessionManager.Instance.CurrentUser;
            MessageManager.Log($"The user \"{user.Login}\" has opened previous requests");

            return user.Requests;
        }

        public void LogOut()
        {
            MessageManager.Log($"The user \"{SessionManager.Instance.CurrentUser.Login}\" has logged out");
            SessionManager.Instance.EndSession();
        }
    }
}

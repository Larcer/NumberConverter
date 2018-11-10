using System;
using System.Collections.Generic;
using System.Text;

using Nameless.NumberConverter.Data;
using Nameless.NumberConverter.Models;
using Nameless.NumberConverter.Managers;
using Nameless.NumberConverter.Properties;

namespace Nameless.NumberConverter.Services
{
    public class NumberConverterService
    {
        private readonly IList<(string, uint)> _romanNumbers;

        public NumberConverterService()
        {
            _romanNumbers = new List<(string, uint)>()
            {
                ( "M", 1000 ), ( "CM", 900 ), ( "D", 500 ),
                ( "CD", 400 ), ( "C", 100 ), ( "XC", 90 ),
                ( "L", 50 ), ( "XL", 40 ), ( "X", 10 ),
                ( "IX", 9 ), ( "V", 5 ), ( "IV", 4 ),
                ( "I", 1 )
            };
        }

        public bool TryConvertToUintNumber(string number, out uint result)
        {
            result = 0;
            try
            {
                result = Convert.ToUInt32(number);
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

        public string ExecuteConversion(uint number, out Request request)
        {
            string romanRepresentation = ToRoman(number);
            request = new Request(number, romanRepresentation);

            SessionManager.Instance.CurrentUser.Requests.Add(request);
            DBManager.Instance.UpdateUser(SessionManager.Instance.CurrentUser);
            MessageManager.Log($"The user \"{SessionManager.Instance.CurrentUser.Login}\" has executed number conversion: {request}");

            return romanRepresentation;
        }

        private string ToRoman(uint arabicNumber)
        {
            StringBuilder romanNumber = new StringBuilder();
            while (arabicNumber != 0)
            {
                foreach (var (representation, number) in _romanNumbers)
                {
                    if (arabicNumber >= number)
                    {
                        romanNumber.Append(representation);
                        arabicNumber -= number;
                        break;
                    }
                }
            }

            return romanNumber.ToString();
        }

        public IList<Request> GetCurrentUserRequests()
        {
            User user = SessionManager.Instance.CurrentUser;
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

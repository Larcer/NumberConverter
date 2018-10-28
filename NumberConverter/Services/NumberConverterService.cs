﻿using System;
using System.Collections.Generic;
using System.Text;
using Nameless.NumberConverter.Models;
using NumberConverter.Managers;

namespace NumberConverter.Services
{
    public class NumberConverterService
    {
        private IList<(string, uint)> _romanNumbers;

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
                MessageManager.UserMessage("The number should not be between " + uint.MinValue + " and " + uint.MaxValue);
            }
            catch (FormatException)
            {
                MessageManager.UserMessage("Type in the positive integer arabic number");
            }

            return false;
        }

        public string ExecuteConvertion(uint number)
        {
            string romanRepresentation = ToRoman(number);
            Request request = new Request(number, romanRepresentation);
            SessionManager.Instance.CurrentUser.Requests.Add(request);
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
            return user.Requests;
        }
    }
}
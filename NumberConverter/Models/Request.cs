using System;

namespace Nameless.NumberConverter.Models
{
    public class Request
    {
        public Guid Guid;
        public uint ArabicNumber;
        public string RomanNumber;
        public DateTime RequestDateTime;

        public Request(uint arabicNumber, string romanNumber)
        {
            Guid = Guid.NewGuid();
            ArabicNumber = arabicNumber;
            RomanNumber = romanNumber;
            RequestDateTime = DateTime.Now;
        }

        public override string ToString()
        {
            return $"{ArabicNumber} to {RomanNumber} at {RequestDateTime}";
        }
    }
}

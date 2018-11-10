using System;

namespace Nameless.NumberConverter.Models
{
    [Serializable]
    public class Request
    {
        public Guid Guid { get; }
        public uint ArabicNumber { get; }
        public string RomanNumber { get; }
        public DateTime RequestDateTime { get; }

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

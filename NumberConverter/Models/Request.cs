using System;
using System.Data.Entity.ModelConfiguration;

namespace Nameless.NumberConverter.Models
{
    [Serializable]
    public class Request
    {
        public Guid Guid { get; set; }
        public int ArabicNumber { get; set; }
        public string RomanNumber { get; set; }
        public DateTime RequestDateTime { get; set; }
        public Guid UserGuid { get; set; }
        public User User { get; set; }

        public Request()
        {

        }

        public Request(int arabicNumber, string romanNumber)
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

        public class RequestEntityConfiguration : EntityTypeConfiguration<Request>
        {
            public RequestEntityConfiguration()
            {
                ToTable("Requests");
                HasKey(r => r.Guid);

                Property(r => r.Guid)
                    .HasColumnName("Guid")
                    .IsRequired();
                Property(r => r.ArabicNumber)
                    .HasColumnName("ArabicNumber")
                    .IsRequired();
                Property(r => r.RomanNumber)
                    .HasColumnName("RomanNumber")
                    .IsRequired();
                Property(r => r.RequestDateTime)
                    .HasColumnName("RequestDateTime")
                    .IsRequired()
                    .HasColumnType("datetime2");

                HasRequired(r => r.User)
                    .WithMany(u => u.Requests)
                    .HasForeignKey(r => r.UserGuid)
                    .WillCascadeOnDelete(true);
            }
        }
    }
}

using Nameless.NumberConverter.Models;
using System.Data.Entity;
using Nameless.NumberConverter.Migrations;

namespace Nameless.NumberConverter.Data
{
    internal class NumberConverterDBContext : DbContext
    {
        public NumberConverterDBContext() : base("NewNumberConverterDB")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<NumberConverterDBContext, Configuration>(true));
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Request> Requests { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new User.UserEntityConfiguration());
            modelBuilder.Configurations.Add(new Request.RequestEntityConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}

namespace Nameless.NumberConverter.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NumberConverter : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Requests",
                c => new
                    {
                        Guid = c.Guid(nullable: false),
                        ArabicNumber = c.Int(nullable: false),
                        RomanNumber = c.String(nullable: false),
                        RequestDateTime = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UserGuid = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Guid)
                .ForeignKey("dbo.Users", t => t.UserGuid, cascadeDelete: true)
                .Index(t => t.UserGuid);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Guid = c.Guid(nullable: false),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        Login = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        Password = c.String(nullable: false),
                        LastLoginDateTime = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Guid);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Requests", "UserGuid", "dbo.Users");
            DropIndex("dbo.Requests", new[] { "UserGuid" });
            DropTable("dbo.Users");
            DropTable("dbo.Requests");
        }
    }
}

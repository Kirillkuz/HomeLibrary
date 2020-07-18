namespace FirstWPF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddrelationManytoMany : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Books",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Author = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserProfileBooks",
                c => new
                    {
                        UserProfile_Id = c.Int(nullable: false),
                        Book_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserProfile_Id, t.Book_Id })
                .ForeignKey("dbo.UserProfiles", t => t.UserProfile_Id, cascadeDelete: true)
                .ForeignKey("dbo.Books", t => t.Book_Id, cascadeDelete: true)
                .Index(t => t.UserProfile_Id)
                .Index(t => t.Book_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserProfileBooks", "Book_Id", "dbo.Books");
            DropForeignKey("dbo.UserProfileBooks", "UserProfile_Id", "dbo.UserProfiles");
            DropIndex("dbo.UserProfileBooks", new[] { "Book_Id" });
            DropIndex("dbo.UserProfileBooks", new[] { "UserProfile_Id" });
            DropTable("dbo.UserProfileBooks");
            DropTable("dbo.Books");
        }
    }
}

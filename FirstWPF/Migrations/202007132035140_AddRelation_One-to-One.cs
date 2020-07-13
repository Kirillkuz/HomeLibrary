namespace FirstWPF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRelation_OnetoOne : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserProfiles",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Name = c.String(),
                        Age = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.LoginInfoes", t => t.Id)
                .Index(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserProfiles", "Id", "dbo.LoginInfoes");
            DropIndex("dbo.UserProfiles", new[] { "Id" });
            DropTable("dbo.UserProfiles");
        }
    }
}

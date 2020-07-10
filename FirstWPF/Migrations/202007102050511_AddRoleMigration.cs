namespace FirstWPF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRoleMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RoleName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.LoginInfoes", "Role_Id", c => c.Int());
            CreateIndex("dbo.LoginInfoes", "Role_Id");
            AddForeignKey("dbo.LoginInfoes", "Role_Id", "dbo.Roles", "Id");
            DropColumn("dbo.LoginInfoes", "Role");
        }
        
        public override void Down()
        {
            AddColumn("dbo.LoginInfoes", "Role", c => c.String());
            DropForeignKey("dbo.LoginInfoes", "Role_Id", "dbo.Roles");
            DropIndex("dbo.LoginInfoes", new[] { "Role_Id" });
            DropColumn("dbo.LoginInfoes", "Role_Id");
            DropTable("dbo.Roles");
        }
    }
}

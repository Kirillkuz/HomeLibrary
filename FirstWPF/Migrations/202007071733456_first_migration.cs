namespace FirstWPF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class first_migration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.LoginInfoes", "Role", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.LoginInfoes", "Role");
        }
    }
}

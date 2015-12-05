namespace Domainclasses.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sd : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Products", "imege");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "imege", c => c.String(nullable: false));
        }
    }
}

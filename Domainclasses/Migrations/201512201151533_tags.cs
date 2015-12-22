namespace Domainclasses.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tags : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProductCategories", "Tags", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ProductCategories", "Tags");
        }
    }
}

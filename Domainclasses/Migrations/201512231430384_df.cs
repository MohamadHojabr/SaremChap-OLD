namespace Domainclasses.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class df : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Chapters", "Tags", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Chapters", "Tags");
        }
    }
}

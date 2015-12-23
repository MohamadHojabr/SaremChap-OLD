namespace Domainclasses.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class bd : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Subjects", "Tags", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Subjects", "Tags");
        }
    }
}

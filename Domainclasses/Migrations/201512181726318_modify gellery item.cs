namespace Domainclasses.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modifygelleryitem : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.GalleryItems", "FileName", c => c.String());
            AddColumn("dbo.GalleryItems", "FileType", c => c.Int(nullable: false));
            DropColumn("dbo.GalleryItems", "url");
        }
        
        public override void Down()
        {
            AddColumn("dbo.GalleryItems", "url", c => c.String(nullable: false));
            DropColumn("dbo.GalleryItems", "FileType");
            DropColumn("dbo.GalleryItems", "FileName");
        }
    }
}

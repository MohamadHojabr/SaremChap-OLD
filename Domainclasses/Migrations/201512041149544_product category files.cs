namespace Domainclasses.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class productcategoryfiles : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProductFiles",
                c => new
                    {
                        FileId = c.Int(nullable: false, identity: true),
                        FileName = c.String(maxLength: 255),
                        FileType = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.FileId);
            
            CreateTable(
                "dbo.ProductCategoryFiles",
                c => new
                    {
                        ProductCategoryFileId = c.Int(nullable: false, identity: true),
                        FileName = c.String(maxLength: 255),
                        FileType = c.Int(nullable: false),
                        ProductCategoryID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ProductCategoryFileId)
                .ForeignKey("dbo.ProductCategories", t => t.ProductCategoryID, cascadeDelete: true)
                .Index(t => t.ProductCategoryID);
            
            DropColumn("dbo.ProductCategories", "imege");
            DropTable("dbo.Files");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Files",
                c => new
                    {
                        FileId = c.Int(nullable: false, identity: true),
                        FileName = c.String(maxLength: 255),
                        FileType = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.FileId);
            
            AddColumn("dbo.ProductCategories", "imege", c => c.String(nullable: false));
            DropForeignKey("dbo.ProductCategoryFiles", "ProductCategoryID", "dbo.ProductCategories");
            DropIndex("dbo.ProductCategoryFiles", new[] { "ProductCategoryID" });
            DropTable("dbo.ProductCategoryFiles");
            DropTable("dbo.ProductFiles");
        }
    }
}

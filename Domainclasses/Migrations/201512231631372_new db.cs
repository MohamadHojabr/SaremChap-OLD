namespace Domainclasses.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newdb : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SubjectFiles",
                c => new
                    {
                        FileId = c.Int(nullable: false, identity: true),
                        FileName = c.String(maxLength: 255),
                        FileType = c.Int(nullable: false),
                        SubjectId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.FileId)
                .ForeignKey("dbo.Subjects", t => t.SubjectId, cascadeDelete: true)
                .Index(t => t.SubjectId);
            
            CreateTable(
                "dbo.ProductFiles",
                c => new
                    {
                        FileId = c.Int(nullable: false, identity: true),
                        FileName = c.String(maxLength: 255),
                        FileType = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.FileId)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId);
            
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
            
            AddColumn("dbo.Chapters", "Tags", c => c.String());
            AddColumn("dbo.Subjects", "Tags", c => c.String());
            AddColumn("dbo.Products", "Tags", c => c.String());
            AddColumn("dbo.ProductCategories", "Tags", c => c.String());
            AddColumn("dbo.GalleryItems", "FileName", c => c.String());
            AddColumn("dbo.GalleryItems", "FileType", c => c.Int(nullable: false));
            DropColumn("dbo.Subjects", "SubjectImage");
            DropColumn("dbo.Products", "imege");
            DropColumn("dbo.ProductCategories", "imege");
            DropColumn("dbo.GalleryItems", "url");
        }
        
        public override void Down()
        {
            AddColumn("dbo.GalleryItems", "url", c => c.String(nullable: false));
            AddColumn("dbo.ProductCategories", "imege", c => c.String(nullable: false));
            AddColumn("dbo.Products", "imege", c => c.String(nullable: false));
            AddColumn("dbo.Subjects", "SubjectImage", c => c.String(nullable: false));
            DropForeignKey("dbo.ProductCategoryFiles", "ProductCategoryID", "dbo.ProductCategories");
            DropForeignKey("dbo.ProductFiles", "ProductId", "dbo.Products");
            DropForeignKey("dbo.SubjectFiles", "SubjectId", "dbo.Subjects");
            DropIndex("dbo.ProductCategoryFiles", new[] { "ProductCategoryID" });
            DropIndex("dbo.ProductFiles", new[] { "ProductId" });
            DropIndex("dbo.SubjectFiles", new[] { "SubjectId" });
            DropColumn("dbo.GalleryItems", "FileType");
            DropColumn("dbo.GalleryItems", "FileName");
            DropColumn("dbo.ProductCategories", "Tags");
            DropColumn("dbo.Products", "Tags");
            DropColumn("dbo.Subjects", "Tags");
            DropColumn("dbo.Chapters", "Tags");
            DropTable("dbo.ProductCategoryFiles");
            DropTable("dbo.ProductFiles");
            DropTable("dbo.SubjectFiles");
        }
    }
}

namespace Domainclasses.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addsubjectfiles : DbMigration
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
            
            DropColumn("dbo.Subjects", "SubjectImage");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Subjects", "SubjectImage", c => c.String(nullable: false));
            DropForeignKey("dbo.SubjectFiles", "SubjectId", "dbo.Subjects");
            DropIndex("dbo.SubjectFiles", new[] { "SubjectId" });
            DropTable("dbo.SubjectFiles");
        }
    }
}

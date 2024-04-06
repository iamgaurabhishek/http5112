namespace MyPassionProjectW2024n01605783.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Blog_Workout_collabs : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Blogs",
                c => new
                    {
                        BlogId = c.Int(nullable: false, identity: true),
                        BlogHeading = c.String(),
                        BlogContent = c.String(),
                        BlogShortDescription = c.String(),
                        BlogFeaturedImageUrl = c.String(),
                        BlogPublishedDate = c.DateTime(nullable: false),
                        BlogAuthor = c.String(),
                        TagId = c.Int(nullable: false),
                        CommentId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.BlogId)
                .ForeignKey("dbo.Comments", t => t.CommentId, cascadeDelete: true)
                .ForeignKey("dbo.Tags", t => t.TagId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.TagId)
                .Index(t => t.CommentId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        CommentId = c.Int(nullable: false, identity: true),
                        CommentDescription = c.String(),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CommentId)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Tags",
                c => new
                    {
                        TagId = c.Int(nullable: false, identity: true),
                        TagName = c.String(),
                    })
                .PrimaryKey(t => t.TagId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Blogs", "UserId", "dbo.Users");
            DropForeignKey("dbo.Blogs", "TagId", "dbo.Tags");
            DropForeignKey("dbo.Blogs", "CommentId", "dbo.Comments");
            DropForeignKey("dbo.Comments", "UserId", "dbo.Users");
            DropIndex("dbo.Comments", new[] { "UserId" });
            DropIndex("dbo.Blogs", new[] { "UserId" });
            DropIndex("dbo.Blogs", new[] { "CommentId" });
            DropIndex("dbo.Blogs", new[] { "TagId" });
            DropTable("dbo.Tags");
            DropTable("dbo.Comments");
            DropTable("dbo.Blogs");
        }
    }
}

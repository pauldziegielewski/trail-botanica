namespace Collab_Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class traildesign : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.TrailFeatures", newName: "FeatureTrails");
            DropPrimaryKey("dbo.FeatureTrails");
            CreateTable(
                "dbo.Designs",
                c => new
                    {
                        Design_Id = c.Int(nullable: false, identity: true),
                        Architect_Name = c.String(),
                        Description = c.String(),
                        Drawing_Plan = c.String(),
                        TrailID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Design_Id)
                .ForeignKey("dbo.Trails", t => t.TrailID, cascadeDelete: true)
                .Index(t => t.TrailID);
            
            AddPrimaryKey("dbo.FeatureTrails", new[] { "Feature_FeatureID", "Trail_TrailID" });
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Designs", "TrailID", "dbo.Trails");
            DropIndex("dbo.Designs", new[] { "TrailID" });
            DropPrimaryKey("dbo.FeatureTrails");
            DropTable("dbo.Designs");
            AddPrimaryKey("dbo.FeatureTrails", new[] { "Trail_TrailID", "Feature_FeatureID" });
            RenameTable(name: "dbo.FeatureTrails", newName: "TrailFeatures");
        }
    }
}

namespace Collab_Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Features",
                c => new
                    {
                        FeatureID = c.Int(nullable: false, identity: true),
                        FeatureName = c.String(),
                    })
                .PrimaryKey(t => t.FeatureID);
            
            CreateTable(
                "dbo.Trails",
                c => new
                    {
                        TrailID = c.Int(nullable: false, identity: true),
                        TrailName = c.String(),
                        LocationID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TrailID)
                .ForeignKey("dbo.Locations", t => t.LocationID, cascadeDelete: true)
                .Index(t => t.LocationID);
            
            CreateTable(
                "dbo.Locations",
                c => new
                    {
                        LocationID = c.Int(nullable: false, identity: true),
                        LocationName = c.String(),
                    })
                .PrimaryKey(t => t.LocationID);
            
            CreateTable(
                "dbo.Plants",
                c => new
                    {
                        Plant_Id = c.Int(nullable: false, identity: true),
                        Plant_Name = c.String(),
                        Plant_Type = c.String(),
                        Plant_Image = c.String(),
                    })
                .PrimaryKey(t => t.Plant_Id);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.TrailFeatures",
                c => new
                    {
                        Trail_TrailID = c.Int(nullable: false),
                        Feature_FeatureID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Trail_TrailID, t.Feature_FeatureID })
                .ForeignKey("dbo.Trails", t => t.Trail_TrailID, cascadeDelete: true)
                .ForeignKey("dbo.Features", t => t.Feature_FeatureID, cascadeDelete: true)
                .Index(t => t.Trail_TrailID)
                .Index(t => t.Feature_FeatureID);
            
            CreateTable(
                "dbo.PlantTrails",
                c => new
                    {
                        Plant_Plant_Id = c.Int(nullable: false),
                        Trail_TrailID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Plant_Plant_Id, t.Trail_TrailID })
                .ForeignKey("dbo.Plants", t => t.Plant_Plant_Id, cascadeDelete: true)
                .ForeignKey("dbo.Trails", t => t.Trail_TrailID, cascadeDelete: true)
                .Index(t => t.Plant_Plant_Id)
                .Index(t => t.Trail_TrailID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.PlantTrails", "Trail_TrailID", "dbo.Trails");
            DropForeignKey("dbo.PlantTrails", "Plant_Plant_Id", "dbo.Plants");
            DropForeignKey("dbo.Trails", "LocationID", "dbo.Locations");
            DropForeignKey("dbo.TrailFeatures", "Feature_FeatureID", "dbo.Features");
            DropForeignKey("dbo.TrailFeatures", "Trail_TrailID", "dbo.Trails");
            DropIndex("dbo.PlantTrails", new[] { "Trail_TrailID" });
            DropIndex("dbo.PlantTrails", new[] { "Plant_Plant_Id" });
            DropIndex("dbo.TrailFeatures", new[] { "Feature_FeatureID" });
            DropIndex("dbo.TrailFeatures", new[] { "Trail_TrailID" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Trails", new[] { "LocationID" });
            DropTable("dbo.PlantTrails");
            DropTable("dbo.TrailFeatures");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Plants");
            DropTable("dbo.Locations");
            DropTable("dbo.Trails");
            DropTable("dbo.Features");
        }
    }
}

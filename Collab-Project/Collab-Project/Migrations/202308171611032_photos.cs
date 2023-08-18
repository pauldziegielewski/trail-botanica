namespace Collab_Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class photos : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Trails", "TrailHasPic", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Trails", "TrailHasPic");
        }
    }
}

namespace Collab_Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class trialpic : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Trails", "PicExtension", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Trails", "PicExtension");
        }
    }
}

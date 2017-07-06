namespace MediaStoreApi.Infrastructure.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class added_new_setting_MiniatureFolderName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Media", "MiniatureFolderName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Media", "MiniatureFolderName");
        }
    }
}

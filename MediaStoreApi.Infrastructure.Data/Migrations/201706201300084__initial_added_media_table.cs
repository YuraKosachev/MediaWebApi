namespace MediaStoreApi.Infrastructure.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _initial_added_media_table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Media",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        DateOfCreate = c.DateTime(nullable: false),
                        MediaType = c.String(),
                        FileExtension = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Media");
        }
    }
}

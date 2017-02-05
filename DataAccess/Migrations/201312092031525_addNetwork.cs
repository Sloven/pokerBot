namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addNetwork : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.NetworkRespawns",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 15),
                        Description = c.String(maxLength: 20),
                        Code = c.String(maxLength: 15),
                        NetworkStream = c.Binary(),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.NetworkRespawns");
        }
    }
}

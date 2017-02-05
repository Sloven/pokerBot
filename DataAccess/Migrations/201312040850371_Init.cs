namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BitVectors",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        ByteArray = c.Binary(maxLength: 4000),
                        Rank = c.Int(nullable: false),
                        Suit = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.BitVectors");
        }
    }
}

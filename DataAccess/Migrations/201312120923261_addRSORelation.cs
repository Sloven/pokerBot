namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addRSORelation : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RankSuitOutputRelations",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Rank = c.Int(nullable: false),
                        Suit = c.Int(nullable: false),
                        NetworkOutput = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.RankSuitOutputRelations");
        }
    }
}

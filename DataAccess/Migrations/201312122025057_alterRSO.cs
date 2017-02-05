namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class alterRSO : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BitVectors", "Owner", c => c.Int(nullable: false));
            AddColumn("dbo.RankSuitOutputRelations", "NetworkClass", c => c.Int(nullable: false));
            DropColumn("dbo.RankSuitOutputRelations", "NetworkOutput");
        }
        
        public override void Down()
        {
            AddColumn("dbo.RankSuitOutputRelations", "NetworkOutput", c => c.Int(nullable: false));
            DropColumn("dbo.RankSuitOutputRelations", "NetworkClass");
            DropColumn("dbo.BitVectors", "Owner");
        }
    }
}

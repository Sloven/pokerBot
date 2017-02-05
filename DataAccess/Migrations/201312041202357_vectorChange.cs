namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class vectorChange : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BitVectors", "CropedImg", c => c.Binary(maxLength: 4000));
        }
        
        public override void Down()
        {
            DropColumn("dbo.BitVectors", "CropedImg");
        }
    }
}

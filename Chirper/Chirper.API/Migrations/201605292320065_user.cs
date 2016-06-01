namespace Chirper.API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class user : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "ChirpName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "ChirpName");
        }
    }
}

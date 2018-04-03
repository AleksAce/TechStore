namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedMainpageBoolandDateTime : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "isForMainPage", c => c.Boolean(nullable: false));
            AddColumn("dbo.Products", "addedToMainPageDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "addedToMainPageDate");
            DropColumn("dbo.Products", "isForMainPage");
        }
    }
}

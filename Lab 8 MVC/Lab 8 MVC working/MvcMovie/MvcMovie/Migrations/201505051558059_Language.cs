namespace MvcMovie.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Language : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Movies", "Language", c => c.String(maxLength: 60));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Movies", "Language");
        }
    }
}

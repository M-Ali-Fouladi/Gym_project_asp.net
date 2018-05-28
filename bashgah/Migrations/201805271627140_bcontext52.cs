namespace bashgah.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class bcontext52 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.users", "birth", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.users", "birth");
        }
    }
}

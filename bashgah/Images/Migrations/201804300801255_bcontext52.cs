namespace bashgah.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class bcontext52 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.viewmodels", "CustomerId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.viewmodels", "CustomerId", c => c.Int(nullable: false));
        }
    }
}

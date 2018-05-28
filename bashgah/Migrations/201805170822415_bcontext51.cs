namespace bashgah.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class bcontext51 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.viewmodels", "birthdate", c => c.String());
            DropColumn("dbo.viewmodels", "family");
            DropColumn("dbo.viewmodels", "Idd");
            DropColumn("dbo.viewmodels", "date");
            DropColumn("dbo.viewmodels", "time");
        }
        
        public override void Down()
        {
            AddColumn("dbo.viewmodels", "time", c => c.String());
            AddColumn("dbo.viewmodels", "date", c => c.String());
            AddColumn("dbo.viewmodels", "Idd", c => c.Int(nullable: false));
            AddColumn("dbo.viewmodels", "family", c => c.String());
            DropColumn("dbo.viewmodels", "birthdate");
        }
    }
}

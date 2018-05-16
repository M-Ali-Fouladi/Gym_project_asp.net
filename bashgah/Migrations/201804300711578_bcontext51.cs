namespace bashgah.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class bcontext51 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.viewmodels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        name = c.String(),
                        family = c.String(),
                        Idd = c.Int(nullable: false),
                        date = c.String(),
                        time = c.String(),
                        CustomerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.viewmodels");
        }
    }
}

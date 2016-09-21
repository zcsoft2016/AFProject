namespace ProjetOrion.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class image : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Utilisateurs", "Image", c => c.Binary());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Utilisateurs", "Image");
        }
    }
}

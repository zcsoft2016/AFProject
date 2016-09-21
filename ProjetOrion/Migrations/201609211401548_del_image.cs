namespace ProjetOrion.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class del_image : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Utilisateurs", "Image");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Utilisateurs", "Image", c => c.Binary());
        }
    }
}

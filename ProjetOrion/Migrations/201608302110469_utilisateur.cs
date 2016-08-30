namespace ProjetOrion.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class utilisateur : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Utilisateurs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Pseudo = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        MotDePasse = c.String(nullable: false, maxLength: 100),
                        ConfirmerMotDePasse = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Utilisateurs");
        }
    }
}

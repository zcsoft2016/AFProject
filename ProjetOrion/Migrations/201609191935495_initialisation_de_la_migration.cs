namespace ProjetOrion.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initialisation_de_la_migration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Utilisateurs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Pseudo = c.String(nullable: false, unicode: false),
                        Email = c.String(nullable: false, unicode: false),
                        MotDePasse = c.String(nullable: false, maxLength: 100, storeType: "nvarchar"),
                        ConfirmerMotDePasse = c.String(unicode: false),
                        Photo = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Utilisateurs");
        }
    }
}

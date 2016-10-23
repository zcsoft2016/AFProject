namespace ProjetOrion.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modiffUser : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Utilisateurs", "ConfirmerMotDePasse", c => c.String(maxLength: 100, storeType: "nvarchar"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Utilisateurs", "ConfirmerMotDePasse", c => c.String(unicode: false));
        }
    }
}

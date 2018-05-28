namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FisrtMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Amigoes", "Liberado", c => c.Boolean(nullable: false));
            AddColumn("dbo.Emprestimoes", "Livre", c => c.Boolean(nullable: false));
            AddColumn("dbo.Emprestimoes", "Excluido", c => c.Boolean(nullable: false));
            AddColumn("dbo.Jogoes", "Liberado", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Jogoes", "Liberado");
            DropColumn("dbo.Emprestimoes", "Excluido");
            DropColumn("dbo.Emprestimoes", "Livre");
            DropColumn("dbo.Amigoes", "Liberado");
        }
    }
}

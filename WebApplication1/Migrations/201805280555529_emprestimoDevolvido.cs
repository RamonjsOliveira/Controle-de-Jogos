namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class emprestimoDevolvido : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Emprestimoes", "Devolvido", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Emprestimoes", "Devolvido");
        }
    }
}

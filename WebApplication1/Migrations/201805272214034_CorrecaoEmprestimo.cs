namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CorrecaoEmprestimo : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Emprestimoes", "Livre");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Emprestimoes", "Livre", c => c.Boolean(nullable: false));
        }
    }
}

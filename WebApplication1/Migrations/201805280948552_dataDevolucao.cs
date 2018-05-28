namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dataDevolucao : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Emprestimoes", "DataDevolucao", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Emprestimoes", "DataDevolucao");
        }
    }
}

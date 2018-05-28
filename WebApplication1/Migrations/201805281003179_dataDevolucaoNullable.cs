namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dataDevolucaoNullable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Emprestimoes", "DataDevolucao", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Emprestimoes", "DataDevolucao", c => c.DateTime(nullable: false));
        }
    }
}

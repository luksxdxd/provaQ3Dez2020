namespace encriptDecript.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TextoModel1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TextoModels", "CopyTextoss", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.TextoModels", "CopyTextoss");
        }
    }
}

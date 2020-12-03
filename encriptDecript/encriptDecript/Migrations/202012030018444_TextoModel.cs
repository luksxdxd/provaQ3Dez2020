namespace encriptDecript.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TextoModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TextoModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Textoss = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.TextoModels");
        }
    }
}

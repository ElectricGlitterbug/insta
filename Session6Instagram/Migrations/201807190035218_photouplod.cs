namespace Session6Instagram.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class photouplod : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Photos", "Picture", c => c.Binary());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Photos", "Picture", c => c.String());
        }
    }
}

namespace TvShowTracker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPosterUrl : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Shows", "PosterUrl", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Shows", "PosterUrl");
        }
    }
}

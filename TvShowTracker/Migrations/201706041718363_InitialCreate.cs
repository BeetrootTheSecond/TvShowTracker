namespace TvShowTracker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Episodes",
                c => new
                    {
                        EpisodeId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        EpisodeNumber = c.Int(nullable: false),
                        Overview = c.String(),
                        TmdbId = c.Int(nullable: false),
                        ReleaseDate = c.DateTime(),
                        Watched = c.Boolean(nullable: false),
                        Season_SeasonId = c.Int(),
                    })
                .PrimaryKey(t => t.EpisodeId)
                .ForeignKey("dbo.Seasons", t => t.Season_SeasonId)
                .Index(t => t.Season_SeasonId);
            
            CreateTable(
                "dbo.Shows",
                c => new
                    {
                        ShowId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Overview = c.String(),
                        TmdbId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ShowId);
            
            CreateTable(
                "dbo.Seasons",
                c => new
                    {
                        SeasonId = c.Int(nullable: false, identity: true),
                        SeasonNumber = c.Int(nullable: false),
                        EpisodeCount = c.Int(nullable: false),
                        TmdbId = c.Int(nullable: false),
                        Show_ShowId = c.Int(),
                    })
                .PrimaryKey(t => t.SeasonId)
                .ForeignKey("dbo.Shows", t => t.Show_ShowId)
                .Index(t => t.Show_ShowId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Seasons", "Show_ShowId", "dbo.Shows");
            DropForeignKey("dbo.Episodes", "Season_SeasonId", "dbo.Seasons");
            DropIndex("dbo.Seasons", new[] { "Show_ShowId" });
            DropIndex("dbo.Episodes", new[] { "Season_SeasonId" });
            DropTable("dbo.Seasons");
            DropTable("dbo.Shows");
            DropTable("dbo.Episodes");
        }
    }
}

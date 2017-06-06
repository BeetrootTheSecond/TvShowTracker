using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMDbLib.Client;
using TMDbLib.Objects.TvShows;

namespace TvShowTracker
{

    public class ShowDatabase : DbContext
    {
        public DbSet<Show> Shows { get; set; }
        public DbSet<Episode> Episodes { get; set; }
        public DbSet<Season> seasons { get; set; }
    }

    public class Show
    {
        public int ShowId { get; set; }
        public string Name { get; set; }
        public string Overview { get; set; }
        public int TmdbId { get; set; }

        public virtual List<Season> Seasons { get; set; }

        public Show()
        {
            Seasons = new List<Season>();
        }
    }

    public class Season
    {
        public int SeasonId { get; set; }

        public int SeasonNumber { get; set; }
        public int EpisodeCount { get; set; }
        public int TmdbId { get; set; }

        public virtual List<Episode> Episodes { get; set; }

        public Season()
        {
            Episodes = new List<Episode>();
        }

    }

    public class Episode
    {
        public int EpisodeId { get; set; }
        public string Name { get; set; }
        public int EpisodeNumber { get; set; }
        public string Overview { get; set; }
        public int TmdbId { get; set; }

        public DateTime? ReleaseDate { get; set; }


        public bool Watched { get; set; }

    }

    public class ShowData
    {
        public static ShowDatabase db = new ShowDatabase();
        private string apikey = "dc21e8c3cb3e79d39f776b428b13222c";
        public Show getShow(int TmdbID)
        {


            if (db.Shows.Where(x => x.TmdbId == TmdbID).Count() == 0)
            {

                TMDbClient client = new TMDbClient(apikey);

                TvShow tvshow = client.GetTvShowAsync(TmdbID).Result;

                List<Season> allSeason = new List<Season>();
                foreach (var season in tvshow.Seasons)
                {

                    List<Episode> allEpisode = new List<Episode>();

                    TvSeason tvseason = client.GetTvSeasonAsync(TmdbID, season.SeasonNumber).Result;

                    foreach (var episode in tvseason.Episodes)
                    {
                        Episode newEpisode = new Episode
                        {
                            Name = episode.Name,
                            EpisodeNumber = episode.EpisodeNumber,
                            Overview = episode.Overview,
                            TmdbId = TmdbID,
                            ReleaseDate = episode.AirDate,
                            Watched = false
                        };

                        allEpisode.Add(newEpisode);
                    }

                    Season newSeason = new Season
                    {
                        SeasonNumber = season.SeasonNumber,
                        TmdbId = TmdbID,
                        EpisodeCount = season.EpisodeCount,
                        Episodes = allEpisode
                    };
                    allSeason.Add(newSeason);
                }

                Show show = new Show { Name = tvshow.Name, TmdbId = TmdbID, Overview = tvshow.Overview, Seasons = allSeason };
                db.Shows.Add(show);
                db.SaveChanges();
            }
            
            return db.Shows.Where(x => x.TmdbId == TmdbID).FirstOrDefault();
        }



        public List<Show> getWatchlist()
        {

            //string query = "Select* FROM Shows Right JOIN Seasons ON Shows.ShowId = Seasons.Show_ShowId RIGHT JOIN Episodes ON Seasons.SeasonId = Episodes.Season_SeasonId WHERE Episodes.Watched = 0";
           // return db.Shows.SqlQuery(query).ToList();
            return db.Shows.Where(x =>
                x.Seasons.Where(y =>
                    y.Episodes.Where(z =>
                        z.Watched == false
                    ).Count() > 0
                ).Count() > 0
            ).ToList();
        }

        public void updateEpisode(int EpisodeId)
        {
            Episode currentEpisode = db.Episodes.Where(x => x.EpisodeId == EpisodeId).First();
            currentEpisode.Watched = true;

            db.SaveChanges();

        }


    }

}



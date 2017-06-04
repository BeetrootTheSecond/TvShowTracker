using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TMDbLib.Client;
using TMDbLib.Objects.TvShows;



namespace TvShowTracker.Pages
{
    /// <summary>
    /// Interaction logic for ShowsTreeView.xaml
    /// </summary>
    public partial class ShowsTreeView : Page
    {
        public ShowsTreeView(int id)
        {
            InitializeComponent();
            var apikey = "dc21e8c3cb3e79d39f776b428b13222c";
            TMDbClient client = new TMDbClient(apikey);
            TvShow tvshow = client.GetTvShowAsync(id).Result;

            TreeViewItem treeItem = null;

            treeItem = new TreeViewItem();
            treeItem.Header = tvshow.Name;
            foreach (var season in tvshow.Seasons)
            {
                
                TreeViewItem CurrentSeason = new TreeViewItem() { Header = "Season " + season.SeasonNumber };
                if (season.EpisodeCount == 0)
                    break;

                    foreach (var newEpisode in client.GetTvSeasonAsync(id, season.SeasonNumber).Result.Episodes)
                    {
                        CurrentSeason.Items.Add(new TreeViewItem()
                        {
                            Header = String.Format("S{0} E{1} {2}",
                                newEpisode.SeasonNumber,
                                newEpisode.EpisodeNumber,
                                newEpisode.Name)
                        });
                }
                treeItem.Items.Add(CurrentSeason);
            }
            ShowsTV.Items.Add(treeItem);
        }
    }
}

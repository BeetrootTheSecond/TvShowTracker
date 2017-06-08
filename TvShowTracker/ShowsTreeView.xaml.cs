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




namespace TvShowTracker.Pages
{
    /// <summary>
    /// Interaction logic for ShowsTreeView.xaml
    /// </summary>
    public partial class ShowsTreeView : Page
    {
        static public List<TheShow> showsList = new List<TheShow>();
        
        public ShowsTreeView(int id)
        {
            InitializeComponent();

            
            TreeViewItem treeItem = new TreeViewItem();

            //ShowData show = new ShowData();

            Show tvshow = ShowData.getShow(id);
            
            treeItem.Header = tvshow.Name;

            foreach (var season in tvshow.Seasons)
            {

                TreeViewItem CurrentSeason = new TreeViewItem() { Header = "Season " + season.SeasonNumber };
                if (season.EpisodeCount == 0)
                    break;

                foreach (var newEpisode in season.Episodes)
                {
                    CurrentSeason.Items.Add(new TreeViewItem()
                    {
                        
                        Header = 
                        String.Format("S{0} E{1} {2}",
                            season.SeasonNumber,
                            newEpisode.EpisodeNumber,
                            newEpisode.Name)
                    });
                }
                treeItem.Items.Add(CurrentSeason);
            }
            //}
            ShowsTV.Items.Add(treeItem);
        }
    }

    public class TheShow
    {
        public TheShow()

        {
            this.Tvshow = new List<Show>();
        }


        public List<Show> Tvshow

        { get; set; }

    }
}

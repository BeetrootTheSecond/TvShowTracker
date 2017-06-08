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

namespace TvShowTracker
{
    /// <summary>
    /// Interaction logic for ShowDetails.xaml
    /// </summary>
    public partial class ShowDetails : Page
    {
        Search returnPage = null;
        public ShowDetails(int id, Search currentSearch)
        {
            InitializeComponent();
            returnPage = currentSearch;

            Show show = ShowData.TheShowDetail(id);


            String fullFilePath = @"http://image.tmdb.org/t/p/w640" + show.BackDrop;


            BitmapImage bi = new BitmapImage();
            bi.BeginInit();
            bi.UriSource = new Uri(fullFilePath, UriKind.RelativeOrAbsolute);
            bi.EndInit();

            Image showPoster = new Image();
            showPoster.Source = bi;
            showPoster.Margin = new Thickness(10);
            showPoster.VerticalAlignment = VerticalAlignment.Top;
            Grid.SetRow(showPoster, 1);
            Grid.SetColumn(showPoster, 0);
            this.Details.Children.Add(showPoster);

        }
        

        private void BackButonClick(object sender, RoutedEventArgs e)
        {
            
            NavigationHelper.NavigationService.Navigate(returnPage);

        }

        private void AddToWatchlistClick(object sender, RoutedEventArgs e)
        {

        }
    }
}

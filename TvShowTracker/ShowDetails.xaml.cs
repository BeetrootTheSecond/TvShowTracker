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

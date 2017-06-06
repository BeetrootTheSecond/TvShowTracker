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
using TvShowTracker.Pages;

namespace TvShowTracker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //frame.NavigationService.Navigate(new ShowsTreeView(60735)); // the flash 60735 38472
            frame.NavigationService.Navigate(new WatchList());
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            frame.NavigationService.Navigate(new WatchList());
        }

        private void Shows_Click(object sender, RoutedEventArgs e)
        {
            frame.NavigationService.Navigate(new ShowsTreeView(65294)); // the flash 60735 38472
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            frame.NavigationService.Navigate(new Search());
        }
    }
}

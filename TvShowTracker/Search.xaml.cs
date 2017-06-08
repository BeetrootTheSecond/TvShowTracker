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
    /// Interaction logic for Search.xaml
    /// </summary>
    public partial class Search : Page
    {
        //ShowData show = new ShowData();
        public string searchText = "";
        public Search()
        {
            InitializeComponent();
            
        }

        public void Results(List<Show> SearchResults)
        {

           if(SearchResults.Count == 0)
            {
                RowDefinition currentRow = new RowDefinition();

                currentRow.MinHeight = 50;

                currentRow.Height = new GridLength(50);
                SearchDisplay.RowDefinitions.Add(currentRow);



                Border ShowDetails = new Border();
                ShowDetails.BorderBrush = Brushes.Black;
                ShowDetails.BorderThickness = new Thickness(1);
                ShowDetails.Background = Brushes.LightGray;
                ShowDetails.Margin = new Thickness(5);

                TextBlock Title = new TextBlock();
                Title.Text = "No Search Results Found!";
                Title.TextWrapping = System.Windows.TextWrapping.Wrap;
                //Title.Margin = new Thickness(5);
                Grid.SetColumn(Title, 0);
                Grid.SetRow(Title, 0);
                ShowDetails.Child = Title;

                Grid.SetRow(ShowDetails, SearchDisplay.RowDefinitions.IndexOf(currentRow));
                Grid.SetColumn(ShowDetails, 0);
                Grid.SetColumnSpan(ShowDetails,2);
                this.SearchDisplay.Children.Add(ShowDetails);


            }


            var SearchresultsOf3 = new List<List<Show>>();

            for (int i = 0; i < SearchResults.Count; i += 3)
            {
                SearchresultsOf3.Add(SearchResults.GetRange(i, Math.Min(3, SearchResults.Count - i)));
            }



            foreach(var setOfShows in SearchresultsOf3)
            {
                RowDefinition currentRow = new RowDefinition();

                currentRow.MinHeight = 50;

                SearchDisplay.RowDefinitions.Add(currentRow);

                int columnsNumber = 0;

                foreach (var Theshows in setOfShows)
                {

                    Border ShowDetails = new Border();
                    ShowDetails.BorderBrush = Brushes.Black;
                    ShowDetails.BorderThickness = new Thickness(1);
                    ShowDetails.Background = Brushes.LightGray;
                    ShowDetails.Margin = new Thickness(5);

                    Grid.SetRow(ShowDetails, SearchDisplay.RowDefinitions.IndexOf(currentRow));
                    Grid.SetColumn(ShowDetails, columnsNumber);
                    this.SearchDisplay.Children.Add(ShowDetails);
                    


                    string fullFilePath;

                    TextBlock Title = new TextBlock();

                    if (Theshows.PosterUrl == null)
                    {
                        fullFilePath = @"https://www.etyellowpages.com/images/cinema/NoPoster.jpg";

                        
                        Title.Text = Theshows.Name;
                        Title.TextWrapping = System.Windows.TextWrapping.Wrap;
                        Title.Margin = new Thickness(5);
                        Grid.SetColumn(Title, columnsNumber);
                        Grid.SetRow(Title, SearchDisplay.RowDefinitions.IndexOf(currentRow));
                        Title.HorizontalAlignment = HorizontalAlignment.Center;
                        


                    }
                    else
                    {
                        fullFilePath = @"http://image.tmdb.org/t/p/w640" + Theshows.PosterUrl;
                    }

                    BitmapImage bi = new BitmapImage();
                    bi.BeginInit();
                    bi.UriSource = new Uri(fullFilePath, UriKind.RelativeOrAbsolute);
                    bi.EndInit();

                    Image showPoster = new Image();

                    showPoster.Source = bi;
                    showPoster.Margin = new Thickness(10);
                    showPoster.VerticalAlignment = VerticalAlignment.Top;
                    Grid.SetRow(showPoster, SearchDisplay.RowDefinitions.IndexOf(currentRow));
                    Grid.SetColumn(showPoster, columnsNumber);
                    this.SearchDisplay.Children.Add(showPoster);


                    this.SearchDisplay.Children.Add(Title);


                    Button details = new Button();
                    //details.Visibility = Visibility.Hidden;
                    details.Opacity = 0.2;
                    Grid.SetRow(details, SearchDisplay.RowDefinitions.IndexOf(currentRow));
                    Grid.SetColumn(details, columnsNumber);
                    this.SearchDisplay.Children.Add(details);
                    details.Click += delegate
                    {
                        NavigationHelper.NavigationService.Navigate(new ShowDetails(Theshows.TmdbId,this));
                    };


                    columnsNumber++;
                }


            }

    }
        

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            List<Show> results = ShowData.Textsearch(searchText+" * ");
            SearchDisplay.RowDefinitions.Clear();
            SearchDisplay.Children.Clear();
            Results(results);
        }
               
        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
      {
            var textbox = sender as TextBox;
            searchText = textbox.Text;
            
        }
    }
}

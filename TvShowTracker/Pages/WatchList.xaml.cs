﻿using System;
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
    /// Interaction logic for WatchList.xaml
    /// </summary>
    public partial class WatchList : Page
    {
        public WatchList()
        {


            InitializeComponent();
            setup();
        }
        public void setup() { 
            ShowData shows = new ShowData();

            this.WatchlistGrid.RowDefinitions.Clear();

            var allShows = shows.getWatchlist();


            foreach (var currentshows in allShows)
            {

                foreach (var currentseason in currentshows.Seasons)
                {
                    if (currentseason.EpisodeCount > 0)
                    {
                        foreach (var currentEpisode in currentseason.Episodes)
                        {
                            if (currentEpisode.Watched == false)
                            {
                                WatchlistBox(currentshows, currentseason, currentEpisode);
                            }
                        }
                    }

                }

            }

        }


        public void WatchlistBox(Show currentshow, Season currentSeason, Episode currentEpisode)
        {
            RowDefinition currentRow = new RowDefinition();

            //currentRow.Height = new GridLength(50);
            currentRow.MinHeight = 50;

            this.WatchlistGrid.RowDefinitions.Add(currentRow);

            Border Episode = new Border();
            Episode.BorderBrush = Brushes.Black;
            Episode.BorderThickness = new Thickness(1);
            Episode.Background = Brushes.LightGray;
            Episode.Margin = new Thickness(5);

            Grid layout = new Grid();
            layout.RowDefinitions.Add(new RowDefinition());
            layout.RowDefinitions.Add(new RowDefinition());
            layout.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(80) });
            layout.ColumnDefinitions.Add(new ColumnDefinition());

            Button watched = new Button();
            watched.Content = "Watched";
            watched.Margin = new Thickness(5);
            Grid.SetColumn(watched, 1);
            Grid.SetRow(watched, 0);
            layout.Children.Add(watched);


            watched.Click += delegate
            {


                ShowData shows = new ShowData();
                shows.updateEpisode(currentEpisode.EpisodeId);

                this.WatchlistGrid.Children.Remove(Episode);
                //layout.RowDefinitions.Remove(currentRow);

                //this.WatchlistGrid.RowDefinitions.Clear();
                //this.WatchlistGrid.RowDefinitions.RemoveAt(this.WatchlistGrid.RowDefinitions.IndexOf(currentRow));
                //setup();
            };


            TextBlock Title = new TextBlock();
            Title.Text = currentshow.Name;
            Title.TextWrapping = System.Windows.TextWrapping.Wrap;
            //Title.Margin = new Thickness(5);
            Grid.SetColumn(Title, 0);
            Grid.SetRow(Title, 0);
            layout.Children.Add(Title);

            TextBlock EpisodeNumber = new TextBlock();
            EpisodeNumber.Text = string.Format("S{0} E{1}", currentSeason.SeasonNumber, currentEpisode.EpisodeNumber); ;
            //Title.Margin = new Thickness(5);
            Grid.SetColumn(EpisodeNumber, 0);
            Grid.SetRow(EpisodeNumber, 1);
            layout.Children.Add(EpisodeNumber);

            TextBlock EpisodeName = new TextBlock();
            EpisodeName.Text = currentEpisode.Name;
            EpisodeName.TextWrapping = System.Windows.TextWrapping.Wrap;
            //Title.Margin = new Thickness(5);
            Grid.SetColumn(EpisodeName, 1);
            Grid.SetRow(EpisodeName, 1);
            layout.Children.Add(EpisodeName);


            Episode.Child = layout;
            Grid.SetRow(Episode, this.WatchlistGrid.RowDefinitions.IndexOf(currentRow));

            this.WatchlistGrid.Children.Add(Episode);


            // return currentRow;
        }


    }
}

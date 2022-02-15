using FavoritePlayersWPF.Models;
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
using System.Windows.Shapes;

namespace FavoritePlayersWPF.Windows
{
    /// <summary>
    /// Interaction logic for StatisticsWindow.xaml
    /// </summary>
    public partial class StatisticsWindow : Window
    {
        private TeamStatisticsClass stats;

        public StatisticsWindow(TeamStatisticsClass stats)
        {
            InitializeComponent();
            this.stats = stats;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            name.Content = stats.Name;
            fifa.Content = stats.FifaCode;
            gamesPlayed.Content = stats.NumberOfGamesPlayed;
            wins.Content = stats.Wins;
            losses.Content = stats.Losses;
            draws.Content = stats.Draws;
            goals.Content = stats.Goals.Value;
            goalsTaken.Content = stats.GoalsTaken;
            diff.Content = stats.diff;
        }
    }
}

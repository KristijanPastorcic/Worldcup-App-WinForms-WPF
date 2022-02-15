using FavoritePlayersWPF.Models;
using FavoritePlayersWPF.Windows;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
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
using WorldcupDAL;
using WorldcupDAL.Enums;
using WorldcupDAL.Models;

namespace FavoritePlayersWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly string SETTINGS = Directory.GetParent(Directory.GetCurrentDirectory())
        .Parent.Parent.FullName + System.IO.Path.DirectorySeparatorChar + "settings.txt";

        private readonly string PLAYERS = Directory.GetParent
            (Directory.GetCurrentDirectory())
            .Parent.Parent.FullName + System.IO.Path.DirectorySeparatorChar + "players.txt";

        private const char SEPARATOR = '|';
        private const string MALE = "Male";
        private const string ASSETS_DIR = "Assets";
        private const char SEPARATOR_X = 'x';
        private const string FULL_SCREEN = "fullscreen";

        private IDictionary<Team, Team> matchesData = new Dictionary<Team, Team>();
        private IList<Data> data;
        private Gender gender;
        public MainWindow()
        {
            if (!File.Exists(SETTINGS) || string.IsNullOrWhiteSpace(File.ReadAllText(SETTINGS)))
            {
                var win = new SettingsWindow(SETTINGS);
                bool? result = win.ShowDialog();
                if (result.HasValue && result.Value.Equals(true))
                {
                    Init();
                }
                else
                {
                    Close();
                    return;
                } 
            }
            Init();
        }

        private void Init()
        {
            try
            {
                SetupCulture();
                InitializeComponent();
                SetupResolution();
                FillComboBoxCountriesAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Properties.Resources.error, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }



        private void SetupCulture()
        {
            try
            {
                string[] settings = File.ReadAllText(SETTINGS).Split(SEPARATOR);
                Thread.CurrentThread.CurrentCulture = new CultureInfo(settings[0]);
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(settings[0]);
                gender = settings[1].Equals(MALE) ? Gender.Male : Gender.Female;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Properties.Resources.error, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SetupResolution()
        {
            try
            {
                string[] settings = File.ReadAllText(SETTINGS).Split(SEPARATOR);
                string[] resolution = settings[2].Split(SEPARATOR_X);
                if (resolution[0].Equals(FULL_SCREEN))
                {
                    this.WindowState = WindowState.Maximized;
                    return;
                }
                Width = double.Parse(resolution[0]);
                Height = double.Parse(resolution[1]);
                WindowStartupLocation = WindowStartupLocation.CenterScreen;
            }
            catch (Exception)
            {
                
            }
        }


        //Prilikom učitavanja aplikacije, potrebno je učitati omiljenu reprezentaciji postavljenu u Windows Forms aplikaciji.Istu je
        //moguće promijeniti kroz ComboBox kontrolu. 

        private async void FillComboBoxCountriesAsync()
        {
            IEnumerable<CountryTeam> teems = await WorldcupRepository.GetCountriesAsync(gender);
            cbFavoriteTeem.ItemsSource = teems;
            if (File.Exists(PLAYERS) && !string.IsNullOrWhiteSpace(File.ReadAllText(PLAYERS)))
            {
                string[] players = File.ReadAllLines(PLAYERS);
                var favorite = players[0];
                foreach (CountryTeam teem in cbFavoriteTeem.Items)
                {
                    if (teem.ToString().Equals(favorite))
                    {
                        cbFavoriteTeem.SelectedItem = teem; // trigers selcted item change
                        break;
                    }
                }
            }
        }

        //        Pored toga, potrebno je odabrati jednu od reprezentacija koja je igrala
        //protiv izabrane reprezentacije.Reprezentacije trebaju biti prikazane u obliku “NAZIV(FIFA_KOD)”. 

        private async void GetDataAndSetRivalTeemsAsync()
        {
            try
            {
                var c = cbFavoriteTeem.SelectedItem as CountryTeam;
                var selectedTeem = c.ToString();
                var data = await WorldcupRepository.GetTeemDataAsync(c.FifaCode, gender);
                this.data = data;
                IList<Team> rivalTeems = new List<Team>();
                foreach (Data match in data)
                {

                    Team homeTeam = match.HomeTeam;
                    Team awayTeam = match.AwayTeam;
                    if (!selectedTeem.Equals(awayTeam.ToString()))
                    {
                        matchesData.Add(new KeyValuePair<Team, Team>(awayTeam, homeTeam));
                        rivalTeems.Add(awayTeam);
                    }
                    else
                    {
                        matchesData.Add(new KeyValuePair<Team, Team>(homeTeam, awayTeam));
                        rivalTeems.Add(homeTeam);
                    }
                }
                cbRival.ItemsSource = rivalTeems;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Properties.Resources.error, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void cbFavoriteTeem_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                matchesData.Clear();
                GetDataAndSetRivalTeemsAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Properties.Resources.error, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


            //        Nakon odabira obje
            //reprezentacije, treba biti vidljiv rezultat odigrane utakmice između odabranih reprezentacija(npr. „2 : 1“).
        private void cbRival_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                var c = cbFavoriteTeem.SelectedItem as CountryTeam;
                if (c == null)
                {
                    return;
                }

                Team rival = cbRival.SelectedItem as Team;
                if (rival != null)
                {
                    matchesData.TryGetValue(rival, out Team favorite);
                    lblResult.Content = string.Empty;
                    lblResult.Content += $"{Properties.Resources.match_result} {favorite.Goals} : {rival.Goals}";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Properties.Resources.error, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        //Prilikom zatvaranja aplikacije, potrebno je korisnika tražiti potvrdu želi li to stvarno učiniti. Potvrdu je moguće izvršiti uz
        //pomoć tipki na tipkovnici na isti način kako je već opisano za promjenu postavki.

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show(Properties.Resources.warning,
        Properties.Resources.warning_message, MessageBoxButton.OKCancel, MessageBoxImage.Question);
            if (result == MessageBoxResult.Cancel)
            {
                e.Cancel = true;
            }
        }

        private void saveMenu_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var team = cbFavoriteTeem.SelectedItem as CountryTeam;
                File.WriteAllText(PLAYERS, team.ToString());
                MessageBox.Show(
                    Properties.Resources.save_message,
                    Properties.Resources.save, MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Properties.Resources.error, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        //        Postavljene postavke aplikacije moguće je promijeniti i kroz prozor „Postavke“. Potrebno je tražiti potvrdu od korisnika
        //za promjene.Opciju “Potvrdi” moguće je izvršiti uz pomoć tipke “Enter” na tipkovnici, a opciju “Odustani” pomoću tipke
        //“Esc” na tipkovnici. Potvrđeni odabir potrebno je pohraniti u tekstualnu datoteku. 
        private void settingsmenu_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string old = File.ReadAllText(SETTINGS);
                var win = new SettingsWindow(SETTINGS);
                bool? result = win.ShowDialog();
                if (result.HasValue && result.Value.Equals(true))
                {
                    MessageBoxResult messageBoxResult = MessageBox.Show(
                        Properties.Resources.confirm_change,
                        Properties.Resources.language_confirm,
                        MessageBoxButton.OKCancel);
                    if (messageBoxResult == MessageBoxResult.Cancel)
                    {
                        File.WriteAllText(SETTINGS, old);
                    }
                    SetupResolution();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Properties.Resources.error, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        //naziv, FIFA kod, broj// odigranih/ pobjeda/ poraza/ neodlučenih, golova zabijenih/primljenih/razlika.

        private void btn_fav_statistics_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                CountryTeam ct = cbFavoriteTeem.SelectedItem as CountryTeam;
                Team team = cbRival.SelectedItem as Team;
                if (cbFavoriteTeem == null || team == null)
                {
                    MessageBox.Show(Properties.Resources.choose_team);
                    return;
                }

                var name = ct.CountryName;
                var fifa = ct.FifaCode;
                var numberOfGames = data.Count;

                int wins = 0;
                int lost = 0;
                int draw = 0;
                float? goals = 0.0f;
                float? goalsIN = 0.0f;
                float? diff;
                foreach (Data data in this.data)
                {
                    if (data.WinnerCode == fifa)
                    {
                        wins++;
                    }
                    else if (data.WinnerCode != fifa)
                    {
                        lost++;
                    }
                    else
                    {
                        draw++;
                    }
                }

                foreach (var match in matchesData)
                {
                    if (match.Value.Goals.HasValue)
                    {
                        goals += match.Value.Goals;
                    }
                    if (match.Key.Goals.HasValue)
                    {
                        goalsIN += match.Key.Goals;
                    }
                }

                try
                {
                    diff = goals / goalsIN;
                }
                catch (Exception)
                {
                    diff = 0.0f;
                }

                var stats = new TeamStatisticsClass
                {
                    Name = name,
                    FifaCode = fifa,
                    NumberOfGamesPlayed = numberOfGames,
                    Wins = wins,
                    Losses = lost,
                    Draws = draw,
                    Goals = goals,
                    GoalsTaken = goalsIN,
                    diff = diff
                };

                new StatisticsWindow(stats).Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Properties.Resources.error, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void btn_rival_statistic_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                CountryTeam ct = cbFavoriteTeem.SelectedItem as CountryTeam;
                Team rival = cbRival.SelectedItem as Team;
                if (cbFavoriteTeem == null || rival == null)
                {
                    MessageBox.Show(Properties.Resources.choose_team);
                    return;
                }

                var name = rival.Country;
                var fifa = rival.Code;
                var rivalData = await WorldcupRepository.GetTeemDataAsync(rival.Code, gender);
                var numberOfGames = rivalData.Count;

                int wins = 0;
                int lost = 0;
                int draw = 0;
                float? goals = 0.0f;
                float? goalsIN = 0.0f;
                float? diff;
                foreach (Data data in rivalData)
                {
                    if (data.WinnerCode == fifa)
                    {
                        wins++;
                    }
                    else if (data.WinnerCode != fifa)
                    {
                        lost++;
                    }
                    else
                    {
                        draw++;
                    }
                }

                foreach (var match in matchesData)
                {
                    if (match.Key.Goals.HasValue)
                    {
                        goals += match.Value.Goals;
                    }
                    if (match.Value.Goals.HasValue)
                    {
                        goalsIN += match.Key.Goals;
                    }
                }

                try
                {
                    diff = goals / goalsIN;
                }
                catch (Exception)
                {
                    diff = 0.0f;
                }

                var stats = new TeamStatisticsClass
                {
                    Name = name,
                    FifaCode = fifa,
                    NumberOfGamesPlayed = numberOfGames,
                    Wins = wins,
                    Losses = lost,
                    Draws = draw,
                    Goals = goals,
                    GoalsTaken = goalsIN,
                    diff = diff
                };

                new StatisticsWindow(stats).Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Properties.Resources.error, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}

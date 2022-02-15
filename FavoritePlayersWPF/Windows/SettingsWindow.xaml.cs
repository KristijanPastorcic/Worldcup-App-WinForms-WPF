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
using System.Windows.Shapes;
using WorldcupDAL.Enums;

namespace FavoritePlayersWPF.Windows
{
    /// <summary>
    /// Interaction logic for SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        private readonly string settings;
        private const string HR = "hr", EN = "en";
        private const char SEPARATOR = '|';
        private string[] languages = { EN, HR };

        private string[] resolutions = { "640x480", "1280x720", "1920x1080", "fullscreen" };
        public SettingsWindow(string settings)
        {
            this.settings = settings;

            if (File.Exists(settings))
            {
                try
                {
                    string[] lines = File.ReadAllText(settings).Split(SEPARATOR);
                    SetupCulture(lines[0]);
                    InitializeComponent();
                    cbLanguages.ItemsSource = languages;
                    lsResolutions.ItemsSource = resolutions;
                    lsResolutions.SelectedItem = lines.Length > 2 ? lines[2] : resolutions[0];
                    cbLanguages.SelectedItem = lines[0];
                    cbLanguages.SelectionChanged += CbLanguages_SelectionChanged;
                    if (lines[1].Equals("Male"))
                    {
                        rbMale.IsChecked = true;
                    }
                    else
                    {
                        rbFemale.IsChecked = true;
                    }
                }
                catch (Exception)
                {
                    Init();
                }
            }
            else
            {
                Init();
            }

        }

        private void Init()
        {
            InitializeComponent();
            rbMale.IsChecked = true;
            cbLanguages.ItemsSource = languages;
            lsResolutions.ItemsSource = resolutions;
            cbLanguages.SelectedIndex = 0;
            lsResolutions.SelectedIndex = 0;
            cbLanguages.SelectionChanged += CbLanguages_SelectionChanged;
        }

        private void CbLanguages_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var lang = cbLanguages.SelectedItem as string;
            SetupCulture(lang);
            InitializeComponent();
        }

        private void SetupCulture(string culture)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo(culture);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(culture);
        }



        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            var gender = rbMale.IsChecked.Value ? rbMale.Tag as string : rbFemale.Tag as string;
            var culture = cbLanguages.SelectedItem as string;
            var resolution = lsResolutions.SelectedItem as string; 
            
            File.WriteAllText(settings, culture + SEPARATOR + gender + SEPARATOR + resolution);
            DialogResult = true;
            Close();
        }

        private void btnCancle_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}

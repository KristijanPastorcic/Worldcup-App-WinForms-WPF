using FavoritePlayerWinForms.Properties;
using FavoritePlayerWinForms.UserControles;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WorldcupDAL;
using WorldcupDAL.Enums;
using WorldcupDAL.Models;

namespace FavoritePlayerWinForms.Forms
{
    public partial class MainForm : Form
    {
        private readonly string SETTINGS = Directory.GetParent
            (Directory.GetCurrentDirectory())
            .Parent.Parent.FullName + Path.DirectorySeparatorChar + "settings.txt";

        private readonly string PLAYERS = Directory.GetParent
            (Directory.GetCurrentDirectory())
            .Parent.Parent.FullName + Path.DirectorySeparatorChar + "players.txt";

        public int count { get; set; }
        private const char SEPARATOR = '|';
        private const string MALE = "Male";
        private const string ASSETS_DIR = "Assets";

        private bool saved = true;

        private IList<Data> data;

        private IList<PlayerUC> selectedPlayers = new List<PlayerUC>();

        private Gender gender;

        private delegate void pnlAllLoaded();
        private event pnlAllLoaded playersLoaded;

        public MainForm()
        {
            if (!File.Exists(SETTINGS) || string.IsNullOrWhiteSpace(File.ReadAllText(SETTINGS)))
            {
                //Početne postavke
                var cultureForm = new SetupForm(SETTINGS);
                if (cultureForm.ShowDialog() == DialogResult.OK)
                {
                    Init();
                }
                else
                {
                    Close();
                }
            }
            else
            {
                Init();
            }
        }

        private void Init()
        {
            try
            {
                Setup();
                this.Controls.Clear();
                InitializeComponent();
                FillComboBoxCountriesAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Resources.error, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Setup()
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
                MessageBox.Show(ex.Message, Resources.error, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }




        private async void FillComboBoxCountriesAsync()
        {
            lblStrip.Text = Resources.loading_countries;
            IEnumerable<CountryTeam> teems = await WorldcupRepository.GetCountriesAsync(gender);
            foreach (var item in teems)
            {
                cbCountries.Items.Add(item);
            }
            lblStrip.Text = string.Empty;

            if (File.Exists(PLAYERS) && !string.IsNullOrEmpty(File.ReadAllText(PLAYERS)))
            {   // trigers selected index change and loads players
                LoadPlayersFromFile();
            }
        }

        // učitaj spremljene igrače i izbaci iz svih omiljene nakon učitavanja 
        private void LoadPlayersFromFile()
        {
            try
            {
                string[] players = File.ReadAllLines(PLAYERS);
                foreach (var item in cbCountries.Items)
                {
                    if (item.ToString() == players[0])
                    {
                        cbCountries.SelectedItem = item;
                        break;
                    }
                }
                playersLoaded += RemoveFavoritesFromAllPlayers;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Resources.error, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void RemoveFavoritesFromAllPlayers()
        {

            try
            {
                string[] players = File.ReadAllLines(PLAYERS);
                for (int i = 1; i < players.Length; i++)
                {
                    PlayerUC playerUC = PlayerUC.FromFilleLine(players[i]);
                    foreach (PlayerUC uc in pnlAll.Controls)
                    {
                        if (uc.PlayerName == playerUC.PlayerName)
                        {
                            if (playerUC.PlayerImage.ImageLocation != null)
                            {
                                uc.PlayerImage.Image = Image.FromFile(playerUC.PlayerImage.ImageLocation);
                                uc.PlayerImage.ImageLocation = playerUC.PlayerImage.ImageLocation;
                            }
                            selectedPlayers.Add(uc);
                        }
                    }
                }

                btnTransferPlayers_Click(null, null);
                saved = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Resources.error, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }





        private void cbCountries_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (count++ > 0)
                {
                    pnl.Controls.Clear();
                    saved = false;
                    File.WriteAllText(PLAYERS, string.Empty);
                    playersLoaded -= LoadPlayersFromFile;
                }
                GetDataAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Resources.cant_get_data, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void GetDataAsync()
        {
            try
            {
                lblStrip.Text = Resources.loading_all_players;
                var c = cbCountries.SelectedItem as CountryTeam;
                var data = await WorldcupRepository.GetTeemDataAsync(c.FifaCode, gender);
                this.data = data;
                SetAllPlayers();
                lblStrip.Text = string.Empty;
                playersLoaded?.Invoke(); // svi igrači učitani obavjesti petplatitelje ako ih ima 
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Resources.cant_get_data, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetAllPlayers()
        {
            try
            {
                pnlAll.Controls.Clear();
                List<Player> startingEleven = data[0].HomeTeamStatistics.StartingEleven;
                List<Player> substitutes = data[0].HomeTeamStatistics.Substitutes;
                IEnumerable<Player> all = startingEleven.Union(substitutes);
                foreach (Player player in all)
                {
                    var playerUC = new PlayerUC();

                    playerUC.ContextMenuStrip = contextMenuStrip;
                    playerUC.MouseClick += PlayerUC_MouseClick;

                    playerUC.PlayerName = player.Name;
                    playerUC.Position = player.Position;
                    playerUC.ShirtNumber = player.ShirtNumber;
                    playerUC.Capitan = player.Captain.HasValue ? player.Captain.Value : false;
                    pnlAll.Controls.Add(playerUC);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Resources.cant_get_data, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }












        //Također, moguće je označiti više igrača odjednom i prebaciti ih u drugi panel.
        private void PlayerUC_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Middle)
                {
                    PlayerUC uc = sender as PlayerUC;

                    if (!selectedPlayers.Contains(uc))
                    {
                        uc.BorderStyle = BorderStyle.None;
                        selectedPlayers.Add(uc);
                    }
                    else
                    {
                        uc.BorderStyle = BorderStyle.FixedSingle;
                        selectedPlayers.Remove(uc);
                    }

                    if (selectedPlayers.Count > 0)
                    {
                        btnTransferPlayers.Enabled = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Resources.cant_get_data, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnTransferPlayers_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (var item in selectedPlayers)
                {
                    SetupForPnl(item);
                    pnl.Controls.Add(item);
                }
                selectedPlayers.Clear();
                btnTransferPlayers.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Resources.cant_get_data, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // priprema kontrole za panel omiljenih
        void SetupForPnl(PlayerUC uc)
        {
            try
            {
                uc.ImageUpdated += Uc_ImageUpdated;
                uc.ContextMenuStrip = null;
                uc.BtnImageUpdate.Enabled = true;
                uc.MouseClick -= PlayerUC_MouseClick;
                uc.MouseDown -= uc.PlayerUC_MouseDown;
                uc.DoubleClick += PlayerUC_DoubleClick;
                uc.BorderStyle = BorderStyle.FixedSingle;
                uc.Favorite = true;
                saved = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Resources.cant_get_data, MessageBoxButtons.OK, MessageBoxIcon.Error);                
            }
        }

        private void Uc_ImageUpdated()
        {
            saved = false;
        }

        // brisanje omiljenih duplim klikom na korisnicku kontrolu
        private void PlayerUC_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                PlayerUC uc = sender as PlayerUC;
                pnl.Controls.Remove(uc);
                pnlAll.Controls.Add(uc);
                saved = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Resources.cant_get_data, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // povlačenjem (Drag and drop)
        private void pnl_DragDrop(object sender, DragEventArgs e)
        {
            try
            {
                var uc = e.Data.GetData(typeof(PlayerUC)) as PlayerUC;
                if (!pnl.Controls.Contains(uc))
                {
                    SetupForPnl(uc);
                    pnl.Controls.Add(uc);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Resources.cant_get_data, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pnl_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        //odabirom iz kontekstnog menija
        private void addToFavoritesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                var uc = contextMenuStrip.SourceControl as PlayerUC;
                SetupForPnl(uc);
                pnl.Controls.Add(uc);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Resources.cant_get_data, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        //Odabire je potrebno pohraniti u tekstualnu datoteku i koristiti prilikom sljedećih učitavanja aplikacije.
        private void toolStripMenuSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (pnl.Controls.Count != 0)
                {
                    StringBuilder data = new StringBuilder();
                    data.AppendLine(cbCountries.SelectedItem.ToString());
                    foreach (PlayerUC uc in pnl.Controls)
                    {

                        string picPath = uc.PlayerImage.ImageLocation;
                        string destination = "X";
                        if (picPath != null)
                        {
                            destination = Directory.GetParent(
                                Directory.GetCurrentDirectory())
                                .Parent.Parent.FullName
                                + Path.DirectorySeparatorChar
                                + ASSETS_DIR
                                + Path.DirectorySeparatorChar
                                + Path.GetFileName(picPath);
                            if (File.Exists(destination) == false)
                            {
                                // TODO: create assets dir 
                                File.Copy(picPath, destination);
                            }
                        }
                        data.AppendLine(uc.FormatForFilleLine()
                            + SEPARATOR + destination);
                    }
                    File.WriteAllText(PLAYERS, data.ToString());
                }
                else
                {
                    File.WriteAllText(PLAYERS, string.Empty);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            MessageBox.Show(Resources.favorites_saved);
            saved = true;
        }


        //Zatvaranje aplikacije i postavke
        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                string settings = File.ReadAllText(SETTINGS);
                var cultureForm = new SetupForm(SETTINGS);
                if (cultureForm.ShowDialog() == DialogResult.OK)
                {
                    if (MessageBox.Show(
                        Resources.confirm_change,
                        Resources.language_confirm,
                        MessageBoxButtons.OKCancel) == DialogResult.OK)
                    {
                        Init();
                    }
                    else
                    {
                        File.WriteAllText(SETTINGS, settings);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Resources.cant_get_data, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (saved == false)
            {
                if (MessageBox.Show(Resources.do_u_wish_to_save, Resources.info, MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    toolStripMenuSave_Click(null, null);
                }
            }
        }




    }
}

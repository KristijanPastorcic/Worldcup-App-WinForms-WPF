using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FavoritePlayerWinForms.UserControles
{
    public partial class PlayerUC : UserControl
    {
        public delegate void imageUpdated();
        public event imageUpdated ImageUpdated;

        private const char SEPARATOR = '|';
        public PlayerUC()
        {
            InitializeComponent();
        }



        public PictureBox PlayerImage { get => picture; }
        public Button BtnImageUpdate
        {
            get => btnImageUpdate;
        }

        public CheckBox CbFavorite
        {
            get => cbFavorite;
        }
        public string PlayerName
        {
            get => name.Text;
            set => name.Text = value;
        }
        public string Position
        {
            get => position.Text;
            set => position.Text = value;
        }
        public string ShirtNumber
        {
            get => dressNo.Text;
            set => dressNo.Text = value;
        }

        public bool Capitan
        {
            get => cbCapitan.Checked;
            set => cbCapitan.Checked = value;
        }

        public bool Favorite
        {
            get => cbFavorite.Checked;
            set => cbFavorite.Checked = value;
        }

        public void PlayerUC_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                StartDnD(this);
            }
        }

        private void picture_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                StartDnD(this);
            }
        }
        private void StartDnD(PlayerUC player)
        {
            player.DoDragDrop(this, DragDropEffects.Copy);
        }

        private void btnImageUpdate_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "*.jpg;*.jpeg;*.png;*.bmp;|";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                var path = ofd.FileName;
                picture.Image = Image.FromFile(ofd.FileName);
                picture.ImageLocation = ofd.FileName;
                ImageUpdated?.Invoke();
            }
        }

        public String FormatForFilleLine()
        {
            return $"{PlayerName}{SEPARATOR}{ShirtNumber}{SEPARATOR}{Position}{SEPARATOR}" +
                $"{Capitan}";
        }

        public static PlayerUC FromFilleLine(string data)
        {
            string[] props = data.Split(SEPARATOR);
            PlayerUC playerUC = new PlayerUC
            {
                PlayerName = props[0],
                ShirtNumber = props[1],
                Position = props[2],
                Capitan = bool.Parse(props[3]),
            };
            if (props[4].Equals("X") == false)
            {
                playerUC.picture.Image = Image.FromFile(props[4]);
                playerUC.picture.ImageLocation = props[4];
            }
            return playerUC;
        }

  
        
    }
}

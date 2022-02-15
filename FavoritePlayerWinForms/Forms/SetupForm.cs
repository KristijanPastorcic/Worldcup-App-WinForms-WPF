using FavoritePlayerWinForms.Properties;
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

namespace FavoritePlayerWinForms.Forms
{
    public partial class SetupForm : Form
    {
        private const string HR = "hr", EN = "en";
        private const char SEPARATOR = '|';
        private string[] languages = { EN, HR };
        private readonly string file;

        private void btnSave_Click(object sender, EventArgs e)
        {

            string gender;
            var culture = cbLanguage.SelectedItem as String;
            if (rbMale.Checked)
            {
                gender = rbMale.Tag as string;
            }
            else
            {
                gender = rbFemale.Tag as string;
            }
            File.WriteAllText(file, culture + "|" + gender);
            Close();
        }

        public SetupForm(string file)
        {
            this.file = file;
            InitializeComponent();
            cbLanguage.DataSource = languages.ToList();
            if (File.Exists(file) && !string.IsNullOrWhiteSpace(File.ReadAllText(file)))
            {

                string[] settings = File.ReadAllText(file).Split(SEPARATOR);
                SetupCulture(settings[0]);
                if (settings[1].Equals("Male"))
                {
                    rbMale.Checked = true;
                }
                else
                {
                    rbFemale.Checked = true;
                }
            }
            cbLanguage.SelectedIndexChanged += CbLanguage_SelectedIndexChanged;
        }

        private void CbLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            string culture = cbLanguage.SelectedItem as string;
            SetupCulture(culture);
        }

        private void SetupCulture(string culture)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo(culture);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(culture);
            this.Controls.Clear();
            InitializeComponent();
            cbLanguage.DataSource = languages.ToList();
            string current = Thread.CurrentThread.CurrentCulture.Name;
            cbLanguage.SelectedItem = current;
            cbLanguage.SelectedIndexChanged += CbLanguage_SelectedIndexChanged;
        }
    }
}

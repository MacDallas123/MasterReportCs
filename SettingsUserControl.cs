using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DocumentFormat.OpenXml.Spreadsheet;
using MasterReport.Properties;
using MetroFramework;
using System.IO;

namespace MasterReport
{
    public partial class SettingsUserControl : UserControl
    {
        private Settings settings;

        private string logoLocation = "";
        public SettingsUserControl()
        {
            InitializeComponent();

            this.settings = new Settings();

            structureName.Text = this.settings.structureName;
            structureLocation.Text = this.settings.structureLocation;
            structureEmail.Text = this.settings.structureEmail;
            structurePhone.Text = this.settings.structurePhone;
            structureDescription.Text = this.settings.structureDescription;

            if(string.IsNullOrEmpty(this.settings.structureLogo))
            {
                logoPictureBox.Image = Resources.image_120px;
            }
            else {
                if (File.Exists(this.settings.structureLogo))
                    logoPictureBox.ImageLocation = this.settings.structureLogo;
                else MessageBox.Show(this, "L'image choisie comme logo n'existe plus à l'emplacement désigné, elle a probablement été déplacée, renommée ou supprimée. Veuillez en choisir une nouvelle", "Erreur chargement logo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void chooseImageBtn_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Images (*.png, *.jpg, *.jpeg, *.bmp, *.gif)|*.png;*.jpg;*.jpeg;*.bmp;*.gif";
            openFileDialog.Title = "Sélectionner un fichier image";
            openFileDialog.Multiselect = false;
            if(openFileDialog.ShowDialog() == DialogResult.OK)
            {
                logoPictureBox.ImageLocation = openFileDialog.FileName;

                this.logoLocation = openFileDialog.FileName;
            }
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            this.settings.structureLocation = logoLocation;
            this.settings.structureName = structureName.Text;
            this.settings.structureLocation = structureLocation.Text;
            this.settings.structureEmail = structureEmail.Text;
            this.settings.structurePhone = structurePhone.Text;
            this.settings.structureLogo = this.logoLocation;
            this.settings.structureDescription = structureDescription.Text;

            this.settings.Save();

            //MetroMessageBox.Show(this, "Sauvegarde effectuée avec succès", "Opération reussie");
            MessageBox.Show("Sauvegarde effectuée avec succès", "Opération reussie");
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;

namespace MasterReport
{
    public partial class ImportExportUserControl : UserControl
    {

        private const string db_name = "main-database.db";
        private SQLiteConnection cnx;
        private SQLiteCommand cmd;
        private SQLiteDataReader dataReader;
        private SQLiteDataAdapter dataAdapter;
        private string cnx_str = $"data source={db_name};version=3";

        public string period
        {
            get { return periodComboBox.SelectedItem.ToString(); }
        }

        public ImportExportUserControl()
        {
            InitializeComponent();

            
        }


        private void loadYears()
        {
            YearsList.Items.Clear();

            List<int> years = new List<int>();

            string query = "SELECT date FROM suivis";
            try
            {
                this.cnx = new SQLiteConnection(this.cnx_str);

                this.cnx.Open();
                this.cmd = new SQLiteCommand(query, this.cnx);
                this.dataReader = this.cmd.ExecuteReader();

                while (dataReader.Read())
                {
                    string date = dataReader.GetString(0);
                    if (date.Trim() != "")
                    {
                        DateTime dateTime = DateTime.Parse(date);

                        int year = dateTime.Year;

                        if (!years.Contains(year))
                        {
                            years.Add(year);
                        }
                    }
                }

                years.Sort();

                foreach (int year in years)
                {
                    YearsList.Items.Add(year.ToString());
                }

                int current_year = DateTime.Today.Year;

                if (!YearsList.Items.Contains(current_year.ToString()))
                {
                    YearsList.Items.Add(current_year.ToString());
                }
            }
            catch (Exception ex)
            {
                // MessageBox.Show(ex.Message, "Erreur : Données d'années", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            this.cnx.Close();
        }

        private void chooseFileBtn_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Fichier Excel (*.xlsx)|*.xlsx";
            openFileDialog.Title = "Sélectionner le fichier excel à importer";
            openFileDialog.Multiselect = false;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                filenameTextBox.Text = openFileDialog.FileName;
            }
        }

        private void ImportExportUserControl_Load(object sender, EventArgs e)
        {
            periodComboBox.SelectedIndex = 1;
            
            loadYears();

            try
            {
                YearsList.SelectedIndex = YearsList.Items.Count == 0 ? 0 : YearsList.Items.Count - 1;
                MonthsList.SelectedIndex = DateTime.Now.Month - 1;
            } catch (Exception) { }
        }

        public void disableControls()
        {
            filenameLabel.Enabled = false;
            periodLabel.Enabled = false;
            filenameTextBox.Enabled = false;
            periodComboBox.Enabled = false;
            chooseFileBtn.Enabled = false;
            importBtn.Enabled = false;
        }
        
        public void enableControls()
        {
            filenameLabel.Enabled = true;
            periodLabel.Enabled = true;
            filenameTextBox.Enabled = true;
            periodComboBox.Enabled = true;
            chooseFileBtn.Enabled = true;
            importBtn.Enabled = true;
        }

        public void showInformationSpinner()
        {
            processSpinner.Visible = true;
            spinnerLabel.Visible = true;
            spinnerPercentage.Visible = true;
        }
        
        public void hideInformationSpinner()
        {
            processSpinner.Visible = false;
            spinnerLabel.Visible = false;
            spinnerPercentage.Visible = false;
        }

        public void showExportInformationSpinner()
        {
            exportMessageLabel.Visible = true;
            exportSpinner.Visible = true;
            exportToXlsxBtn.Enabled = false;
        }

        public void hideExportInformationSpinner()
        {
            exportMessageLabel.Visible = false;
            exportSpinner.Visible = false;
            exportToXlsxBtn.Enabled = true;
        }

        private void importBtn_Click(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}

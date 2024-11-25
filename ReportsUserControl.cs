using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.SQLite;
using DrawingFont = System.Drawing.Font;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Document = iTextSharp.text.Document;
using MasterReport.Properties;
using ItextFont = iTextSharp.text.Font;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using System.Web.UI.Design.WebControls;
using System.Diagnostics;
using iText.Commons.Utils;

namespace MasterReport
{
    public partial class ReportsUserControl : UserControl
    {

        private const string db_name = "main-database.db";
        private SQLiteConnection cnx;
        private SQLiteCommand cmd;
        private SQLiteDataReader dataReader;
        private SQLiteDataAdapter dataAdapter;
        private string cnx_str = $"data source={db_name};version=3";
        private DataTable dt;

        private Settings settings;

        private Dictionary<string, int> dbDatas;

        public ReportsUserControl()
        {
            InitializeComponent();
            this.cnx = new SQLiteConnection(this.cnx_str);
            this.dt = new DataTable();
            this.loadYears();
            this.settings = new Settings();

            this.dbDatas = new Dictionary<string, int>();
            try
            {
                monthsList.SelectedIndex = DateTime.Now.Month - 1;
                yearsList.SelectedIndex = yearsList.Items.Count == 0 ? 0 : yearsList.Items.Count - 1;
                
                monthsListGlob.SelectedIndex = DateTime.Now.Month - 1;
                yearsListGlob.SelectedIndex = yearsList.Items.Count == 0 ? 0 : yearsList.Items.Count - 1;
            }
            catch (Exception ex) { }
        }

        private void loadYears()
        {
            yearsList.Items.Clear();
            yearsListGlob.Items.Clear();

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
                    yearsList.Items.Add(year.ToString());
                }

                int current_year = DateTime.Today.Year;

                if (!yearsList.Items.Contains(current_year.ToString()))
                {
                    yearsList.Items.Add(current_year.ToString());
                }
                

                foreach (var item in yearsList.Items)
                {
                    yearsListGlob.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                // MessageBox.Show(ex.Message, "Erreur : Données d'années", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            this.cnx.Close();
        }

        private void ReportsUserControl_Load(object sender, EventArgs e)
        {
            this.reloadInf();
            this.reloadInfGlob();
        }

        private int getSoldPerMonth(string month, DataTable table)
        {
            int sold = 0;
            try
            {
                string filter = $"mois = '{month}'";

                DataRow[] filteredRows = table.Select(filter);
                sold = filteredRows.Length == 0 ? 0 : int.Parse(filteredRows[filteredRows.Length - 1][5].ToString());
            } catch (Exception ex) { MessageBox.Show(ex.Message, "Erreur : solde mois"); }

            return sold;
        }

        private Dictionary<string, int> getGlobSoldPerMonth(string month, DataTable table)
        {
            Dictionary<string, int> dict = new Dictionary<string, int>();
            
            try
            {
                string filter = $"mois = '{month}'";
                DataRow[] filteredRows = table.Select(filter);

                bool newCustomer = true;
                string name = "";
                string nextName = "";
                for (int i = 0; i < filteredRows.Length - 1; i++)
                {
                    if (newCustomer)
                    {
                        name = filteredRows[i]["nom"].ToString();
                        newCustomer = false;
                    }

                    nextName = filteredRows[i + 1]["nom"].ToString();
                    if (string.Compare(name, nextName) != 0)
                    {
                        int value = int.Parse(filteredRows[i]["solde"].ToString());
                        dict.Add(name, value);
                        newCustomer = true;
                    }
                }

                if(filteredRows.Length > 0)
                {
                    string lastName = filteredRows[filteredRows.Length - 1]["nom"].ToString();
                    int lastValue = int.Parse(filteredRows[filteredRows.Length - 1]["solde"].ToString());
                    if(!dict.ContainsKey(lastName))
                        dict.Add(lastName, lastValue);
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Erreur : solde mois"); }

            return dict;
        }

        private void loadSold(long customer_idx)
        {
            this.cnx = new SQLiteConnection(this.cnx_str);

            try { this.cnx.Open(); } catch (Exception) { }
            try
            {
                int current_year = DateTime.Now.Year;
                string str_year = yearsList.SelectedItem == null ? current_year.ToString() : yearsList.SelectedItem.ToString();

                int month_pos = monthsList.SelectedIndex + 1;
                string str_month = month_pos < 10 ? $"0{month_pos.ToString()}" : month_pos.ToString();

                string query = $"SELECT *, substr(date, 4, 2) as mois, substr(date, 7, 4) as annee FROM suivis WHERE client = {customer_idx} AND annee = '{str_year}' ORDER BY id, date(date)"; // ORDER BY date(date) ASC;
                //if(yearReportCheckbox.Checked)
                //    query = $"SELECT *, substr(date, 4, 2) as mois, substr(date, 7, 4) as annee FROM suivis WHERE client = {customer_idx} AND annee = '{str_year}' ORDER BY id, date(date)"; // ORDER BY date(date) ASC;
                //else
                //    query = $"SELECT *, substr(date, 4, 2) as mois, substr(date, 7, 4) as annee FROM suivis WHERE client = {customer_idx} AND annee = '{str_year}' AND mois = '{str_month}' ORDER BY id, date(date)"; // ORDER BY date(date) ASC;

                /*this.cnx = new SQLiteConnection(this.cnx_str);

                this.cnx.Open();*/

                this.dt = new DataTable();
                this.cmd = new SQLiteCommand(query, this.cnx);
                this.dataAdapter = new SQLiteDataAdapter(this.cmd);

                this.dataAdapter.Fill(this.dt);

                int sold = 0;
                if (yearReportCheckbox.Checked)
                {
                    for(int i = 1; i < 13; i++)
                    {
                        string m = i < 10 ? $"0{i}" : i.ToString();
                        sold += getSoldPerMonth(m, this.dt);
                    }
                }
                else sold += getSoldPerMonth(str_month, this.dt);

                soldAmountInf.Text = $"{sold} FCFA";

            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Erreur : lecture solde..i", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            this.cnx.Close();
        }

        private void loadGlobInf()
        {
            this.cnx = new SQLiteConnection(this.cnx_str);

            try { this.cnx.Open(); } catch (Exception) { }
            try
            {
                int current_year = DateTime.Now.Year;
                string str_year = yearsListGlob.SelectedItem == null ? current_year.ToString() : yearsListGlob.SelectedItem.ToString();

                int month_pos = monthsListGlob.SelectedIndex + 1;
                string str_month = month_pos < 10 ? $"0{month_pos.ToString()}" : month_pos.ToString();

                //string query = $"SELECT *, substr(date, 4, 2) as mois, substr(date, 7, 4) as annee FROM suivis WHERE annee = '{str_year}' ORDER BY client, id, date(date)"; // ORDER BY date(date) ASC;
                string query = $"SELECT s.*, c.nom, substr(date, 4, 2) as mois, substr(date, 7, 4) as annee FROM suivis s INNER JOIN clients c ON c.id = s.client WHERE annee = '{str_year}' ORDER BY client, id, date(date)"; // ORDER BY date(date) ASC;

                this.dt = new DataTable();
                this.dt.Clear();
                this.dbDatas = new Dictionary<string, int>();

                this.cmd = new SQLiteCommand(query, this.cnx);
                this.dataAdapter = new SQLiteDataAdapter(this.cmd);
                this.dataAdapter.Fill(this.dt);
                

                int sold = 0;
                string bestCustomerName = "";
                int totalSold = 0;
                if (yearReportCheckboxGlob.Checked)
                {
                    for (int i = 1; i < 13; i++)
                    {
                        string m = i < 10 ? $"0{i}" : i.ToString();
                        Dictionary<string, int> monthDatas = this.getGlobSoldPerMonth(m, this.dt);
                        foreach (var pair in monthDatas)
                        {
                            if (this.dbDatas.ContainsKey(pair.Key))
                            {
                                this.dbDatas[pair.Key] += pair.Value;
                            }
                            else
                            {
                                this.dbDatas[pair.Key] = pair.Value;
                            }
                        }
                    }

                    foreach (var pair in this.dbDatas)
                    {
                        totalSold += pair.Value;
                    }

                    var max = this.dbDatas.OrderByDescending(key => key.Value).First();

                    bestCustomerName = max.Key;
                    sold = max.Value;
                }
                else 
                {
                    bool newCustomer = true;
                    string name = "";
                    string nextName = "";
                    int currentMax = 0;
                    string maxName = "";

                    DataRow[] filteredRows = this.dt.Select($"mois = '{str_month}'");
                    for(int i = 0; i < filteredRows.Length - 1; i++)
                    {
                        if(newCustomer)
                        {
                            name = filteredRows[i]["nom"].ToString();
                            newCustomer = false;
                        }

                        nextName = filteredRows[i + 1]["nom"].ToString();
                        if (string.Compare(name, nextName) != 0)
                        {
                            int value = int.Parse(filteredRows[i]["solde"].ToString());
                            if (currentMax < value)
                            {
                                maxName = name;
                                currentMax = value;
                            }

                            if (!this.dbDatas.ContainsKey(name))
                                this.dbDatas[name] = value;

                            totalSold += value;
                            newCustomer = true;
                        }
                    }

                    //MessageBox.Show(filteredRows[filteredRows.Length - 1]["nom"/*8*/].ToString() + "; solde : " + filteredRows[filteredRows.Length - 1]["solde"/*5*/].ToString());
                    if(filteredRows.Length > 0)
                    {
                        string lastName = filteredRows[filteredRows.Length - 1]["nom"].ToString();
                        int lastValue = int.Parse(filteredRows[filteredRows.Length - 1]["solde"].ToString());
                        if (currentMax < lastValue)
                        {
                            maxName = lastName;
                            currentMax = lastValue;
                        }

                        if (!this.dbDatas.ContainsKey(lastName))
                            this.dbDatas[lastName] = lastValue;

                        totalSold += lastValue;
                    }
                    sold = currentMax;
                    bestCustomerName = maxName;
                
                }

                greatCustomerInfLabel.Text = $"{bestCustomerName} ({sold} FCFA)";
                totalSoldGlobLabel.Text = $"{totalSold} FCFA";

            }
            catch (Exception ex)
            {
                //MessageBox.Show(this, ex.Message, "Erreur : lecture solde..g", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            this.cnx.Close();
        }

        private void checkPeriod()
        {
            try
            {
                string month = monthsList.SelectedItem == null ? "" : monthsList.SelectedItem.ToString();
                string year = yearsList.SelectedItem == null ? "" : yearsList.SelectedItem.ToString();
                monthYearInf.Text = yearReportCheckbox.Checked ? $"{year}" : $"{month} {year}";
            }
            catch (Exception) { }
        }
        private void checkPeriodGlob()
        {
            try
            {
                string month = monthsListGlob.SelectedItem == null ? "" : monthsListGlob.SelectedItem.ToString();
                string year = yearsListGlob.SelectedItem == null ? "" : yearsListGlob.SelectedItem.ToString();
                monthYearInfGlob.Text = yearReportCheckboxGlob.Checked ? $"{year}" : $"{month} {year}";
            }
            catch (Exception) { }
        }

        private void yearReportCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if(yearReportCheckbox.Checked)
            {
                monthTitleLabel.Visible = false;
                monthsList.Visible = false;
            }
            else
            {
                monthTitleLabel.Visible = true;
                monthsList.Visible = true;
            }

            this.checkPeriod();
            this.reloadInf();
        }

        
        private void yearsList_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.checkPeriod();
            this.reloadInf();
        }

        private void monthsList_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.checkPeriod();
            this.reloadInf();
        }

        public void reloadInf()
        {
            try
            {
                long customer_idx = long.Parse(customerIdInf.Text);
                this.loadSold(customer_idx);
            }
            catch (Exception) {  }
        }

        public void reloadInfGlob()
        {
            try
            {
                this.loadGlobInf();
            }
            catch (Exception) { }
        }

        private void customerIdInf_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void yearReportCheckboxGlob_CheckedChanged(object sender, EventArgs e)
        {
            if (yearReportCheckboxGlob.Checked)
            {
                monthTitleLabelGlob.Visible = false;
                monthsListGlob.Visible = false;
            }
            else
            {
                monthTitleLabelGlob.Visible = true;
                monthsListGlob.Visible = true;
            }

            this.checkPeriodGlob();
            this.reloadInfGlob();
        }

        private void yearsListGlob_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.checkPeriodGlob();
            this.reloadInfGlob();
        }

        private void monthsListGlob_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.checkPeriodGlob();
            this.reloadInfGlob();
        }


        private DataTable FollowUps(long customer_idx, bool perMonth)
        {
            DataTable table = new DataTable();
            try
            {
                int current_year = DateTime.Now.Year;
                string str_year = yearsList.SelectedItem == null ? current_year.ToString() : yearsList.SelectedItem.ToString();

                int month_pos = monthsList.SelectedIndex + 1;
                string str_month = month_pos < 10 ? $"0{month_pos.ToString()}" : month_pos.ToString();

                // string query = $"SELECT *, substr(date, 4, 2) as mois, substr(date, 7, 4) as annee FROM suivis WHERE client = {customer_idx} AND annee = '{str_year}' AND mois = '{str_month}' ORDER BY id, date(date)"; // ORDER BY date(date) ASC;
                string query = "";
                if(perMonth)
                    query = $"SELECT *, substr(date, 4, 2) as mois, substr(date, 7, 4) as annee FROM suivis WHERE client = {customer_idx} AND annee = '{str_year}' AND mois = '{str_month}' ORDER BY id, date(date)"; // ORDER BY date(date) ASC;
                else
                    query = $"SELECT *, substr(date, 4, 2) as mois, substr(date, 7, 4) as annee FROM suivis WHERE client = {customer_idx} AND annee = '{str_year}' ORDER BY id, date(date)"; // ORDER BY date(date) ASC;
                
                this.cnx = new SQLiteConnection(this.cnx_str);

                this.cnx.Open();

                this.cmd = new SQLiteCommand(query, this.cnx);
                SQLiteDataAdapter dataAdapter = new SQLiteDataAdapter(this.cmd);

                dataAdapter.Fill(table);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Erreur : Données de suivi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            this.cnx.Close();

            return table;
        }

        private void saveAsPdfInd_Click(object sender, EventArgs e)
        {
            try
            {

                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Fichier PDF (*.pdf)|*.pdf";
                saveFileDialog.Title = "Sélectionner l'emplacement de sauvegarde du fichier";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // get the save path
                    string fileName = saveFileDialog.FileName;

                    // Company about informations
                    string structureName = settings.structureName;
                    string structureLocation = settings.structureLocation;
                    string structureEmail = settings.structureEmail;
                    string structurePhone = settings.structurePhone;
                    string structureLogo = settings.structureLogo;
                    string structureDescription = settings.structureDescription;

                    Document doc = new Document(PageSize.A4);

                    BaseFont arial = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                    ItextFont fact_title = new ItextFont(arial, 24, ItextFont.BOLDITALIC);
                    ItextFont f_period_title = new ItextFont(arial, 16, ItextFont.BOLDITALIC);
                    ItextFont f_13_bold = new ItextFont(arial, 13, ItextFont.BOLD);
                    ItextFont f_13_normal = new ItextFont(arial, 13, ItextFont.NORMAL);
            
                    FileStream fs = new FileStream(fileName, FileMode.Create);
                    using (fs)
                    {
                        PdfWriter.GetInstance(doc, fs);
                        doc.Open();

                        // Grand Titre
                        PdfPTable table0 = new PdfPTable(1);

                        // PdfPCell main_cell = new PdfPCell(new Phrase($"Bilan Mensuel Client", fact_title));
                        PdfPCell main_cell = null; // new PdfPCell(new Phrase($"Bilan Mensuel Client", fact_title));
                        main_cell = yearReportCheckboxGlob.Checked ?
                                new PdfPCell(new Phrase($"Bilan Annuel Client", fact_title)) :
                                new PdfPCell(new Phrase($"Bilan Mensuel Client", fact_title));
                        main_cell.HorizontalAlignment = Element.ALIGN_CENTER;

                        main_cell.Border = iTextSharp.text.Rectangle.NO_BORDER;

                        table0.WidthPercentage = 80;
                        table0.HorizontalAlignment = Element.ALIGN_CENTER;
                        table0.AddCell(main_cell);

                        doc.Add(table0);

                        // Ajouter l'image du logo si existante
                        /*if(!string.IsNullOrEmpty(this.settings.structureLogo) && File.Exists(this.settings.structureLogo))
                        {
                            Image logo = Image.GetInstance(this.settings.structureLogo);
                            logo.ScaleToFit(75f, 75f);

                            float xPos = doc.PageSize.Width - logo.ScaledWidth - 80f;
                            float yPos = doc.PageSize.Height - logo.ScaledHeight - 80f;
                            logo.SetAbsolutePosition(xPos, yPos);
                            doc.Add(logo);
                        }*/

                        // Affichage des informations à propos
                        PdfPTable table1 = new PdfPTable(1);
                        float[] width = new float[] { 40f, 60f };

                        PdfPCell cell1 = new PdfPCell(new Phrase($"Nom de la structure : {structureName}", f_13_bold));
                        PdfPCell cell2 = new PdfPCell(new Phrase($"Localisation : {structureLocation}", f_13_bold));
                        PdfPCell cell3 = new PdfPCell(new Phrase($"Contact(s) : {structurePhone}", f_13_bold));
                        PdfPCell cell4 = new PdfPCell(new Phrase($"Email : {structureEmail}", f_13_bold));
                        PdfPCell cell5 = new PdfPCell(new Phrase($"Description : {structureDescription}", f_13_bold));

                        cell1.Border = iTextSharp.text.Rectangle.NO_BORDER;
                        cell2.Border = iTextSharp.text.Rectangle.NO_BORDER;
                        cell3.Border = iTextSharp.text.Rectangle.NO_BORDER;
                        cell4.Border = iTextSharp.text.Rectangle.NO_BORDER;
                        cell5.Border = iTextSharp.text.Rectangle.NO_BORDER;

                        cell1.HorizontalAlignment = Element.ALIGN_LEFT;
                        cell2.HorizontalAlignment = Element.ALIGN_LEFT;
                        cell3.HorizontalAlignment = Element.ALIGN_LEFT;
                        cell4.HorizontalAlignment = Element.ALIGN_LEFT;
                        cell5.HorizontalAlignment = Element.ALIGN_LEFT;

                        table1.WidthPercentage = 50;
                        table1.HorizontalAlignment = Element.ALIGN_LEFT;
                        table1.AddCell(cell1);
                        table1.AddCell(cell2);
                        table1.AddCell(cell3);
                        table1.AddCell(cell4);
                        table1.AddCell(cell5);

                        table1.SpacingAfter = 20;
                        table1.SpacingBefore = 20;

                        doc.Add(table1);

                        //Client
                        string ctmName = customerNameInf.Text;
                        table1 = new PdfPTable(1);

                        cell1 = new PdfPCell(new Phrase($"Nom du client : {ctmName}", f_13_bold));

                        cell1.Border = iTextSharp.text.Rectangle.NO_BORDER;
                        cell1.HorizontalAlignment = Element.ALIGN_JUSTIFIED;

                        table1.SpacingAfter = 10;
                        table1.SpacingBefore = 10;

                        table1.AddCell(cell1);

                        PdfPTable table2 = new PdfPTable(1);
                        table2.AddCell(table1);
                        table2.HorizontalAlignment = Element.ALIGN_RIGHT;
                        table2.WidthPercentage = 60;

                        doc.Add(table2);

                        // Dynamic Gen Datas
                        /*iTextSharp.text.Paragraph paragraph = new iTextSharp.text.Paragraph(new Phrase($"Date : {DateTime.Now.ToLongDateString()}\n", f_13_normal));
                        paragraph.Alignment = Element.ALIGN_JUSTIFIED;
                        doc.Add(paragraph);*/

                        // Period Title
                        /*table0 = new PdfPTable(1);

                        string mYValue = monthYearInf.Text; 
                        PdfPCell period_cell = new PdfPCell(new Phrase($"{mYValue}", f_period_title));
                        period_cell.HorizontalAlignment = Element.ALIGN_CENTER;

                        period_cell.Border = iTextSharp.text.Rectangle.NO_BORDER;

                        table0.WidthPercentage = 80;
                        table0.HorizontalAlignment = Element.ALIGN_CENTER;
                        table0.AddCell(period_cell);

                        doc.Add(table0);*/



                        /** PARTIE VARIABLE **/
                        // Survey Table

                        long customer_idx = long.Parse(customerIdInf.Text);
                        DataTable table = null;
                        if (yearReportCheckbox.Checked) table = this.FollowUps(customer_idx, false);
                        else table = this.FollowUps(customer_idx, true);

                        string[] header = {
                            "N°",
                            "Date de dépôt",
                            "Date de retrait",
                            "Débit",
                            "Crédit",
                            "Solde"
                        };

                        string[] all_months =
                        {
                            "Janvier", "Fevrier", "Mars",
                            "Avril", "Mai", "Juin", "Juillet",
                            "Août", "Septembre", "Octobre",
                            "Novembre", "Décembre"
                        }; 

                        int i = 0;
                        int nb = 0;
                        string previous_month = "";
                        string current_month = "";
                        while(i < table.Rows.Count)
                        {
                        
                            // Period Title
                            table0 = new PdfPTable(1);

                            int mIndex = int.Parse(table.Rows[i]["mois"].ToString());
                            string mValue = all_months[mIndex - 1];
                            string yValue = table.Rows[i]["annee"].ToString();
                            string mYValue = $"{mValue} {yValue}";

                            PdfPCell period_cell = new PdfPCell(new Phrase($"\n{mYValue}", f_period_title));
                            period_cell.HorizontalAlignment = Element.ALIGN_CENTER;

                            period_cell.Border = iTextSharp.text.Rectangle.NO_BORDER;

                            table0.WidthPercentage = 80;
                            table0.HorizontalAlignment = Element.ALIGN_CENTER;
                            table0.AddCell(period_cell);

                            doc.Add(table0);

                            table1 = new PdfPTable(6);

                            // Header
                            for (int j = 0; j < 6; j++)
                            {
                                cell1 = new PdfPCell(new Phrase(header[j], f_13_bold));
                                cell1.HorizontalAlignment = Element.ALIGN_CENTER;
                                cell1.FixedHeight = 20;
                                table1.AddCell(cell1);
                            }

                            previous_month = table.Rows[i]["mois"].ToString();
                            current_month = table.Rows[i]["mois"].ToString();

                            nb = 1;
                            // Table Content
                            while (string.Compare(previous_month, current_month, StringComparison.OrdinalIgnoreCase) == 0 && i < table.Rows.Count)
                            {
                                //MessageBox.Show($"{i} {current_month} {previous_month}");
                                // Number
                                cell1 = new PdfPCell(new Phrase(nb.ToString(), f_13_normal));
                                cell1.FixedHeight = 20;
                                table1.AddCell(cell1);

                                // Deposit date
                                cell1 = new PdfPCell(new Phrase(table.Rows[i]["date"].ToString(), f_13_normal));
                                cell1.FixedHeight = 20;
                                table1.AddCell(cell1);

                                // Withdraw date
                                cell1 = new PdfPCell(new Phrase(table.Rows[i]["retrait"].ToString(), f_13_normal));
                                cell1.FixedHeight = 20;
                                table1.AddCell(cell1);

                                // Debit
                                cell1 = new PdfPCell(new Phrase(table.Rows[i]["debit"].ToString(), f_13_normal));
                                cell1.FixedHeight = 20;
                                table1.AddCell(cell1);

                                // Credit
                                cell1 = new PdfPCell(new Phrase(table.Rows[i]["credit"].ToString(), f_13_normal));
                                cell1.FixedHeight = 20;
                                table1.AddCell(cell1);

                                // Solde
                                cell1 = new PdfPCell(new Phrase(table.Rows[i]["solde"].ToString(), f_13_normal));
                                cell1.FixedHeight = 20;
                                table1.AddCell(cell1);

                                i++;
                                nb++;
                                if(i < table.Rows.Count) current_month = table.Rows[i]["mois"].ToString();

                                /*if (string.Compare(previous_month, current_month, StringComparison.OrdinalIgnoreCase) != 0 || i >= table.Rows.Count)
                                    break;*/
                            }

                            table1.WidthPercentage = 100;
                            width = new float[] { 60f, 100f, 100f, 100f, 100f, 100f };
                            table1.SetWidths(width);
                            table1.SpacingBefore = 5;
                            doc.Add(table1);
                        }

                    


                    
                        /*
                        if(yearReportCheckbox.Checked)
                        {
                            table1 = new PdfPTable(5);

                            string[] header = {
                                "N°",
                                "Date Dde dépôt",
                                "Date de retrait",
                                "Débit",
                                "Crédit",
                                "Solde"
                            };

                            // Header
                            for (int i = 0; i < 6; i++)
                            {
                                cell1 = new PdfPCell(new Phrase(header[i], f_13_bold));
                                cell1.HorizontalAlignment = Element.ALIGN_CENTER;
                                cell1.FixedHeight = 20;
                                table1.AddCell(cell1);
                            }

                            // Table Content
                            long customer_idx = long.Parse(customerIdInf.Text);
                            DataTable table = this.FollowUps(customer_idx);

                            for (int i = 0; i < table.Rows.Count; i++)
                            {
                                // Number
                                cell1 = new PdfPCell(new Phrase(i.ToString(), f_13_normal));
                                cell1.FixedHeight = 20;
                                table1.AddCell(cell1);

                                // Deposit date
                                cell1 = new PdfPCell(new Phrase(table.Rows[i]["date"].ToString(), f_13_normal));
                                cell1.FixedHeight = 20;
                                table1.AddCell(cell1);

                                // Withdraw date
                                cell1 = new PdfPCell(new Phrase(table.Rows[i]["retrait"].ToString(), f_13_normal));
                                cell1.FixedHeight = 20;
                                table1.AddCell(cell1);

                                // Debit
                                cell1 = new PdfPCell(new Phrase(table.Rows[i]["debit"].ToString(), f_13_normal));
                                cell1.FixedHeight = 20;
                                table1.AddCell(cell1);

                                // Credit
                                cell1 = new PdfPCell(new Phrase(table.Rows[i]["credit"].ToString(), f_13_normal));
                                cell1.FixedHeight = 20;
                                table1.AddCell(cell1);

                                // Solde
                                cell1 = new PdfPCell(new Phrase(table.Rows[i]["solde"].ToString(), f_13_normal));
                                cell1.FixedHeight = 20;
                                table1.AddCell(cell1);
                            }


                            table1.WidthPercentage = 100;
                            width = new float[] { 80f, 100f, 100f, 100f, 100f, 100f };
                            table1.SetWidths(width);
                            table1.SpacingBefore = 20;
                            doc.Add(table1);
                        }
                        else
                        {
                            table1 = new PdfPTable(5);

                            string[] header = {
                                "N°",
                                "Date Dde dépôt",
                                "Date de retrait",
                                "Débit",
                                "Crédit",
                                "Solde"
                            };

                            // Header
                            for (int i = 0; i < 6; i++)
                            {
                                cell1 = new PdfPCell(new Phrase(header[i], f_13_bold));
                                cell1.HorizontalAlignment = Element.ALIGN_CENTER;
                                cell1.FixedHeight = 20;
                                table1.AddCell(cell1);
                            }

                            // Table Content
                            long customer_idx = long.Parse(customerIdInf.Text);
                            DataTable table = this.FollowUps(customer_idx);

                            for (int i = 0; i < table.Rows.Count; i++)
                            {
                                // Number
                                cell1 = new PdfPCell(new Phrase(i.ToString(), f_13_normal));
                                cell1.FixedHeight = 20;
                                table1.AddCell(cell1);

                                // Deposit date
                                cell1 = new PdfPCell(new Phrase(table.Rows[i]["date"].ToString(), f_13_normal));
                                cell1.FixedHeight = 20;
                                table1.AddCell(cell1);

                                // Withdraw date
                                cell1 = new PdfPCell(new Phrase(table.Rows[i]["retrait"].ToString(), f_13_normal));
                                cell1.FixedHeight = 20;
                                table1.AddCell(cell1);

                                // Debit
                                cell1 = new PdfPCell(new Phrase(table.Rows[i]["debit"].ToString(), f_13_normal));
                                cell1.FixedHeight = 20;
                                table1.AddCell(cell1);

                                // Credit
                                cell1 = new PdfPCell(new Phrase(table.Rows[i]["credit"].ToString(), f_13_normal));
                                cell1.FixedHeight = 20;
                                table1.AddCell(cell1);

                                // Solde
                                cell1 = new PdfPCell(new Phrase(table.Rows[i]["solde"].ToString(), f_13_normal));
                                cell1.FixedHeight = 20;
                                table1.AddCell(cell1);
                            }


                            table1.WidthPercentage = 100;
                            width = new float[] { 80f, 100f, 100f, 100f, 100f, 100f };
                            table1.SetWidths(width);
                            table1.SpacingBefore = 20;
                            doc.Add(table1);
                        }

                        */



                        doc.Close();
                        if(MessageBox.Show("Souhaitez-vous ouvrir le document ?", "Confirmer opération", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            Process.Start(fileName);
                        }
                    }
                
                    //BaseFont arial = BaseFont.CreateFont(@"..\..\Resources\OLDENGL.TTF", BaseFont.WINANSI, BaseFont.NOT_EMBEDDED);

                    //BaseFont arial = BaseFont.CreateFont(@"E:\Fonts\Downloads\ROCK.TTF", BaseFont.WINANSI, BaseFont.NOT_EMBEDDED);

                    //iTextSharp.text.Font.BOLDITALIC
                }
                //else MessageBox.Show("La génération a échoué, le chemin spécifié est invalide", "Opération impossible");
            } catch (Exception ex)
            {
                MessageBox.Show("L'accès au fichier spécifié a échoué, vérifier qu'il n'est pas utilisé par un autre programme", "Opération impossible", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //MessageBox.Show(ex.Message, "Opération impossible", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void materialButton2_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Fichier PDF (*.pdf)|*.pdf";
                saveFileDialog.Title = "Sélectionner l'emplacement de sauvegarde du fichier";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // get the save path
                    string fileName = saveFileDialog.FileName;

                    // Company about informations
                    string structureName = settings.structureName;
                    string structureLocation = settings.structureLocation;
                    string structureEmail = settings.structureEmail;
                    string structurePhone = settings.structurePhone;
                    string structureLogo = settings.structureLogo;
                    string structureDescription = settings.structureDescription;

                    Document doc = new Document(PageSize.A4);

                    BaseFont arial = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                    ItextFont fact_title = new ItextFont(arial, 24, ItextFont.BOLDITALIC);
                    ItextFont f_period_title = new ItextFont(arial, 16, ItextFont.BOLDITALIC);
                    ItextFont f_13_bold = new ItextFont(arial, 13, ItextFont.BOLD);
                    ItextFont f_13_normal = new ItextFont(arial, 13, ItextFont.NORMAL);

                    FileStream fs = new FileStream(fileName, FileMode.Create);
                    using (fs)
                    {
                        PdfWriter.GetInstance(doc, fs);
                        doc.Open();

                        // Grand Titre
                        PdfPTable table0 = new PdfPTable(1);

                        PdfPCell main_cell = null; // new PdfPCell(new Phrase($"Bilan Mensuel Client", fact_title));
                        main_cell = yearReportCheckboxGlob.Checked ? 
                                new PdfPCell(new Phrase($"Bilan Annuel Structure", fact_title)) :
                                new PdfPCell(new Phrase($"Bilan Mensuel Structure", fact_title));

                        main_cell.HorizontalAlignment = Element.ALIGN_CENTER;

                        main_cell.Border = iTextSharp.text.Rectangle.NO_BORDER;

                        table0.WidthPercentage = 80;
                        table0.HorizontalAlignment = Element.ALIGN_CENTER;
                        table0.AddCell(main_cell);

                        doc.Add(table0);

                        // Affichage des informations à propos
                        PdfPTable table1 = new PdfPTable(1);
                        float[] width = new float[] { 40f, 60f };

                        PdfPCell cell1 = new PdfPCell(new Phrase($"Nom de la structure : {structureName}", f_13_bold));
                        PdfPCell cell2 = new PdfPCell(new Phrase($"Localisation : {structureLocation}", f_13_bold));
                        PdfPCell cell3 = new PdfPCell(new Phrase($"Contact(s) : {structurePhone}", f_13_bold));
                        PdfPCell cell4 = new PdfPCell(new Phrase($"Email : {structureEmail}", f_13_bold));
                        PdfPCell cell5 = new PdfPCell(new Phrase($"Description : {structureDescription}", f_13_bold));

                        cell1.Border = iTextSharp.text.Rectangle.NO_BORDER;
                        cell2.Border = iTextSharp.text.Rectangle.NO_BORDER;
                        cell3.Border = iTextSharp.text.Rectangle.NO_BORDER;
                        cell4.Border = iTextSharp.text.Rectangle.NO_BORDER;
                        cell5.Border = iTextSharp.text.Rectangle.NO_BORDER;

                        cell1.HorizontalAlignment = Element.ALIGN_LEFT;
                        cell2.HorizontalAlignment = Element.ALIGN_LEFT;
                        cell3.HorizontalAlignment = Element.ALIGN_LEFT;
                        cell4.HorizontalAlignment = Element.ALIGN_LEFT;
                        cell5.HorizontalAlignment = Element.ALIGN_LEFT;

                        table1.WidthPercentage = 50;
                        table1.HorizontalAlignment = Element.ALIGN_LEFT;
                        table1.AddCell(cell1);
                        table1.AddCell(cell2);
                        table1.AddCell(cell3);
                        table1.AddCell(cell4);
                        table1.AddCell(cell5);

                        table1.SpacingAfter = 20;
                        table1.SpacingBefore = 20;

                        doc.Add(table1);

                        //Client
                        /*string ctmName = customerNameInf.Text;
                        table1 = new PdfPTable(1);

                        cell1 = new PdfPCell(new Phrase($"Nom du client : {ctmName}", f_13_bold));

                        cell1.Border = iTextSharp.text.Rectangle.NO_BORDER;
                        cell1.HorizontalAlignment = Element.ALIGN_JUSTIFIED;

                        table1.SpacingAfter = 10;
                        table1.SpacingBefore = 10;

                        table1.AddCell(cell1);

                        PdfPTable table2 = new PdfPTable(1);
                        table2.AddCell(table1);
                        table2.HorizontalAlignment = Element.ALIGN_RIGHT;
                        table2.WidthPercentage = 60;

                        doc.Add(table2);*/

                        // Dynamic Gen Datas
                        /*iTextSharp.text.Paragraph paragraph = new iTextSharp.text.Paragraph(new Phrase($"Date : {DateTime.Now.ToLongDateString()}\n", f_13_normal));
                        paragraph.Alignment = Element.ALIGN_JUSTIFIED;
                        doc.Add(paragraph);*/

                        // Period Title
                        table0 = new PdfPTable(1);

                        string mYValue = monthYearInfGlob.Text; 
                        PdfPCell period_cell = new PdfPCell(new Phrase($"{mYValue}", f_period_title));
                        period_cell.HorizontalAlignment = Element.ALIGN_CENTER;

                        period_cell.Border = iTextSharp.text.Rectangle.NO_BORDER;

                        table0.WidthPercentage = 80;
                        table0.SpacingBefore = 20;
                        table0.HorizontalAlignment = Element.ALIGN_CENTER;
                        table0.AddCell(period_cell);

                        doc.Add(table0);



                        /** PARTIE VARIABLE **/
                        // Survey Table

                        // DataTable table = null;
                        Dictionary<string, int> table = this.dbDatas.OrderByDescending(key => key.Value).ToDictionary(x => x.Key, y => y.Value);

                        /* if (yearReportCheckbox.Checked) table = this.FollowUps(customer_idx, false);
                        else table = this.FollowUps(customer_idx, true); */

                        string[] header = {
                            "N°",
                            "Nom du client",
                            "solde"
                        };

                        table1 = new PdfPTable(3);

                        // Header
                        for (int j = 0; j < 3; j++)
                        {
                            cell1 = new PdfPCell(new Phrase(header[j], f_13_bold));
                            cell1.HorizontalAlignment = Element.ALIGN_CENTER;
                            cell1.FixedHeight = 20;
                            table1.AddCell(cell1);
                        }

                        int i = 1;
                        foreach(var pair in table)
                        {
                            // Number
                            cell1 = new PdfPCell(new Phrase(i.ToString(), f_13_normal));
                            cell1.FixedHeight = 20;
                            cell1.HorizontalAlignment = Element.ALIGN_CENTER;
                            table1.AddCell(cell1);

                            // Name
                            cell1 = new PdfPCell(new Phrase(pair.Key, f_13_normal));
                            cell1.FixedHeight = 20;
                            cell1.HorizontalAlignment = Element.ALIGN_LEFT;
                            table1.AddCell(cell1);

                            // Sold
                            cell1 = new PdfPCell(new Phrase($"{pair.Value} FCFA", f_13_normal));
                            cell1.FixedHeight = 20;
                            // if (pair.Value < 0) cell1.BackgroundColor = BaseColor.RED.Brighter();
                            cell1.HorizontalAlignment = Element.ALIGN_CENTER;
                            table1.AddCell(cell1);

                            i++;
                        }

                        table1.WidthPercentage = 100;
                        width = new float[] { 20f, 50f, 30f };
                        table1.SetWidths(width);
                        table1.SpacingBefore = 10;
                        doc.Add(table1);

                        table1 = new PdfPTable(1);

                        string total = totalSoldGlobLabel.Text;
                        PdfPCell total_cell = new PdfPCell(new Phrase($"REVENU TOTAL : {total}", f_13_bold));
                        total_cell.HorizontalAlignment = Element.ALIGN_RIGHT;

                        total_cell.Border = iTextSharp.text.Rectangle.NO_BORDER;

                        table1.WidthPercentage = 100;
                        table1.SpacingBefore = 30;
                        table1.HorizontalAlignment = Element.ALIGN_RIGHT;
                        table1.AddCell(total_cell);

                        doc.Add(table1);

                        doc.Close();
                        if (MessageBox.Show("Souhaitez-vous ouvrir le document ?", "Confirmer opération", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            Process.Start(fileName);
                        }
                    }
                }
                //else MessageBox.Show("La génération a échoué, le chemin spécifié est invalide", "Opération impossible");
            } catch (Exception)
            {
                MessageBox.Show("L'accès au fichier spécifié a échoué, vérifier qu'il n'est pas utilisé par un autre programme", "Opération impossible", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
    }
}

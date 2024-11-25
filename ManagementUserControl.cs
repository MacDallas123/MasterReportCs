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
using ClosedXML.Excel;
using System.IO;
using MasterReport.Properties;
using DocumentFormat.OpenXml.Wordprocessing;
using System.Diagnostics;
using DocumentFormat.OpenXml.Spreadsheet;

namespace MasterReport
{
    public partial class ManagementUserControl : UserControl
    {
        private const string db_name = "main-database.db";
        private SQLiteConnection cnx;
        private SQLiteCommand cmd;
        private SQLiteDataReader dataReader;
        private SQLiteDataAdapter dataAdapter;
        private string cnx_str = $"data source={db_name};version=3";
        private List<long> customers_idx;

        private DataTable dtt;

        private int notificationTimer = 0;
        private bool followUpEditionOn = false;

        public ManagementUserControl()
        {
            InitializeComponent();
            /*this.cnx = new SQLiteConnection(this.cnx_str);
            this.customers_idx = new List<long>();
            dtt = new DataTable();
            this.fillUserList();
            this.loadYears();*/

            this.cnx = new SQLiteConnection(this.cnx_str);
            this.customers_idx = new List<long>();
            dtt = new DataTable();
            this.fillUserList();
            this.loadYears();

            selectedCustomerEditionPanel.Location = selectedCustomerPanel.Location;
            selectedCustomerPanel.BringToFront();

            try
            {
                MonthsList.SelectedIndex = DateTime.Now.Month - 1;
                YearsList.SelectedIndex = YearsList.Items.Count == 0 ? 0 : YearsList.Items.Count - 1;
            }
            catch (Exception ex) { }

            timer1.Start();
        }

        private void fillUserList()
        {
            string query = "SELECT * FROM clients ORDER BY nom COLLATE NOCASE";
            DataTable dt = new DataTable();
            try
            {
                this.cnx.Open();
                this.cmd = new SQLiteCommand(query, this.cnx);
                this.dataAdapter = new SQLiteDataAdapter(this.cmd);

                this.dataAdapter.Fill(dt);

                customerBindingSource.DataSource = dt;
                customerList.DataSource = customerBindingSource;
                customerList.Columns[0].Visible = false;

                selectedCustomerLabel.DataBindings.Add("text", customerBindingSource, "Nom");
                selectedCustomerNameText.DataBindings.Add("text", customerBindingSource, "Nom");
                selectedCustomerIdLabel.DataBindings.Add("text", customerBindingSource, "Id");

                reportCustomerNameText.DataBindings.Add("text", customerBindingSource, "Nom");
            }
            catch (Exception ex)
            {
                //MessageBox.Show(this, ex.Message, "Erreur : Liste clients", MessageBoxButtons.OK, MessageBoxIcon.Error); 
            }

            this.cnx.Close();
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

                if(!YearsList.Items.Contains(current_year.ToString()))
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

        private void loadFollowUps(long customer_idx)
        {
            try
            {
                int current_year = DateTime.Now.Year;
                string str_year = YearsList.SelectedItem == null ? current_year.ToString() : YearsList.SelectedItem.ToString();

                int month_pos = MonthsList.SelectedIndex + 1;
                string str_month = month_pos < 10 ? $"0{month_pos.ToString()}" : month_pos.ToString();

                string query = $"SELECT *, substr(date, 4, 2) as mois, substr(date, 7, 4) as annee FROM suivis WHERE client = {customer_idx} AND annee = '{str_year}' AND mois = '{str_month}' ORDER BY id, date(date)"; // ORDER BY date(date) ASC;

                this.cnx = new SQLiteConnection(this.cnx_str);

                this.cnx.Open();
                this.dtt.Clear();
                //this.dt = new DataTable();
                this.cmd = new SQLiteCommand(query, this.cnx);
                SQLiteDataAdapter dataAdapter = new SQLiteDataAdapter(this.cmd);

                dataAdapter.Fill(this.dtt);
                followUpBindingSource.DataSource = this.dtt;
                followUpList.DataSource = followUpBindingSource;

                followUpList.Columns[0].Visible = false;
                followUpList.Columns[1].Visible = false;
                followUpList.Columns[7].Visible = false;
                followUpList.Columns[8].Visible = false;

                selectedFollowUpIdLabel.DataBindings.Clear();
                selectedFollowUpIdLabel.DataBindings.Add("text", followUpBindingSource, "Id");
                // try { selectedFollowUpIdLabel.DataBindings.Add("text", followUpBindingSource, "Id"); } catch (Exception) { }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Erreur : Données de suivi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            this.cnx.Close();
        }

        private void refresh()
        {
            customerName.Text = "";
            fillUserList();
        }

        private int getLastSold()
        {
            int last_value = 0;
            try
            {
                int last_row_idx = this.dtt.Rows.Count - 1;
                int last_column_idx = 5;//this.dt.Columns.Count - 1;
                last_value = int.Parse(this.dtt.Rows[last_row_idx][last_column_idx].ToString());
            }
            catch (Exception ex)
            {
                // MessageBox.Show(ex.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return last_value;
        }

        private int getPreviousSold(long idx)
        {
            int previous_value = 0;
            try
            {
                
                // int selected_row_idx = followUpBindingSource.Find("Id", int.Parse(selectedCustomerIdLabel.Text)) - 1;
                int selected_row_idx = followUpBindingSource.Find("Id", idx) - 1;
                int sold_column_idx = 5;//this.dt.Columns.Count - 1;
                previous_value = int.Parse(this.dtt.Rows[selected_row_idx][sold_column_idx].ToString());
            }
            catch (Exception ex)
            {
                // MessageBox.Show(ex.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return previous_value;
        }

        private long getSelectedCustomerState()
        {
            long idx = long.Parse(selectedCustomerIdLabel.Text);
            string query = $"SELECT id FROM etats WHERE client = {idx}";

            long state_idx = 0;
            try
            {
                this.cnx.Open();
                this.cmd = new SQLiteCommand(query, this.cnx);
                this.dataReader = this.cmd.ExecuteReader();

                while (dataReader.Read())
                {
                    state_idx = dataReader.GetInt64(0);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erreur : Données d'etat", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            this.cnx.Close();

            return state_idx;
        }

        private long getSelectedStateDetail(long state_idx)
        {
            string query = $"SELECT id FROM details_etats WHERE etat = {state_idx}";

            long sd_idx = 0;
            try
            {
                this.cnx.Open();
                this.cmd = new SQLiteCommand(query, this.cnx);
                this.dataReader = this.cmd.ExecuteReader();

                while (dataReader.Read())
                {
                    sd_idx = dataReader.GetInt64(0);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erreur : Données d'etat", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            this.cnx.Close();

            return sd_idx;
        }

        private void updateSoldValues()
        {
            try
            {
                FollowUp fu = new FollowUp();
                long customer_idx = long.Parse(selectedCustomerIdLabel.Text);

                int previous_sold = 0;
                for (int i = 0; i < this.dtt.Rows.Count; i++)
                {
                    long idx = long.Parse(this.dtt.Rows[i][0].ToString());

                    //int previous_sold = this.getPreviousSold(idx);
                    int debit =  int.Parse(this.dtt.Rows[i][3].ToString());
                    int credit = int.Parse(this.dtt.Rows[i][4].ToString());
                    int sold = previous_sold + debit - credit;
                    //MessageBox.Show($"idx : {idx}, previous sold : {previous_sold}, debit : {debit}, credit : {credit}; new sold : {sold}");

                    if (fu.updateSold(idx, customer_idx, sold)) {
                            previous_sold = sold;
                    }
                    else MessageBox.Show(fu.error);
                }

                this.loadFollowUps(customer_idx);

            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Erreur : Mise à jour", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ManagementUserControl_Load(object sender, EventArgs e)
        {
            this.cnx = new SQLiteConnection(this.cnx_str);
            this.customers_idx = new List<long>();
            dtt = new DataTable();
            this.fillUserList();
            this.loadYears();

            selectedCustomerEditionPanel.Location = selectedCustomerPanel.Location;
            selectedCustomerPanel.BringToFront();

            try
            {
                MonthsList.SelectedIndex = DateTime.Now.Month - 1;
                YearsList.SelectedIndex = YearsList.Items.Count == 0 ? 0 : YearsList.Items.Count - 1;
            }
            catch (Exception) { }

            withdrawDate.Value = depositDate.Value.AddDays(3);
            selectedFollowUpIdLabel.SendToBack();
        }

        private void customerList_SelectionChanged(object sender, EventArgs e)
        {
            // MessageBox.Show(this, selectedCustomerIdLabel.Text);
            try
            {
                long idx =  long.Parse(selectedCustomerIdLabel.Text);
                this.loadFollowUps(idx);
            } catch (Exception) {  }
            
        }

        private void editCustomerBtn_Click(object sender, EventArgs e)
        {
            if(!string.IsNullOrEmpty(selectedCustomerNameText.Text))
            {
                selectedCustomerEditionPanel.BringToFront();
                selectedCustomerNameText.Focus();
            }
        }

        private void searchText_TextChanged(object sender, EventArgs e)
        {
            string filterText = searchText.Text;
            if (string.IsNullOrEmpty(filterText))
            {
                customerBindingSource.RemoveFilter();
            }
            else
            {
                customerBindingSource.Filter = $"Nom LIKE '%{filterText}%'";
            }
        }

        private void addCustomerBtn_Click(object sender, EventArgs e)
        {
            string cName = customerName.Text;
            if(!string.IsNullOrEmpty(cName))
            {
                Customer customer = new Customer(cName);
                if(customer.create())
                {
                    notifMessage.BackColor = System.Drawing.Color.Green;
                    notifMessage.Text = "Client ajouté avec succès";
                    notifMessage.Visible = true;

                    this.refresh();
                }
                else
                {
                    notifMessage.BackColor = System.Drawing.Color.Red;
                    notifMessage.Text = "Echec de l'opération";
                    notifMessage.Visible = true;
                }
            }
            else
            {
                notifMessage.BackColor = System.Drawing.Color.Red;
                notifMessage.Text = "Ce client n'a pas de nom";
                notifMessage.Visible = true;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if(notifMessage.Visible)
            {
                if(notificationTimer >= 100)
                {
                    notifMessage.Visible = false;
                    notificationTimer = 0;
                }
                notificationTimer++;
            }
            
            if(notifMessageFollowUp.Visible)
            {
                if(notificationTimer >= 100)
                {
                    notifMessageFollowUp.Visible = false;
                    notificationTimer = 0;
                }
                notificationTimer++;
            }
        }

        private void deleteCustomerBtn_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(selectedCustomerNameText.Text))
            {
                if (MessageBox.Show(this, "Etes-vous sûr de vouloir supprimer ce client ?", "Confirmation suppression", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    long cId = long.Parse(selectedCustomerIdLabel.Text);
                    Customer customer = new Customer();
                    if (customer.delete(cId))
                    {
                        notifMessage.BackColor = System.Drawing.Color.Green;
                        notifMessage.Text = "Client supprimé avec succès";
                        notifMessage.Visible = true;

                        this.refresh();
                    }
                    else
                    {
                        notifMessage.BackColor = System.Drawing.Color.Red;
                        notifMessage.Text = "Echec de l'opération";
                        notifMessage.Visible = true;
                    }
                }
            }
            else MessageBox.Show(this, "Aucun client selectionné", "Suppression impossible", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void saveModificationBtn_Click(object sender, EventArgs e)
        {
            long cId = long.Parse(selectedCustomerIdLabel.Text);
            string cName = selectedCustomerNameText.Text;
            if (!string.IsNullOrEmpty(cName))
            {
                Customer customer = new Customer();
                if (customer.update(cId, cName))
                {
                    notifMessage.BackColor = System.Drawing.Color.Green;
                    notifMessage.Text = "Client modifié avec succès";
                    notifMessage.Visible = true;

                    this.refresh();
                    selectedCustomerPanel.BringToFront();
                }
                else
                {
                    notifMessage.BackColor = System.Drawing.Color.Red;
                    notifMessage.Text = "Echec de l'opération";
                    notifMessage.Visible = true;
                }
            }
            else
            {
                notifMessage.BackColor = System.Drawing.Color.Red;
                notifMessage.Text = "Ce client n'a pas de nom";
                notifMessage.Visible = true;
            }

        }

        private void cancelModification_Click(object sender, EventArgs e)
        {
            selectedCustomerPanel.BringToFront();
        }

        private void showSurveyBtn_Click(object sender, EventArgs e)
        {
            metroTabControl1.SelectTab(1);
        }

        private void chooseCustomerBtn_Click(object sender, EventArgs e)
        {
            metroTabControl1.SelectTab(0);
        }

        private void MonthsList_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                long idx = long.Parse(selectedCustomerIdLabel.Text);
                this.loadFollowUps(idx);
            }
            catch (Exception) { }
        }

        private void YearsList_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                long idx = long.Parse(selectedCustomerIdLabel.Text);
                this.loadFollowUps(idx);
            }
            catch (Exception) { }
        }

        private void completeSurveyBtn_Click(object sender, EventArgs e)
        {
            try
            {
                long idx = long.Parse(selectedCustomerIdLabel.Text);

                int debit = int.Parse(debitText.Text);
                int credit = int.Parse(creditText.Text);
                int sold = getLastSold() + debit - credit;

                DateTime? deposit_date = depositDate.Value;
                DateTime? withdraw_date = null;
                if (defineWithdrawDateCheckbox.Checked) { withdraw_date = withdrawDate.Value; }

                FollowUp fu = new FollowUp(deposit_date, debit, credit, sold, withdraw_date);
                if (fu.save(idx))
                {
                    StateDetails sd = new StateDetails(debit, deposit_date, withdraw_date);
                    if (sd.save(getSelectedCustomerState()))
                    {
                        this.loadFollowUps(idx);

                        notifMessageFollowUp.BackColor = System.Drawing.Color.Green;
                        notifMessageFollowUp.Text = "Données complétées à jour avec succès";
                        notifMessageFollowUp.Visible = true;

                        depositDate.Value = DateTime.Today;
                        withdrawDate.Value = depositDate.Value.AddDays(3);
                        debitText.Text = "0";
                        creditText.Text = "0";
                    }
                    else MessageBox.Show(sd.error);
                }
                else MessageBox.Show(fu.error);
            } catch (Exception ex) { MessageBox.Show(ex.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);  }

            
        }

        private void customerList_DoubleClick(object sender, EventArgs e)
        {
            metroTabControl1.SelectTab(1);
        }

        private void materialButton2_Click(object sender, EventArgs e)
        {
            this.updateSurveyBtn.Visible = true;
            this.completeSurveyBtn.Visible = false;
            this.cancelBtn.Visible = true;

            int selected_row_idx = followUpBindingSource.Find("Id", int.Parse(selectedFollowUpIdLabel.Text));

            depositDate.Value = DateTime.Parse(followUpList.Rows[selected_row_idx].Cells[2].Value.ToString());
            debitText.Text = followUpList.Rows[selected_row_idx].Cells[3].Value.ToString();
            creditText.Text = followUpList.Rows[selected_row_idx].Cells[4].Value.ToString();
            depositDate.Enabled = false;
            try
            {
                withdrawDate.Value = DateTime.Parse(followUpList.Rows[selected_row_idx].Cells[6].Value.ToString());
            } catch (Exception) { }
        }

        private void updateSurveyBtn_Click(object sender, EventArgs e)
        {
            long survey_idx = long.Parse(selectedFollowUpIdLabel.Text);
            long customer_idx = long.Parse(selectedCustomerIdLabel.Text);

            int debit = int.Parse(debitText.Text);
            int credit = int.Parse(creditText.Text);
            int sold = getPreviousSold(survey_idx) + debit - credit;

            DateTime? deposit_date = depositDate.Value;
            DateTime? withdraw_date = null;
            if(defineWithdrawDateCheckbox.Checked) { withdraw_date = withdrawDate.Value; }

            long state_idx = getSelectedCustomerState();
            long sd_idx = getSelectedStateDetail(state_idx);

            FollowUp fu = new FollowUp();
            if (fu.update(survey_idx, customer_idx, deposit_date, debit, credit, sold, withdraw_date))
            {

                StateDetails sd = new StateDetails();
                if (sd.update(sd_idx, state_idx, debit, deposit_date, withdraw_date))
                {
                    this.loadFollowUps(customer_idx);

                    notifMessageFollowUp.BackColor = System.Drawing.Color.Green;
                    notifMessageFollowUp.Text = "Données mise à jour avec succès";
                    notifMessageFollowUp.Visible = true;

                    this.updateSurveyBtn.Visible = false;
                    this.completeSurveyBtn.Visible = true;
                    this.cancelBtn.Visible = false;

                    depositDate.Value = DateTime.Today;
                    withdrawDate.Value = depositDate.Value.AddDays(3);
                    debitText.Text = "0";
                    creditText.Text = "0";

                    this.updateSoldValues();
                }
                else MessageBox.Show(sd.error);
            }
            else MessageBox.Show(fu.error);
        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            
        }

        private void materialButton3_Click(object sender, EventArgs e)
        {
            long survey_idx = long.Parse(selectedFollowUpIdLabel.Text);
            long customer_idx = long.Parse(selectedCustomerIdLabel.Text);

            if (MessageBox.Show(this, "Supprimer cette donnée ?", "Confirmation opération", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                FollowUp fu = new FollowUp();
                if (fu.delete(survey_idx))
                {
                    this.loadFollowUps(customer_idx);

                    notifMessageFollowUp.BackColor = System.Drawing.Color.Green;
                    notifMessageFollowUp.Text = "Donnée supprimée avec succès";
                    notifMessageFollowUp.Visible = true;

                    this.updateSurveyBtn.Visible = false;
                    this.completeSurveyBtn.Visible = true;

                    depositDate.Value = DateTime.Today;
                    withdrawDate.Value = depositDate.Value.AddDays(3);
                    debitText.Text = "0";
                    creditText.Text = "0";

                    this.updateSoldValues();
                } else MessageBox.Show(fu.error);
            }
        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            this.updateSurveyBtn.Visible = false;
            this.completeSurveyBtn.Visible = true;
            this.cancelBtn.Visible = false;

            depositDate.Value = DateTime.Today;
            withdrawDate.Value = depositDate.Value.AddDays(3);
            debitText.Text = "0";
            creditText.Text = "0";
        }

        private void defineWithdrawDateCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if (defineWithdrawDateCheckbox.Checked)
            {
                withdrawDate.Enabled = true;
            }
            else withdrawDate.Enabled = false;
        }
    }
}

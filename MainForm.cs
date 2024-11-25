using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework;
using MetroFramework.Forms;
using System.Data.SQLite;
using ClosedXML.Excel;
using System.IO;
using MasterReport.Properties;
using System.Diagnostics;

namespace MasterReport
{
    public partial class MainForm : MetroForm
    {
        private const string db_name = "main-database.db";

        private int row_cnt = 0;
        private const int blank_max = 10;
        private int blank_cnt = 0;
        private int data_nb_cols = 8;
        private double progress_value = 0;
        private int max_customers = 300;

        private string str_year = "";
        private int month_pos = 0;

        private string importedFileName = "";
        private string defaultPeriod = "";
        private string exportFileName = "";
        private string exportError = "";

        private SQLiteConnection cnx;
        private SQLiteCommand cmd;
        private SQLiteDataReader dataReader;
        private SQLiteDataAdapter dataAdapter;

        private DataTable exportDt;

        private string cnx_str = $"data source={db_name};version=3";

        private Settings settings;

        private List<Customer> customers;
        public MainForm()
        {
            InitializeComponent();

            this.createDB();
            this.createTables();
            this.settings = new Settings();
            customers = new List<Customer>();
            timer1.Start();
        }

        // To create the main-database file
        private void createDB()
        {
            if (!File.Exists(db_name))
            {
                SQLiteConnection.CreateFile(db_name);
            }
        }

        // To create all the tables of our db
        private void createTables()
        {
            string query1 = "CREATE TABLE IF NOT EXISTS clients(" +
                "id INTEGER PRIMARY KEY," +
                "nom TEXT(50))";

            string query2 = "CREATE TABLE IF NOT EXISTS suivis(" +
                "id INTEGER PRIMARY KEY," +
                "client INTEGER REFERENCES clients(id) ON DELETE CASCADE," +
                "date TEXT(50)," +
                "debit INTEGER," +
                "credit INTEGER," +
                "solde INTEGER," +
                "retrait TEXT(50))";

            string query3 = "CREATE TABLE IF NOT EXISTS  etats(" +
                "id INTEGER PRIMARY KEY," +
                "client INTEGER REFERENCES clients(id) ON DELETE CASCADE," +
                "date TEXT(50)," +
                "montant INTEGER)";

            string query4 = "CREATE TABLE IF NOT EXISTS  details_etats(" +
                "id INTEGER PRIMARY KEY," +
                "etat INTEGER REFERENCES etats(id) ON DELETE CASCADE," +
                "montant INTEGER," +
                "depot TEXT(50)," +
                "retrait TEXT(50))";

            if (File.Exists(db_name))
            {
                try
                {
                    cnx = new SQLiteConnection(cnx_str);

                    cnx.Open();

                    cmd = new SQLiteCommand(query1, cnx);
                    cmd.ExecuteNonQuery();

                    cmd = new SQLiteCommand(query2, cnx);
                    cmd.ExecuteNonQuery();

                    cmd = new SQLiteCommand(query3, cnx);
                    cmd.ExecuteNonQuery();

                    cmd = new SQLiteCommand(query4, cnx);
                    cmd.ExecuteNonQuery();

                    cnx.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Erreur : db tables", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // Past from month-year to correct date format
        private string correctFormat(string shortDate)
        {
            string correct = "";
            try
            {
                string[] splittedDate = shortDate.Split(' ');

                int month = 0;
                int year = int.Parse(splittedDate[1]);

                switch (splittedDate[0].Trim().ToLower())
                {
                    case "janvier":
                        month = 1;
                        break;
                    case "février":
                        month = 2;
                        break;
                    case "mars":
                        month = 3;
                        break;
                    case "avril":
                        month = 4;
                        break;
                    case "mai":
                        month = 5;
                        break;
                    case "juin":
                        month = 6;
                        break;
                    case "juillet":
                        month = 7;
                        break;
                    case "août":
                        month = 8;
                        break;
                    case "septembre":
                        month = 9;
                        break;
                    case "octobre":
                        month = 10;
                        break;
                    case "novembre":
                        month = 11;
                        break;
                    case "décembre":
                        month = 12;
                        break;

                }

                DateTime date = new DateTime(year, month, 1).AddMonths(1).AddDays(-1);
                correct = date.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return correct;
        }

        // get lastInsertId of any table
        private long customerLastId(string table = "clients")
        {
            long lastid = 0;
            try
            {
                string query = $"SELECT MAX(id) FROM {table}";
                this.cnx = new SQLiteConnection(cnx_str);
                cnx.Open();

                this.cmd = new SQLiteCommand(query, cnx);

                object result = this.cmd.ExecuteScalar();
                lastid = result != DBNull.Value ? Convert.ToInt64(result) : 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return lastid;
        }

        // Skip all blank lines in correct formatted file
        private void skipBlanks(IXLWorksheet worksheet, int cell = 1)
        {
            row_cnt++;
            while (worksheet.Row(row_cnt).Cell(cell).Value.IsBlank)
            {
                if (blank_cnt >= blank_max)
                {
                    Console.WriteLine("Too many blank in your file, please correct his format or change it with another correct formatted file");
                    break;
                }
                blank_cnt++;
                row_cnt++;
            }
            blank_cnt = 0;
        }

        // To get all datas in our xlsx file
        private void loadDatas(string xlsx, string defaultPeriod)
        {
            if (File.Exists(xlsx))
            {
                using (var workbook = new XLWorkbook(xlsx))
                {

                    /***** To get 'Suivi' Worsheet Datas *****/
                    var worksheet = workbook.Worksheet("Suivi");

                    // To get title
                    skipBlanks(worksheet);
                    var title = worksheet.Row(row_cnt).Cell(1).Value;
                    settings.followUpTitle = title.GetText();

                    // get month and year
                    skipBlanks(worksheet);
                    var month_year = worksheet.Row(row_cnt).Cell(1).Value;

                    //get table title
                    skipBlanks(worksheet);
                    List<String> th_list = new List<string>(data_nb_cols);
                    for (int i = 1; i < data_nb_cols + 1; i++)
                    {
                        th_list.Add(worksheet.Row(row_cnt).Cell(i).Value.GetText());
                    }

                    // default date
                    DateTime? defaultDate = null;
                    try { defaultDate = DateTime.Parse(correctFormat(defaultPeriod)); } catch (Exception) { defaultDate = null; }
                    // MessageBox.Show(this, "Date : " + defaultDate);

                    // get entries datas
                    skipBlanks(worksheet, 2);
                    var starting_line = worksheet.Row(row_cnt).Cell(2).Value.GetText();

                    bool listing_open = false; //
                    bool extra_lines = false;

                    Customer customer;
                    int customer_cnt = 1;
                    // operationProgressDetail.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                    for (int i = 0; i < max_customers; i++)
                    {
                        customer = new Customer();
                        State state = new State();

                        listing_open = false;
                        extra_lines = false;

                        string str_test = "";
                        try { str_test = worksheet.Row(row_cnt).Cell(2).Value.GetText(); } catch (Exception) { str_test = ""; }
                        if (str_test.ToLower().Contains("solde"))
                        {
                            customer_cnt = 0;
                            listing_open = true;
                        }

                        row_cnt++;
                        /* Last datas to verify that the last row is different than the current */
                        DateTime? prec_date = null;
                        var prec_label = "";
                        int prec_debit = 0;
                        int prec_credit = 0;
                        int prec_sold = 0;
                        int prec_detail = 0;
                        DateTime? prec_deposit = null;
                        DateTime? prec_withdraw = null;

                        /* Current datas to ensure correct casting */
                        DateTime? current_date = null;
                        var current_label = "";
                        int current_debit;
                        int current_credit;
                        int current_sold;
                        int current_detail;
                        DateTime? current_deposit = null;
                        DateTime? current_withdraw = null;

                        while (listing_open)
                        {
                            current_date = null;
                            current_label = "";
                            current_debit = 0;
                            current_credit = 0;
                            current_sold = 0;
                            current_detail = 0;
                            current_deposit = null;
                            current_withdraw = null;

                            var date = worksheet.Row(row_cnt).Cell(1).Value;
                            var label = worksheet.Row(row_cnt).Cell(2).Value;
                            var debit = worksheet.Row(row_cnt).Cell(3).Value;
                            var credit = worksheet.Row(row_cnt).Cell(4).Value;
                            var sold = worksheet.Row(row_cnt).Cell(5).Value;
                            //var detail = worksheet.Row(row_cnt).Cell(6).Value;
                            var detail = worksheet.Row(row_cnt).Cell(3).Value;
                            var deposit = worksheet.Row(row_cnt).Cell(7).Value;
                            var withdraw = worksheet.Row(row_cnt).Cell(8).Value;

                            try { current_date = date.GetDateTime(); } catch (Exception ex) { current_date = defaultDate; }
                            try { current_label = StrWorker.Capitalize(label.GetText()); } catch (Exception ex) { current_label = ""; }
                            try { current_debit = (int)debit.GetNumber(); } catch (Exception ex) { current_debit = 0; }
                            try { current_credit = (int)credit.GetNumber(); } catch (Exception ex) { current_credit = 0; }
                            try { current_sold = (int)sold.GetNumber(); } catch (Exception ex) { current_sold = 0; }

                            try { current_detail = (int)detail.GetNumber(); } catch (Exception ex) { current_detail = 0; }
                            try { current_deposit = deposit.GetDateTime(); } catch (Exception ex) { current_deposit = null; }
                            try { current_withdraw = withdraw.GetDateTime(); } catch (Exception ex) { current_withdraw = null; }

                            // operationProgressDetail.Caption = current_label;
                            if (prec_date != current_date ||
                                prec_debit != current_debit ||
                                prec_credit != current_credit ||
                                prec_sold != current_sold ||
                                prec_withdraw != current_withdraw)
                            {
                                customer.name = current_label;

                                // create follow-ups for this customer
                                FollowUp fu = new FollowUp(current_date, current_debit, current_credit, current_sold, current_withdraw);
                                customer.addFollowUp(fu);
                                //Console.WriteLine($"{date}\t{label}\t{debit}\t{credit}\t{sold}\t{detail}\t{deposit}\t{withdraw}\n");

                            }
                            /*if (prec_detail != current_detail ||
                                prec_deposit != current_deposit ||
                                prec_withdraw != current_withdraw)*/
                            // MessageBox.Show($"detail : {worksheet.Row(row_cnt).Cell(6).Value}; deposit : {current_deposit};  date : {current_date}; withdraw: {worksheet.Row(row_cnt).Cell(8).Value}", "Infos");
                            if (current_detail != 0 && current_deposit != null && current_withdraw != null)
                            {
                                StateDetails stateDetails = new StateDetails(current_detail, current_deposit, current_withdraw);
                                state.addDetails(stateDetails);
                            }

                            row_cnt++;
                            try
                            {
                                if (worksheet.Row(row_cnt).Cell(2).Value.GetText().ToLower().Contains("solde"))
                                //if (str_test.ToLower().Contains("solde"))
                                {

                                    try { state.amount = (int)worksheet.Row(row_cnt).Cell(5).Value.GetNumber(); } catch (Exception ex) { state.amount = 0; }

                                    customer.addState(state);
                                    listing_open = false;
                                }
                            }
                            catch (Exception ex)
                            { }

                            try { prec_date = date.GetDateTime(); } catch (Exception ex) { prec_date = defaultDate; }
                            try { prec_label = StrWorker.Capitalize(label.GetText()); } catch (Exception ex) { prec_label = ""; }
                            try { prec_debit = (int)debit.GetNumber(); } catch (Exception ex) { prec_debit = 0; }
                            try { prec_credit = (int)debit.GetNumber(); } catch (Exception ex) { prec_credit = 0; }
                            try { prec_sold = (int)sold.GetNumber(); } catch (Exception ex) { prec_sold = 0; }

                            try { prec_detail = (int)detail.GetNumber(); } catch (Exception ex) { prec_detail = 0; }
                            try { prec_deposit = deposit.GetDateTime(); } catch (Exception ex) { prec_deposit = null; }
                            try { prec_withdraw = withdraw.GetDateTime(); } catch (Exception ex) { prec_withdraw = null; }

                        }
                        skipBlanks(worksheet, 2);
                        if (!current_label.Trim().Equals(""))
                            customers.Add(customer);

                        progress_value = i;
                    }
                }
            }
            else
            {
                //MessageBox.Show(this, "Le fichier que vous avez choisi n'existe pas", "Error lors de la procédure", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            //operationProgressDetail.Caption = "Finalisation...";
            foreach (Customer customer in customers)
            {
                //MessageBox.Show($"Nom : {customer.name}");
                if (!customer.save())
                {
                    MessageBox.Show(customer.error);
                    break;
                }
                else
                {
                    //MessageBox.Show($"Nom : {customer.name} saved \n Warnings : {customer.error}");
                }

            }

            //MessageBox.Show(this, "Importation terminée", "Opération terminée", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //operationProgressDetail.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
        }

        // Create followUp Report
        private void exportFollowUp(XLWorkbook workbook, SQLiteCommand localCmd, int current_year, int month_pos)
        {
            this.dataReader = localCmd.ExecuteReader();

            /** ***** FICHE DE SUIVI ***** **/
            var worksheet = workbook.Worksheets.Add("Suivi"); // Nouvelle feuille de calcul

            worksheet.Style.Font.FontName = "calibri";

            // Titre du document
            worksheet.Range("A1:E1").Merge(); // Fusion
            var titleCell = worksheet.Cell("A1");
            titleCell.Value = "SUIVI MENSUEL DE CREANCES";
            titleCell.Style.Font.Bold = true;
            titleCell.Style.Font.FontSize = 14;
            titleCell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

            // Mois-année document
            worksheet.Range("A2:E2").Merge(); // Fusion
            var monthYearCell = worksheet.Cell("A2");
            monthYearCell.Value = new DateTime(current_year, month_pos, 1);
            monthYearCell.Style.DateFormat.Format = "MMMM-yyyy";
            monthYearCell.Style.Font.Bold = true;
            monthYearCell.Style.Font.FontSize = 14;
            monthYearCell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

            // En-tête document
            var header = worksheet.Range("A6:H6");
            header.Style.Font.Bold = true;
            header.Style.Font.FontSize = 11;

            string[] headers_content =
            {
                    "DATES",
                    "LIBELLES",
                    "DEBITS",
                    "CREDITS",
                    "SOLDES",
                    "DETAILS",
                    "DATES DE DEPOT",
                    "DATES DE RETRAIT"
                };

            string[] cells_names =
            {
                    "A6", "B6", "C6", "D6", "E6", "F6", "G6", "H6"
                };

            for (int i = 0; i < headers_content.Length; i++)
            {
                worksheet.Cell(cells_names[i]).Value = headers_content[i];
                worksheet.Cell(cells_names[i]).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                //worksheet.Cell(cells_names[i]).Style.Border.InsideBorder = XLBorderStyleValues.Thin;
            }

            int line = 9;
            bool newCustomer = true;
            string previousName = "";
            int previousSold = 0;
            while (this.dataReader.Read())
            {
                //MessageBox.Show($"Previous : {previousName}; current : {this.dataReader.GetString(8)}");
                if (string.Compare(previousName, this.dataReader.GetString(8)) != 0 && !string.IsNullOrEmpty(previousName))
                {
                    var outing = worksheet.Range(line, 1, line, 8);
                    outing.Style.Font.Bold = true;
                    outing.Style.Font.FontSize = 11;
                    outing.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                    for (int i = 1; i < 6; i++)
                    {
                        outing.Cell(1, i).Style.Border.OutsideBorder = XLBorderStyleValues.Medium;
                    }
                    DateTime date = new DateTime(current_year, month_pos, 1);
                    date = date.AddMonths(1).AddDays(-1);
                    outing.Cell(1, 2).Value = $"Solde au {date.ToLongDateString()}";
                    outing.Cell(1, 5).Value = previousSold;

                    newCustomer = true;

                    line += 3;
                }

                if (newCustomer)
                {
                    var entering = worksheet.Range(line, 1, line, 8);
                    entering.Style.Font.Bold = true;
                    entering.Style.Font.FontSize = 11;
                    entering.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                    for (int i = 1; i < 6; i++)
                    {
                        entering.Cell(1, i).Style.Border.OutsideBorder = XLBorderStyleValues.Medium;
                    }
                    DateTime date = new DateTime(current_year, month_pos, 1);
                    entering.Cell(1, 2).Value = $"Solde au {date.ToLongDateString()}";
                    entering.Cell(1, 5).Value = 0;

                    newCustomer = false;

                    line++;
                    //continue;
                }

                var data = worksheet.Range(line, 1, line, 8);
                data.Style.Font.FontSize = 11;
                data.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                for (int i = 1; i < 6; i++)
                {
                    data.Cell(1, i).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                }

                try
                {
                    data.Cell(1, 1).Value = DateTime.Parse(this.dataReader.GetString(2));
                    data.Cell(1, 1).Style.DateFormat.Format = "dd-MMMM";
                }
                catch (Exception) { } // date
                // data.Cell(1, 1).Value = DateTime.Parse(this.dataReader.GetString(2)); // date
                // data.Cell(1, 1).Style.DateFormat.Format = "dd-MMMM";

                data.Cell(1, 2).Value = this.dataReader.GetString(8); // nom
                data.Cell(1, 3).Value = this.dataReader.GetInt32(3); // debit
                data.Cell(1, 4).Value = this.dataReader.GetInt32(4); // credits
                data.Cell(1, 5).Value = this.dataReader.GetInt32(5); // solde
                data.Cell(1, 6).Value = ""; //this.dataReader.GetString(3); // details

                // data.Cell(1, 7).Value = this.dataReader.GetString(5); // date depôt
                try
                {
                    data.Cell(1, 7).Value = DateTime.Parse(this.dataReader.GetString(2));
                    data.Cell(1, 7).Style.DateFormat.Format = "dd-MMMM";
                }
                catch (Exception) { } // date dépôt
                                      //data.Cell(1, 7).Value = DateTime.Parse(this.dataReader.GetString(2)); // date dépôt
                                      //data.Cell(1, 7).Style.DateFormat.Format = "dd-MMMM";

                try
                {
                    data.Cell(1, 8).Value = DateTime.Parse(this.dataReader.GetString(6));
                    data.Cell(1, 8).Style.DateFormat.Format = "dd-MMMM";
                }
                catch (Exception) { }  // date retrait
                                       // data.Cell(1, 8).Value = DateTime.Parse(this.dataReader.GetString(6)); // date retrait
                                       // data.Cell(1, 8).Style.DateFormat.Format = "dd-MMMM";

                previousName = this.dataReader.GetString(8);
                previousSold = this.dataReader.GetInt32(5);

                line++;

            }

            // -- START THE LAST LINE
            var lastOuting = worksheet.Range(line, 1, line, 8);
            lastOuting.Style.Font.Bold = true;
            lastOuting.Style.Font.FontSize = 11;
            lastOuting.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

            for (int i = 1; i < 6; i++)
            {
                lastOuting.Cell(1, i).Style.Border.OutsideBorder = XLBorderStyleValues.Medium;
            }
            DateTime lastDate = new DateTime(current_year, month_pos, 1);
            lastDate = lastDate.AddMonths(1).AddDays(-1);
            lastOuting.Cell(1, 2).Value = $"Solde au {lastDate.ToLongDateString()}";
            lastOuting.Cell(1, 5).Value = previousSold;
            // -- END THE LAST LINE

            worksheet.Columns().AdjustToContents();
        }

        // Insert State in sheet
        private void insertState(IXLWorksheet worksheet, int line, int i, string name, DataTable table)
        {
            // name
            var nameCol = worksheet.Cell(line, 2);
            nameCol.Value = name;
            nameCol.Style.Font.Bold = true;
            nameCol.Style.Font.FontSize = 11;

            // amount
            var amountCol = worksheet.Cell(line, 5);
            amountCol.Value = table.Rows[i][3].ToString();
            amountCol.Style.Font.FontSize = 11;
            amountCol.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

            // deposit
            var depositCol = worksheet.Cell(line, 5);
            try
            {
                depositCol.Value = DateTime.Parse(table.Rows[i][2].ToString());
                depositCol.Style.DateFormat.Format = "dd-MMMM";
            }
            catch (Exception) { } // date
            depositCol.Style.Font.FontSize = 11;

            // withdraw
            var withdrawCol = worksheet.Cell(line, 6);
            try
            {
                withdrawCol.Value = DateTime.Parse(table.Rows[i][6].ToString());
                withdrawCol.Style.DateFormat.Format = "dd-MMMM";
            }
            catch (Exception) { } // date
            withdrawCol.Style.Font.FontSize = 11;
        }
        
        // Create states Report
        private void exportStates(XLWorkbook workbook, SQLiteCommand localCmd, int current_year, int month_pos)
        {
            this.dataAdapter = new SQLiteDataAdapter(localCmd);

            DataTable table = new DataTable("states table");
            this.dataAdapter.Fill(table);

            /** ***** FICHE D'ETAT ***** **/
            var worksheet = workbook.Worksheets.Add("Etat"); // Nouvelle feuille de calcul

            worksheet.Style.Font.FontName = "calibri";

            // Titre du document
            worksheet.Range("A1:E1").Merge(); // Fusion
            var titleCell = worksheet.Cell("A1");
            titleCell.Value = "ETATS DES CREANCES";
            titleCell.Style.Font.Bold = true;
            titleCell.Style.Font.FontSize = 14;
            titleCell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

            // Mois-année document
            worksheet.Range("A2:E2").Merge(); // Fusion
            var monthYearCell = worksheet.Cell("A2");
            monthYearCell.Value = new DateTime(current_year, month_pos, 1);
            monthYearCell.Style.DateFormat.Format = "MMMM-yyyy";
            monthYearCell.Style.Font.Bold = true;
            monthYearCell.Style.Font.FontSize = 14;
            monthYearCell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

            // En-tête document
            var header = worksheet.Range("A6:H6");
            header.Style.Font.Bold = true;
            header.Style.Font.FontSize = 11;

            string[] headers_content =
            {
                    "NOMS",
                    "MONTANTS",
                    "DETAILS",
                    "DATES DE DEPOT",
                    "DATES DE RETRAIT"
                };

            string[] cells_names =
            {
                    "B5", "C5", "D5", "E5", "F5"
            };

            for (int i = 0; i < headers_content.Length; i++)
            {
                worksheet.Cell(cells_names[i]).Value = headers_content[i];
                worksheet.Cell(cells_names[i]).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            }

            int line = 7;
            bool newCustomer = true;
            string name = "";
            string nextName = "";
            int previousSold = 0;
            for(int i = 0; i < table.Rows.Count - 1; i++)
            {
                if(newCustomer)
                {
                    name = table.Rows[i][8].ToString();
                    newCustomer = false;
                }

                nextName = table.Rows[i + 1][8].ToString();
                if (string.Compare(name, nextName) != 0) {

                    this.insertState(worksheet, line, i, name, table);

                    line++;

                    newCustomer = true;
                }
            }

            this.insertState(worksheet, line, table.Rows.Count - 1, table.Rows[table.Rows.Count - 1][8].ToString(), table);

            worksheet.Columns().AdjustToContents();
        }

        // Export all datas to xlsx
        private void exportDatas(string path, string str_year, int month_pos)
        {
            try
            {
                XLWorkbook workbook = new XLWorkbook(); // Xlsx file initialization

                int current_year = DateTime.Now.Year;
                // string str_year = importExportUserControl.YearsList.SelectedItem == null ? current_year.ToString() : importExportUserControl.YearsList.SelectedItem.ToString();

                // int month_pos = importExportUserControl.MonthsList.SelectedIndex + 1;
                string str_month = month_pos < 10 ? $"0{month_pos.ToString()}" : month_pos.ToString();

                string query = $"SELECT *, substr(date, 4, 2) as mois, substr(date, 7, 4) as annee FROM suivis s" +
                    $" INNER JOIN clients as c" +
                    $" ON c.id = s.client" +
                    $" WHERE annee = '{str_year}' AND mois = '{str_month}' ORDER BY c.id, date(date)"; // ORDER BY date(date) ASC;

                this.cnx = new SQLiteConnection(this.cnx_str);

                this.cnx.Open();

                this.cmd = new SQLiteCommand(query, this.cnx);
                SQLiteCommand cmd2 = new SQLiteCommand(query, this.cnx);

                /** ***** FICHE DE SUIVI ***** **/
                this.exportFollowUp(workbook, this.cmd, current_year, month_pos);

                /** ***** FICHE D'ETATS ***** **/
                this.exportStates(workbook, cmd2, current_year, month_pos);

                //worksheet.Columns().AdjustToContents();
                workbook.SaveAs(path);
            }
            catch (Exception ex)
            {
                this.exportError = ex.Message;
                //MessageBox.Show(this, ex.Message, "Erreur : exportation", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            this.cnx.Close();
        }

        private void IEUserControlImportBtn_click(object sender, EventArgs e)
        {
            string file = importExportUserControl.filenameTextBox.Text;
            if(file.Trim().CompareTo("") == 0)
            {
                MessageBox.Show(this, "Aucun fichier sélectionné", "Opération impossible", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                importExportUserControl.disableControls();
                importExportUserControl.showInformationSpinner();

                this.defaultPeriod = importExportUserControl.period;
                backgroundWorker1.RunWorkerAsync();
            }
        }

        private void reportChooseCustomerBtn_click(object sender, EventArgs e)
        {
            managementUserControl.BringToFront();

            managementBtn.BackColor = Color.MintCream;
            reportsBtn.BackColor = Color.WhiteSmoke;
            importExportBtn.BackColor = Color.WhiteSmoke;
            settingsBtn.BackColor = Color.WhiteSmoke;

            activeMarkerPanel.Location = managementBtn.Location;

            managementUserControl.metroTabControl1.SelectTab(0);
        }

        private void managementViewReportBtn_click(object sender, EventArgs e)
        {
            reportsUserControl.BringToFront();

            managementBtn.BackColor = Color.WhiteSmoke;
            reportsBtn.BackColor = Color.MintCream;
            importExportBtn.BackColor = Color.WhiteSmoke;
            settingsBtn.BackColor = Color.WhiteSmoke;

            activeMarkerPanel.Location = reportsBtn.Location;

            reportsUserControl.reloadInf();
            reportsUserControl.metroTabControl1.SelectTab(0);


            reportsUserControl.reloadInfGlob();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            importExportUserControl.importBtn.Click += IEUserControlImportBtn_click;

            importExportUserControl.exportToXlsxBtn.Click += exportToXlslxBtn_click;

            reportsUserControl.chooseCustomerBtn.Click += reportChooseCustomerBtn_click;

            managementUserControl.viewReportBtnCus.Click += managementViewReportBtn_click;
            managementUserControl.viewReportBtnSur.Click += managementViewReportBtn_click;

            reportsUserControl.customerNameInf.DataBindings.Add("text", managementUserControl.customerBindingSource, "Nom");
            reportsUserControl.customerIdInf.DataBindings.Add("text", managementUserControl.customerBindingSource, "Id");
            
            DateTime date = DateTime.Now;
            DateTime set_current_date;
            try { set_current_date = this.settings.current_date; } catch (Exception) { set_current_date = DateTime.Now; }
            DateTime set_max_date = this.settings.date_max;

            //if(date.CompareTo(DateTime.Now) < 0)
            if(set_current_date.CompareTo(set_max_date) > 0 ||
                this.settings.days_current > this.settings.days_max)
            {
                MessageBox.Show("Delai d'utilisation de l'application dépassé, veuillez contacter le développeur de l'application");
                splitContainer1.Enabled = false;
            }
        }

        private void exitBtn_Click(object sender, EventArgs e)
        {
            //if (MetroMessageBox.Show(this, "Quitter L'application ?", "Confirmation") == DialogResult.OK)
            if (MessageBox.Show(this, "Quitter l'application ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void settingsBtn_Click(object sender, EventArgs e)
        {
            settingsUserControl.BringToFront();

            managementBtn.BackColor = Color.WhiteSmoke;
            reportsBtn.BackColor = Color.WhiteSmoke;
            importExportBtn.BackColor = Color.WhiteSmoke;
            settingsBtn.BackColor = Color.MintCream;

            activeMarkerPanel.Location = settingsBtn.Location;
        }

        private void importExportBtn_Click(object sender, EventArgs e)
        {
            importExportUserControl.BringToFront();

            managementBtn.BackColor = Color.WhiteSmoke;
            reportsBtn.BackColor = Color.WhiteSmoke;
            importExportBtn.BackColor = Color.MintCream;
            settingsBtn.BackColor = Color.WhiteSmoke;

            activeMarkerPanel.Location = importExportBtn.Location;
        }

        private void reportsBtn_Click(object sender, EventArgs e)
        {
            reportsUserControl.BringToFront();

            managementBtn.BackColor = Color.WhiteSmoke;
            reportsBtn.BackColor = Color.MintCream;
            importExportBtn.BackColor = Color.WhiteSmoke;
            settingsBtn.BackColor = Color.WhiteSmoke;

            activeMarkerPanel.Location = reportsBtn.Location;

            reportsUserControl.reloadInf();

            reportsUserControl.reloadInfGlob();
        }

        private void managementBtn_Click(object sender, EventArgs e)
        {
            managementUserControl.BringToFront();

            managementBtn.BackColor = Color.MintCream;
            reportsBtn.BackColor = Color.WhiteSmoke;
            importExportBtn.BackColor = Color.WhiteSmoke;
            settingsBtn.BackColor = Color.WhiteSmoke;

            activeMarkerPanel.Location = managementBtn.Location;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            int value = (int)(progress_value / (max_customers / 100));
            importExportUserControl.spinnerPercentage.Text = $"{value}%";
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            string importedFileName = importExportUserControl.filenameTextBox.Text;
            if (!string.IsNullOrEmpty(importedFileName))
                loadDatas(importedFileName, this.defaultPeriod);
            else MessageBox.Show(this, "Le chemin d'accès spécifié est introuvable", "Erreur : fichier importation", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            importExportUserControl.enableControls();
            importExportUserControl.hideInformationSpinner();
            MessageBox.Show(this, "Importation terminée, veuillez redémarrer le logiciel pour que les modifications prennent effet", "Opération terminée", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void exportToXlslxBtn_click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Fichier Excel (*.xlsx)|*.xlsx";
            saveFileDialog.Title = "Sélectionner l'emplacement de sauvegarde du fichier";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filename =  saveFileDialog.FileName;

                if(!string.IsNullOrEmpty(filename))
                {
                    importExportUserControl.showExportInformationSpinner();
                    this.exportFileName = filename;

                    int current_year = DateTime.Now.Year;
                    this.str_year = importExportUserControl.YearsList.SelectedItem == null ? current_year.ToString() : importExportUserControl.YearsList.SelectedItem.ToString();
                    this.month_pos = importExportUserControl.MonthsList.SelectedIndex + 1;
                    backgroundWorker2.RunWorkerAsync();

                    /* this.exportDatas(this.exportFileName);

                    if(MessageBox.Show(this, "L'exportation s'est bien terminée. Souhaitez-vous ouvrir le fichier exporté ?", "Opération réussie", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        Process.Start(this.exportFileName);
                    } */

                } else MessageBox.Show(this, "Le chemin d'accès spécifié est introuvable", "Erreur : fichier exportation", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void importExportUserControl_Load(object sender, EventArgs e)
        {
            //importExportUserControl.exportToXlsxBtn.Click += exportToXlslxBtn_click;
        }

        private void backgroundWorker2_DoWork(object sender, DoWorkEventArgs e)
        {

            this.exportDatas(this.exportFileName, this.str_year, this.month_pos);
        }

        private void backgroundWorker2_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            importExportUserControl.hideExportInformationSpinner();
            
            if(string.IsNullOrEmpty(this.exportError))
            {
                if (MessageBox.Show(this, "L'exportation s'est bien terminée. Souhaitez-vous ouvrir le fichier exporté ?", "Opération réussie", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Process.Start(this.exportFileName);
                }
            }
            else
            {
                MessageBox.Show(this, this.exportError, "Opération réussie", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            }

            
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            MessageBox.Show($"Saving...{this.settings.current_date} : {this.settings.days_current}");
            //this.settings.current_date = DateTime.Now;
            
            if(this.settings.current_date.ToShortDateString().CompareTo(DateTime.Now.ToShortDateString()) != 0)
            {
                this.settings.days_current++;
            }

            this.settings.current_date = DateTime.Now;

            this.settings.Save();
        }
    }
}

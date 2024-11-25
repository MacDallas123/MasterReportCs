using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace MasterReport
{
    internal class FollowUp
    {
        //public Customer customer { get; set; }
        public DateTime? date { get; set; }
        
        public int debit { get; set; }
        public int credit { get; set; }
        public int pay { get; set; } // solde

        public DateTime? withdraw { get; set; }

        private const string db_name = "main-database.db";
        private SQLiteConnection cnx;
        private SQLiteCommand cmd;
        private string cnx_str = $"data source={db_name};version=3";

        // To manage errors
        public string error { get; set; }

        public FollowUp(DateTime? date, int debit, int credit, int pay, DateTime? withdraw) {
            this.date = date;
            this.debit = debit;
            this.credit = credit;
            this.pay = pay;
            this.withdraw = withdraw;

            this.error = "";
            this.cnx = new SQLiteConnection(cnx_str);
        }

        public FollowUp() {
            this.error = "";
            this.cnx = new SQLiteConnection(cnx_str);
        }

        public Boolean save(long id)
        {
            bool passed = false;
            try
            {
                string query = $"INSERT INTO suivis(client, date, debit, credit, solde, retrait) VALUES ({id}, '{this.date.ToString()}', {this.debit}, {this.credit}, {this.pay}, '{this.withdraw.ToString()}')";
                cnx.Open();

                this.cmd = new SQLiteCommand(query, cnx);

                if (this.cmd.ExecuteNonQuery() == 1)
                {
                    passed = true;
                }
                cnx.Close();
            }
            catch (Exception ex)
            {
                this.error += "  Follow Up : " + ex.Message;
            }
            return passed;
        }

        public Boolean update(long id, long customer_id, DateTime? updated_date, int updated_debit, int updated_credit, int updated_sold, DateTime? updated_withdraw)
        {
            bool passed = false;
            try
            {
                string query = $"UPDATE suivis SET client = {customer_id}, date = '{updated_date.ToString()}', debit = {updated_debit}, credit = {updated_credit}, solde = {updated_sold}, retrait = '{updated_withdraw.ToString()}' WHERE id = {id};";
                cnx.Open();

                this.cmd = new SQLiteCommand(query, cnx);

                if (this.cmd.ExecuteNonQuery() == 1)
                {
                    passed = true;
                }
                cnx.Close();
            }
            catch (Exception ex)
            {
                this.error += "  Follow Up : " + ex.Message;
            }
            return passed;
        }

        public Boolean updateSold(long id, long customer_id, int updated_sold)
        {
            bool passed = false;
            try
            {
                string query = $"UPDATE suivis SET client = {customer_id}, solde = {updated_sold} WHERE id = {id};";
                cnx.Open();

                this.cmd = new SQLiteCommand(query, cnx);

                if (this.cmd.ExecuteNonQuery() == 1)
                {
                    passed = true;
                }
                cnx.Close();
            }
            catch (Exception ex)
            {
                this.error += "  Follow Up : " + ex.Message;
            }
            return passed;
        }

        public bool delete(long id)
        {
            bool passed = false;
            try
            {
                string query = $"DELETE FROM suivis WHERE id = {id};";
                cnx.Open();

                this.cmd = new SQLiteCommand(query, cnx);

                if (this.cmd.ExecuteNonQuery() == 1)
                {
                    passed = true;
                }
                cnx.Close();
            }
            catch (Exception ex)
            {
                this.error += "  Follow Up : " + ex.Message;
            }
            return passed;
        }
    }
}

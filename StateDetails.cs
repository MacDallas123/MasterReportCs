using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterReport
{
    internal class StateDetails
    {
        public State state { get; set; }
        public int amount { get; set; }
        public DateTime? deposit { get; set; }
        public DateTime? withdraw { get; set; }

        private const string db_name = "main-database.db";
        private SQLiteConnection cnx;
        private SQLiteCommand cmd;
        private string cnx_str = $"data source={db_name};version=3";

        // To manage errors
        public string error { get; set; }

        public StateDetails(int amount, DateTime? deposit, DateTime? withdraw) {
            this.amount = amount;
            this.deposit = deposit;
            this.withdraw = withdraw;

            this.error = "";
            this.cnx = new SQLiteConnection(cnx_str);
        }

        public StateDetails()
        {
            this.error = "";
            this.cnx = new SQLiteConnection(cnx_str);
        }


        public Boolean save(long id)
        {
            bool passed = false;
            try
            {
                string query = $"INSERT INTO details_etats(etat, montant, depot, retrait) VALUES ({id}, {this.amount}, '{this.deposit.ToString()}', '{this.withdraw.ToString()}')";
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
                this.error += "  Details : " + ex.Message;
            }
            return passed;
        }

        public Boolean update(long state_detail_idx, long state_idx, int updated_amount, DateTime? updated_deposit, DateTime? updated_withdraw)
        {
            bool passed = false;
            try
            {
                string query = $"UPDATE details_etats SET montant = {updated_amount}, depot = '{updated_deposit}', retrait = '{updated_withdraw}' WHERE id = {state_detail_idx} AND etat = {state_idx}";
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
                this.error += "  Details : " + ex.Message;
            }
            return passed;
        }

        public Boolean updateDebit(long state_detail_idx, long state_idx, int updated_amount)
        {
            bool passed = false;
            try
            {
                string query = $"UPDATE details_etats SET montant = {updated_amount} WHERE id = {state_detail_idx} AND etat = {state_idx}";
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
                this.error += "  Details : " + ex.Message;
            }
            return passed;
        }

    }
}

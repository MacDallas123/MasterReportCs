using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace MasterReport
{
    internal class State
    {
        //public Customer customer { get; set; }
        public int amount { get; set; }

        public List<StateDetails> details { get; set; }

        private const string db_name = "main-database.db";
        private SQLiteConnection cnx;
        private SQLiteCommand cmd;
        private string cnx_str = $"data source={db_name};version=3";

        // To manage errors
        public string error { get; set; }

        public State(int amount) {
            this.amount = amount;

            this.details = new List<StateDetails>();

            this.error = "";
            this.cnx = new SQLiteConnection(cnx_str);
        }

        public State() {
            this.details = new List<StateDetails>();

            this.error = "";
            this.cnx = new SQLiteConnection(cnx_str);
        }

        private long getLastInsertId()
        {
            long lastid = 0;
            try
            {
                string query = $"SELECT MAX(id) FROM etats";
                cnx.Open();

                this.cmd = new SQLiteCommand(query, cnx);

                object result = this.cmd.ExecuteScalar();
                lastid = result != DBNull.Value ? Convert.ToInt64(result) : 0;
                cnx.Close();
            }
            catch (Exception ex)
            { this.error += ex.Message; }
            return lastid;
        }

        public void addDetails(StateDetails stateDetails){
            this.details.Add(stateDetails);
        }

        public Boolean save(long id)
        {
            bool passed = false;
            try
            {
                long lastId = this.getLastInsertId() + 1;

                string query = $"INSERT INTO etats(client, montant) VALUES ({id}, {this.amount})";
                cnx.Open();

                this.cmd = new SQLiteCommand(query, cnx);

                if (this.cmd.ExecuteNonQuery() == 1)
                {
                    passed = true;
                    foreach (var detail in this.details)
                    {

                        if (!detail.save(lastId))
                        {
                            passed = false;
                            this.error += detail.error;
                            break;
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                this.error += "  State : " + ex.Message;
            }
            cnx.Close();
            return passed;
        }


        public bool completeDetail(long id, int montant, DateTime? deposit, DateTime? withdraw)
        {
            bool passed = false;
            try
            {
                string query = $"INSERT INTO details_etats(etat, montant, depot, retrait) VALUES ({id}, {montant}, '{deposit.ToString()}', '{withdraw.ToString()}')";
                cnx.Open();

                this.cmd = new SQLiteCommand(query, cnx);

                if (this.cmd.ExecuteNonQuery() == 1) passed = true;
                else passed = false;
            }
            catch (Exception ex)
            {
                this.error += "  Details : " + ex.Message;
            }
            cnx.Close();

            return passed;
        }

        public bool create(long id)
        {
            bool passed = false;
            try
            {
                string current_str_date = DateTime.Now.ToString();
                string query = $"INSERT INTO etats(client, montant, date) VALUES ({id}, {this.amount}, {current_str_date})";
                cnx.Open();

                this.cmd = new SQLiteCommand(query, cnx);

                if (this.cmd.ExecuteNonQuery() == 1) passed = true;
                else passed = false;
                
            }
            catch (Exception ex)
            {
                this.error += "  State : " + ex.Message;
            }
            cnx.Close();
            return passed;
        }

    }
}

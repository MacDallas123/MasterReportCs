using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace MasterReport
{
    internal class Customer
    {
        public string name { get; set; }

        public List<FollowUp> followUps { get; set; }
        public List<State> states { get; set; }

        private const string db_name = "main-database.db";
        private SQLiteConnection cnx;
        private SQLiteCommand cmd;
        private string cnx_str = $"data source={db_name};version=3";

        // To manage errors
        public string error { get; set; }

        public Customer(string name) {
            this.name = name;

            this.followUps = new List<FollowUp>();
            this.states = new List<State>();

            this.error = "";
            this.cnx = new SQLiteConnection(cnx_str);
        }

        public Customer() {
            this.followUps = new List<FollowUp>();
            this.states = new List<State>();

            this.error = "";
            this.cnx = new SQLiteConnection(cnx_str);
        }

        public void addFollowUp(FollowUp followUp) { 
            this.followUps.Add(followUp);
        }
        
        public void addState(State state) { 
            this.states.Add(state);
        }

        private long getLastInsertId()
        {
            long lastid = 0;
            try
            {
                string query = $"SELECT MAX(id) FROM clients";
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

        // Save Customer with all her dependencies
        public bool save()
        {
            bool passed = false;
            try
            {
                long lastId = this.getLastInsertId() + 1;

                string query = $"INSERT INTO clients(nom) VALUES (\"{this.name}\")";
                cnx.Open();

                this.cmd = new SQLiteCommand(query, cnx);

                if (this.cmd.ExecuteNonQuery() == 1)
                {
                    passed = true;

                    foreach (var followUp in this.followUps)
                    {
                        
                        if (!followUp.save(lastId))
                        {
                            passed = false;
                            this.error += followUp.error;
                            break; 
                        }
                        
                    }
                    foreach (var state in this.states)
                    {
                        if (!state.save(lastId))
                        {
                            passed = false;
                            this.error += state.error;
                            break;
                        }
                    }
                }
                cnx.Close();
            }
            catch (Exception ex)
            {
                this.error += "  Custommer : " + ex.Message;
            }
            return passed;
        }

        // Add New Customer
        public bool create()
        {
            bool passed = false;
            try
            {
                string query = $"INSERT INTO clients(nom) VALUES (\"{this.name}\")";
                cnx.Open();

                this.cmd = new SQLiteCommand(query, cnx);

                if (this.cmd.ExecuteNonQuery() == 1) passed = true;
                else passed = false;
                cnx.Close();
            }
            catch (Exception ex)
            {
                this.error += "  Custommer : " + ex.Message;
            }
            return passed;
        }

         // Delete Customer
        public bool delete(long id)
        {
            bool passed = false;
            try
            {
                string query = $"DELETE FROM clients WHERE id = {id}";
                cnx.Open();

                this.cmd = new SQLiteCommand(query, cnx);

                if (this.cmd.ExecuteNonQuery() == 1) passed = true;
                else passed = false;

                cnx.Close();
            }
            catch (Exception ex)
            {
                this.error += "  Custommer : " + ex.Message;
            }
            return passed;
        }

         // Update Customer
        public bool update(long id, string updated_name)
        {
            bool passed = false;
            try
            {
                string query = $"UPDATE clients SET nom = \"{updated_name}\" WHERE id = {id}";
                cnx.Open();

                this.cmd = new SQLiteCommand(query, cnx);

                if (this.cmd.ExecuteNonQuery() == 1) passed = true;
                else passed = false;

                cnx.Close();
            }
            catch (Exception ex)
            {
                this.error += "  Custommer : " + ex.Message;
            }
            return passed;
        }

        // Save followUp
        private Boolean saveFollowUp(FollowUp followUp) {
            bool passed = false;
            try
            {
                long clientId = this.getLastInsertId();
                string query = $"INSERT INTO suivis(client, date, debit, credit, solde, retrait) VALUES ({clientId}, '{followUp.date.ToString()}', {followUp.debit}, {followUp.credit}, {followUp.pay}, '{followUp.withdraw.ToString()}')";
                cnx.Open();

                this.cmd = new SQLiteCommand(query, cnx);

                if (this.cmd.ExecuteNonQuery() == 1)
                {
                    passed = true;
                }
            }
            catch (Exception ex)
            { }
            return passed;
        }

    }
}

using System;
using System.Data.SqlClient;
using HomeChefBackend.Models;
using MySql.Data.MySqlClient;

namespace HomeChefBackend
{
    public class UserManagement
    {
        private static string cs = "server=localhost;port=3306;user=sghruddy;password=Thisissostupid123!;database=homechef_administration;";
        public string Login(string email, String password)
        {
            using (var connection = new MySqlConnection(cs))
            {
                try
                {
                    connection.Open();
                    Console.WriteLine("Opening Login Connection To Database");
                    var mySql = "SELECT * FROM homechef_administration.users WHERE email = '" + email + "'  and password = '" + password + "'";
                    var cmd = new MySqlCommand(mySql, connection);
                    using (MySqlDataReader rdr = cmd.ExecuteReader())
                    {
                        if (rdr.HasRows)
                        {
                            try
                            {
                                rdr.Read();
                                var value =  rdr.GetFieldValue<string>(0);
                                return value;
                            }
                            catch (Exception ex)
                            {
                                return "Failed";
                            }
                        }
                        else
                        {
                            return "Failed";
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    return "Failed";
                    //TODO: Get rid of all these try catches.
                }
            }
        }

        public bool CreateAccount(string id, string email, string password)
        {
            //TODO: ADD user to the user settings table too
            if (!doesAccountExist(email))
            {
                string insertQuery = "insert into homechef_administration.users(userid,email,password) values('" + id + "','"+ email + "','" + password + "');";
                MySqlConnection connection = new MySqlConnection(cs);
                MySqlCommand MySqlCommand = new MySqlCommand(insertQuery, connection);
                MySqlDataReader rdr;
                try
                {
                    connection.Open();
                    rdr = MySqlCommand.ExecuteReader();

                    while (rdr.Read())
                    {
                    }

                    connection.Close();
                    return true;
                }

                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    return false;
                }
            }
            else
            {
                return false;
            }
            
        }

        public bool DeleteAccount(string userId)
        {
            try
            {
                string query = "delete from homechef_administration.users where userid='" + userId + "';";
                MySqlConnection connection = new MySqlConnection(cs);
                MySqlCommand cmd = new MySqlCommand(query, connection);
                MySqlDataReader rdr;
                connection.Open();
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                }
                connection.Close();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }

        private bool doesAccountExist(string email)
        {
            try{
                string checkExistsQuery = "SELECT * FROM homechef_administration.users WHERE email = '" + email+"'";

                MySqlConnection connection = new MySqlConnection(cs);
                MySqlCommand MySqlCommand = new MySqlCommand(checkExistsQuery, connection);
                MySqlDataReader rdr;
                connection.Open();
                rdr = MySqlCommand.ExecuteReader();

                while (rdr.Read())
                {
                    if (rdr.HasRows)
                    {
                        //if already exists
                        return true;
                    }
                }
                connection.Close();
                return false;
            }
            catch(Exception ex)
            {
                Console.WriteLine("Delete account failed:" + ex);
                return false;
            }
        }

        public bool AddUserToPreferencesDB(string userid)
        {
            var preferencesid = Guid.NewGuid().ToString();
            //todo: make dietryrequirements lower case.
            string insertQuery = "insert into homechef_administration.preferences(preferencesid,userid) values('" + preferencesid + "','" + userid + "');";
            MySqlConnection connection = new MySqlConnection(cs);
            MySqlCommand MySqlCommand = new MySqlCommand(insertQuery, connection);
            MySqlDataReader rdr;
            try
            {
                connection.Open();
                rdr = MySqlCommand.ExecuteReader();

                while (rdr.Read())
                {
                }

                connection.Close();
                return true;
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }
    }
}

using System;
using System.Data;
using System.Data.SqlClient;
using HomeChefBackend.Models;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace HomeChefBackend
{
    public class PreferenceManagement
    {
        private static string cs = "server=localhost;port=3306;user=sghruddy;password=Thisissostupid123!;database=homechef_administration;";

        public bool SavePreferences(string userid, string dietryrequirements, string intollerances, string measurementunits, string portion)
        {
            var portionAsInt = Int16.Parse(portion);
            //todo: make dietryrequirements lower case.
        
            string insertQuery = "UPDATE homechef_administration.preferences SET dietryrequirements="
                    +dietryrequirements+"', intollerances='"+intollerances+"',measuringunit='"+measurementunits+"',portion='"+portionAsInt+
                    "WHERE userid ="+userid+"' );";
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


        public UserPreferencesModel GetUserPreferences (string userId)
        {
            using (var connection = new MySqlConnection(cs))
            {
                try
                {
                    connection.Open();
                    Console.WriteLine("Opening Login Connection To Database");
                    var mySql = "SELECT * FROM homechef_administration.preferences WHERE userid = '"+userId+"'";
                    var cmd = new MySqlCommand(mySql, connection);
                    using (MySqlDataReader rdr = cmd.ExecuteReader())
                    {
                        if (rdr.HasRows)
                        {
                            rdr.Read();
                            var dietryRequirements = "";
                            var intollerances = "";
                            var measurement = false;
                            if (!rdr.IsDBNull(3))
                            {
                                dietryRequirements = rdr.GetFieldValue<string>(3);
                            }
                            if (!rdr.IsDBNull(4))
                            {
                                intollerances = rdr.GetFieldValue<string>(4);
                            }
                            ///TODO:When saving make sure its integer too
                            if (rdr.GetFieldValue<int>(5) == 1)
                            {
                                measurement = true;
                            }
                            var userPreferences = new UserPreferencesModel(
                                rdr.GetFieldValue<string>(0),
                                rdr.GetFieldValue<string>(1),
                                rdr.GetFieldValue<int>(2),
                                dietryRequirements.Split(","),
                                intollerances.Split(","),
                                measurement);
                            return userPreferences;
                            
                        }
                        else
                        {
                            throw new Exception("Could not retrieve users preferences");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    throw new Exception("Could not retrieve preferences " + ex);
                    //TODO: Get rid of all these try catches.
                }
            }

        }
    }
}

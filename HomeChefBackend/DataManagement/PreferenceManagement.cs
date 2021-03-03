using System;
using System.Data.SqlClient;
using HomeChefBackend.Models;
using MySql.Data.MySqlClient;

namespace HomeChefBackend
{
    public class PreferenceManagement
    {
        private static string cs = "server=localhost;port=3306;user=sghruddy;password=Thisissostupid123!;database=homechef_administration;";
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
                            var userPreferences = new UserPreferencesModel(
                                rdr.GetFieldValue<string>(0),
                                rdr.GetFieldValue<string>(1),
                                rdr.GetFieldValue<int>(2),
                                rdr.GetFieldValue<string[]>(3),
                                rdr.GetFieldValue<bool>(4));
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

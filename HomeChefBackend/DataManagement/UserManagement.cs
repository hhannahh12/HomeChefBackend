using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace HomeChefBackend
{
    public class UserManagement
    {
        private static string cs = @"server=localhost;userid=sghruddy;password=Thisissostupid123!;database=homechef_administration";

        public UserManagement()
        {

        }
        public static List<string> GetUsers()
        {
            var connection = new MySqlConnection(cs);
            var MySql = "select * from users";
            if(connection.State != System.Data.ConnectionState.Open)
            {
                connection.Open();
            }
            var cmd = new MySqlCommand(MySql, connection);
            using MySqlDataReader rdr = cmd.ExecuteReader();
            {
                var UserIds = new List<string>();
                while (rdr.Read())
                {
                    UserIds.Add(rdr.GetString(0));
                }
                return UserIds;
            }
        }
    }
}

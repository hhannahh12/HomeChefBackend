using System;
using System.Data.SqlClient;
using HomeChefBackend.Models;
using MySql.Data.MySqlClient;

namespace HomeChefBackend
{
    public class UserManagement
    {
        private static string cs = "server=localhost;port=3306;user=sghruddy;password=Thisissostupid123!;database=homechef_administration;";

        public Result Login(string email, String password)
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
                        // if(rdr.ha)
                        var c = "hello";


                        if (rdr.HasRows)
                        {
                            try
                            {
                                rdr.Read();
                                var a = rdr.GetFieldValue<string>(1);
                                return new Result()
                                {
                                    //TODO:Might just return id.. not sure on use of email yet.
                                    result = Result.LoginResult.success,
                                    user = new User(rdr.GetFieldValue<string>(0), rdr.GetFieldValue<string>(1))
                                };
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.ToString());
                                return new Result()
                                {
                                    result = Result.LoginResult.failure
                                };
                            }
                        }
                        else
                        {
                            return new Result()
                            {
                                result = Result.LoginResult.failure
                            };

                        }
                    }
                    
                }

                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    return new Result()
                    {
                        result = Result.LoginResult.failure
                    };
                }
            }
            
            
        }
    }
}

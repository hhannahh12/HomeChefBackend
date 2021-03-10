using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using HomeChefBackend.Models;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace HomeChefBackend
{
    public class PantryManagement
    {
        private static string cs = "server=localhost;port=3306;user=sghruddy;password=Thisissostupid123!;database=homechef_administration;";

        public string GetPantryId(string userId)
        {
            using (var connection = new MySqlConnection(cs))
            {
                try
                {
                    connection.Open();
                    Console.WriteLine("Opening Login Connection To Database");
                    var mySql = "SELECT * FROM homechef_administration.pantry WHERE userid = '" + userId + "'";
                    var cmd = new MySqlCommand(mySql, connection);
                    using (MySqlDataReader rdr = cmd.ExecuteReader())
                    {
                        if (rdr.HasRows)
                        {
                            rdr.Read();
                            return rdr.GetFieldValue<string>(0);
                        }
                        else
                        {
                            return "failed";
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    throw new Exception("Could not retrieve preferences id " + ex);
                }
            }
        }
        public  bool SaveIngredients(string query)
        {
            return true;
            //TODO: REDUNDANT METHOD
        
        }
        public bool AddIngredients(string id, IngredientModel[] ingredients)
        {
            var existingIngredients = GetPantry(id);
            var uniqueIngredients = new IngredientModel();
            existingIngredients.Concat(ingredients).Distinct();
            var uniqueIngredientsJson = JsonConvert.SerializeObject(uniqueIngredients).ToString();

            string insertQuery = "UPDATE homechef_administration.pantry SET ingredients= '" + uniqueIngredientsJson + "' WHERE pantryid ='" + id + "';";
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

        public bool RemoveIngredients(string id, string ingredients)
        {
            var existingIngredients = GetPantry(id).Split(",");
            var removeIngredients = ingredients.Split(",");
            var ingredientsString = "";
            ingredientsString = existingIngredients.Except(removeIngredients).ToArray().ToString() ;
            
            string insertQuery = "UPDATE homechef_administration.pantry SET ingredients= '" + ingredientsString + "' WHERE pantryid ='" + id + "';";
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

        public IngredientModel[] GetPantry(string pantryid)
        {
            using (var connection = new MySqlConnection(cs))
            {
                try
                {
                    connection.Open();
                    Console.WriteLine("Opening Login Connection To Database");
                    var mySql = "SELECT * FROM homechef_administration.pantry WHERE pantryid = '" + pantryid + "'";
                    var cmd = new MySqlCommand(mySql, connection);
                    using (MySqlDataReader rdr = cmd.ExecuteReader())
                    {
                        if (rdr.HasRows)
                        {
                            rdr.Read();
                            var value = rdr.GetFieldValue<string>(2);
                            return JsonSerializer.Deserialize<IngredientModel[]>(value, );
                            
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
        
    }
}

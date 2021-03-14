using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using HomeChefBackend.Models;
using MoreLinq;

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
            string addIngredients = "";
            var existingIngredients = GetPantry(id);
            if (ingredients == null)
            {
                return true;
            }
            if (existingIngredients == null)
            {
                addIngredients = ingredients.ToString();
            }
            else
            {
                var a = ingredients.Concat(existingIngredients).ToArray().Where(g =>g!= null).ToArray();
     
                var c = a.DistinctBy(x=>x.id).ToArray();
                foreach(var b in c)
                {
                    addIngredients += JsonConvert.SerializeObject(b) + "$";

                }
            }

            string insertQuery = "UPDATE homechef_administration.pantry SET ingredients= '" + addIngredients + "' WHERE pantryid ='" + id + "';";
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

        public bool RemoveIngredients(string id, IngredientModel[] ingredients)
        {
            
            string removeIngredients = "";
            var existingIngredients = GetPantry(id).Where(x=>x!=null).ToArray();
            if (ingredients == null)
            {
                return true;
            }
            if (existingIngredients == null)
            {
                return true;
            }
            else
            {
                var ingredientsList = existingIngredients.Where(a => ingredients.All(b => b.id != a.id)).ToArray();
                foreach (var ingredient in ingredientsList)
                {
                    removeIngredients += JsonConvert.SerializeObject(ingredient) + "$";
                }
            }
            string insertQuery = "UPDATE homechef_administration.pantry SET ingredients= '" + removeIngredients + "' WHERE pantryid ='" + id + "';";
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
            //TODO: Why does it always return null to begin..
            try
            {
                using (var connection = new MySqlConnection(cs))
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
                            var value = rdr.GetFieldValue<string?>(2);
                            var ingredientsString = value.Split("$");
                            var returnedIngredientsArray = new IngredientModel[ingredientsString.Length];
                            var indexer = 0;
                            foreach(var ingredient in ingredientsString)
                            {
                                returnedIngredientsArray[indexer] = JsonConvert.DeserializeObject<IngredientModel>(ingredient);
                                indexer++;
                            }
                            return returnedIngredientsArray;
                        }
                        return new IngredientModel[0];
                    }
                }
            }
            catch(Exception ex)
            {
                return new IngredientModel[0];
            }
        }  
    }
}

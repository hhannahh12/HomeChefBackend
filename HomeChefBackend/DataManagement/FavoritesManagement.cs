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
    public class FavoritesManagement
    {
        private static string cs = "server=localhost;port=3306;user=sghruddy;password=Thisissostupid123!;database=homechef_administration;";
        public RecipeInfoModel[] GetFavorites(string favoritesId)
        {
            try
            {
                using (var connection = new MySqlConnection(cs))
                {
                    connection.Open();
                    var mySql = "SELECT * FROM homechef_administration.favorites WHERE favorites = '" + favoritesId + "'";
                    var cmd = new MySqlCommand(mySql, connection);

                    using (MySqlDataReader rdr = cmd.ExecuteReader())
                    {
                        if (rdr.HasRows)
                        {
                            rdr.Read();
                            var value = rdr.GetFieldValue<string?>(2);
                            var favoritesString = value.Split("$");
                            var returnedFavoritesArray = new RecipeInfoModel[favoritesString.Length];
                            var indexer = 0;
                            foreach (var favorite in favoritesString)
                            {
                                returnedFavoritesArray[indexer] = JsonConvert.DeserializeObject<RecipeInfoModel>(favorite);
                                indexer++;
                            }
                            return returnedFavoritesArray;
                        }
                        return new RecipeInfoModel[0];
                    }
                }
            }
            catch (Exception ex)
            {
                return new RecipeInfoModel[0];
            }
        }
        public bool AddFavorite(string id, RecipeInfoModel favorite)
        {
            string addFavorites = "";
            var existingFavorites = GetFavorites(id);
            if (favorite == null)
            {
                return true;
            }
            if (existingFavorites == null)
            {
                addFavorites = favorite.ToString();
            }
            else
            {

                existingFavorites[existingFavorites.Length] = favorite;

                var c = existingFavorites.DistinctBy(x => x.id).ToArray();
                foreach (var b in c)
                {
                    addFavorites += JsonConvert.SerializeObject(b) + "$";

                }
            }

            string insertQuery = "UPDATE homechef_administration.favorites SET favorites= '" + addFavorites + "' WHERE favoritesid ='" + id + "';";
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
        public bool RemoveFavorites(string id, RecipeInfoModel favorite)
        {

            string removeIngredients = "";
            var existingFavorites = GetFavorites(id).Where(x => x != null).ToArray();
            if (favorite == null)
            {
                return true;
            }
            if (existingFavorites == null)
            {
                return true;
            }
            else
            {
                var favoritesList = existingFavorites.Where(a => favorite.id != a.id).ToArray();
                foreach (var ingredient in favoritesList)
                {
                    removeIngredients += JsonConvert.SerializeObject(ingredient) + "$";
                }
            }
            string insertQuery = "UPDATE homechef_administration.favorites SET favorites= '" + removeIngredients + "' WHERE favoritesid ='" + id + "';";
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

        

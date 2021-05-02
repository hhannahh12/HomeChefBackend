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

        public FavoriteRecipeModel[] GetFavorites(string favoritesId)
        {
            try
            {
                using (var connection = new MySqlConnection(cs))
                {
                    connection.Open();
                    var mySql = "SELECT * FROM homechef_administration.favorites WHERE favoritesid = '" + favoritesId + "'";
                    var cmd = new MySqlCommand(mySql, connection);

                    using (MySqlDataReader rdr = cmd.ExecuteReader())
                    {
                        if (rdr.HasRows)
                        {
                            rdr.Read();
                            var value = rdr.GetFieldValue<string?>(2);
                            var favoritesString = value.Split("$");
                            var returnedFavoritesArray = new FavoriteRecipeModel[favoritesString.Length];
                            var indexer = 0;
                            foreach (var favorite in favoritesString)
                            {
                                returnedFavoritesArray[indexer] = JsonConvert.DeserializeObject<FavoriteRecipeModel>(favorite);
                                indexer++;
                            }
                            return returnedFavoritesArray.Where(a => a != null).ToArray();
                
                        }
                        return new FavoriteRecipeModel[0];
                    }
                }
            }
            catch (Exception ex)
            {
                return new FavoriteRecipeModel[0];
            }
        }
        public FavoriteRecipeModel GetFavoriteById(string favoritesId,string recipeId)
        {
            try
            {
                using (var connection = new MySqlConnection(cs))
                {
                    connection.Open();
                    var mySql = "SELECT * FROM homechef_administration.favorites WHERE favoritesid = '" + favoritesId + "'";
                    var cmd = new MySqlCommand(mySql, connection);

                    using (MySqlDataReader rdr = cmd.ExecuteReader())
                    {
                        if (rdr.HasRows)
                        {
                            rdr.Read();
                            var value = rdr.GetFieldValue<string?>(2);
                            var favoritesString = value.Split("$");
                            var returnedFavoritesArray = new FavoriteRecipeModel[favoritesString.Length];
                            var indexer = 0;
                            foreach (var favorite in favoritesString)
                            {
                                returnedFavoritesArray[indexer] = JsonConvert.DeserializeObject<FavoriteRecipeModel>(favorite);
                                indexer++;
                            }
                            foreach(var returnedFavorite in returnedFavoritesArray)
                            {
                                if(returnedFavorite.recipeId == recipeId)
                                {
                                    return returnedFavorite;
                                }
                            }
                        }
                        return new FavoriteRecipeModel();
                    }
                }
            }
            catch (Exception ex)
            {
                return new FavoriteRecipeModel();
            }
        }
        public bool AddFavorite(FavoriteRecipeModel favorite)
        {

            string addFavorites = "";
            var existingFavorites = GetFavorites(favorite.favoritesId);
            if (favorite == null)
            {
                return true;
            }
            if (existingFavorites.Length == 0 )
            {
                addFavorites = JsonConvert.SerializeObject(favorite);
            }
            else
            {
                var favoriteCombined = existingFavorites.Concat(favorite).ToArray();
                if (favoriteCombined.Length > 1)
                {
                    favoriteCombined = favoriteCombined.DistinctBy(x => x.recipeId).ToArray();
                    favoriteCombined = favoriteCombined.Where(a => a != null).ToArray();
                }
              
                foreach (var fav in favoriteCombined)
                {
                    addFavorites += JsonConvert.SerializeObject(fav) + "$";

                }
            }

            string insertQuery = "UPDATE homechef_administration.favorites SET favorites= '" + addFavorites + "' WHERE favoritesid ='" +favorite.favoritesId + "';";
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
        public bool RemoveFavorites(FavoriteRecipeModel favorite)
        {

            string removeIngredients = "";
            var existingFavorites = GetFavorites(favorite.favoritesId).Where(x => x != null).ToArray();
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
                var favoritesList = existingFavorites.Where(a => favorite.recipeId != a.recipeId).ToArray();
                foreach (var ingredient in favoritesList)
                {
                    removeIngredients += JsonConvert.SerializeObject(ingredient) + "$";
                }
            }
            string insertQuery = "UPDATE homechef_administration.favorites SET favorites= '" + removeIngredients + "' WHERE favoritesid ='" + favorite.favoritesId + "';";
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
        public bool AddUserToFavoritesDB(string userid)
        {
            var favoritesid = Guid.NewGuid().ToString();
            string insertQuery = "insert into homechef_administration.favorites(favoritesid,userid) values('" + favoritesid + "','" + userid + "');";
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

        public string GetFavoritesId(string userId)
        {
            using (var connection = new MySqlConnection(cs))
            {
                try
                {
                    connection.Open();
                    Console.WriteLine("Opening Login Connection To Database");
                    var mySql = "SELECT * FROM homechef_administration.favorites WHERE userid = '" + userId + "'";
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
                    throw new Exception("Could not retrieve favorites id " + ex);
                }
            }
        }

        public bool DeleteFavorites(string userid)
        {
            try
            {
                string query = "delete from homechef_administration.favorites where userid='" + userid + "';";
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
    }
}

        

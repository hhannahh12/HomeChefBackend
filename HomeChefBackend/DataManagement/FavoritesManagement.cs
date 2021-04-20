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
        public bool AddFavorite(FavoriteRecipeModel favorite)
        {

            string addFavorites = "";
            var existingFavorites = GetFavorites(favorite.favoritesId);
            if (favorite == null)
            {
                return true;
            }
            if (existingFavorites == null || existingFavorites[0]== null)
            {
                addFavorites = JsonConvert.SerializeObject(favorite);
            }
            else
            {

                existingFavorites[existingFavorites.Length-1] = favorite;
                var c = existingFavorites;
                if (existingFavorites.Length > 1)
                {
                    c = existingFavorites.DistinctBy(x => x.recipeId).ToArray();
                    c = c.Where(a => a != null).ToArray();
                }
              
                foreach (var b in c)
                {
                    addFavorites += JsonConvert.SerializeObject(b) + "$";

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
            //todo: make dietryrequirements lower case.
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
    }
}

        

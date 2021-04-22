using HomeChefBackend;
using HomeChefBackend.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Tests
{
    [TestClass]
    public class FavoritesTests
    {
        [TestMethod]
        public void CreateAccount_Login_AddFavorite_RemoveFavorite_HappyPath_Works()
        {
            var userManagement = new UserManagement();
            var favoriteManagement = new FavoritesManagement();

            var email = "test@test.com";
            var password = "password";
            var userid = Guid.NewGuid().ToString();



            //test that create account works
            var created = userManagement.CreateAccount(userid, email, password);
            Assert.IsTrue(created);

            var createFavoriteDb = favoriteManagement.AddUserToFavoritesDB(userid);
            Assert.IsTrue(createFavoriteDb);

            //test that login can now work 
            var login = userManagement.Login(email, password);
            Assert.IsTrue(login == userid);

            //test you can get users favoriteId 
            var favoriteId = favoriteManagement.GetFavoritesId(userid);
            Assert.IsNotNull(favoriteId);

            //create favorite and test add favorite
            var testRecipeId = Guid.NewGuid().ToString();
            var favorite = new FavoriteRecipeModel()
            {
                favoritesId = favoriteId,
                imageUrl = "testimage.jpg",
                ingredients = new List<string>(),
                instructions = new List<string>(),
                servings = "2",
                readyIn = "20",
                recipeId = testRecipeId,
                title = "Test Recipe"
            };

            var addFavorite = favoriteManagement.AddFavorite(favorite);
            Assert.IsTrue(addFavorite);

            //check that favorite was added to database
            var checkFavorite = favoriteManagement.GetFavorites(favoriteId);
            Assert.IsTrue(checkFavorite.Length != 0);

            var favoriteRecipeId = checkFavorite[0].recipeId;
            Assert.IsTrue(favoriteRecipeId == testRecipeId);

            //check that remove favorites works
            var removeFavorite = favoriteManagement.RemoveFavorites(favorite);
            Assert.IsTrue(removeFavorite);

            //check that remove worked 
            var checkFavoriteRemoved = favoriteManagement.GetFavorites(favoriteId);
            Assert.IsTrue(checkFavoriteRemoved.Length == 0) ;


            //test that delete favorites works 
            var deletedFavorites = favoriteManagement.DeleteFavorites(userid);
            Assert.IsTrue(deletedFavorites);

            //test that delete account can work 
            var deletedUser = userManagement.DeleteAccount(userid);
            Assert.IsTrue(deletedUser);

            //test that user no longer exists 
            var checkDeleted = userManagement.Login(email, password);
            Assert.IsTrue(checkDeleted != userid);

        }

    }
}
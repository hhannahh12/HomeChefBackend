using HomeChefBackend;
using HomeChefBackend.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Tests
{
    [TestClass]
    public class PantryTests
    {
        [TestMethod]
        public void CreateAccount_Login_AddIngredient_RemoveIngredient_HappyPath_Works()
        {
            var userManagement = new UserManagement();
            var pantryManagement = new PantryManagement();

            var email = "test@test.com";
            var password = "password";
            var userid = Guid.NewGuid().ToString();


            //test that create account works
            var created = userManagement.CreateAccount(userid, email, password);
            Assert.IsTrue(created);

            var createFavoriteDb = userManagement.AddUserToPantryDB(userid);
            Assert.IsTrue(createFavoriteDb);

            //test that login can now work 
            var login = userManagement.Login(email, password);
            Assert.IsTrue(login == userid);

            //test you can get users pantryId
            var pantryId = pantryManagement.GetPantryId(userid);
            Assert.IsNotNull(pantryId);

            //create ingredient and test add ingredient
            var ingredientId = 1234;
            var ingredient = new IngredientModel()
            {
                id = ingredientId,
                image = "Test.jpg",
                name = "apple"
            };
            IngredientModel[] ingredientList = { ingredient };
      
            var addFavorite = pantryManagement.AddIngredients(pantryId,ingredientList);
            Assert.IsTrue(addFavorite);

            //check that ingredient was added to database
            var checkPantry = pantryManagement.GetPantry(pantryId);
            Assert.IsTrue(checkPantry.Length != 0);

            var favoriteRecipeId = checkPantry[0].id;
            Assert.IsTrue(favoriteRecipeId == 1234);

            //check that remove favorites works
            var removeIngredient = pantryManagement.RemoveIngredients(pantryId,ingredientList);
            Assert.IsTrue(removeIngredient);

            //check that remove worked 
            var checkPantryRemoved = pantryManagement.GetPantry(pantryId);
            Assert.IsTrue(checkPantryRemoved[0] == null) ;

            //test that delete favorites works 
            var deletedFavorites = pantryManagement.DeletePantry(userid);
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
using HomeChefBackend;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Tests
{
    [TestClass]
    public class UserTests
    {
        [TestMethod]
        public void CreateAccount_Login_Delete_HappyPath_Works()
        {
            var userManagement = new UserManagement();

            var email = "test@test.com";
            var password = "password";
            var userid = Guid.NewGuid().ToString();
            
            //test that create account works
            var created = userManagement.CreateAccount(userid, email, password);
            Assert.IsTrue(created);

            //test that login can now work 
            var login = userManagement.Login(email, password);
            Assert.IsTrue(login == userid);

            //test that delete account can work 
            var deleted = userManagement.DeleteAccount(userid);
            Assert.IsTrue(deleted);

            //test that user no longer exists 
            var checkDeleted = userManagement.Login(email, password);
            Assert.IsTrue(checkDeleted != userid);
        }

        [TestMethod]
        public void CreateAccount_Login_WithIncorrectDetails_DoesntWork()
        {
            var userManagement = new UserManagement();

            var email = "test@test.com";
            var password = "password";
            var wrongPassword = "wrongPassword";
            var userid = Guid.NewGuid().ToString();

            //test that create account works
            var created = userManagement.CreateAccount(userid, email, password);
            Assert.IsTrue(created);

            //test that login can now work 
            var login = userManagement.Login(email, wrongPassword);
            Assert.IsTrue(login != userid);

            //delete account for next test 
            var deleted = userManagement.DeleteAccount(userid);
            Assert.IsTrue(deleted);
        }
       
    }
}

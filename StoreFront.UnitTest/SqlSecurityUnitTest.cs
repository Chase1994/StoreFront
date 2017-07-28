using System;
using StoreFront.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace StoreFront.UnitTest
{
    [TestClass]
    public class SqlSecurityUnitTest
    {
        StoreFrontDataEntities db = new StoreFrontDataEntities();
        SqlSecurityManager sm = new SqlSecurityManager();

        [TestMethod]
        public void AuthenticateUser_ValidParams()
        {
            //arrange
            string username = "TestAccount";
            string password = "TestPassword1";
            Boolean expected = true;
            //act
            Boolean actual = sm.AuthenticateUser(username, password);
            //assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void AuthenticateUser_InvalidParams()
        {
            //arrange
            string username = "TestAccount1";
            string password = "TestPassword2";
            Boolean expected = false;
            //act
            Boolean actual = sm.AuthenticateUser(username, password);
            //assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void IsAdmin_ValidAdmin()
        {
            //arrange
            string username = "TestAccount";
            Boolean expected = true;
            //act
            Boolean actual = sm.IsAdmin(username);
            //assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void IsAdmin_InValidAdmin()
        {
            //arrange
            string username = "garyoak";
            Boolean expected = false;
            //act
            Boolean actual = sm.IsAdmin(username);
            //assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void LoadUser_ValidUsername()
        {
            //arrange
            string username = "TestAccount";
            //act
            User actual = sm.LoadUser(username);
            //assert
            Assert.IsNotNull(actual);
        }

        [TestMethod]
        public void LoadUser_InValidUsername()
        {
            //arrange
            string username = "TestAccount2";
            //act
            User actual = sm.LoadUser(username);
            //assert
            Assert.IsNull(actual);
        }
    }
}

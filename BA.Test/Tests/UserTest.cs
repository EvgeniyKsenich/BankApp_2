using BA.Database.Enteties;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using Moq;
using DA.Business.Utiles;
using System.Linq;

namespace BA.Test.Tests
{
    [TestFixture]
    public class UserTest
    {
        [Test]
        public void GetUserModel()
        {
            var ViewModelEngine = new ViewModelEngine();
            var Accounts = new List<Account>();
            Accounts.Add
                (new Account()
                {
                    Balance = 0,
                    Id = 1
                });
            var User = new User()
            {
                Name = "Test",
                Password = "1111",
                DateOfBirth = DateTime.Now,
                Email = "test@gmail.com",
                Surname = "User",
                Id = 1,
                UserName = "Test_User_1",
                Accounts = Accounts
            };
            var ViewUser = ViewModelEngine.GetUserViewModel(User);

            Assert.AreEqual(ViewUser.Name, User.Name);
            Assert.AreEqual(ViewUser.Surname, User.Surname);
            Assert.AreEqual(ViewUser.Balance, User.Accounts.ToList().FirstOrDefault().Balance);
            Assert.AreEqual(ViewUser.UserName, User.UserName);
        }
    }

}

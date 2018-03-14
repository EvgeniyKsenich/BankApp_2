using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Moq;
using BA.Database.UnitOfWork;
using AutoMapper;
using DA.Business.Servises;
using BA.Database.Enteties;
using DA.Business.Utiles;
using BA.Database.Сommon.Repositories;

namespace BA.Test.Tests
{
    [TestFixture]
    class TransactionTest
    {
        [Test]
        public void GetTransactionModel()
        {
            var ViewModelEngine = new ViewModelEngine();
            var Account = new Account()
            {
                Balance = 0,
                Id = 1,
                User = new User()
                {
                    UserName = "User"
                }
            };
            var Transaction = new Transaction()
            {
                Id = 1,
                Date = DateTime.Now,
                Summa = 100,
                Type = 1,
                AccountInitiator = Account,
                AccountRecipient = Account
            };

            var ViewUser = ViewModelEngine.GetTransactionViewModel(Transaction);

            Assert.AreEqual(ViewUser.Date, Transaction.Date);
            Assert.AreEqual(ViewUser.Summa, Transaction.Summa);
            Assert.AreEqual(ViewUser.Type, Transaction.Type);
            Assert.AreEqual(ViewUser.AccountInfoInitiator, Transaction.AccountInitiator.User.UserName);
            Assert.AreEqual(ViewUser.AccountInfoRecipient, Transaction.AccountRecipient.User.UserName);
        }

        [Test]
        public void DepositFine()
        {
            var amount = 100;
            var UnitOfWork = new Mock<IUnitOfWork>();
            var Mapper = new Mock<IMapper>();

            var TransactionServises = new TransactionServises(UnitOfWork.Object, Mapper.Object);
            var Account = new Account()
            {
                Balance = 0,
                Id = 1
            };
            UnitOfWork.Setup(e => e.Transaction ).Returns(new Mock<ITransactionRepositories<Transaction>>().Object);
            UnitOfWork.Setup(e => e.Accounts.Get("Test_User_1")).Returns(Account);
            TransactionServises.Deposit("Test_User_1", amount);

            Assert.AreEqual(Account.Balance, amount);
        }

        [Test]
        public void WithdrawFine()
        {
            var startSumma = 120;
            var amount = 100;
            var UnitOfWork = new Mock<IUnitOfWork>();
            var Mapper = new Mock<IMapper>();

            var TransactionServises = new TransactionServises(UnitOfWork.Object, Mapper.Object);
            var Account = new Account()
            {
                Balance = startSumma,
                Id = 1
            };
            UnitOfWork.Setup(e => e.Transaction).Returns(new Mock<ITransactionRepositories<Transaction>>().Object);
            UnitOfWork.Setup(e => e.Accounts.Get("Test_User_1")).Returns(Account);
            TransactionServises.Withdraw("Test_User_1", amount);

            Assert.AreEqual(Account.Balance, startSumma - amount);
        }

        [Test]
        public void TransferFine()
        {
            var startSummaAccount1 = 120;
            var startSummaAccount2 = 10;
            var amount = 100;
            var UnitOfWork = new Mock<IUnitOfWork>();
            var Mapper = new Mock<IMapper>();

            var TransactionServises = new TransactionServises(UnitOfWork.Object, Mapper.Object);
            var Account1 = new Account()
            {
                Balance = startSummaAccount1,
                Id = 1
            };
            var Account2 = new Account()
            {
                Balance = startSummaAccount2,
                Id = 1
            };
            UnitOfWork.Setup(e => e.Transaction).Returns(new Mock<ITransactionRepositories<Transaction>>().Object);
            UnitOfWork.Setup(e => e.Accounts.Get("Test_User_1")).Returns(Account1);
            UnitOfWork.Setup(e => e.Accounts.Get("Test_User_2")).Returns(Account2);
            TransactionServises.Transfer(amount, "Test_User_1", "Test_User_2");

            Assert.AreEqual(Account1.Balance, startSummaAccount1 - amount);
            Assert.AreEqual(Account2.Balance, startSummaAccount2 + amount);
        }

        [Test]
        public void DepositMinusAmount()
        {
            var amount = -100;
            var StartBalance = 0;
            var UnitOfWork = new Mock<IUnitOfWork>();
            var Mapper = new Mock<IMapper>();

            var TransactionServises = new TransactionServises(UnitOfWork.Object, Mapper.Object);
            var Account = new Account()
            {
                Balance = StartBalance,
                Id = 1
            };
            UnitOfWork.Setup(e => e.Transaction).Returns(new Mock<ITransactionRepositories<Transaction>>().Object);
            UnitOfWork.Setup(e => e.Accounts.Get("Test_User_1")).Returns(Account);
            TransactionServises.Deposit("Test_User_1", amount);

            Assert.AreEqual(Account.Balance, StartBalance);
        }

        [Test]
        public void WithdrawMinusAmount()
        {
            var amount = -100;
            var StartBalance = 120;
            var UnitOfWork = new Mock<IUnitOfWork>();
            var Mapper = new Mock<IMapper>();

            var TransactionServises = new TransactionServises(UnitOfWork.Object, Mapper.Object);
            var Account = new Account()
            {
                Balance = StartBalance,
                Id = 1
            };
            UnitOfWork.Setup(e => e.Transaction).Returns(new Mock<ITransactionRepositories<Transaction>>().Object);
            UnitOfWork.Setup(e => e.Accounts.Get("Test_User_1")).Returns(Account);
            TransactionServises.Withdraw("Test_User_1", amount);

            Assert.AreEqual(Account.Balance, StartBalance);
        }

        [Test]
        public void TransferMinusAmount()
        {
            var startSummaAccount1 = 120;
            var startSummaAccount2 = 10;
            var amount = -100;
            var UnitOfWork = new Mock<IUnitOfWork>();
            var Mapper = new Mock<IMapper>();

            var TransactionServises = new TransactionServises(UnitOfWork.Object, Mapper.Object);
            var Account1 = new Account()
            {
                Balance = startSummaAccount1,
                Id = 1
            };
            var Account2 = new Account()
            {
                Balance = startSummaAccount2,
                Id = 1
            };
            UnitOfWork.Setup(e => e.Transaction).Returns(new Mock<ITransactionRepositories<Transaction>>().Object);
            UnitOfWork.Setup(e => e.Accounts.Get("Test_User_1")).Returns(Account1);
            UnitOfWork.Setup(e => e.Accounts.Get("Test_User_2")).Returns(Account2);
            TransactionServises.Transfer(amount, "Test_User_1", "Test_User_2");

            Assert.AreEqual(Account1.Balance, startSummaAccount1);
            Assert.AreEqual(Account2.Balance, startSummaAccount2);
        }

        [Test]
        public void WithdrawMoreThatBalance()
        {
            var startSumma = 120;
            var amount = 150;
            var UnitOfWork = new Mock<IUnitOfWork>();
            var Mapper = new Mock<IMapper>();

            var TransactionServises = new TransactionServises(UnitOfWork.Object, Mapper.Object);
            var Account = new Account()
            {
                Balance = startSumma,
                Id = 1
            };
            UnitOfWork.Setup(e => e.Transaction).Returns(new Mock<ITransactionRepositories<Transaction>>().Object);
            UnitOfWork.Setup(e => e.Accounts.Get("Test_User_1")).Returns(Account);
            TransactionServises.Withdraw("Test_User_1", amount);

            Assert.AreEqual(Account.Balance, startSumma);
        }

        [Test]
        public void TransferMoreThatBalance()
        {
            var startSummaAccount1 = 120;
            var startSummaAccount2 = 10;
            var amount = 150;
            var UnitOfWork = new Mock<IUnitOfWork>();
            var Mapper = new Mock<IMapper>();

            var TransactionServises = new TransactionServises(UnitOfWork.Object, Mapper.Object);
            var Account1 = new Account()
            {
                Balance = startSummaAccount1,
                Id = 1
            };
            var Account2 = new Account()
            {
                Balance = startSummaAccount2,
                Id = 1
            };
            UnitOfWork.Setup(e => e.Transaction).Returns(new Mock<ITransactionRepositories<Transaction>>().Object);
            UnitOfWork.Setup(e => e.Accounts.Get("Test_User_1")).Returns(Account1);
            UnitOfWork.Setup(e => e.Accounts.Get("Test_User_2")).Returns(Account2);
            TransactionServises.Transfer(amount, "Test_User_1", "Test_User_2");

            Assert.AreEqual(Account1.Balance, startSummaAccount1);
            Assert.AreEqual(Account2.Balance, startSummaAccount2);
        }
    }
}

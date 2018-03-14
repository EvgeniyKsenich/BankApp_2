using AutoMapper;
using BA.Database.UnitOfWork;
using BA.Database.Сommon.Repositories;
using System;
using BA.Database.Enteties;
using DA.Business.Repositories;

namespace DA.Business.Servises
{
    public class TransactionServises: ITransactionServisesRepositoryes
    {
        private IMapper _Mapper;
        private IUnitOfWork _Unit;

        public TransactionServises(IUnitOfWork Unit, IMapper mapper)
        {
            _Mapper = mapper;
            _Unit = Unit;
        }

        public Transaction CreateTransaction(double amount, Account initiator, Account receiver, int type)
        {
            Transaction transaction = new Transaction();
            try{
                transaction = new BA.Database.Enteties.Transaction()
                {
                    Summa = amount,
                    Date = DateTime.Now,
                    Type = type,
                    AccountRecipient = receiver,
                    AccountInitiator = initiator
                };
            }
            catch (Exception exception)
            {

            }
            return transaction;
        }

        public bool Deposit(string userName, double amount)
        {
            try
            {
                var account = _Unit.Accounts.Get(userName);

                if (amount < 0)
                    return false;

                account.Balance += amount;

                var transaction_ = CreateTransaction(amount, account, account, 1);
                _Unit.Transaction.Add(transaction_);

                _Unit.Save();
                return true;
            }
            catch (Exception exception)
            {
                return false;
            }
        }

        public bool Withdraw(string UserName, double amount)
        {
            try
            {
                var account = _Unit.Accounts.Get(UserName);
                if (account.Balance < amount || amount < 0)
                    return false;

                account.Balance -= amount;

                var transaction_ = CreateTransaction(amount, account, account, 2);
                _Unit.Transaction.Add(transaction_);

                _Unit.Save();
                return true;
            }
            catch (Exception exception)
            {
                return false;
            }
        }

        public bool Transfer(double amount, string userInitiatorName, string userReceiverName)
        {
            try
            {
                var userInitiator = _Unit.Accounts.Get(userInitiatorName);
                if (userInitiator == null)
                    return false;
                if (userInitiator.Balance < amount || amount < 0)
                    return false;

                var userReceiver = _Unit.Accounts.Get(userReceiverName);
                if (userReceiver == null)
                    return false;

                var transaction = CreateTransaction(amount, userInitiator, userReceiver, 3);
                _Unit.Transaction.Add(transaction);

                userInitiator.Balance -= amount;
                userReceiver.Balance += amount;
                _Unit.Save();

                return true;
            }
            catch (Exception exception)
            {
                return false;
            }
        }
    }
}

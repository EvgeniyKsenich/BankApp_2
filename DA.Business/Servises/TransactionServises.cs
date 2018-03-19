using AutoMapper;
using BA.Database.UnitOfWork;
using System;
using BA.Database.Enteties;
using DA.Business.Repositories;
using System.Threading;
using System.Collections.Generic;
using DA.Business.Utiles;

namespace DA.Business.Servises
{
    public class TransactionServises : ITransactionServisesRepositoryes
    {
        private IMapper _Mapper;
        private IUnitOfWork _Unit;


        public TransactionServises(IUnitOfWork Unit, IMapper mapper)
        {
            _Mapper = mapper;
            _Unit = Unit;
        }

        public Transaction CreateTransaction(double amount, Account initiator, Account receiver, int type, ref bool error, ref string errorMessage)
        {
            error = false;
            Transaction transaction = new Transaction();
            try
            {
                transaction = new BA.Database.Enteties.Transaction()
                {
                    Summa = amount,
                    Date = DateTime.Now,
                    Type = type,
                    AccountRecipient = receiver,
                    AccountInitiator = initiator
                };
                return transaction;
            }
            catch (Exception exception)
            {
                errorMessage = "Thomething goes wrong! Cant create transaction.";
                error = true;
                return transaction;
            }
        }

        public bool Deposit(string userName, double amount, ref string errorMessage)
        {
            try
            {
                var account = _Unit.Accounts.Get(userName);

                if (amount <= 0)
                {
                    errorMessage = "Amount less that 1";
                    return false;
                }

                account.Balance += amount;

                var error = false;
                var transaction_ = CreateTransaction(amount, account, account, 1, ref error, ref errorMessage);
                if (error)
                {
                    return false;
                }

                _Unit.Transaction.Add(transaction_);

                _Unit.Save();
                return true;
            }
            catch (Exception exception)
            {
                errorMessage = "Thomething goes wrong!";
                return false;
            }

        }

        public bool Withdraw(string userName, double amount, ref string errorMessage)
        {
            try
            {
                var account = _Unit.Accounts.Get(userName);
                if (account.Balance < amount)
                {
                    errorMessage = "Not enoth money";
                    return false;
                }
                if (amount <= 0)
                {
                    errorMessage = "Amount less that 1";
                    return false;
                }

                account.Balance -= amount;

                var error = false;
                var transaction_ = CreateTransaction(amount, account, account, 2, ref error, ref errorMessage);
                if (error)
                {
                    return false;
                }

                _Unit.Transaction.Add(transaction_);

                _Unit.Save();
                return true;
            }
            catch (Exception exception)
            {
                errorMessage = "Thomething goes wrong!";
                return false;
            }
        }

        public bool Transfer(double amount, string userInitiatorName, string userReceiverName, ref string errorMessage)
        {
            try
            {
                var userInitiator = _Unit.Accounts.Get(userInitiatorName);
                if (userInitiator == null)
                {
                    errorMessage = "Unknown initiator";
                    return false;
                }
                if (userInitiator.Balance < amount)
                {
                    errorMessage = "Not enoth money";
                    return false;
                }
                if (amount <= 0)
                {
                    errorMessage = "Amount less that 1";
                    return false;
                }

                var userReceiver = _Unit.Accounts.Get(userReceiverName);
                if (userReceiver == null)
                {
                    errorMessage = "Unknown reciver";
                    return false;
                }

                var error = false;
                var transaction = CreateTransaction(amount, userInitiator, userReceiver, 3, ref error, ref errorMessage);
                if (error)
                {
                    return false;
                }

                _Unit.Transaction.Add(transaction);

                userInitiator.Balance -= amount;
                userReceiver.Balance += amount;
                _Unit.Save();

                return true;
            }
            catch (Exception exception)
            {
                errorMessage = "Thomething goes wrong!";
                return false;
            }

        }

    }
}

using AutoMapper;
using BA.Database.UnitOfWork;
using System;
using BA.Database.Enteties;
using DA.Business.Repositories;
using System.Threading;
using System.Collections.Generic;
using DA.Business.Utiles;
using DA.Business.Modeles;

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

        public BooleanErrorModel Deposit(string userName, double amount)
        {
            var returnModel = new BooleanErrorModel();
            try
            {
                var account = _Unit.Accounts.Get(userName);
                if (amount <= 0)
                {
                    returnModel.ErrorMessage = "Amount less that 1";
                    returnModel.Error = true;
                    return returnModel;
                }

                account.Balance += amount;

                var error = false;
                var errorMessage = string.Empty;
                var transaction_ = CreateTransaction(amount, account, account, 1, ref error, ref errorMessage);
                if (error)
                {
                    returnModel.ErrorMessage = errorMessage;
                    returnModel.Error = true;
                    return returnModel;
                }

                _Unit.Transaction.Add(transaction_);

                _Unit.Save();
                return returnModel;
            }
            catch (Exception exception)
            {
                returnModel.ErrorMessage = "Thomething goes wrong!";
                returnModel.Error = true;
                return returnModel;
            }

        }

        public BooleanErrorModel Withdraw(string userName, double amount)
        {
            var returnModel = new BooleanErrorModel();
            try
            {
                var account = _Unit.Accounts.Get(userName);
                if (account.Balance < amount)
                {
                    returnModel.ErrorMessage = "Not enoth money";
                    returnModel.Error = true;
                    return returnModel;
                }
                if (amount <= 0)
                {
                    returnModel.ErrorMessage = "Amount less that 1";
                    returnModel.Error = true;
                    return returnModel;
                }

                account.Balance -= amount;

                var error = false;
                var errorMessage = string.Empty;
                var transaction_ = CreateTransaction(amount, account, account, 2, ref error, ref errorMessage);
                if (error)
                {
                    returnModel.ErrorMessage = errorMessage;
                    returnModel.Error = true;
                    return returnModel;
                }

                _Unit.Transaction.Add(transaction_);

                _Unit.Save();
                return returnModel;
            }
            catch (Exception exception)
            {
                returnModel.ErrorMessage = "Thomething goes wrong!";
                returnModel.Error = true;
                return returnModel;
            }
        }

        public BooleanErrorModel Transfer(double amount, string userInitiatorName, string userReceiverName)
        {
            var returnModel = new BooleanErrorModel();
            try
            {
                var userInitiator = _Unit.Accounts.Get(userInitiatorName);
                if (userInitiator == null)
                {
                    returnModel.ErrorMessage = "Unknown initiator"; ;
                    returnModel.Error = true;
                    return returnModel;
                }
                if (userInitiator.Balance < amount)
                {
                    returnModel.ErrorMessage = "Not enoth money";
                    returnModel.Error = true;
                    return returnModel;
                }
                if (amount <= 0)
                {
                   returnModel.ErrorMessage = "Amount less that 1";
                   returnModel.Error = true;
                   return returnModel;
                }  

                var userReceiver = _Unit.Accounts.Get(userReceiverName);
                if (userReceiver == null)
                {
                    returnModel.ErrorMessage = "Unknown reciver";
                    returnModel.Error = true;
                    return returnModel;
                }   

                var error = false;
                var errorMessage = string.Empty;
                var transaction = CreateTransaction(amount, userInitiator, userReceiver, 3, ref error, ref errorMessage);
                if (error)
                {
                    returnModel.ErrorMessage = errorMessage;
                    returnModel.Error = true;
                    return returnModel;
                }

                _Unit.Transaction.Add(transaction);

                userInitiator.Balance -= amount;
                userReceiver.Balance += amount;
                _Unit.Save();

                return returnModel;
            }
            catch (Exception exception)
            {
                returnModel.ErrorMessage = "Thomething goes wrong!";
                returnModel.Error = true;
                return returnModel;
            }

        }

    }
}

using BA.Business.ViewModel;
using BA.Database.Enteties;
using BA.Database.UnitOfWork;
using DA.Business.Repositories;
using DA.Business.ViewModel;
using System.Linq;
using System.Collections.Generic;

namespace DA.Business.Utiles
{
    public class ViewModelEngine : IViewModelEngine
    {
        public UserView GetUserViewModel(User User)
        {
            var account = User.Accounts.ToList().FirstOrDefault();

            var userView = new UserView()
            {
                Id = User.Id,
                Name = User.Name,
                Surname = User.Surname,
                UserName = User.UserName,
                Balance = account.Balance
            };

            return userView;
        }

        public TransactionView GetTransactionViewModel(Transaction Transaction)
        {
            var transactionView = new TransactionView()
            {
                Id = Transaction.Id,
                Date = Transaction.Date,
                Summa = Transaction.Summa,
                Type = Transaction.Type,
                AccountInfoInitiator = Transaction.AccountInitiator.User.UserName,
                AccountInfoRecipient = Transaction.AccountRecipient.User.UserName
            };

            return transactionView;
        }
    }
}

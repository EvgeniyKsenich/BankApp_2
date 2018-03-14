using BA.Business.ViewModel;
using BA.Database.Enteties;
using DA.Business.Modeles;
using DA.Business.ViewModel;
using System.Collections.Generic;

namespace DA.Business.Repositories
{
    public interface IUserServises
    {
        UserView GetUserViewModel(string UserName);
        UserView GetUserViewModel(User User);
        IEnumerable<UserView> GetList();
        bool Register(UserModel model);
        IEnumerable<TransactionView> GetTransactionList(string Username);
        IEnumerable<UserView> GetListForTransactions(string CurrentUserName);
    }
}

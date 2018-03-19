using BA.Business.ViewModel;
using BA.Database.Enteties;
using DA.Business.Modeles;
using DA.Business.ViewModel;
using System.Collections.Generic;

namespace DA.Business.Repositories
{
    public interface IUserServises
    {
        UserView GetUserViewModel(string UserName, ref bool error, ref string errorMessage);
        UserView GetUserViewModel(User User, ref bool error, ref string errorMessage);
        bool Register(UserModel model, ref string errorMessage);
        IEnumerable<UserView> GetList(ref bool error, ref string errorMessage);
        IEnumerable<TransactionView> GetTransactionList(string Usernam, ref bool error, ref string errorMessage);
        IEnumerable<UserView> GetListForTransactions(string CurrentUserName, ref bool error, ref string errorMessage);
    }
}

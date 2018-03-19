using BA.Business.ViewModel;
using BA.Database.Enteties;
using DA.Business.Modeles;
using DA.Business.ViewModel;
using System.Collections.Generic;

namespace DA.Business.Repositories
{
    public interface IUserServises
    {
        UserViewErrorModel GetUserViewModel(string UserName);
        UserViewErrorModel GetUserViewModel(User User);
        BooleanErrorModel Register(UserModel model);
        ListUserViewErrorModel GetList();
        LisTransactionsErrorModel GetTransactionList(string Usernam);
        ListUserViewErrorModel GetListForTransactions(string CurrentUserName);
    }
}

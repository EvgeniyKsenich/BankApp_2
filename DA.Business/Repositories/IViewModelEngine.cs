using BA.Business.ViewModel;
using BA.Database.Enteties;
using DA.Business.ViewModel;

namespace DA.Business.Repositories
{
    public interface IViewModelEngine
    {
        UserView GetUserViewModel(User User);

        TransactionView GetTransactionViewModel(Transaction Transaction);
    }
}

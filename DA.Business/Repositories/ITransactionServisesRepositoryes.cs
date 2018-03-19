using BA.Database.Enteties;
using DA.Business.Modeles;

namespace DA.Business.Repositories
{
    public interface ITransactionServisesRepositoryes
    {
        bool Deposit(string UserName, double amount, ref string error);
        bool Withdraw(string UserName, double amount, ref string error);
        bool Transfer(double amount, string UserInitiatorName, string UserReceiverName, ref string error);
        Transaction CreateTransaction(double amount, Account Initiator, Account Receiver, int type, ref bool error, ref string errorMessage);
    }
}

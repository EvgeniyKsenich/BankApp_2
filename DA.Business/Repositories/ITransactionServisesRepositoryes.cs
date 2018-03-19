using BA.Database.Enteties;
using DA.Business.Modeles;

namespace DA.Business.Repositories
{
    public interface ITransactionServisesRepositoryes
    {
        BooleanErrorModel Deposit(string UserName, double amount);
        BooleanErrorModel Withdraw(string UserName, double amount);
        BooleanErrorModel Transfer(double amount, string UserInitiatorName, string UserReceiverName);
        Transaction CreateTransaction(double amount, Account Initiator, Account Receiver, int type, ref bool error, ref string errorMessage);
    }
}

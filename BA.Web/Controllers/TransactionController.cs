using DA.Business.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BA.Web.Controllers
{
    [Produces("application/json")]
    [Route("api/Transaction")]
    public class TransactionController : Controller
    {
        private ITransactionServisesRepositoryes _transactionServises;

        public TransactionController(ITransactionServisesRepositoryes TransactionServises)
        {
            _transactionServises = TransactionServises;
        }

        [Authorize]
        [Route("Deposit")]
        public IActionResult Deposit(double amount)
        {
            var result = _transactionServises.Deposit(User.Identity.Name, amount);
            return Ok(result.ToString());
        }

        [Authorize]
        [Route("Withdraw")]
        public IActionResult Withdraw(double amount)
        {
            var result = _transactionServises.Withdraw(User.Identity.Name, amount);
            return Ok(result.ToString());
        }

        [Authorize]
        [Route("Transfer")]
        public IActionResult Transfer(double amount, string UserReceiverName)
        {
            var result = _transactionServises.Transfer(amount, User.Identity.Name, UserReceiverName);
            return Ok(result.ToString());
        }
    }
}

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

        public TransactionController(ITransactionServisesRepositoryes transactionServises)
        {
            _transactionServises = transactionServises;
        }

        [Authorize]
        [Route("Deposit")]
        public IActionResult Deposit(double amount)
        {
            var result = _transactionServises.Deposit(User.Identity.Name, amount);
            if (!result.Error)
                return Ok(result.ErrorMessage);

            return BadRequest(!result.Error);
        }

        [Authorize]
        [Route("Withdraw")]
        public IActionResult Withdraw(double amount)
        {
            string error = string.Empty;
            var result = _transactionServises.Withdraw(User.Identity.Name, amount);
            if (!result.Error)
                return Ok(result.ErrorMessage);

            return BadRequest(!result.Error);
        }

        [Authorize]
        [Route("Transfer")]
        public IActionResult Transfer(double amount, string userReceiverName)
        {
            var result = _transactionServises.Transfer(amount, User.Identity.Name, userReceiverName);
            if (!result.Error)
                return Ok(result.ErrorMessage);

            return BadRequest(!result.Error);
        }
    }
}

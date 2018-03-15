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
            string error = string.Empty;
            var result = _transactionServises.Deposit(User.Identity.Name, amount,ref error);
            if (result)
                return Ok(result.ToString());

            return BadRequest(error);
        }

        [Authorize]
        [Route("Withdraw")]
        public IActionResult Withdraw(double amount)
        {
            string error = string.Empty;
            var result = _transactionServises.Withdraw(User.Identity.Name, amount,ref error);
            if(result)
                return Ok(result.ToString());

            return BadRequest(error);
        }

        [Authorize]
        [Route("Transfer")]
        public IActionResult Transfer(double amount, string userReceiverName)
        {
            string error = string.Empty;
            var result = _transactionServises.Transfer(amount, User.Identity.Name, userReceiverName,ref error);
            if (result)
                return Ok(result.ToString());

            return BadRequest(error);
        }
    }
}

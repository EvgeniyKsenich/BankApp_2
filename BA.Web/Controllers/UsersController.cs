using System;
using System.Collections.Generic;
using BA.Business.ViewModel;
using DA.Business.Modeles;
using DA.Business.Repositories;
using DA.Business.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BA.Web.Controllers
{
    [Produces("application/json")]
    [Route("api/Users")]
    public class UsersController : Controller
    {
        private IUserServises _userServises;

        public UsersController(IUserServises UserServises)
        {
            _userServises = UserServises;
        }

        [HttpGet]
        [Authorize]
        public IEnumerable<UserView> Get()
        {
            string errorMessage = string.Empty;
            bool error = false;
            var users = _userServises.GetListForTransactions(User.Identity.Name,ref error, ref errorMessage);
            if (error)
            {
                Response.StatusCode = 400;
            }
            return users;
        }

        [HttpPost]
        [Route("Register")]
        public IActionResult Post([FromBody]UserModel user_)
        {
            string error = string.Empty;
            var result = _userServises.Register(user_, ref error);
            if (result)
                return Ok(result.ToString());

            return BadRequest(error);
        }

        [Authorize]
        [Route("GetCurrentUser")]
        public UserView GetCurrentUser()
        {
            string errorMessage = string.Empty;
            bool error = false;
            var userView = _userServises.GetUserViewModel(User.Identity.Name, ref error, ref errorMessage);
            if (error)
            {
                Response.StatusCode = 400;
            }
            return userView;
        }

        [Authorize]
        [Route("Transactions")]
        public IEnumerable<TransactionView> GetTransactionList()
        {
            string errorMessage = string.Empty;
            bool error = false;
            var transactions =  _userServises.GetTransactionList(User.Identity.Name, ref error, ref errorMessage);
            if (error)
            {
                Response.StatusCode = 400;
            }
            return transactions;
        }

    }
}

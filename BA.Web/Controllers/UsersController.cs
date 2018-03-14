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
            var users = _userServises.GetListForTransactions(User.Identity.Name);
            return users;
        }

        [HttpPost]
        [Route("Register")]
        public bool Post([FromBody]UserModel User_)
        {
            return _userServises.Register(User_);
        }

        [Authorize]
        [Route("GetCurrentUser")]
        public UserView GetCurrentUser()
        {
            return _userServises.GetUserViewModel(User.Identity.Name);
        }

        [Authorize]
        [Route("Transactions")]
        public IEnumerable<TransactionView> GetTransactionList()
        {
            return _userServises.GetTransactionList(User.Identity.Name);
        }

    }
}

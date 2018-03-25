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
            var model = _userServises.GetListForTransactions(User.Identity.Name);
            if (model.Error)
            {
                Response.StatusCode = 400;
            }
            return model.Items;
        }

        [HttpPost]
        [Route("Register")]
        public IActionResult Post([FromBody]UserModel user)
        {
            var model = _userServises.Register(user);
            if (!model.Error)
                return Ok(model.Error);

            return BadRequest(model.ErrorMessage);
        }

        [Authorize]
        [Route("GetCurrentUser")]
        public UserView GetCurrentUser()
        {
            var model = _userServises.GetUserViewModel(User.Identity.Name);
            if (model.Error)
            {
                Response.StatusCode = 400;
            }
            return model.Item;
        }

        [Authorize]
        [Route("Transactions")]
        public IEnumerable<TransactionView> GetTransactionList()
        {
            var model =  _userServises.GetTransactionList(User.Identity.Name);
            if (model.Error)
            {
                Response.StatusCode = 400;
            }
            return model.Items;
        }

    }
}

using AutoMapper;
using BA.Business.Modeles;
using BA.Business.Repositories;
using BA.Business.ViewModel;
using BA.Database.Enteties;
using BA.Database.UnitOfWork;
using DA.Business.Modeles;
using DA.Business.Repositories;
using DA.Business.ViewModel;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DA.Business.Servises
{
    public class UserServises : IUserServises
    {
        private IMapper _Mapper;
        private IUnitOfWork _UOF;
        private IViewModelEngine _ViewModelEngine;
        private IPasswordEngine _PasswordEngine;
        private IOptions<Identity> _identity;

        public UserServises(IPasswordEngine passwordEngine, IUnitOfWork unit, IMapper mapper, IViewModelEngine viewModelEngine, IOptions<Identity> Identity)
        {
            _Mapper = mapper;
            _UOF = unit;
            _ViewModelEngine = viewModelEngine;
            _PasswordEngine = passwordEngine;
            _identity = Identity;
        }

        public UserView GetUserViewModel(string userName, ref bool error, ref string errorMessage)
        {
            error = false;
            try
            {
                var user = _UOF.Users.Get(userName);
                if (user == null)
                {
                    errorMessage = "Can not find user";
                    error = true;
                    return null;
                }
                var userView = _ViewModelEngine.GetUserViewModel(user);
                if (userView == null)
                {
                    errorMessage = "Can not create user view";
                    error = true;
                    return null;
                }
                error = false;
                return userView;
            }
            catch (Exception exception)
            {
                error = true;
                errorMessage = "Thomething goes wrong!";
                return null;
            }
        }

        public UserView GetUserViewModel(User user, ref bool error, ref string errorMessage)
        {
            error = false;
            try
            {
                var userView = _ViewModelEngine.GetUserViewModel(user);
                if (userView == null)
                {
                    errorMessage = "Can not create user view";
                    error = true;
                    return null;
                }
                error = false;
                return userView;
            }
            catch(Exception exception)
            {
                error = true;
                errorMessage = "Thomething goes wrong!";
                return null;
            }
        }

        public IEnumerable<UserView> GetList(ref bool error, ref string errorMessage)
        {
            error = false;
            var userViewList = new List<UserView>();
            try
            {
                var userList = _UOF.Users.GetList();
                foreach (var user in userList)
                {
                    userViewList.Add(GetUserViewModel(user, ref error, ref errorMessage));
                    if (error)
                    {
                        return new List<UserView>();
                    }
                }
                return userViewList;
            }
            catch(Exception exception)
            {
                error = true;
                errorMessage = "Thomething goes wrong!";
                return userViewList;
            }
        }

        public IEnumerable<UserView> GetListForTransactions(string currentUserName, ref bool error, ref string errorMessage)
        {
            error = false;
            var userViewList = new List<UserView>();
            try
            {
                var userList = _UOF.Users.GetList();
                foreach (var user in userList)
                {
                    if (user.UserName != currentUserName)
                    {
                        var Model = GetUserViewModel(user, ref error, ref errorMessage);
                        if (error)
                        {
                            return new List<UserView>();
                        }
                        Model.Balance = 0;
                        userViewList.Add(Model);
                    }
                }
                return userViewList;
            }
            catch (Exception exception)
            {
                error = true;
                errorMessage = "Thomething goes wrong!";
                return userViewList;
            }
        }

        public bool Register(UserModel user, ref string error)
        {
            try
            {
                if (user != null)
                {
                    var entetiUser = _Mapper.Map<User>(user);
                    entetiUser.Password = _PasswordEngine.GetHash(string.Concat(entetiUser.Password, _identity.Value.Salt));
                    entetiUser.Accounts.Add(new Account()
                    {
                        Balance = 0
                    });
                    _UOF.Users.Add(entetiUser);
                    _UOF.Save();

                    return true;
                }
                error = "Empty user data.";
                return false;
            }
            catch (Exception exception)
            {
                error = "Thomething goes wrong!";
                return false;
            }
        }

        public IEnumerable<TransactionView> GetTransactionList(string username, ref bool error, ref string errorMessage)
        {
            error = false;
            var list = new List<TransactionView>();
            try{
                List<Transaction>transactions = _UOF.Transaction.GetList(username).ToList().OrderByDescending(c => c.Date).ToList();
                foreach (var item in transactions)
                {
                    var transactionViewModel = _ViewModelEngine.GetTransactionViewModel(item);
                    if(transactionViewModel == null)
                    {
                        error = true;
                        errorMessage = "Can not create transaction";
                        return new List<TransactionView>();
                    }
                    list.Add(transactionViewModel);
                }
                return list;
            }
            catch (Exception exception)
            {
                error = true;
                errorMessage = "Thomething goes wrong!";
                return list;
            }
        }

    }
}

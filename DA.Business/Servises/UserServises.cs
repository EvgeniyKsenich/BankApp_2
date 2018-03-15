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
        private IMapper _mapper;
        private IUnitOfWork _UOF;
        private IViewModelEngine _viewModelEngine;
        private IPasswordEngine _passwordEngine;
        private IOptions<Identity> _identity;

        public UserServises(IPasswordEngine PasswordEngine, IUnitOfWork Unit, IMapper mapper, IViewModelEngine ViewModelEngine, IOptions<Identity> Identity)
        {
            _mapper = mapper;
            _UOF = Unit;
            _viewModelEngine = ViewModelEngine;
            _passwordEngine = PasswordEngine;
            _identity = Identity;
        }

        public UserView GetUserViewModel(string userName)
        {
            try
            {
                var user = _UOF.Users.Get(userName);
                return _viewModelEngine.GetUserViewModel(user);
            }
            catch (Exception exception)
            {
                return null;
            }
        }

        public UserView GetUserViewModel(User user)
        {
            try{
                return _viewModelEngine.GetUserViewModel(user);
            }
            catch(Exception exception)
            {
                return null;
            }
        }

        public IEnumerable<UserView> GetList()
        {
            var userViewList = new List<UserView>();
            try
            {
                var userList = _UOF.Users.GetList();
                foreach (var user in userList)
                {
                    userViewList.Add(GetUserViewModel(user));
                }
                return userViewList;
            }
            catch(Exception exception)
            {
                return userViewList;
            }
        }

        public IEnumerable<UserView> GetListForTransactions(string currentUserName)
        {
            var userViewList = new List<UserView>();
            try
            {
                var userList = _UOF.Users.GetList();
                foreach (var user in userList)
                {
                    if (user.UserName != currentUserName)
                    {
                        var Model = GetUserViewModel(user);
                        Model.Balance = 0;
                        userViewList.Add(Model);
                    }
                }
                return userViewList;
            }
            catch (Exception exception)
            {
                return userViewList;
            }
        }

        public bool Register(UserModel user)
        {
            try
            {
                if (user != null)
                {
                    var entetiUser = _mapper.Map<User>(user);
                    entetiUser.Password = _passwordEngine.GetHash(string.Concat(entetiUser.Password, _identity.Value.Salt));
                    entetiUser.Accounts.Add(new Account()
                    {
                        Balance = 0
                    });
                    _UOF.Users.Add(entetiUser);
                    _UOF.Save();

                    return true;
                }
                return false;
            }
            catch (Exception exception)
            {
                return false;
            }
        }

        public IEnumerable<TransactionView> GetTransactionList(string Username)
        {         
            var list = new List<TransactionView>();
            try{
                List<Transaction>transactions = _UOF.Transaction.GetList(Username).ToList().OrderByDescending(c => c.Date).ToList();
                foreach (var item in transactions)
                {
                    list.Add(_viewModelEngine.GetTransactionViewModel(item));
                }
                return list;
            }
            catch (Exception exception)
            {
                return list;
            }
        }

    }
}

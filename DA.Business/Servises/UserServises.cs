using AutoMapper;
using BA.Database.UnitOfWork;
using System.Collections.Generic;
using BA.Database.Enteties;
using DA.Business.Repositories;
using DA.Business.Modeles;
using BA.Business.ViewModel;
using DA.Business.ViewModel;
using BA.Business.Repositories;
using Microsoft.Extensions.Options;
using BA.Business.Modeles;
using System;

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

        public UserView GetUserViewModel(string UserName)
        {
            try
            {
                var user = _UOF.Users.Get(UserName);
                return _viewModelEngine.GetUserViewModel(user);
            }
            catch (Exception exception)
            {
                return null;
            }
        }

        public UserView GetUserViewModel(User User)
        {
            try{
                return _viewModelEngine.GetUserViewModel(User);
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

        public IEnumerable<UserView> GetListForTransactions(string CurrentUserName)
        {
            var userViewList = new List<UserView>();
            try
            {
                var userList = _UOF.Users.GetList();
                foreach (var user in userList)
                {
                    if (user.UserName != CurrentUserName)
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

        public bool Register(UserModel User)
        {
            try
            {
                if (User != null)
                {
                    var entetiUser = _mapper.Map<User>(User);
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
                var transactions = _UOF.Transaction.GetList(Username);
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

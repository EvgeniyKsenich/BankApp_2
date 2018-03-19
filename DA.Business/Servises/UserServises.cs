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

        public UserViewErrorModel GetUserViewModel(string userName)
        {
            var returnModel = new UserViewErrorModel();
            try
            {
                var user = _UOF.Users.Get(userName);
                if (user == null)
                {
                    returnModel.ErrorMessage = "Can not find user";
                    returnModel.Error = true;
                    return returnModel;
                }
                var userView = _ViewModelEngine.GetUserViewModel(user);
                if (userView == null)
                {
                    returnModel.ErrorMessage = "Can not create user view";
                    returnModel.Error = true;
                    return returnModel;
                }
                returnModel.Error = false;
                returnModel.Item = userView;
                return returnModel;
            }
            catch (Exception exception)
            {
                returnModel.Error = true;
                returnModel.ErrorMessage = "Thomething goes wrong!";
                return returnModel;
            }
        }

        public UserViewErrorModel GetUserViewModel(User user)
        {
            var returnModel = new UserViewErrorModel();
            try
            {
                var userView = _ViewModelEngine.GetUserViewModel(user);
                if (userView == null)
                {
                    returnModel.ErrorMessage = "Can not create user view";
                    returnModel.Error = true;
                    return returnModel;
                }
                returnMdel.Error = false;
                returnModel.Item = userView;
                return returnModel;
            }
            catch(Exception exception)
            {
                returnModel.ErrorMessage = "Thomething goes wrong!";
                returnModel.Error = true; 
                return returnModel;
            }
        }

        public ListUserViewErrorModel GetList()
        {
            var returnModel = new ListUserViewErrorModel();
            try
            {
                var userViewList = new List<UserView>();
                var userList = _UOF.Users.GetList();
                foreach (var user in userList)
                {
                    var model = GetUserViewModel(user);
                    userViewList.Add(model.Item);
                    if (model.Error)
                    {
                        returnModel.Error = true;
                        returnModel.ErrorMessage = model.ErrorMessage;
                        return returnModel;
                    }
                }
                returnModel.Error = false;
                returnModel.Items = userViewList;
                return returnModel;
            }
            catch(Exception exception)
            {
                returnModel.ErrorMessage = "Thomething goes wrong!";
                returnModel.Error = true;
                return returnModel;
            }
        }

        public ListUserViewErrorModel GetListForTransactions(string currentUserName)
        {
            var returnMdel = new ListUserViewErrorModel();
            var userViewList = new List<UserView>();
            try
            {
                var userList = _UOF.Users.GetList();
                foreach (var user in userList)
                {
                    if (user.UserName != currentUserName)
                    {
                        var Model = GetUserViewModel(user);
                        if (Model.Error)
                        {
                            returnMdel.Error = true;
                            returnMdel.ErrorMessage = Model.ErrorMessage;
                            return returnMdel;
                        }
                        Model.Item.Balance = 0;
                        userViewList.Add(Model.Item);
                    }
                }
                returnMdel.Error = false;
                returnMdel.Items = userViewList;
                return returnMdel;
            }
            catch (Exception exception)
            {
                returnMdel.Error = true;
                returnMdel.ErrorMessage = "Thomething goes wrong!";
                return returnMdel;
            }
        }

        public BooleanErrorModel Register(UserModel user)
        {
            var returnodel = new BooleanErrorModel();
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

                    returnodel.Error = false;
                    return returnodel;
                }
                returnodel.Error = true;
                returnodel.ErrorMessage = "Empty user data.";
                return returnodel;
            }
            catch (Exception exception)
            {
                returnodel.Error = true;
                returnodel.ErrorMessage = "Thomething goes wrong!";
                return returnodel;
            }
        }

        public LisTransactionsErrorModel GetTransactionList(string username)
        {
            var errorModel = new LisTransactionsErrorModel();
            try{
                var list = new List<TransactionView>();
                IEnumerable<Transaction>transactions = _UOF.Transaction.GetList(username).ToList().OrderByDescending(c => c.Date).ToList();
                foreach (var item in transactions)
                {
                    var transactionViewModel = _ViewModelEngine.GetTransactionViewModel(item);
                    if(transactionViewModel == null)
                    {
                        errorModel.Error = true;
                        errorModel.ErrorMessage = "Can not create transaction";
                        return errorModel;
                    }
                    list.Add(transactionViewModel);
                }
                errorModel.Items = list;
                errorModel.Error = false;
                return errorModel;
            }
            catch (Exception exception)
            {
                errorModel.Error = true;
                errorModel.ErrorMessage = "Thomething goes wrong!";
                return errorModel;
            }
        }

    }
}

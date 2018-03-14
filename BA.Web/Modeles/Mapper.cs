using AutoMapper;
using BA.Database.Enteties;
using DA.Business.Modeles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BA.Web.Modeles
{
    public class Mapper : Profile
    {
        public Mapper()
        {
            CreateMap<UserModel, User>().ForMember(x => x.Accounts, opt => opt.Ignore()); 

            CreateMap<Account, AccountModel>();

            CreateMap<TransactionModel, Transaction>();
                            //.ForMember(x => x.AccountInfoInitiator, opt => opt.Ignore())
                            //.ForMember(x => x.AccountInfoRecipient, opt => opt.Ignore()); 
        }
    }
}

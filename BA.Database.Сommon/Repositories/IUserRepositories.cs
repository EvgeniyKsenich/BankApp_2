using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BA.Database.Сommon.Repositories
{
    public interface IUserRepositories<T> where T:class
    {
        T GetUser(string Username);

        IEnumerable<T> GetList();

        void Add(T User);
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace BA.Database.Сommon.Repositories
{
    public interface IRepositories<T> where T : class
    {
        IEnumerable<T> GetList();

        T Get(string Name);

        void Add(T Item);
    }
}

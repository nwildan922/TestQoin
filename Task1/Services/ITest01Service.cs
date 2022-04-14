using System.Collections.Generic;
using Task1.Extensions;
using Task1.Models;

namespace Task1.Services
{
    public interface ITest01Service
    {
        void Add(Test01 model);
        void Update(Test01 model, int id);
        void Remove(Test01 model);
        Test01 GetById(int id);
        PagedResult<Test01> Get(int currentPage, int maxPerPage);
    }
}

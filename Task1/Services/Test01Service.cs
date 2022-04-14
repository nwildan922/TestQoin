using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Task1.Extensions;
using Task1.Models;

namespace Task1.Services
{
    public class Test01Service : ITest01Service
    {
        private readonly EFContext _context;
        public Test01Service()
        {
            _context = new EFContext();
        }

        public void Add(Test01 model)
        {
            model.Created = DateTime.Now;
            _context.Add(model);
            _context.SaveChanges();
        }

        public PagedResult<Test01> Get(int currentPage, int maxPerPage)
        {
            return _context.Test01.GetPaged(currentPage, maxPerPage);
        }

        public Test01 GetById(int id)
        {
            return _context.Test01.Find(id);
        }

        public void Remove(Test01 model)
        {
            var data = _context.Test01.Find(model.Id);
            if (data != null)
            {
                _context.Remove(data);
                _context.SaveChanges();
            }
        }

        public void Update(Test01 model, int id)
        {
            var data = _context.Test01.Find(id);
            if (data != null) 
            {
                data.Nama = model.Nama;
                data.Status = model.Status;
                data.Updated = DateTime.Now;
                
                _context.SaveChanges();
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using WebAPI.Datas;
using WebAPI.Models;

namespace WebAPI.Services
{
    public class AccessRepository : IAccessRepository
    {
        private readonly MyDbContext _context;

        public AccessRepository(MyDbContext context)
        {
            _context = context;
        }

        public List<Models.Access> GetAll()
        {
            var access = _context.Access.Select(x => new Models.Access
            {
                Id = x.Id,
                Count = x.Count
            });
            return access.ToList();
        }

        public void Update(Models.Access access)
        {
            var _access = _context.Access.SingleOrDefault(x => x.Id == access.Id);
            _access.Count = access.Count;
            _context.SaveChanges();
        }
        

    }
}

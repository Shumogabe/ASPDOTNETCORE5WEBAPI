using System;
using System.Collections.Generic;
using WebAPI.Models;

namespace WebAPI.Services
{
    public interface IAccessRepository
    {
        List<Models.Access> GetAll();
        void Update(Models.Access access);

    }
}

using System;
using System.Collections.Generic;
using WebAPI.Models;

namespace WebAPI.Services
{
    public interface IAccountRepository
    {
        List<Account> GetAll();
        Account GetById(Guid id);
        Account Add(AccountModel accountModel);
        void Update(Account account);
        void Delete(Guid id);
        Account Login(string username, string password);

    }
}

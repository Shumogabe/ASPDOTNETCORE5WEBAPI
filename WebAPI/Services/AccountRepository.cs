using System;
using System.Collections.Generic;
using System.Linq;
using WebAPI.Datas;
using WebAPI.Models;

namespace WebAPI.Services
{
    public class AccountRepository : IAccountRepository
    {
        private readonly MyDbContext _context;

        public AccountRepository(MyDbContext context)
        {
            _context = context;
        }
        public Account Add(AccountModel accountModel)
        {
            var account = new Accounts
            {
                Name = accountModel.Name,
                Email = accountModel.Email,
                Phone = accountModel.Phone,
                Username = accountModel.Username,
                Password = accountModel.Password,
                Level = accountModel.Level,
            };
            _context.Add(account);
            _context.SaveChanges();
            return new Account
            {
                Id = account.Id,
                Name = account.Name,
                Email = account.Email,
                Phone = account.Phone,
                Username = account.Username,
                Password = account.Password,
                Level = account.Level,
            };
        }
        public void Delete(Guid id)
        {
            var account = _context.Accounts.SingleOrDefault(x => x.Id == id);
            if (account != null)
            {
                _context.Remove(account);
                _context.SaveChanges();
            }
        }
        public List<Account> GetAll()
        {
            var accounts = _context.Accounts.Select(x => new Account
            {
                Id = x.Id,
                Name = x.Name,
                Phone = x.Phone,
                Email = x.Email,
                Level = x.Level,
                Username = x.Username,
                Password = x.Password,
            });
            return accounts.ToList();
        }

        public Account GetById(Guid id)
        {
            var account = _context.Accounts.SingleOrDefault(x => x.Id == id);
            if (account != null)
            {
                return new Account
                {
                    Id = account.Id,
                    Name = account.Name,
                    Phone = account.Phone,
                    Email = account.Email,
                    Level = account.Level,
                    Username = account.Username,
                    Password = account.Password,
                };
            }
            return null;
        }

        public void Update(Account account)
        {
            var _account = _context.Accounts.SingleOrDefault(x => x.Id == account.Id);
            _account.Name = account.Name;
            _account.Phone = account.Phone;
            _account.Email = account.Email;
            _account.Level = account.Level;
            _account.Username = account.Username;
            _account.Password = account.Password;
            _context.SaveChanges();
        }
        public Account Login(string username, string password)
        {
            var account = _context.Accounts.SingleOrDefault(x => x.Username == username && x.Password == password);
            if (account != null)
            {
                return new Account
                {
                    Id = account.Id,
                    Name = account.Name,
                    Phone = account.Phone,
                    Email = account.Email,
                    Level = account.Level,
                    Username = account.Username,
                    Password = account.Password,
                };
            }
            return null;
        }

    }
}

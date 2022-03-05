using System;

namespace WebAPI.Models
{
    public class AccountModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public byte Level { get; set; }
    }
    public class Account : AccountModel
    {
        public Guid Id { get; set; }
    }
}

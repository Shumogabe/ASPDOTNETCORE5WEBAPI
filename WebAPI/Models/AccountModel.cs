using System;

namespace WebAPI.Models
{
    public class LoginModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
    public class AccountModel : LoginModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public byte Level { get; set; }
    }
    public class Account : AccountModel
    {
        public Guid Id { get; set; }
    }
}

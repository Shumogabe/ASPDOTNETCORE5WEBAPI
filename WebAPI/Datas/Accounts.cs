using System;

namespace WebAPI.Datas
{
    public class Accounts
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public byte Level { get; set; }

    }
}

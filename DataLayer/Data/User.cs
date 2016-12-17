using System.Collections.Generic;

namespace DataLayer.Data
{
    public class User
    {
        public ICollection<Account> Accounts { get; set; }

        public string Name { get; set; }

        public string Password { get; set; }
    }
}
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Core
{
    [Table("User")]
    public class User
    {
        public ICollection<Account> Accounts { get; set; }

        public long Id { get; set; }

        [Required]
        [StringLength(30)]
        public string Name { get; set; }

        public static User NullUser => new User
        {
            Accounts = new List<Account>(),
            Name = "",
            Password = ""
        };

        [Required]
        [StringLength(48)]
        public string Password { get; set; }

        public User()
        {
            Accounts = new HashSet<Account>();
        }
    }
}
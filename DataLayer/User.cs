namespace Data.Core
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("User")]
    public partial class User
    {
        public virtual ICollection<Account> Accounts { get; set; }

        public long Id { get; set; }

        [Required]
        [StringLength(30)]
        public string Name { get; set; }

        [Required]
        [StringLength(48)]
        public string Password { get; set; }

        public User()
        {
            Accounts = new HashSet<Account>();
        }
    }
}
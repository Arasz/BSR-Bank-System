using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Core
{
    [Table("Account")]
    public class Account
    {
        [Column(TypeName = "money")]
        public decimal Balance { get; set; }

        public long Id { get; set; }

        [Required]
        [StringLength(26)]
        public string Number { get; set; }

        public ICollection<Operation> Operations { get; set; }

        public User User { get; set; }

        public long UserId { get; set; }

        public Account()
        {
            Operations = new HashSet<Operation>();
        }
    }
}
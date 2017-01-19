namespace Data.Core
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Account")]
    public partial class Account
    {
        [Column(TypeName = "money")]
        public decimal Balance { get; set; }

        public long Id { get; set; }

        [Required]
        [StringLength(26)]
        public string Number { get; set; }

        public virtual ICollection<Operation> Operations { get; set; }

        public virtual User User { get; set; }

        public long UserId { get; set; }

        public Account()
        {
            Operations = new HashSet<Operation>();
        }
    }
}
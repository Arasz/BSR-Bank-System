namespace Data.Core
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Account")]
    public partial class Account
    {
        [Column(TypeName = "money")]
        public decimal Balance { get; set; }

        public long Id { get; set; }

        [Required]
        [StringLength(26)]
        public string Number { get; set; }

        public long Owner { get; set; }

        public virtual User User { get; set; }
    }
}
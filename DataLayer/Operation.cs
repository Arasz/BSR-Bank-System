using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Core
{
    [Table("Operation")]
    public class Operation
    {
        public Account Account { get; set; }

        public long? AccountId { get; set; }

        [Column(TypeName = "money")]
        public decimal Amount { get; set; }

        [Column(TypeName = "money")]
        public decimal Balance { get; set; }

        public DateTime CreationDate { get; set; }

        [Column(TypeName = "money")]
        public decimal Credit { get; set; }

        [Column(TypeName = "money")]
        public decimal Debit { get; set; }

        public long Id { get; set; }

        [Required]
        [StringLength(26)]
        public string Source { get; set; }

        [Required]
        [StringLength(26)]
        public string Target { get; set; }

        [Required]
        [StringLength(200)]
        public string Title { get; set; }

        [Required]
        [StringLength(50)]
        public string Type { get; set; }
    }
}
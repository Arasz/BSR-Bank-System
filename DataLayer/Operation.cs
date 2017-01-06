namespace Data.Core
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Operation")]
    public class Operation
    {
        [Required]
        [StringLength(20)]
        public string AccountNumber { get; set; }

        [Column(TypeName = "money")]
        public decimal Amount { get; set; }

        [Column(TypeName = "money")]
        public decimal Balance { get; set; }

        public DateTime CreationDate { get; set; }

        public long Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Title { get; set; }

        [Required]
        [StringLength(50)]
        public string Type { get; set; }
    }
}
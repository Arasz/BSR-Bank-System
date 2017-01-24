using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Data.Core
{
    [Table("Operation"), DataContract]
    public class Operation
    {
        [DataMember]
        public Account Account { get; set; }

        public long? AccountId { get; set; }

        [Column(TypeName = "money"), DataMember]
        public decimal Amount { get; set; }

        [Column(TypeName = "money"), DataMember]
        public decimal Balance { get; set; }

        [DataMember]
        public DateTime CreationDate { get; set; }

        [Column(TypeName = "money"), DataMember]
        public decimal Credit { get; set; }

        [Column(TypeName = "money"), DataMember]
        public decimal Debit { get; set; }

        public long Id { get; set; }

        [Required, DataMember]
        [StringLength(26)]
        public string Source { get; set; }

        [Required, DataMember]
        [StringLength(26)]
        public string Target { get; set; }

        [Required, DataMember]
        [StringLength(200)]
        public string Title { get; set; }

        [Required, DataMember]
        [StringLength(50)]
        public string Type { get; set; }
    }
}
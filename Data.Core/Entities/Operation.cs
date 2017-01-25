using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Data.Core.Entities
{
    [Table("Operation"), DataContract]
    public class Operation
    {
        public Account Account { get; set; }

        public long? AccountId { get; set; }

        [DataMember]
        public decimal Amount { get; set; }

        [DataMember]
        public decimal Balance { get; set; }

        [DataMember]
        public DateTime CreationDate { get; set; }

        [DataMember]
        public decimal Credit { get; set; }

        [DataMember]
        public decimal Debit { get; set; }

        public long Id { get; set; }

        [Required(AllowEmptyStrings = true), DataMember]
        [StringLength(26)]
        public string Source { get; set; }

        [Required(AllowEmptyStrings = true), DataMember]
        [StringLength(26)]
        public string Target { get; set; }

        [Required(AllowEmptyStrings = true), DataMember]
        [StringLength(200)]
        public string Title { get; set; }

        [Required(AllowEmptyStrings = true), DataMember]
        [StringLength(50)]
        public string Type { get; set; }
    }
}
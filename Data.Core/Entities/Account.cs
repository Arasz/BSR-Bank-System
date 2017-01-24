using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Data.Core.Entities
{
    [Table("Account"), DataContract(IsReference = true)]
    public class Account
    {
        [Column(TypeName = "money"), DataMember]
        public decimal Balance { get; set; }

        public long Id { get; set; }

        [Required, DataMember]
        [StringLength(26)]
        public string Number { get; set; }

        public ICollection<Operation> Operations { get; set; }

        [DataMember]
        public User User { get; set; }

        public long UserId { get; set; }

        public Account()
        {
            Operations = new HashSet<Operation>();
        }

        public override string ToString() => $"{Number}:{Balance}";
    }
}
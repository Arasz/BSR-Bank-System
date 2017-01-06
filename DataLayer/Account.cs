using System.Runtime.Serialization;

namespace Data.Core
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Account"), DataContract]
    public class Account
    {
        [Column(TypeName = "money"), DataMember]
        public decimal Balance { get; set; }

        public long Id { get; set; }

        [StringLength(26), DataMember, Required]
        public string Number { get; set; }

        public long Owner { get; set; }

        public virtual User User { get; set; }
    }
}
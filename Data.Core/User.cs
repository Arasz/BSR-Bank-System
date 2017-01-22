using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Data.Core
{
    [Table("User"), DataContract(IsReference = true)]
    public class User
    {
        [DataMember]
        public ICollection<Account> Accounts { get; set; }

        public long Id { get; set; }

        [Required, DataMember]
        [StringLength(30)]
        public string Name { get; set; }

        public static User NullUser => new User
        {
            Accounts = new List<Account>(),
            Name = "",
            Password = ""
        };

        [Required, DataMember]
        [StringLength(48)]
        public string Password { get; set; }

        public User()
        {
            Accounts = new HashSet<Account>();
        }
    }
}
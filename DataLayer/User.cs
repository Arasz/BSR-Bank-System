using System.Runtime.Serialization;

namespace Data.Core
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("User"), DataContract]
    public class User
    {
        [DataMember]
        public virtual ICollection<Account> Accounts { get; set; }

        public long Id { get; set; }

        [Required, StringLength(30), DataMember]
        public string Name { get; set; }

        [Required, StringLength(48), DataMember]
        public string Password { get; set; }

        public User()
        {
            Accounts = new HashSet<Account>();
        }
    }
}
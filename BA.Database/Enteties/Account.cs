using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BA.Database.Enteties
{
    public class Account
    {
        public Account()
        {
            this.Initiator = new HashSet<Transaction>();
            this.Recipient = new HashSet<Transaction>();
        }

        public int Id { get; set; }

        [Required]
        public double Balance { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        [InverseProperty("AccountInitiator")]
        public virtual IEnumerable<Transaction> Initiator { get; set; }

        [InverseProperty("AccountRecipient")]
        public virtual IEnumerable<Transaction> Recipient { get; set; }
    }
}

using System;
using System.ComponentModel.DataAnnotations;

namespace BA.Database.Enteties
{
    public class Transaction
    {
        public int Id { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public double Summa { get; set; }

        [Required]
        public int Type { get; set; }

        public int AccountInitiatorId { get; set; }
        public virtual Account AccountInitiator { get; set; }

        public int AccountRecipientId { get; set; }
        public virtual Account AccountRecipient { get; set; }
    }
}

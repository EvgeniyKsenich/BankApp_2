using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DA.Business.ViewModel
{
    public class TransactionView
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public double Summa { get; set; }

        public int Type { get; set; }

        public virtual string AccountInfoInitiator { get; set; }

        public virtual string AccountInfoRecipient { get; set; }
    }
}

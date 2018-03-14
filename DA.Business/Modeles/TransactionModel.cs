using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DA.Business.Modeles
{
    public class TransactionModel
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public double Summa { get; set; }

        public int Type { get; set; }
    }
}

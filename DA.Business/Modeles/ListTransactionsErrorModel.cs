using DA.Business.ViewModel;
using System.Collections.Generic;

namespace DA.Business.Modeles
{
    public class LisTransactionsErrorModel
    {
        public bool Error { get; set; }

        public string ErrorMessage { get; set; }

        public IEnumerable<TransactionView> Items { get; set; }

        public LisTransactionsErrorModel()
        {
            Error = false;
            ErrorMessage = string.Empty;
            Items = new List<TransactionView>();
        }
    }
}

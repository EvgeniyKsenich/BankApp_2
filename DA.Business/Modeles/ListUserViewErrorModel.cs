using BA.Business.ViewModel;
using System.Collections.Generic;

namespace DA.Business.Modeles
{
    public class ListUserViewErrorModel
    {
        public bool Error { get; set; }

        public string ErrorMessage { get; set; }

        public IEnumerable<UserView> Items { get; set; }

        public ListUserViewErrorModel()
        {
            Error = false;
            ErrorMessage = string.Empty;
            Items = new List<UserView>();
        }
    }
}

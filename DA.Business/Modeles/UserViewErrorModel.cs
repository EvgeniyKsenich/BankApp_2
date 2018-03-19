using BA.Business.ViewModel;

namespace DA.Business.Modeles
{
    public class UserViewErrorModel
    {
        public bool Error { get; set; }

        public string ErrorMessage { get; set; }

        public UserView Item { get; set; }

        public UserViewErrorModel()
        {
            Error = false;
            ErrorMessage = string.Empty;
        }
    }
}

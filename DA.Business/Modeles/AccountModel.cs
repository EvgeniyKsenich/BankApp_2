using BA.Database.Сommon.Repositories;

namespace DA.Business.Modeles
{
    public class AccountModel : IPrototype
    {
        public int Id { get; set; }

        public int UsurId { get; set; }

        public double Balance { get; set; } 

        public IPrototype Clone()
        {
            return new AccountModel()
            {
                Id = Id,
                UsurId = UsurId,
                Balance = Balance
            };
        }
    }
}
